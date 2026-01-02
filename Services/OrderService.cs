using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ITicketService _ticketService;
        private readonly IPackageService _packageService;
        private readonly ICartService _cartService;
        
        private readonly IMapper _mapper;

        public OrderService(IOrderRepo orderRepo, ITicketService ticketService, IPrizeService prizeService, IPackageService packageService, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _ticketService = ticketService;
            _packageService = packageService;
            _mapper = mapper;
        }


        public async Task AddOrder(int userId, List<int> PackagesIds)
        {
            var cart = await _cartService.GetCartByUserId(userId);

            if (cart != null)
            {
                var cartItems = cart.CartItems;

                foreach (var item in cartItems)
                {
                    var prize = item.Prize;

                    for(int i = 0; i < item.Quantity; i++)
                    {
                        await _ticketService.AddTicket({userId, prize.Id}); 
                    }
                }

                var packages = _packageService.GetPackagesByIds(PackagesIds);
                double totalAmount = 0;
                foreach (var package in packages)
                {
                    totalAmount += package.Price;
                }
                
                var order = new Order
                {
                    UserId = userId,
                    Prizes= cartItems.Select(ci => ci.Prize).ToList(),
                    Packages = packages.ToList(),
                    OrderDate = DateTime.UtcNow,
                    TotalAmount = totalAmount
                };


                await _orderRepo.AddOrder(order);
                await _cartService.PurchaseCart(userId);
            }
        }

        public async Task<IEnumerable<ReadOrderDTO>> GetOrders()
        {
            var orders = await _orderRepo.GetOrders();
            return _mapper.Map<IEnumerable<ReadOrderDTO>>(orders);
        }

    }
}
