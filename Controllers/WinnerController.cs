using ChineseAuctionAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using static ChineseAuctionAPI.DTO.WinnerDTO;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;

namespace ChineseAuctionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WinnerController : ControllerBase
    {
        private readonly IWinnerService _winnerService;
        
        public WinnerController(IWinnerService winnerService)
        {
            _winnerService = winnerService;
        }

        [HttpPost("AddWinnerToPrize/{prizeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddWinnerToPrize([FromBody]CreateWinnerDTO createWinnerDTO, int prizeId)
        {
            try
            {
                await _winnerService.AddWinnerToPrize(createWinnerDTO);
                return Ok(new { message = "Winner added to prize successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetWinnersByPrizeId/{prizeId}")]
        public async Task<IActionResult> GetWinnersByPrizeId(int prizeId)
        {
            try
            {
                var winners = await _winnerService.GetWinnersByPrizeId(prizeId);
                return Ok(winners);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("GetAllWinners")]
        public async Task<IActionResult> GetAllWinners()
        {
            try
            {
                var winners = await _winnerService.GetAllWinners();
                return Ok(winners);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}
