using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.DTO;
using static ChineseAuctionAPI.DTO.UserDTO;

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
            public ReadSimplePrizeDTO Prize { get; set; }
            public ReadUserDTO User { get; set; }
        }
    }
}
