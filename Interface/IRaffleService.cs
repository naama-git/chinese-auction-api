using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Interface
{
    public interface IRaffleService
    {
        public Task<Winner> PerformRaffle(int prizeId);

    }
}
