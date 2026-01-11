using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.DTO;
using static ChineseAuctionAPI.DTO.UserDTO;
namespace ChineseAuctionAPI.Interface
{
    public interface IUserService
    {
        public Task<ResponseUserDTO> AddUser(SignInDTO signInDTO);
        public Task<ResponseUserDTO> LogInUser(LogInDTO user);
        public Task<IEnumerable<ReadUserDTO>> GetAllUsers();
        public Task<ReadUserDTO> GetUserById(int id);
        public Task<ResponseUserDTO> Me(string token);
    }
}
