using ChineseAuctionAPI.Models;
using System.ComponentModel.DataAnnotations;
using static ChineseAuctionAPI.DTO.UserDTO;

namespace ChineseAuctionAPI.DTO
{
    public class WinnerDTO
    {
        public class CreateWinnerDTO
        {
            public int UserId { get; set; }
            [Required(ErrorMessage = "Prize ID is required")]
            public int PrizeId { get; set; }
        }
        public class ReadWinnerDTO
        {
            public int Id { get; set; }
            public ResponseUserDTO User { get; set; }
            public PrizeForWinnerDTO Prize { get; set; }
        }
    }
}
