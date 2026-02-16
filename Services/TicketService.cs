using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using static ChineseAuctionAPI.DTO.TicketDTO;

namespace ChineseAuctionAPI.Services
{
    public class TicketService : ITicketService
    {
        private const string Location = "TicketService";
        private readonly ITicketRepo _repo;

        private readonly IUserService _userService;
        private readonly IPrizeRepo _prizeRepo;

        private readonly IMapper _mapper;
        
        public TicketService(ITicketRepo repo, IMapper mapper, IUserService userService, IPrizeRepo prizeRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _userService = userService;
            _prizeRepo = prizeRepo;
        }

        
        public async Task AddTicket(TicketCreateDTO ticketDTO)
        {
            
            var prize=_prizeRepo.GetPrizeById(ticketDTO.PrizeId);
            if (prize == null) {
                throw new ErrorResponse(404, nameof(AddTicket), "Prize not found.", $"Ticket creation failed: Prize ID {ticketDTO.PrizeId} does not exist.", "POST", Location);
            }
            var user = _userService.GetUserById(ticketDTO.UserId);
            if (user==null)
            {
                throw new ErrorResponse(404, nameof(AddTicket), "User not found.", $"Ticket creation failed: User ID {ticketDTO.UserId} does not exist.", "POST",Location);
            }

            Ticket ticketEntity = _mapper.Map<Ticket>(ticketDTO);

            if (ticketEntity == null)
            {
                throw new ErrorResponse(500, "AddTicket", "Error processing ticket.", "Mapping TicketCreateDTO to Ticket failed.", "POST", Location);
            }
            await _repo.AddTicket(ticketEntity);
        }


        public async Task AddTicketsRange(List<TicketCreateDTO> tickets, int prizeId)

        {
            if (tickets == null || !tickets.Any())
            {
                throw new ErrorResponse(400, "AddTicketsRange", "No tickets provided.", "The tickets list is null or empty.", "POST", Location);
            }

            List<Ticket> ticketEntities= _mapper.Map<List<Ticket>>(tickets);

            if (ticketEntities == null || ticketEntities.Count != tickets.Count)
            {
                throw new ErrorResponse(500, "AddTicketsRange", "Error processing ticket batch.", "Mapping tickets list failed or returned incomplete results.", "POST", Location);
            }
            await _repo.AddTicketsRange(ticketEntities, prizeId);
        }


        public async Task<IEnumerable<TicketReadDTO>> GetTicketsByPrizeId(int prizeId)
        {
            var prize = await _prizeRepo.GetPrizeById(prizeId);
            if (prize==null)
            {
                throw new ErrorResponse(404, "GetTicketsByPrizeId", "Prize not found.", $"Cannot fetch tickets for non-existent Prize ID {prizeId}.", "GET", Location);
            }

            var tickets = await _repo.GetTicketsByPrizeId(prizeId);
            if (tickets == null) return Enumerable.Empty<TicketReadDTO>();
            return _mapper.Map<IEnumerable<TicketReadDTO>>(tickets);
        }

        public async Task<IEnumerable<TicketReadDTO>> GetTicketsByUserIdAndprizeId(int userId, int prizeId)
        {
            var tickets = await _repo.GetTicketsByUserIdAndprizeId(userId,prizeId);

            if (tickets == null) return Enumerable.Empty<TicketReadDTO>();
            return _mapper.Map<IEnumerable<TicketReadDTO>>(tickets);

        }

        public async Task<int> GetNumOfSoldTickets()
        {
            int numOfTickets = await _repo.GetNumOfSoldTickets();

            return numOfTickets;
        }



    }
}
