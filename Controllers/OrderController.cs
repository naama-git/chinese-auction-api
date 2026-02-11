using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChineseAuctionAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
     

        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ReadOrderDTO>> AddOrder([FromBody] List<int> PackagesIds)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("Unauthorized user");
            }

            int userId = int.Parse(userIdClaim.Value);

            var order=await _orderService.AddOrder(userId, PackagesIds);

            return Ok(order);
        }

   

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ReadSimpleOrderDTO>>> GetOrders([FromQuery] OrderQParams orderParams)
        {
            var orders = await _orderService.GetOrders(orderParams);
            return Ok(orders);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ReadOrderDTO>>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderById(id);
            return Ok(order);
        }


    }
}
