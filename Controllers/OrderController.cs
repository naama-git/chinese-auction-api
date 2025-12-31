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
        public IActionResult Index()
        {
            return View();
        }

        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(CreateOrderDTO order)
        {
            await _orderService.AddOrder(order);
            return Ok(201);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderService.GetOrders();
            return Ok(orders);
        }
    }
}
