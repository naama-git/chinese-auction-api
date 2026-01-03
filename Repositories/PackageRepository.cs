using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Repositories
{
    public class PackageRepository: IPackageRepo
    {
        private readonly ChineseAuctionDBcontext _context;

        public PackageRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }
        public async Task AddPackage(Package package)
        {
            await _context.packages.AddAsync(package);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Package>> GetPackages()
        {
            return await _context.packages.ToListAsync();
        }

        public async Task<Package> GetPackageById(int id)
        {
            return await _context.packages.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdatePackage(Package package)
        {
            _context.packages.Update(package);
            await _context.SaveChangesAsync();
        }
        public async Task DeletePackage(int id)
        {
            await _context.packages
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        }
        public async Task<IEnumerable<Package>> GetPackagesByIds(List<int> packageIds)
        {
            return await _context.packages
                .Where(p => packageIds.Contains(p.Id))
                .ToListAsync();
        }
    }
}
