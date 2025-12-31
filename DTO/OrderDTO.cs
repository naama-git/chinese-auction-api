using ChineseAuctionAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;

namespace ChineseAuctionAPI.DTO
{
    public class CreateOrderDTO
    {
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "At least one PrizeId is required")]
        public List<int> PrizeIds { get; set; }

        [Required(ErrorMessage = "Order date is required")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Packages are required")]
        public List<int> PackagesIds { get; set; }

    }

    public class ReadOrderDTO
    {
        [Required(ErrorMessage = "User is required")]
        public User User { get; set; }

        [Required(ErrorMessage = "At least one Prize is required")]
        public List<Prize> Prizes { get; set; }

        [Required(ErrorMessage = "Order date is required")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Packages are required")]
        public List<Package> Packages { get; set; }
        public decimal FinalPrice { get; set; } = 0;
    }
}