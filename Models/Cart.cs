using System.ComponentModel.DataAnnotations;

namespace ChineseAuctionAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        public User User { get; set; }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
