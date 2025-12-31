using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.PackageDTO;

namespace ChineseAuctionAPI.Services
{
    public class PackageService:IPackageService
    {
        private readonly IPackageRepo _packageRepo;
        private readonly IMapper _mapper;

        public PackageService(IPackageRepo packageRepo, IMapper mapper)
        {
            _packageRepo = packageRepo;
            _mapper = mapper;
        }
        public async Task AddPackage(CreatePackageDTO createPackageDTO)
        {
            Package PackageEntity = _mapper.Map<Package>(createPackageDTO);

            await _packageRepo.AddPackage(PackageEntity);
        }
        public async Task<IEnumerable<ReadPackageDTO>> GetPackages()
        {
            var packages = await _packageRepo.GetPackages();
            return _mapper.Map<IEnumerable<ReadPackageDTO>>(packages);
        }
        public async Task<ReadPackageDTO> GetPackageById(int id)
        {
            Package packageEntity = await _packageRepo.GetPackageById(id);
            return _mapper.Map<ReadPackageDTO>(packageEntity);
        }

    }
}
