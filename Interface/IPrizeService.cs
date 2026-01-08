using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Interface
{
    public interface IPrizeService
    {

        public Task<IEnumerable<ReadPrizeDTO>> GetPrizes();
        public Task<IEnumerable<Prize>> GetPrizesEntities();
        public Task<IEnumerable<Prize>> GetPrizesByIds(List<int> prizesIds);
        public Task AddPrize(CreatePrizeDTO prize);
        public Task DeletePrize(int id);
        public Task<ReadPrizeDTO> GetPrizeById(int id);

        public Task UpdatePrize(UpdatePrizeDTO prize);
        public Task UpdatePrizeQty(int prizeId);
        
    }
}
