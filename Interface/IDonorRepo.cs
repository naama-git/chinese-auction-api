using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.QueryParams;

namespace ChineseAuctionAPI.Interface
{
    public interface IDonorRepo
    {
        public Task<IEnumerable<Donor>> GetDonors(DonorQParams donorParams);
        public Task<IEnumerable<Donor>> GetDonors();
        public Task AddDonor(Donor donor);
        public Task UpdateDonor(Donor donor);
        public Task DeleteDonor(int id);
        public Task<Donor> FindDonorById(int id);




    }
}
