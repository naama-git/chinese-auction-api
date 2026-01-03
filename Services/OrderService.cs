using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.PackageDTO;

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
            var cart = await _cartService.GetCartByUserId(userId) ?? throw new Exception("no cart found");

            var cartItems = cart.CartItems;
            if (cartItems == null || cartItems.Count==0)
                throw new Exception("cart is empty");

            var prizesList = cartItems.Where(ci => ci.Prize != null).Select(ci => ci.Prize).ToList();
            if (prizesList.Count==0)
                throw new Exception("no prizes in cart");

            var prizes = _mapper.Map<List<Prize>>(prizesList);

            foreach (var item in cartItems)
            {
                var prize = item.Prize;
                if (prize == null) continue;

                for (int i = 0; i < item.Quantity; i++)
                {
                    await _ticketService.AddTicket(new TicketDTO.TicketCreateDTO { UserId = userId, PrizeId = prize.Id });
                }
            }

            var packagesFromService = await _packageService.GetPackagesByIds(PackagesIds) ?? new List<ReadPackageDTO>();
            var packages = _mapper.Map<List<Package>>(packagesFromService);

            double totalPrice = packages.Sum(p => p.Price);

            var order = new Order
            {
                UserId = userId,
                Prizes = prizes,
                Packages = packages,
                OrderDate = DateTime.UtcNow,
                TotalPrice = totalPrice
            };

            await _orderRepo.AddOrder(order);
            await _cartService.PurchaseCart(userId);
        }

        public async Task<IEnumerable<ReadOrderDTO>> GetOrders()
        {
            var orders = await _orderRepo.GetOrders();
            return _mapper.Map<IEnumerable<ReadOrderDTO>>(orders);
        }

    }
}
