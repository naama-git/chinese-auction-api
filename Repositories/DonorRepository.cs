using AutoFilterer.Extensions;
using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using ChineseAuctionAPI.Models.QueryParams;
using Microsoft.EntityFrameworkCore;
using System;

public class DonorRepository : IDonorRepo
{
    private readonly ChineseAuctionDBcontext _context;

    public DonorRepository(ChineseAuctionDBcontext context)
    {
        _context = context;
    }


    // get all donors
    public async Task<IEnumerable<Donor>> GetDonors(DonorQParams donorParams)
    {

        try
        {
            var donors = _context.donors
                .Include(d => d.Prizes)
                .ApplyFilter(donorParams);


            if (donorParams.PrizesIds?.Any() == true)
            {
                donors = donors.Where(d =>
                    d.Prizes.Any(p =>
                        donorParams.PrizesIds.Contains(p.Id)));


            }

            if (!string.IsNullOrEmpty(donorParams.Name))
            {
                donors = donors.Where(d =>
               (d.FirstName + d.LastName).Contains(donorParams.Name.ToLower().Trim()));
            }


            return await donors.ToListAsync();

        }
        catch (Exception ex)
        {

            throw new ErrorResponse(500, "GetDonors (Params)", "Failed to retrieve the list of donors.", ex.Message, "GET", "DonorRepository");
        }
    }

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
            var donorInDb = await _context.donors.FindAsync(donor.Id);
            if (donorInDb != null)
            {
                _context.Entry(donorInDb).CurrentValues.SetValues(donor);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ErrorResponse(404, "UpdateDonor", "Donor not found.", $"No donor with ID {donor.Id} exists to update.", "PUT", "DonorRepository");
            }
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

