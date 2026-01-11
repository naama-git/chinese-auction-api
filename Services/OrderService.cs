using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using System.Transactions;
using static ChineseAuctionAPI.DTO.PackageDTO;
using static ChineseAuctionAPI.DTO.TicketDTO;

namespace ChineseAuctionAPI.Services
{
    public class OrderService : IOrderService
    {
        private const string Location = "OrderService";
        private readonly IOrderRepo _orderRepo;

        private readonly ITicketService _ticketService;
        private readonly IPrizeService _prizeService;
        private readonly IPackageService _packageService;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        

        private readonly IMapper _mapper;

        public OrderService(IOrderRepo orderRepo, ITicketService ticketService, IPrizeService prizeService, IPackageService packageService,ICartService cartService, IUserService userService, IMapper mapper)
        {
            _orderRepo = orderRepo;

            _ticketService = ticketService;
            _prizeService = prizeService;
            _userService = userService;
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
                    var user= await _userService.GetUserById(userId);
                    if (user == null){
                        throw new ErrorResponse(404, "AddOrder", "User not found", $"User with the provided ID {userId} does not exist.", "POST", Location);
                    }
                    var cart = await _cartService.GetCartByUserId(userId);
                    if (cart == null)
                    {
                        throw new ErrorResponse(404, "AddOrder", "Cart not found", $"Cart for user with the provided ID {userId} does not exist.", "POST", Location);
                    }

                    var cartItems = cart.CartItems;
                    if (cartItems == null || cartItems.Count == 0)
                        throw new ErrorResponse(400, "AddOrder", "Cart is empty", $"Cart for user with the provided ID {userId} is empty.", "POST", Location);


                    var prizesList = cartItems.Where(ci => ci.Prize != null).Select(ci => ci.PrizeId).ToList();

                    var prizes=await _prizeService.GetPrizesByIds(prizesList);

                    // add tickets to DB
                    List <TicketCreateDTO> tickets = [];
                    foreach (var item in cartItems)
                    {
                        var prizeId = item.PrizeId;
                        if (prizeId == 0) continue;
                        for (int i = 0; i < item.Quantity; i++)
                        {
                            tickets.Add(new TicketCreateDTO { UserId = userId, PrizeId = prizeId });

                        }
                        await _ticketService.AddTicketsRange(tickets, prizeId);
                        tickets = [];
                    }
                    

                    
                    // check total quantity of prizes vs total number of tickets in packages
                    var packages = await _packageService.GetPackagesByIds(PackagesIds);
                    var totalQty= cartItems.Sum(ci => ci.Quantity);
                    var totalNumOfTIckets=packages.Sum(p => p.NumOfTickets);
                    if(totalQty != totalNumOfTIckets )
                    {
                        throw new ErrorResponse(400, "AddOrder", $"Insufficient tickets: {totalQty} vs {totalNumOfTIckets}", $"The total quantity of prizes in the cart exceeds the total number of tickets in the selected packages.", "POST", Location);
                    }

                    // calculate price
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
                    throw new ErrorResponse(500, "AddOrder", "Internal Server Error", $"Something went wrong: {ex.Message}", "POST", Location);
                }


            }

        }

        public async Task<IEnumerable<ReadOrderDTO>> GetOrders()
        {
            var orders = await _orderRepo.GetOrders();
            if (orders == null || !orders.Any())
            {
                return Enumerable.Empty<ReadOrderDTO>();
            }
            return _mapper.Map<IEnumerable<ReadOrderDTO>>(orders);
        }

    }
}
