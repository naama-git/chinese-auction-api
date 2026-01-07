
using System.ComponentModel.DataAnnotations;
namespace ChineseAuctionAPI.Models


{
    public class CartItem
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Cart ID is required")]
        public int CartId { get; set; }

        public int PrizeId { get; set; }

        [Required(ErrorMessage = "Prize is required")]
        public Prize Prize { get; set; }

        [Required(ErrorMessage = "Quantity is required"), Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        
    
    }
}
