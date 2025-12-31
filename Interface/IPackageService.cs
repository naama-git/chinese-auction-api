using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.PackageDTO;

namespace ChineseAuctionAPI.Interface
{
    public interface IPackageService
    {
        public Task AddPackage(CreatePackageDTO createPackageDTO);
        public Task<IEnumerable<ReadPackageDTO>> GetPackages();
        public Task<ReadPackageDTO> GetPackageById(int id);
    }
}
