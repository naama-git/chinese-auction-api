using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Repositories
{
    public class PackageRepository: IPackageRepo
    {
        private readonly ChineseAuctionDBcontext _context;
        private const string RepoLocation = "PackageRepository";

        public PackageRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }
        public async Task AddPackage(Package package)
        {
            try
            {
                await _context.packages.AddAsync(package);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex) 
            {
                    throw new ErrorResponse(500, "AddPackage", "add package failed", ex.Message, "POST", RepoLocation);
            }
               
        }
        public async Task<IEnumerable<Package>> GetPackages()
        {
            try
            {
                return await _context.packages
                    .OrderByDescending(p=>p.Price)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetPackages", "Failed to retrieve packages.", ex.Message, "GET", RepoLocation);
            }
        }

        public async Task<Package> GetPackageById(int id)
        {
            try
            {
                var package = await _context.packages.FirstOrDefaultAsync(p => p.Id == id);

                if (package == null)
                {
                    throw new ErrorResponse(404, "GetPackageById", "Package not found.", $"No package found with ID {id}.", "GET", RepoLocation);
                }

                return package;
            }
            catch (ErrorResponse) { throw; }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetPackageById", "Error searching for the package.", ex.Message, "GET", RepoLocation);
            }
        }

        public async Task UpdatePackage(Package package)
        {
            try
            {
                _context.packages.Update(package);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "UpdatePackage", "Failed to update the package.", ex.Message, "PUT", RepoLocation);
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeletePackage(int id)
        {
            try
            {
                var rowsAffected = await _context.packages
                    .Where(p => p.Id == id)
                    .ExecuteDeleteAsync();

                if (rowsAffected == 0)
                {
                    throw new ErrorResponse(404, "DeletePackage", "Package not found.", $"Could not delete package ID {id} because it does not exist.", "DELETE", RepoLocation);
                }
            }
            catch (ErrorResponse) { throw; }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "DeletePackage", "An error occurred during package deletion.", ex.Message, "DELETE", RepoLocation);
            }
        }
        public async Task<IEnumerable<Package>> GetPackagesByIds(List<int> packageIds)
        {
            try
            {
                var uniquePackages = await _context.packages
                    .Where(p => packageIds.Contains(p.Id))
                    .ToListAsync();

                    
                    var result = packageIds
                        .Select(id => uniquePackages.FirstOrDefault(p => p.Id == id))
                        .Where(p => p != null) 
                        .ToList();

                    return result;

            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetPackagesByIds", "Failed to retrieve requested packages.", ex.Message, "GET", RepoLocation);
            }
        }
    }
}
