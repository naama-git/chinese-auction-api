using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Intrefaces;
using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Services
{
    public class DonorService : IDonorService
    {
        private readonly IDonorRepo _repo;
        private readonly IMapper _mapper;
        public DonorService(IDonorRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        public async Task<IEnumerable<DonorReadDTO>> GetDonors()
        {
            var donors = await _repo.GetDonors();
            return _mapper.Map<IEnumerable<DonorReadDTO>>(donors);
        }

        public async Task AddDonor(DonorCreateDTO donor)
        {
            Donor donorEntity = _mapper.Map<Donor>(donor);
            await _repo.AddDonor(donorEntity);
        }

        public async Task DeleteDonor(int id)
        {
            await _repo.DeleteDonor(id);
        }

        public async Task<DonorReadDTO> FindDonorById(int id)
        {
            Donor donorEntity = await _repo.FindDonorById(id);
            return _mapper.Map<DonorReadDTO>(donorEntity);
        }



        public async Task UpdateDonor(DonorCreateDTO donor)
        {

            Donor donorEntity = _mapper.Map<Donor>(donor);
            await _repo.UpdateDonor(donorEntity);
        }
    }
}
