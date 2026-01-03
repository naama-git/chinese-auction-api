using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Interface
{
        public interface ICartRepo
        {
            public  Task<Cart> GetCartByUserId(int userId);
            public  Task addcart(Cart _cart);
            public  Task UpdateCart(Cart cart);
        }
}
