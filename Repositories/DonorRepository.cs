using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
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
    public async Task<IEnumerable<Donor>> GetDonors()
    {
        return await _context.donors.
            Include(d=>d.Prizes)
            .ToListAsync();
    }

    //add new donor
    public async Task AddDonor(Donor donor)
    {
        await _context.donors.AddAsync(donor);
        await _context.SaveChangesAsync();

    }

    // update donor
    public async Task UpdateDonor(Donor donor)
    {
        _context.donors.Update(donor);
        await _context.SaveChangesAsync();
    }


    // delete a donor
    public async Task DeleteDonor(int id)
    {
        await _context.donors
        .Where(d => d.Id == id)
        .ExecuteDeleteAsync(); 
    }


    // find donor by id
    public async Task<Donor> FindDonorById(int id)
    {
        return await _context.donors.
            Include(d => d.Prizes)
            .FirstOrDefaultAsync(d => d.Id == id);
    }
}

