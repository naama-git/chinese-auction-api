using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI.Data
{
    public class DbContextFactory
    {

        private readonly IConfiguration _configuration;
        private static string connectionString;

        public DbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
             connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        
        public static ChineseAuctionDBcontext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChineseAuctionDBcontext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new ChineseAuctionDBcontext(optionsBuilder.Options);
        }
    }

}
