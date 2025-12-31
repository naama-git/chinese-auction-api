using ChineseAuctionAPI.DTO;
using static ChineseAuctionAPI.DTO.TicketDTO;

namespace ChineseAuctionAPI.Interface
{
    public interface ITicketService
    {
        public Task AddTicket(TicketDTO.TicketCreateDTO ticketDTO);
        public Task<IEnumerable<TicketDTO.TicketReadDTO>> GetTicketsByPrizeId(int prizeId);
        public Task<IEnumerable<TicketDTO.TicketReadDTO>> GetTicketsByUserIdAndprizeId(int userId, int prizeId);
    }
}
