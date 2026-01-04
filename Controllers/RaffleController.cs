using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using static ChineseAuctionAPI.DTO.WinnerDTO;

namespace ChineseAuctionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaffleController : ControllerBase
    {

        private readonly IRaffleService _raffleService;
        private readonly IPrizeService _prizeService;


        public RaffleController(IRaffleService raffleService)
        {
            _raffleService = raffleService;
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<CreateWinnerDTO>> ExecuteRaffle(int id)
        {
            var winner = await _raffleService.PerformRaffle(id);

            if (winner == null)
                return BadRequest("לא נמצאו כרטיסים להגרלה זו.");

            return Ok(winner);
        }
    }
}
