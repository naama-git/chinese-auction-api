using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Interface
{
    public interface IPrizeRepo
    {
        public Task<IEnumerable<Prize>> GetPrizes();
        public Task AddPrize(Prize prize);

        public Task UpdatePrize(Prize prize);

        public Task DeletePrize(int id);

        public Task<Prize> GetPrizeById(int id);

        public  Task<IEnumerable<Prize>> GetPrizesByIds(List<int> prizesIds);
    }
}
