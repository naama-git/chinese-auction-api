using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;

namespace ChineseAuctionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrizeController : ControllerBase
    {

        private readonly IPrizeService _prizeService;
        private readonly IValidator<CreatePrizeDTO> _createValidator;
        private readonly IValidator<UpdatePrizeDTO> _updateValidator;


        public PrizeController(IPrizeService prizeService,IValidator<CreatePrizeDTO> createValidator, IValidator<UpdatePrizeDTO> updateValidator)
        {
            _prizeService = prizeService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePrize(CreatePrizeDTO prizeCreateDTO)
        {
            var validationResult = await _createValidator.ValidateAsync(prizeCreateDTO);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            await _prizeService.AddPrize(prizeCreateDTO);
            return Ok(201);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePrize(int id)
        {
            await _prizeService.DeletePrize(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePrize(int id, UpdatePrizeDTO prize)
        {
            var validationResult = await _updateValidator.ValidateAsync(prize);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            await _prizeService.UpdatePrize(prize);
            return Ok();
        }
    }
}
