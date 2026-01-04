using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.WinnerDTO;

namespace ChineseAuctionAPI.Interface
{
    public interface IRaffleService
    {
        public Task<CreateWinnerDTO> PerformRaffle(int prizeId);

    }
}
