using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ChineseAuctionAPI.Repositories
{
    public class DonorRepository :IDonorRepo
    {
        private readonly ChineseAuctionDBcontext _context;
        public DonorRepository(ChineseAuctionDBcontext _context)
        {
            _context = _context;
        }
        //get all Donors
        public async Task<IEnumerable<Donor>> GetDonors()
        {
            return await _context.donors.ToListAsync();
        }
        //add new Donor
        public async Task AddDonor(Donor donor)
        {
            await _context.donors.AddAsync(donor);
            await _context.SaveChangesAsync();
        }
        //update Donor
        public async Task UpdateDonor(Donor donor)
        {
             _context.donors.Update(donor);
            await _context.SaveChangesAsync();
        }
        //delete donor by id
        public async Task DeleteDonor(int id)
        {
            await _context.donors
                .Where(d => d.Id == id)
                .ExecuteDeleteAsync();
        }
        //find donor by Id
        public async  Task<Donor> FindDonorById(int id)
        {
            return await _context.donors.FindAsync(id);
        }
    }
}
