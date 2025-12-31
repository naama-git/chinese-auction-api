using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using static ChineseAuctionAPI.DTO.WinnerDTO;

namespace ChineseAuctionAPI.Services
{
    public class WinnerService :IWinnerService
    {
        private readonly IWinnerRepo _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public WinnerService(IWinnerRepo repo, IMapper mapper, IConfiguration configuration)
        {
            _repo = repo;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task AddWinnerToPrize(CreateWinnerDTO createWinnerDTO, int prizeId)
        {
            var winnerEntity = _mapper.Map<Models.Winner>(createWinnerDTO);
            await _repo.addWinnerToPrize(winnerEntity, prizeId);
        }
        public async Task<IEnumerable<ReadWinnerDTO>> GetWinnersByPrizeId(int prizeId)
        {
            var winners = await _repo.getWinnersByPrizeId(prizeId);
            return _mapper.Map<IEnumerable<ReadWinnerDTO>>(winners);
        }
        public async Task<IEnumerable<ReadWinnerDTO>> GetAllWinners()
        {
            var winners = await _repo.getAllWinners();
            return _mapper.Map<IEnumerable<ReadWinnerDTO>>(winners);
        }
    }
}
