using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Intrefaces;
using Microsoft.AspNetCore.Mvc;

namespace ChineseAuctionAPI.Controllers
{
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
        public async Task<ActionResult<DonorReadDTO>> GetAllDonors()
        {
            var donors = await _donorService.GetDonors();
            return Ok(donors);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDonor(DonorCreateDTO donorCreateDTO)
        {
            await _donorService.AddDonor(donorCreateDTO);
            return Ok();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DonorReadDTO>> GetDonorById(int id)
        {
            var donor = await _donorService.FindDonorById(id);
            return Ok(donor);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDonor(int id)
        {
            await _donorService.DeleteDonor(id);
            return Ok();
        }











    }
}
