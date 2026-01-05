using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace ChineseAuctionAPI.Repositories
{
    public class TicketRepository:ITicketRepo
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
                await _context.tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "AddTicket", "Failed to add the ticket to the system.", ex.Message, "POST", RepoLocation);
            }
        }


        public async Task AddTicketsRange(List<Ticket> tickets)
        {
            try
            {
                await _context.tickets.AddRangeAsync(tickets);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "AddTicketsRange", "Failed to add multiple tickets.", ex.Message, "POST", RepoLocation);
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
        
        public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAndprizeId(int userId,int prizeId)
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



    }
}
