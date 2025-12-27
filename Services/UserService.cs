using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using static ChineseAuctionAPI.DTO.UserDTO;


namespace ChineseAuctionAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;
        public UserService(IUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task AddUser(SignInDTO user)
        {
            User userEntity = _mapper.Map<User>(user);
            await _repo.AddUser(userEntity);
        }
        public async Task LogInUser(LogInDTO user)
        {
            User userEntity = _mapper.Map<User>(user);

            await _repo.LogInUser(userEntity);
        }
    }
}
