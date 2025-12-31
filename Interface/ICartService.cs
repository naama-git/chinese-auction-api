using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.CartDTO;

namespace ChineseAuctionAPI.Interface
{
    public interface ICartService
    {
            public Task AddPrizeToCart(int userId, int prizeId, int quantity = 1);
            public Task<ReadCartDTO> GetCartByUserId(int userId);
            public Task addcart(addCartDTO _cartDto);
            public Task RemovePrizeFromCart(int userId, int prizeId);
    }
}
