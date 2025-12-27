using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Interface;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Repositories
{
    public class PrizeRepository : IPrizeRepo
    {

        private readonly ChineseAuctionDBcontext _context;

        public PrizeRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Prize>> GetPrizes()
        {
            return await _context.prizes
                .Include(p => p.Donor)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Prize> GetPrizeById(int id)
        {
            return await _context.prizes
                .Include(p => p.Donor)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPrize(Prize prize)
        {
            var donor = await _context.donors.Include(d => d.Prizes).FirstOrDefaultAsync(d => d.Id == prize.DonorId);
            if (donor == null)
            {
                throw new Exception("donor is null in AddPrizeRepo");
            }
            donor.Prizes.Add(prize);
            await _context.SaveChangesAsync();


        }

        public async Task UpdatePrize(Prize prize)
        {
            _context.prizes.Update(prize);
            await _context.SaveChangesAsync();

        }

        public async Task DeletePrize(int id)
        {
            await _context.prizes
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        }



    }
}
