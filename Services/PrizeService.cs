using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using System.Drawing;

namespace ChineseAuctionAPI.Services
{
    public class PrizeService : IPrizeService
    {

        private readonly IPrizeRepo _prizeRepo;
        private readonly IMapper _mapper;

        public PrizeService(IPrizeRepo prizeRepo, IMapper mapper)
        {
            _prizeRepo=prizeRepo;
            _mapper=mapper;
        }


        public async Task<IEnumerable<ReadPrizeDTO>> GetPrizes()
        {

            var prizes = await _prizeRepo.GetPrizes();
            return _mapper.Map<IEnumerable<ReadPrizeDTO>>(prizes);

        }

        public async Task AddPrize(CreatePrizeDTO prize)
        {
            Prize PrizeEntity = _mapper.Map<Prize>(prize);
            
            await _prizeRepo.AddPrize(PrizeEntity);
        }

        public async Task DeletePrize(int id)
        {
            await _prizeRepo.DeletePrize(id);
        }

        public async Task<ReadPrizeDTO> GetPrizeById(int id)
        {
            Prize prizeEntity = await _prizeRepo.GetPrizeById(id);
            return _mapper.Map<ReadPrizeDTO>(prizeEntity);
        }

        public async Task UpdatePrize(UpdatePrizeDTO prize)
        {
            Prize prizeEntity = _mapper.Map<Prize>(prize);
            await _prizeRepo.UpdatePrize(prizeEntity);
        }

        public async Task<IEnumerable<Prize>> GetPrizesByIds(List<int> prizesIds)
        {
            return await _prizeRepo.GetPrizesByIds(prizesIds);

        }

        
    }

}
