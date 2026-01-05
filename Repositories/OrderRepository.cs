using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Repositories
{
    public class OrderRepository: IOrderRepo
    {
        private readonly ChineseAuctionDBcontext _context;
        private readonly string RepoLocation = "OrderRepository";

        public OrderRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }


        public async Task AddOrder(Order order)
        {
            try
            {
                await _context.orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "AddOrder", "Failed to create the order.", ex.Message, "POST", RepoLocation);
            }

        }

       
        public async Task<IEnumerable<Order>> GetOrders()
        {
            try
            {
                var orders= await _context.orders.Include(d => d.Prizes).Include(u => u.User).ToListAsync();
                return orders;
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetOrders", "Failed to get the orders.", ex.Message, "Get", RepoLocation);
            }

        }

        
    }
}
