using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.CartDTO;

namespace ChineseAuctionAPI.Interface
{
    public interface ICartService
    {
            public  Task AddPrizeToCart(int userId, int prizeId, int quantity = 1);
            public  Task RemovePrizeFromCart(int userId, int prizeId);
            public  Task addcart(addCartDTO cartDto);
            public  Task<ReadCartDTO> GetCartByUserId(int userId);
            public  Task PurchaseCart(int userId);
    }
}
