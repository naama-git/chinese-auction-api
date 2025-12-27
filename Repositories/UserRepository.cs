using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Repositories
{
    public class UserRepository :IUserRepo
    {
        private readonly ChineseAuctionDBcontext _context;

        public UserRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }
        //SignIn -Add new User
        public async Task AddUser(User user)
        {
            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        //LogIn -Exsist User
        public async Task LogInUser(User user)
        {
            try
            {
                await _context.users.FindAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserName Or Password is Invalide");
            }
        }
    }
}
