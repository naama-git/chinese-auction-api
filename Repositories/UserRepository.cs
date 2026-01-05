using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace ChineseAuctionAPI.Repositories
{
    public class UserRepository : IUserRepo
    {
        private readonly ChineseAuctionDBcontext _context;
        private const string RepoLocation = "UserRepository";

        public UserRepository(ChineseAuctionDBcontext context)
        {
            _context = context;
        }


        //SignIn -Add new User
        public async Task AddUser(User user)
        {
            try
            {
                await _context.users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "AddUser", "Failed to register the new user.", ex.Message, "POST", RepoLocation);
            }
        }

    
        // find user
        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var user = await _context.users
                    .FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    throw new ErrorResponse(404, "GetUserByEmail", "User not found.", $"No user exists with the email: {email}.", "GET", RepoLocation);
                }

                return user;
            }
            catch (ErrorResponse) { throw; } // Pass through our custom 404
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetUserByEmail", "An error occurred while retrieving the user profile.", ex.Message, "GET", RepoLocation);
            }
        }

        // get all users
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _context.users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ErrorResponse(500, "GetAllUsers", "Failed to retrieve the user list.", ex.Message, "GET", RepoLocation);
            }
        }

        public async Task<User> GetUserById(int id)
        {
            
                var user= await _context.users.FirstOrDefaultAsync(u=>u.Id==id);


                if (user == null)
                {
                    throw new ErrorResponse(404, "GetUserById", "User not found.", $"No user exists with the email: {id}.", "GET", RepoLocation);
                }
            return user;
            
            
        }

    }
}
