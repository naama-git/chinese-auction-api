using ChineseAuctionAPI.DTO;

namespace ChineseAuctionAPI.Interface
{
    public interface IPrizeService
    {

        public Task<IEnumerable<ReadPrizeDTO>> GetPrizes();
        public Task AddPrize(CreatePrizeDTO prize);
        public Task DeletePrize(int id);
        public Task<ReadPrizeDTO> GetPrizeById(int id);

        public Task UpdatePrize(UpdatePrizeDTO prize);



    }
}
