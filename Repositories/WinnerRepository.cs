using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Repositories
{
    public class WinnerRepository : IWinnerRepo
    {
        private readonly ChineseAuctionDBcontext _context;
        private const string RepoLocation = "WinnerRepository";

        public WinnerRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }
        public async Task addWinnerToPrize(Winner winner)
        {

            try
            {
                await _context.winners.AddAsync(winner);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "addWinnerToPrize", "Failed to save the prize winner information.", ex.Message, "POST", RepoLocation);
            }
        }
        public async Task<IEnumerable<Winner>> getWinnersByPrizeId(int prizeId)
        {
            try
            {
                return await _context.winners
                    .Include(p => p.Prize)
                    .Include(u => u.User)
                    .Where(w => w.PrizeId == prizeId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "getWinnersByPrizeId", "An error occurred while retrieving winners for the specified prize.", ex.Message, "GET", RepoLocation);
            }
        }
        public async Task<IEnumerable<Winner>> getAllWinners()
        {
            try
            {
                return await _context.winners
                    .Include(p => p.Prize)
                    .Include(u => u.User)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "getAllWinners", "Failed to retrieve the complete list of winners.", ex.Message, "GET", RepoLocation);
            }
        }
    }
}
