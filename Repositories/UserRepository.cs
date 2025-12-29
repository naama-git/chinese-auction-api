using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ChineseAuctionAPI.Repositories
{
    public class UserRepository : IUserRepo
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

    
        // find user
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.users
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
