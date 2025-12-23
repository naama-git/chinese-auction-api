using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Data
{
    public class ChineseAuctionDBcontext:DbContext
    {
        public ChineseAuctionDBcontext(DbContextOptions<ChineseAuctionDBcontext> options) : base(options) { }

        public DbSet<Donor> donors { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Prize> prizes { get; set; }
        public DbSet<Package> packages { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Ticket> tickets { get; set; } 
        public DbSet<Winner> winners {  get; set; } 

    }
}
