using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using System.Drawing;

namespace ChineseAuctionAPI.Services
{
    public class PrizeService : IPrizeService
    {
        private const string Location = "PrizeService";
        private readonly IPrizeRepo _prizeRepo;
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;

        public PrizeService(IPrizeRepo prizeRepo, IMapper mapper, ITicketService ticketService)
        {
            _prizeRepo = prizeRepo;
            _mapper = mapper;
            _ticketService = ticketService;
        }


        public async Task<IEnumerable<ReadPrizeDTO>> GetPrizes()
        {
            var prizes = await _prizeRepo.GetPrizes();
            if (prizes == null) return Enumerable.Empty<ReadPrizeDTO>();

            return _mapper.Map<IEnumerable<ReadPrizeDTO>>(prizes);
        }

        public async Task<IEnumerable<Prize>> GetPrizesEntities()
        {
            var prizes = await _prizeRepo.GetPrizes();
            if (prizes == null) return Enumerable.Empty<Prize>();

            return prizes;
        }

        public async Task AddPrize(CreatePrizeDTO prize)
        {
            Prize PrizeEntity = _mapper.Map<Prize>(prize);
   
            if (PrizeEntity == null)
            {
                throw new ErrorResponse(500, "AddPrize", "Error processing prize data.", "AutoMapper failed to map CreatePrizeDTO.", "POST",Location);
            }

            await _prizeRepo.AddPrize(PrizeEntity);
           
        }

        public async Task DeletePrize(int id)
        {
            var prize = await _prizeRepo.GetPrizeById(id);
            if (prize == null)
            {
                throw new ErrorResponse(404, "DeletePrize", "Prize not found.", $"Delete failed: ID {id} not found.", "DELETE", Location);
            }

            
            var tickets=await _ticketService.GetTicketsByPrizeId(prize.Id);
            if (tickets != null && tickets.Any())
            {
                throw new ErrorResponse(400, "DeletePrize", "Cannot delete this prize because tickets have already been purchased for it.",
                    $"Referential integrity violation: Prize ID {id} ('{prize.Name}') is linked to {tickets.Count()} existing tickets.",
                    "DELETE", Location);
            }

            await _prizeRepo.DeletePrize(id);
        }

        public async Task<ReadPrizeDTO> GetPrizeById(int id)
        {
            Prize prizeEntity = await _prizeRepo.GetPrizeById(id);

            if (prizeEntity == null)
            {
                throw new ErrorResponse(404, "GetPrizeById", "The requested prize was not found.", $"ID {id} not found in repository.", "GET", Location);
            }

            return _mapper.Map<ReadPrizeDTO>(prizeEntity);
        }

        public async Task UpdatePrize(UpdatePrizeDTO prize)
        {

            var existingPrize = await _prizeRepo.GetPrizeById(prize.Id);
            if (existingPrize == null)
            {
                throw new ErrorResponse(404, nameof(UpdatePrize), "Prize for update not found.", $"Update failed: ID {prize.Id} not found.", "PUT", "srv");
            }

            _mapper.Map(prize, existingPrize);
            await _prizeRepo.UpdatePrize(existingPrize);
            
        }

        public async Task<IEnumerable<Prize>> GetPrizesByIds(List<int> prizesIds)
        {
            return await _prizeRepo.GetPrizesByIds(prizesIds);

        }

        
    }

}
