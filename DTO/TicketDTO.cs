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
            public ReadPrizeDTO Prize { get; set; }
            public ResponseUserDTO User { get; set; }
        }
    }
}
