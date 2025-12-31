using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Repositories
{
    public class WinnerRepository : IWinnerRepo
    {
        private readonly ChineseAuctionDBcontext _context;

        public WinnerRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }
        public async Task addWinnerToPrize(Winner winner,int prizeId)
        {
            winner.PrizeId = prizeId;
            await _context.winners.AddAsync(winner);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Winner>> getWinnersByPrizeId(int prizeId)
        {
            return await _context.winners
                .Include(p => p.Prize)
                .Include(u => u.User)
                .Where(w => w.PrizeId == prizeId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Winner>> getAllWinners()
        {
            return await _context.winners
                .Include(p => p.Prize)
                .Include(u => u.User)
                .ToListAsync();
        }
    }
}
