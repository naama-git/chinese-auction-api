using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ChineseAuctionAPI.DTO.WinnerDTO;

namespace ChineseAuctionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaffleController : ControllerBase
    {

        private readonly IRaffleService _raffleService;
       
        public RaffleController(IRaffleService raffleService)
        {
            _raffleService = raffleService;
        }

        [HttpPost("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ReadWinnerDTO>> ExecuteRaffle(int id)
        {
            var winner = await _raffleService.PerformRaffle(id);

            if (winner == null)
                return BadRequest("Raffle could not be performed. because no prizes or no participants available.");

            return Ok(winner);
        } 
    }
}
