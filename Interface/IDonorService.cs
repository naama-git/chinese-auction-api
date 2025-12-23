using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Interface
{
    public interface IDonorService
    {
        public Task<IEnumerable<DonorReadDTO>> GetDonors();
        public Task AddDonor(DonorCreateDTO donor);
        public Task UpdateDonor(DonorCreateDTO donor);
        public Task DeleteDonor(int id);
        public Task<DonorReadDTO> FindDonorById(int id);
    }
}
