using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using System.Transactions;
using static ChineseAuctionAPI.DTO.PackageDTO;
using static ChineseAuctionAPI.DTO.TicketDTO;

namespace ChineseAuctionAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;

        private readonly ITicketService _ticketService;
        private readonly IPrizeService _prizeService;
        private readonly IPackageService _packageService;
        private readonly ICartService _cartService;
        

        private readonly IMapper _mapper;

        public OrderService(IOrderRepo orderRepo, ITicketService ticketService, IPrizeService prizeService, IPackageService packageService,ICartService cartService, IMapper mapper)
        {
            _orderRepo = orderRepo;

            _ticketService = ticketService;
            _prizeService = prizeService;
            _packageService = packageService;
            _cartService=cartService;

            _mapper = mapper;
        }


        public async Task AddOrder(int userId, List<int> PackagesIds)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted
            };

            using (var scope = new TransactionScope(
            TransactionScopeOption.Required,
            transactionOptions,
            TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    
                    var cart = await _cartService.GetCartByUserId(userId) ?? throw new Exception("no cart found");

                    var cartItems = cart.CartItems;
                    if (cartItems == null || cartItems.Count == 0)
                        throw new Exception("cart is empty");


                    var prizesList = cartItems.Where(ci => ci.Prize != null).Select(ci => ci.PrizeId).ToList();

                    if (prizesList.Count == 0)
                        throw new Exception("no prizes in cart");
                    var prizes=await _prizeService.GetPrizesByIds(prizesList);

                    // add tickets to DB
                    List <TicketCreateDTO> tickets = new List<TicketCreateDTO>();
                    foreach (var item in cartItems)
                    {
                        var prizeId = item.PrizeId;
                        if (prizeId == null) continue;
                        for (int i = 0; i < item.Quantity; i++)
                        {
                            tickets.Add(new TicketCreateDTO { UserId = userId, PrizeId = prizeId });

                        }

                    }
                    await _ticketService.AddTicketsRange(tickets);


                    // calculate price
                    
                    var packages = await _packageService.GetPackagesByIds(PackagesIds);
                    
                    double totalPrice = packages.Sum(p => p.Price);

                    var order = new Order
                    {
                        UserId = userId,
                        Prizes = prizes.ToList(),
                        Packages = packages.ToList(),
                        OrderDate = DateTime.UtcNow,
                        TotalPrice = totalPrice
                    };
                  
                    await _orderRepo.AddOrder(order);
                    await _cartService.PurchaseCart(userId);

                    scope.Complete();

                }
                catch (Exception ex)
                {
                    throw new Exception($"Someting went wrong :{ex.Message} ");
                }


            }

        }

        public async Task<IEnumerable<ReadOrderDTO>> GetOrders()
        {
            var orders = await _orderRepo.GetOrders();
            return _mapper.Map<IEnumerable<ReadOrderDTO>>(orders);
        }

    }
}
