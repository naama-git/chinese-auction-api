using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChineseAuctionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrizeController : ControllerBase
    {

        private readonly IPrizeService _prizeService;

        public PrizeController(IPrizeService prizeService)
        {
            _prizeService = prizeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadPrizeDTO>>> GetAllPrizes()
        {
            var prizes = await _prizeService.GetPrizes();
            return Ok(prizes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadPrizeDTO>> GetPrizeById(int id)
        {
            var prize = await _prizeService.GetPrizeById(id);
            if (prize == null) return NotFound();
            return Ok(prize);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDonor(CreatePrizeDTO prizeCreateDTO)
        {
            await _prizeService.AddPrize(prizeCreateDTO);
            return Ok(201);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrize(int id)
        {
            await _prizeService.DeletePrize(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrize(int id, UpdatePrizeDTO prize)
        {
            await _prizeService.UpdatePrize(prize);
            return Ok();
        }
    }
}
