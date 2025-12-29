using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.DTO;
using static ChineseAuctionAPI.DTO.UserDTO;
namespace ChineseAuctionAPI.Interface
{
    public interface IUserService
    {
        public Task AddUser(SignInDTO signInDTO);
        public Task<ResponseLogInDTO> LogInUser(LogInDTO user);
    }
}
