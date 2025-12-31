using ChineseAuctionAPI.Interface;
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

        [HttpPost("AddPrizeToCart/{userId}/{prizeId}")]
        public async Task<IActionResult> AddPrizeToCart(int userId, int prizeId, [FromQuery] int quantity = 1)
        {
            try
            {
                await _cartService.AddPrizeToCart(userId, prizeId, quantity);
                return Ok(new { message = $"Successfully added {quantity} ticket(s) to cart." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("RemovePrizeFromCart/{userId}/{prizeId}")]
        public async Task<IActionResult> RemovePrizeFromCart(int userId, int prizeId)
        {
            try
            {
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

        [HttpGet("GetCartByUserId/{userId}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            try
            {
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
