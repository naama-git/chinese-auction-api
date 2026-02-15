using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using static ChineseAuctionAPI.DTO.WinnerDTO;

namespace ChineseAuctionAPI.Services
{
    public class WinnerService :IWinnerService
    {

        private const string Location = "WinnerService";
        private readonly IWinnerRepo _repo;

        private readonly IUserService _userService;
        private readonly IPrizeService _prizeService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;



        
        public WinnerService(IWinnerRepo repo, IMapper mapper, IPrizeService prizeService, IUserService userService , IOrderService OrderService)
        {
            _repo = repo;
            _mapper = mapper;
            _prizeService = prizeService;
            _userService = userService;
            _orderService = OrderService;
            
        }
        public async Task<ReadWinnerDTO> AddWinnerToPrize(CreateWinnerDTO createWinnerDTO)
        {
            var user = await _userService.GetUserById(createWinnerDTO.UserId);
            if (user == null) {
                throw new ErrorResponse(500, "AddWinnerToPrize", "Error processing winner data.", "Mapping CreateWinnerDTO failed.", "POST",Location);
            }
            var prize=await _prizeService.GetPrizeById(createWinnerDTO.PrizeId);
            if (prize == null) {

                throw new ErrorResponse(404, "GetWinnersByPrizeId", "Prize not found.", $"Cannot fetch winners for non-existent Prize ID {createWinnerDTO.PrizeId}.", "GET", Location);
            }

            var winnerEntity = _mapper.Map<Winner>(createWinnerDTO);

            if (winnerEntity == null)
            {
                throw new ErrorResponse(500, "GetWinnersByPrizeId", "Error processing winner data.", "Mapping CreateWinnerDTO failed.", "POST", Location);
            }


             var winner=await _repo.addWinnerToPrize(winnerEntity);
            return _mapper.Map<ReadWinnerDTO>(winner);
        }

        public async Task<IEnumerable<ReadWinnerDTO>> GetWinnersByPrizeId(int prizeId)
        {
            var prize = await _prizeService.GetPrizeById(prizeId);
            if (prize == null)
            {

                throw new ErrorResponse(404, "GetWinnersByPrizeId", "Prize not found.", $"Cannot fetch winners for non-existent Prize ID {prizeId}.", "GET", Location);
            }
            var winners = await _repo.getWinnersByPrizeId(prizeId);
            if (winners == null) return Enumerable.Empty<ReadWinnerDTO>();
            return _mapper.Map<IEnumerable<ReadWinnerDTO>>(winners);
        }

        public async Task<IEnumerable<ReadWinnerDTO>> GetAllWinners()
        {
            var winners = await _repo.getAllWinners();
            if (winners == null) return Enumerable.Empty<ReadWinnerDTO>();
            return _mapper.Map<IEnumerable<ReadWinnerDTO>>(winners);
        }
        public async Task<double> GetRevanue()
        {
            var orders = _orderService.GetOrders(new OrderQParams ());
            double sum = 0;
            for(int i = 0;i<orders.Result.Count();i++)
            {
                sum += orders.Result.ElementAt(i).TotalPrice;
            }
            return sum;
        }
    }
}
