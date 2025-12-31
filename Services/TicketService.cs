using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.TicketDTO;

namespace ChineseAuctionAPI.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepo _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public TicketService(ITicketRepo repo, IMapper mapper, IConfiguration configuration)
        {
            _repo = repo;
            _mapper = mapper;
            _configuration = configuration;
        }
        public Task AddTicket(TicketDTO ticketDTO)
        {
            Ticket ticketEntity = _mapper.Map<Ticket>(ticketDTO);
            return _repo.AddTicket(ticketEntity);
        }

        public async Task<IEnumerable<TicketReadDTO>> GetTicketsByPrizeId(int prizeId)
        {


        }

        public Task<List<Ticket>> GetTicketsByUserIdAndprizeId(int userId, int prizeId)
        {
            throw new NotImplementedException();
        }
    }
}
