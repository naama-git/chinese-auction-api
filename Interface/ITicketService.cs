using ChineseAuctionAPI.DTO;
using static ChineseAuctionAPI.DTO.TicketDTO;

namespace ChineseAuctionAPI.Interface
{
    public interface ITicketService
    {
        public Task AddTicket(TicketDTO ticketDTO);
        public async Task<IEnumerable<TicketReadDTO>> GetTicketsByPrizeId(int prizeId);
        public Task<List<Models.Ticket>> GetTicketsByUserIdAndprizeId(int userId, int prizeId);
    }
}
