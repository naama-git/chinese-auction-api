using ChineseAuctionAPI.DTO;
using static ChineseAuctionAPI.DTO.TicketDTO;

namespace ChineseAuctionAPI.Interface
{
    public interface ITicketService
    {
        public Task AddTicket(TicketCreateDTO ticketDTO);
        public Task AddTicketsRange(List<TicketCreateDTO> tickets);
        public Task<IEnumerable<TicketReadDTO>> GetTicketsByPrizeId(int prizeId);
        public Task<IEnumerable<TicketReadDTO>> GetTicketsByUserIdAndprizeId(int userId, int prizeId);
    }
}
