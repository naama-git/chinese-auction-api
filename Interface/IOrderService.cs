

using ChineseAuctionAPI.DTO;

namespace ChineseAuctionAPI.Interface
{
    public interface IOrderService
    {
        public Task AddOrder(CreateOrderDTO createOrderDTO);
        public Task<IEnumerable<ReadOrderDTO>> GetOrders();
    }
}
