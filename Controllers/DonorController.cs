using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;

[ApiController] 
[Route("api/[controller]")] 
[Authorize(Roles = "Admin")]
public class DonorController : ControllerBase 
{
    private readonly IDonorService _donorService;
    private readonly IValidator<DonorCreateDTO> _createValidator;
    private readonly IValidator<DonorUpdateDTO> _updateValidator;

    public DonorController(IDonorService donorService,IValidator<DonorCreateDTO> createValidator, IValidator<DonorUpdateDTO> updateValidator)
    {
        _donorService = donorService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
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
    public async Task<IActionResult> CreateDonor([FromBody] DonorCreateDTO donorCreateDTO)
    {
        var validationResult = await _createValidator.ValidateAsync(donorCreateDTO);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult);
        }
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
    public async Task<IActionResult> UpdateDonor(int id, [FromBody] DonorUpdateDTO donor)
    {
        var validationResult = await _updateValidator.ValidateAsync(donor);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult);
        }
        await _donorService.UpdateDonor(donor);
        return Ok();
    }
}
