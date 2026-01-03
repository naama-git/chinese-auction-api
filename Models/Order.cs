
using System.ComponentModel.DataAnnotations;
namespace ChineseAuctionAPI.Models
{
    public class Order
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId {  get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "Order date is required")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Packages are required")]
        public List<Package> Packages { get; set; }

        [Required(ErrorMessage = "Prizes are required")]
        public List<Prize> Prizes { get; set; } 

    }
}