using ChineseAuctionAPI.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ChineseAuctionAPI.DTO.CartDTO;

namespace ChineseAuctionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize(Roles="User")]
        [HttpPost("AddPrizeToCart/{prizeId}")]
        public async Task<IActionResult> AddPrizeToCart(int prizeId, [FromBody] int quantity = 1)
        {
            try
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                {
                    return Unauthorized("Unauthorized user");
                }

                int userId = int.Parse(userIdClaim.Value);

                await _cartService.AddPrizeToCart(userId, prizeId, quantity);
                return Ok(new { message = $"Successfully added {quantity} ticket(s) to cart." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("RemovePrizeFromCart/{prizeId}")]
        [Authorize(Roles="User")]
        public async Task<IActionResult> RemovePrizeFromCart(int prizeId)
        {
            try
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                {
                    return Unauthorized("Unauthorized user");
                }

                int userId = int.Parse(userIdClaim.Value);

                await _cartService.RemovePrizeFromCart(userId, prizeId);
                return Ok(new { message = "Prize removed from cart successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("AddCart")]
        public async Task<IActionResult> AddCart([FromBody] addCartDTO cartDTO)
        {
            try
            {
                await _cartService.addcart(cartDTO);
                return Ok(new { message = "Cart created successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles="User")]
        [HttpGet("GetCartByUserId")]
        public async Task<ActionResult<ReadCartDTO>> GetCartByUserId()
        {
            try
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                {
                    return Unauthorized("Unauthorized user");
                }

                int userId = int.Parse(userIdClaim.Value);

                var cart = await _cartService.GetCartByUserId(userId);
                if (cart == null)
                {
                    return NotFound(new { message = "Cart not found for this user." });
                }
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
