using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using static ChineseAuctionAPI.DTO.PackageDTO;

namespace ChineseAuctionAPI.Services
{
    public class PackageService : IPackageService
    {
        private const string Location = "PackageService";
        private readonly IPackageRepo _packageRepo;
        private readonly IMapper _mapper;

        public PackageService(IPackageRepo packageRepo, IMapper mapper)
        {
            _packageRepo = packageRepo;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ReadPackageDTO>> GetPackages()
        {
            var packages = await _packageRepo.GetPackages();
            if (packages == null)
            {
                return Enumerable.Empty<ReadPackageDTO>();
            }
            return _mapper.Map<IEnumerable<ReadPackageDTO>>(packages);
        }

        public async Task<ReadPackageDTO> GetPackageById(int id)
        {
            Package packageEntity = await _packageRepo.GetPackageById(id);
            if (packageEntity == null)
            {
                throw new ErrorResponse(404, "GetPackageById", "Package not found", $"Package with Id {id} not found", "GET", Location);
            }
            return _mapper.Map<ReadPackageDTO>(packageEntity);
        }

        public async Task<IEnumerable<Package>> GetPackagesByIds(List<int> packageIds)
        {
            var packages = await _packageRepo.GetPackagesByIds(packageIds);
            if (packages == null)
            {
                return Enumerable.Empty<Package>();
            }
            return packages;
        }
        public async Task AddPackage(CreatePackageDTO createPackageDTO)
        {
            var packages = await _packageRepo.GetPackages();
            var exist = packages.Any(p => p.Name == createPackageDTO.Name || p.NumOfTickets == createPackageDTO.NumOfTickets);
            if (exist)
            {
                throw new ErrorResponse(409, "AddPackage", "A package with this name or ticket count already exists.", $"Conflict detected: '{createPackageDTO.Name}' or '{createPackageDTO.NumOfTickets}' is already in use by package.", "POST", Location);
            }
            Package PackageEntity = _mapper.Map<Package>(createPackageDTO);

            if (PackageEntity == null)
            {
                throw new ErrorResponse(
                    500, "AddPackage","An internal error occurred while processing the package data.", 
                    "AutoMapper failed to map PackageCreateDTO to Package entity. Check mapping profiles.", 
                    "POST",Location);
            }

            await _packageRepo.AddPackage(PackageEntity);
        }

        public async Task UpdatePackage(int id, UpdatePackageDTO updatePackageDTO)
        {
            Package packageEntity = await _packageRepo.GetPackageById(id);
            if (packageEntity == null)
            {
                throw new ErrorResponse(404, "UpdatePackage", "Package not found", $"package with Id: {id} not found", "PUT",Location);
            }

            _mapper.Map(updatePackageDTO, packageEntity);

            await _packageRepo.UpdatePackage(packageEntity);
        }

        public async Task DeletePackage(int id)
        {
            Package packageEntity = await _packageRepo.GetPackageById(id);
            if (packageEntity == null)
            {
                throw new ErrorResponse(404, "DeletePackage", "Package not found", $"package with Id: {id} not found", "DELETE", Location);
            }

            await _packageRepo.DeletePackage(id);
        }



    }
}
