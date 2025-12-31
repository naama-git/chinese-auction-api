using static ChineseAuctionAPI.DTO.TicketDTO;

namespace ChineseAuctionAPI.Interface
{
    public interface ITicketRepo
    {
        public Task AddTicket(Models.Ticket ticket);
        public async Task<IEnumerable<TicketReadDTO>> GetTicketsByPrizeId(int prizeId);
        public Task<List<Models.Ticket>> GetTicketsByUserIdAndprizeId(int userId, int prizeId);

    }
}
