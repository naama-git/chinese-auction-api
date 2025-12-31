using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.DTO;

namespace ChineseAuctionAPI.DTO
{
    public class TicketDTO
    {
        public class TicketCreateDTO
        {
            public int PrizeId { get; set; }
            public int UserId { get; set; }
        }
        public class TicketReadDTO
        {
            public int Id { get; set; }
            public ReadPrizeDTO Prize { get; set; }
            public UserDTO.ResponseUserDTO User { get; set; }
        }
    }
}
