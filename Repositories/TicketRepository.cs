using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;
using static ChineseAuctionAPI.DTO.TicketDTO;

namespace ChineseAuctionAPI.Repositories
{
    public class TicketRepository:ITicketRepo
    {
        private readonly ChineseAuctionDBcontext _context;

        public TicketRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }
        public async Task AddTicket(Ticket ticket)
        {
            await _context.tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
        // צפיה בכל הכרטיסים שנרכשו למתנה מסוימת - יעזור לביצוע ההגרלה
        public async Task<IEnumerable<Ticket>> GetTicketsByPrizeId(int prizeId)
        {
                 return await _context.tickets
                .Where(t => t.PrizeId == prizeId)
                .Include(d=>d.User)
                .Include(g=>g.Prize)
                .ToListAsync();
        }
        //כל משתמש יוכל לצפות בכמות הכרטיסים שהזמין לכל מוצר 
        public async Task<IEnumerable<Ticket>> GetTicketsByUserIdAndprizeId(int userId,int prizeId)
        {
                 return await _context.tickets
                .Include(d => d.User)
                .Include(g => g.Prize)
                .Where(t => t.UserId == userId & t.PrizeId == prizeId)
                .ToListAsync();
        } 

    }
}
