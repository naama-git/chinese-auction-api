using ChineseAuctionAPI.Models;
using System.ComponentModel.DataAnnotations;
using static ChineseAuctionAPI.DTO.UserDTO;

namespace ChineseAuctionAPI.DTO
{
    public class CartDTO
    {
        public class addAndDeletePrizeToCartDTO
        {
            [Required(ErrorMessage = "User ID is required")]
            public int UserId { get; set; }
            [Required(ErrorMessage = "Prize ID is required")]
            public int PrizeId { get; set; }
            public int Quantity { get; set; } = 1;
        }

        public class addCartDTO
        {
            [Required(ErrorMessage = "User ID is required")]
            public int UserId { get; set; }
        }

        public class ReadCartDTO
        {
            public ResponseUserDTO User { get; set; }

            public List<CartItemReadDTO> CartItems { get; set; } = new List<CartItemReadDTO>();
        }

        public class CartItemReadDTO
        {
            public int PrizeId { get; set; }
            public PrizeForWinnerDTO Prize { get; set; } 
            public int Quantity { get; set; } 
        }
    }
}
