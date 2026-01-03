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
        public TicketService(ITicketRepo repo, IMapper mapper, IConfiguration configuration)
        {
            _repo = repo;
            _mapper = mapper;
          
        }

        
        public async Task AddTicket(TicketCreateDTO ticketDTO)
        {
            Ticket ticketEntity = _mapper.Map<Ticket>(ticketDTO);
            await _repo.AddTicket(ticketEntity);
        }
        public async Task AddTicketsRange(List<TicketCreateDTO> tickets)
        {
            List<Ticket> ticketEntities= _mapper.Map<List<Ticket>>(tickets);
            await _repo.AddTicketsRange(ticketEntities);
        }


        public async Task<IEnumerable<TicketReadDTO>> GetTicketsByPrizeId(int prizeId)
        {
            var tickets = await _repo.GetTicketsByPrizeId(prizeId);
            return _mapper.Map<IEnumerable<TicketReadDTO>>(tickets);
        }

        public async Task<IEnumerable<TicketReadDTO>> GetTicketsByUserIdAndprizeId(int userId, int prizeId)
        {
            var tickets = await _repo.GetTicketsByUserIdAndprizeId(userId,prizeId);
            return _mapper.Map<IEnumerable<TicketReadDTO>>(tickets);
        }
    }
}
