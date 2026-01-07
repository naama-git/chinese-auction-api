using AutoMapper;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
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
        public async Task<CreateWinnerDTO> PerformRaffle(int prizeId)
        {
            var tickets = await _ticketService.GetTicketsByPrizeId(prizeId);

            var prize = await _prizeService.GetPrizeById(prizeId);
            if (prize == null)
            {
                throw new ErrorResponse(404, "PerformRaffle", "Prize not found.", $"Cannot fetch winners for non-existent Prize ID {prizeId}.", null, Location);
            }

            if (tickets == null || !tickets.Any())
            {
                throw new ErrorResponse(404, "PerformRaffle", "Tickets to this ruffle were not found.", $"Cannot fetch tickets for this Prize ID {prizeId}.", null, Location);
            }

            Random rnd = new Random();
            int winnerIndex = rnd.Next(tickets.Count());
            var winningTicket = tickets.ElementAt(winnerIndex);

            CreateWinnerDTO winner = new()
            {
                UserId = winningTicket.User.Id,
                PrizeId = prizeId,
            };

            await _winnerService.AddWinnerToPrize(winner);

            return winner;
        }
    }
}
