using static ChineseAuctionAPI.DTO.WinnerDTO;

namespace ChineseAuctionAPI.Interface
{
    public interface IWinnerService
    {
        public Task AddWinnerToPrize(CreateWinnerDTO createWinnerDTO);
        public Task<IEnumerable<ReadWinnerDTO>> GetWinnersByPrizeId(int prizeId);
        public Task<IEnumerable<ReadWinnerDTO>> GetAllWinners();
    }
}
