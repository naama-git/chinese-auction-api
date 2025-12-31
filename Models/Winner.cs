using System.ComponentModel.DataAnnotations;


namespace ChineseAuctionAPI.Models
{
    public class Winner
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Prize ID is required")]
        public int PrizeId { get; set; }
        public Prize Prize { get; set; }
    }
}
