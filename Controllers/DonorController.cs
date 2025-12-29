using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using Microsoft.AspNetCore.Mvc;

[ApiController] 
[Route("api/[controller]")] 
public class DonorController : ControllerBase 
{
    private readonly IDonorService _donorService;

    public DonorController(IDonorService donorService)
    {
        _donorService = donorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DonorReadDTO>>> GetAllDonors()
    {
        var donors = await _donorService.GetDonors();
        return Ok(donors);
    }

    [HttpGet("{id}")] 
    public async Task<ActionResult<DonorReadDTO>> GetDonorById(int id)
    {
        var donor = await _donorService.FindDonorById(id);
        if (donor == null) return NotFound();
        return Ok(donor);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDonor(DonorCreateDTO donorCreateDTO)
    {
        await _donorService.AddDonor(donorCreateDTO);
        return Ok(201); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDonor(int id)
    {
        await _donorService.DeleteDonor(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDonor(DonorUpdateDTO donor)
    {
        await _donorService.UpdateDonor(donor);
        return Ok();
    }
}
