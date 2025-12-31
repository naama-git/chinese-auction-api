using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Repositories
{
    public class OrderRepository: IOrderRepo
    {
        private readonly ChineseAuctionDBcontext _context;

        public OrderRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }


        public async Task AddOrder(Order order)
        {
            await _context.orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

       

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.orders.Include(d=>d.Prizes).Include(u=>u.User).ToListAsync();
        }

        
    }
}
