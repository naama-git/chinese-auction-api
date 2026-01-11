using ChineseAuctionAPI.Models;
using System.ComponentModel.DataAnnotations;
using static ChineseAuctionAPI.DTO.UserDTO;

namespace ChineseAuctionAPI.DTO
{
    public class CartDTO
    {
    
        public class addCartDTO
        {
            [Required(ErrorMessage = "User ID is required")]
            public int UserId { get; set; }
        }

        public class ReadCartDTO
        {
            public ReadUserDTO User { get; set; }

            public List<CartItemReadDTO> CartItems { get; set; } = new List<CartItemReadDTO>();
        }

        public class CartItemReadDTO
        {
            
            public int PrizeId { get; set; }
            public ReadSimplePrizeDTO Prize { get; set; }   
            public int Quantity { get; set; }
        }



    }
}
