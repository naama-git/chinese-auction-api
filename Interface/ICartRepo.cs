using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Interface
{
        public interface ICartRepo
        {
        public Task AddPrizeToCart(int userId, int prizeId, int quantity = 1);
        public Task<Cart> GetCartByUserId(int userId);
            public Task addcart(Cart _cart);
            public Task UpdateCart(Cart cart);
            public Task RemovePrizeFromCart(int userId, int prizeId);
        }
}
