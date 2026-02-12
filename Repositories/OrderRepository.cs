using AutoFilterer.Extensions;
using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace ChineseAuctionAPI.Repositories
{
    public class OrderRepository : IOrderRepo
    {
        private readonly ChineseAuctionDBcontext _context;
        private readonly string RepoLocation = "OrderRepository";

        public OrderRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }


        public async Task<Order> AddOrder(Order order)
        {
            try
            {
                await _context.orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return order;
               
                
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "AddOrder", "Failed to create the order.", ex.Message, "POST", RepoLocation);
            }

        }


        public async Task<IEnumerable<Order>> GetOrders(OrderQParams orderQParams)
        {
            try
            {
                var orders = _context.orders
                    .Include(d => d.Prizes)
                    .Include(u => u.User)
                    .Include(p => p.Packages)
                    .ApplyFilter(orderQParams);

                if (!string.IsNullOrEmpty(orderQParams.UserEmail))
                {
                    orders = orders.Where(o =>
                        o.User.Email.Contains(orderQParams.UserEmail));
                }

                if (orderQParams.PackagesIds?.Any() == true)
                {
                    orders = orders.Where(o =>
                        o.Packages.Any(p =>
                            orderQParams.PackagesIds.Contains(p.Id)));
                }

                if (orderQParams.PrizesIds?.Any() == true)
                {
                    orders = orders.Where(o =>
                        o.Prizes.Any(p =>
                            orderQParams.PrizesIds.Contains(p.Id)));
                }


                return await orders.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetOrders", "Failed to get the orders.", ex.Message, "Get", RepoLocation);
            }

        }

        public async Task<Order> GetOrderById(int id)
        {
            try
            {
                var order = await _context.orders

                    .Include(d => d.Prizes)
                    .Include(u => u.User)
                    .Include(p => p.Packages)
                    .FirstOrDefaultAsync(p => p.Id == id);

                return order;
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetOrderById", "Failed to get order with Id" + id, ex.Message, "Get", RepoLocation);
            }
        }


    }
}
