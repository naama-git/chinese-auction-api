using System.ComponentModel.DataAnnotations;
using System.ComponentModel; 

namespace ChineseAuctionAPI.Models
{
    public class Ticket
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Prize ID is required")]
        public int PrizeId { get; set; }
        public Prize Prize { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        public User User { get; set; }
       
    }
}
