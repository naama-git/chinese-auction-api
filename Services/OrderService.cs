using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IPackageRepo _packageService;
        private readonly IPrizeRepo _prizeService;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepo orderRepo, IPackageRepo packageService, IPrizeRepo prizeService, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _packageService = packageService;
            _prizeService = prizeService;
            _mapper = mapper;
        }


        public async Task AddOrder(CreateOrderDTO createOrderDTO)
        {
            var prizes=await _prizeService.GetPrizesByIds(createOrderDTO.PrizeIds);
            var packeges=await _packageService.GetPackagesByIds(createOrderDTO.PackagesIds);

            Order orderEntity =new Order
            {
                UserId = createOrderDTO.UserId,
                OrderDate = createOrderDTO.OrderDate,
                Prizes = prizes.ToList(),
                Packages = packeges.ToList()
            };
            await _orderRepo.AddOrder(orderEntity);
        }

        public async Task<IEnumerable<ReadOrderDTO>> GetOrders()
        {
            var orders = await _orderRepo.GetOrders();
            return _mapper.Map<IEnumerable<ReadOrderDTO>>(orders);
        }

    }
}
