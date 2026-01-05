using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;
using ChineseAuctionAPI.Models.Exceptions;
using System;

public class DonorRepository : IDonorRepo
{
    private readonly ChineseAuctionDBcontext _context;

    public DonorRepository(ChineseAuctionDBcontext context)
    {
        _context = context;
    }


    // get all donors
    public async Task<IEnumerable<Donor>> GetDonors()
    {

        try
        {
            return await _context.donors
                .Include(d => d.Prizes)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            
            throw new ErrorResponse(500, "GetDonors", "Failed to retrieve the list of donors.", ex.Message, "GET", "DonorRepository");
        }
    }

    //add new donor
    public async Task AddDonor(Donor donor)
    {
        try
        {
            await _context.donors.AddAsync(donor);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ErrorResponse(500, "AddDonor", "Failed to add the new donor.", ex.Message, "POST", "DonorRepository");
        }

    }

    // update donor
    public async Task UpdateDonor(Donor donor)
    {
        try
        {
            _context.donors.Update(donor);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ErrorResponse(500, "UpdateDonor", "Failed to update donor details.", ex.Message, "PUT", "DonorRepository");
        }
    }


    // delete a donor
    public async Task DeleteDonor(int id)
    {

        try
        {
            var rowsAffected = await _context.donors
                .Where(d => d.Id == id)
                .ExecuteDeleteAsync();

            if (rowsAffected == 0)
            {
                throw new ErrorResponse(404, "DeleteDonor", "Donor not found.", $"No donor with ID {id} was found for deletion.", "DELETE", "DonorRepository");
            }
        }
        catch (ErrorResponse) { throw; }
        catch (Exception ex)
        {
            throw new ErrorResponse(500, "DeleteDonor", "An error occurred during deletion.", ex.Message, "DELETE", "DonorRepository");
        }
    }


    // find donor by id
    public async Task<Donor> FindDonorById(int id)
    {
        try
        {
            var donor = await _context.donors
                .Include(d => d.Prizes)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (donor == null)
            {
                throw new ErrorResponse(404, "FindDonorById", "Donor not found.", $"The donor with ID {id} does not exist.", "GET", "DonorRepository");
            }

            return donor;
        }
        catch (ErrorResponse) { throw; }
        catch (Exception ex)
        {
            throw new ErrorResponse(500, "FindDonorById", "An error occurred while searching for the donor.", ex.Message, "GET", "DonorRepository");
        }
    }
}

