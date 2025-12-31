using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Interface
{
    public interface IPackageRepo
    {
        public Task AddPackage(Package package);

        public Task<IEnumerable<Package>> GetPackages();

        public Task<Package> GetPackageById(int id);
        public Task<IEnumerable<Package>> GetPackagesByIds(List<int> packageIds);


    }
}
