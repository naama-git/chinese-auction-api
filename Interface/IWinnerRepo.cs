using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Interface
{
    public interface IWinnerRepo
    {
        public Task<Winner> addWinnerToPrize(Winner winner);
        public Task<IEnumerable<Winner>> getWinnersByPrizeId(int prizeId);

        public Task<IEnumerable<Winner>> getAllWinners();
    }
}
