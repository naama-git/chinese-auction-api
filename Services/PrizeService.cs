using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using ChineseAuctionAPI.Models.QueryParams;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Distributed;
using System.Drawing;
using System.Text.Json;

namespace ChineseAuctionAPI.Services
{
    public class PrizeService : IPrizeService
    {
        private const string Location = "PrizeService";
        private readonly IPrizeRepo _prizeRepo;
        private readonly ICategoryService _categoryService;
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _config;

        public PrizeService(IPrizeRepo prizeRepo, IMapper mapper, ITicketService ticketService, ICategoryService categoryService, IDistributedCache cache, IConfiguration connfig)
        {
            _prizeRepo = prizeRepo;
            _mapper = mapper;
            _ticketService = ticketService;
            _categoryService = categoryService;
            _cache = cache;
            _config = connfig;
        }


        
        public async Task<IEnumerable<ReadPrizeDTO>> GetPrizes(PrizeQParams prizeQParams)
        {

            var cachedPrizes = await _cache.GetStringAsync("prizes");
            if (!string.IsNullOrEmpty(cachedPrizes))
            {
                var cprizes = JsonSerializer.Deserialize<IEnumerable<ReadPrizeDTO>>(cachedPrizes);
                Console.WriteLine("from cache");
                return cprizes;
            }
            var prizes = await _prizeRepo.GetPrizes(prizeQParams);
            if (prizes == null) return Enumerable.Empty<ReadPrizeDTO>();

            var ttlMinutes = _config.GetValue<int>("CacheSettings:TtlMinutes");
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(ttlMinutes)
            };

            var prizesDto = _mapper.Map<IEnumerable<ReadPrizeDTO>>(prizes);
            var serializedPrizes = JsonSerializer.Serialize(prizesDto);

            await _cache.SetStringAsync("prizes", serializedPrizes, options);

            return prizesDto;
        }


        public async Task AddPrize(CreatePrizeDTO prize)
        {
            Prize PrizeEntity = _mapper.Map<Prize>(prize);
            var categories = await _categoryService.GetCategoriesByIds(prize.CategoryIds);
            PrizeEntity.Categories.AddRange(categories);
            
   
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
            var categories = await _categoryService.GetCategoriesByIds(prize.CategoryIds);


            _mapper.Map(prize, existingPrize);
            existingPrize.Categories = categories.ToList();
            await _prizeRepo.UpdatePrize(existingPrize);
            
        }

        public async Task<IEnumerable<Prize>> GetPrizesByIds(List<int> prizesIds)
        {
            if (prizesIds == null || !prizesIds.Any())
            {
                return Enumerable.Empty<Prize>();
            }

            var prizes = await _prizeRepo.GetPrizesByIds(prizesIds);

           
            if (prizes.Count() != prizesIds.Count)
            {
                return Enumerable.Empty<Prize>();
            }

            return prizes;

        }

        public async Task UpdatePrizeQty(int prizeId)
        {
            var prize=await _prizeRepo.GetPrizeById(prizeId);
            if (prize==null)
            {
                throw new ErrorResponse(404, "UpdatePrizeQty", "The requested prize was not found.", $"ID {prizeId} not found in repository.", "PUT", Location);
            }
            if (prize.Qty > 1)
            {
                prize.Qty -=1;
                await _prizeRepo.UpdatePrize(prize);
            }
            else{
                await _prizeRepo.DeletePrize(prizeId);
            }
            
        }

        
    }

}
