using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Repositories
{
    public class PrizeRepository : IPrizeRepo
    {

        private readonly ChineseAuctionDBcontext _context;

        private const string RepoLocation = "PrizeRepository";

        public PrizeRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Prize>> GetPrizes()
        {
            try
            {
                
                return await _context.prizes
                    .Include(p => p.Donor)
                    .Include(p => p.Categories)
                    .Include(p=>p.Tickets)
                    .ToListAsync();

                
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetPrizes", "Failed to retrieve prizes.", ex.Message, "GET", RepoLocation);
            }
        }

        public async Task<Prize> GetPrizeById(int id)
        {
            try
            {
                var prize = await _context.prizes
                    .Include(p => p.Donor)
                    .Include(p => p.Categories)
                    .Include(p => p.Tickets)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (prize == null)
                {
                    throw new ErrorResponse(404, "GetPrizeById", "Prize not found.", $"No prize found with ID {id}.", "GET", RepoLocation);
                }

                return prize;
            }
            catch (ErrorResponse) { throw; }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetPrizeById", "An error occurred while searching for the prize.", ex.Message, "GET", RepoLocation);
            }
        }

        public async Task AddPrize(Prize prize)
        {
            try
            {
                var donor = await _context.donors
                    .Include(d => d.Prizes)
                    .FirstOrDefaultAsync(d => d.Id == prize.DonorId);

                if (donor == null)
                {
                    
                    throw new ErrorResponse(404, "AddPrize", "Donor not found.", $"Cannot add prize because donor with ID {prize.DonorId} does not exist.", "POST", RepoLocation);
                }

                donor.Prizes.Add(prize);
                await _context.SaveChangesAsync();
            }
            catch (ErrorResponse) { throw; }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "AddPrize", "Failed to add the new prize.", ex.Message, "POST", RepoLocation);
            }


        }

        public async Task UpdatePrize(Prize prize)
        {
            try
            {
                _context.prizes.Update(prize);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "UpdatePrize", "Failed to update prize details.", ex.Message, "PUT", RepoLocation);
            }

        }

        public async Task DeletePrize(int id)
        {
            try
            {
                var rowsAffected = await _context.prizes
                    .Where(p => p.Id == id)
                    .ExecuteDeleteAsync();

                if (rowsAffected == 0)
                {
                    throw new ErrorResponse(404, "DeletePrize", "Prize not found.", $"Could not delete prize ID {id} because it was not found.", "DELETE", RepoLocation);
                }
            }
            catch (ErrorResponse) { throw; }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "DeletePrize", "An error occurred during deletion.", ex.Message, "DELETE", RepoLocation);
            }
        }

        public async Task<IEnumerable<Prize>> GetPrizesByIds(List<int> prizeIds)
        {
            try
            {
                return await _context.prizes
                    .Where(p => prizeIds.Contains(p.Id))
                    .Include(p => p.Donor)
                    .Include(p => p.Categories)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetPrizesByIds", "Failed to retrieve the selected prizes.", ex.Message, "GET", RepoLocation);
            }
        }
    

    }
}
