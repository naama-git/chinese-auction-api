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
        public async Task AddTicket(TicketDTO.TicketCreateDTO ticketDTO)
        {
            Ticket ticketEntity = _mapper.Map<Ticket>(ticketDTO);
            await _repo.AddTicket(ticketEntity);
        }

        public async Task<IEnumerable<TicketReadDTO>> GetTicketsByPrizeId(int prizeId)
        {
            var tickets = await _repo.GetTicketsByPrizeId(prizeId);
            return _mapper.Map<IEnumerable<TicketReadDTO>>(tickets);
        }

        public async Task<IEnumerable<TicketDTO.TicketReadDTO>> GetTicketsByUserIdAndprizeId(int userId, int prizeId)
        {
            var tickets = await _repo.GetTicketsByUserIdAndprizeId(userId,prizeId);
            return _mapper.Map<IEnumerable<TicketReadDTO>>(tickets);
        }
    }
}
