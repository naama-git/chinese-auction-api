using ChineseAuctionAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using static ChineseAuctionAPI.DTO.PackageDTO;
using static ChineseAuctionAPI.DTO.UserDTO;

namespace ChineseAuctionAPI.DTO
{
    

    public class ReadOrderDTO
    {
        [Required(ErrorMessage = "User is required")]
        public ReadUserDTO User { get; set; }

        [Required(ErrorMessage = "At least one Prize is required")]
        public List<ReadSimplePrizeDTO> Prizes { get; set; }

        [Required(ErrorMessage = "Order date is required")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Packages are required")]
        public List<ReadPackageDTO> Packages { get; set; }

        [Required(ErrorMessage = "Final price is required"),Range(0, double.MaxValue)]
        public double TotalPrice { get; set; } = 0;
    }
}