using ChineseAuctionAPI.Models;

namespace ChineseAuctionAPI.Interface
{
    public interface IUserRepo
    {
        public Task AddUser(User user);
        public Task LogInUser(User user);
    }
}
