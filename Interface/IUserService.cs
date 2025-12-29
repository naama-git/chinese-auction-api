using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.DTO;
using static ChineseAuctionAPI.DTO.UserDTO;
namespace ChineseAuctionAPI.Interface
{
    public interface IUserService
    {
        public Task<ResponseUserDTO> AddUser(SignInDTO signInDTO);
        public Task<ResponseUserDTO> LogInUser(LogInDTO user);
    }
}
