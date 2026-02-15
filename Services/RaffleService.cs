
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models.Exceptions;

using System.Transactions;
using static ChineseAuctionAPI.DTO.WinnerDTO;

namespace ChineseAuctionAPI.Services
{
    public class RaffleService : IRaffleService
    {

        private const string Location = "RaffleService";

        private readonly ITicketService _ticketService;
        private readonly IWinnerService _winnerService;

        private readonly IPrizeService _prizeService;

       
        public RaffleService(ITicketService ticketService, IWinnerService winnerService, IPrizeService prizeService)
        {
            _ticketService = ticketService;
            _winnerService = winnerService;
            _prizeService = prizeService;
            
        }

        public async Task<ReadWinnerDTO> PerformRaffle(int prizeId)
        {
            var options = new TransactionOptions 
            { 
                IsolationLevel = IsolationLevel.ReadCommitted, 
                Timeout = TransactionManager.DefaultTimeout 
            };

            using (var scope = new TransactionScope(
                TransactionScopeOption.Required, 
                options, 
                TransactionScopeAsyncFlowOption.Enabled)) 
            {
                try
                {
                    var prize = await _prizeService.GetPrizeById(prizeId);
                    if (prize == null)
                    {
                        throw new ErrorResponse(404, "PerformRaffle", "Prize not found.", $"Cannot fetch winners for non-existent Prize ID {prizeId}.", null, Location);
                    }

                    var isWinnerExists = await _winnerService.GetWinnersByPrizeId(prizeId);
                    if (isWinnerExists != null && isWinnerExists.Any() && prize.Qty<2)
                    {
                        throw new ErrorResponse(400, "PerformRaffle", "Raffle already performed for this prize.", $"Raffle for Prize ID {prizeId} has already been conducted.", null, Location);
                    }

                    var tickets = await _ticketService.GetTicketsByPrizeId(prizeId);
                    
                    


                    if (tickets == null || !tickets.Any())
                    {
                        throw new ErrorResponse(404, "PerformRaffle", "No tickets found for this lottery.", $"Cannot fetch tickets for this Prize ID {prizeId}.", null, Location);
                    }

                    Random rnd = new Random();
            
                    int winnerIndex = rnd.Next(tickets.Count());
                    var winningTicket = tickets.ElementAt(winnerIndex);

                    CreateWinnerDTO winner = new()
                    {
                        UserId = winningTicket.User.Id,
                        PrizeId = prizeId,
                    };

                    var comWinner=await _winnerService.AddWinnerToPrize(winner);

                    // Decrease the quantity of the prize by 1
                    if (prize.Qty > 1)
                    {
                        await _prizeService.UpdatePrizeQty(prizeId);
                    }
                    
            
                    scope.Complete();
                    return comWinner;
                }

                catch (ErrorResponse ex)
                {
                    
                    throw;
                }
                catch (Exception ex)
                {
                     throw new ErrorResponse(500, "AddOrder", "Internal Server Error", $"Something went wrong: {ex.Message}", "POST", Location);
                }

             }
                } 
            }
}
