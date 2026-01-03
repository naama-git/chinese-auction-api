using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.TicketDTO;

namespace ChineseAuctionAPI.Interface
{
    public interface ITicketRepo
    {
        public Task AddTicket(Models.Ticket ticket);
        public Task<IEnumerable<Ticket>> GetTicketsByPrizeId(int prizeId);
        public Task<IEnumerable<Ticket>> GetTicketsByUserIdAndprizeId(int userId, int prizeId);

        public Task AddTicketsRange(List<Ticket> tickets);

    }
}
