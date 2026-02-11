

using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Interface
{
    public interface IOrderService
    {
        public Task<ReadOrderDTO> AddOrder(int userId, List<int> PackagesIds);
        public Task<IEnumerable<ReadSimpleOrderDTO>> GetOrders(OrderQParams orderQParams);
        public Task<ReadOrderDTO> GetOrderById(int id);
    }
}
