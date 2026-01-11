using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;


namespace ChineseAuctionAPI.Repositories
{
    public class TicketRepository : ITicketRepo
    {
        private readonly ChineseAuctionDBcontext _context;
        private const string RepoLocation = "TicketRepository";

        public TicketRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }

        public async Task AddTicket(Ticket ticket)
        {
            try
            {
                var prize = await _context.prizes
                    .Include(p => p.Tickets)
                    .FirstOrDefaultAsync(d => d.Id == ticket.PrizeId);

                if (prize == null)
                {

                    throw new ErrorResponse(404, "AddTicket", "Prize not found.", $"Cannot add ticket because prize with ID {ticket.PrizeId} does not exist.", "POST", RepoLocation);
                }

                prize.Tickets.Add(ticket);
                await _context.SaveChangesAsync();
            }
            catch (ErrorResponse) { throw; }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "AddTicket", "Failed to add the new prize.", ex.Message, "POST", RepoLocation);
            }

        }


        public async Task AddTicketsRange(List<Ticket> tickets, int prizeId)
        {
            try
            {
                var prize = await _context.prizes
                    .Include(p => p.Tickets)
                    .FirstOrDefaultAsync(p => p.Id == prizeId);

                if (prize == null)
                {

                    throw new ErrorResponse(404, "AddTicketsRange", "Prize not found.", $"Cannot add ticket because prize with ID {prizeId} does not exist.", "POST", RepoLocation);
                }

                prize.Tickets.AddRange(tickets);
                await _context.SaveChangesAsync();
            }
            catch (ErrorResponse) { throw; }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "AddTicketsRange", "Failed to add the new prize.", ex.Message, "POST", RepoLocation);
            }
        }



        // for ruffle
        public async Task<IEnumerable<Ticket>> GetTicketsByPrizeId(int prizeId)
        {
            try
            {
                return await _context.tickets
                    .Where(t => t.PrizeId == prizeId)
                    .Include(d => d.User)
                    .Include(g => g.Prize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetTicketsByPrizeId", "An error occurred while retrieving tickets for the specified prize.", ex.Message, "GET", RepoLocation);
            }
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAndprizeId(int userId, int prizeId)
        {
            try
            {
                return await _context.tickets
                    .Include(d => d.User)
                    .Include(g => g.Prize)
                    .Where(t => t.UserId == userId && t.PrizeId == prizeId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetTicketsByUserIdAndprizeId", "An error occurred while retrieving user-specific tickets for this prize.", ex.Message, "GET", RepoLocation);
            }
        }

        public async Task<int> GetNumOfSoldTicketsByPrizeId(int prizeId)
        {
            try
            {
                return await _context.tickets
                    .Where(t => t.PrizeId == prizeId)
                    .CountAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetTicketsByUserIdAndprizeId", "An error occurred while retrieving user-specific tickets for this prize.", ex.Message, "GET", RepoLocation);
            }
        }



    }
}
