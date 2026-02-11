

using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Interface
{
    public interface IOrderRepo
    {
        public Task<Order> AddOrder(Order order);
        public Task<IEnumerable<Order>> GetOrders(OrderQParams orderQParams);
        public Task<Order> GetOrderById(int id);

    }
}
