using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Intrefaces
{
    public interface IDonorRepo
    {
        public Task<IEnumerable<Donor>> GetDonors();

        public Task AddDonorAsync(Donor donor);

        public Task UpdateDonor(Donor donor);

        public Task deleteDonor(int id);

        public Task<Donor> FindDonorById(int id);

    }
}
