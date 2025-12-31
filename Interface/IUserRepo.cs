using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Interface
{
    public interface IUserRepo
    {
        public Task AddUser(User user);
        public Task<User> GetUserByEmail(string email);
    }
}
