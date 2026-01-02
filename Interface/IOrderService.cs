

using ChineseAuctionAPI.DTO;

namespace ChineseAuctionAPI.Interface
{
    public interface IOrderService
    {
        public Task AddOrder(int userId, List<int> PackagesIds);
        public Task<IEnumerable<ReadOrderDTO>> GetOrders();
    }
}
