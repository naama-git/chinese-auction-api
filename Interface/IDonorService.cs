using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.QueryParams;

namespace ChineseAuctionAPI.Interface
{
    public interface IDonorService
    {
        public Task<IEnumerable<DonorReadDTO>> GetDonors(DonorQParams donorParams);
        public Task AddDonor(DonorCreateDTO donor);
        public Task UpdateDonor(DonorUpdateDTO donor);
        public Task DeleteDonor(int id);
        public Task<DonorReadDTO> FindDonorById(int id);
    }
}
