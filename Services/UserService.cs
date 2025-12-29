using AutoMapper;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static ChineseAuctionAPI.DTO.UserDTO;


namespace ChineseAuctionAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepo repo, IMapper mapper, IConfiguration configuration)
        {
            _repo = repo;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task AddUser(SignInDTO user)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Password = passwordHash;

            User userEntity = _mapper.Map<User>(user);
            userEntity.IsAdmin = false;
            await _repo.AddUser(userEntity);
        }

        public async Task<ResponseLogInDTO> LogInUser(LogInDTO user)
        {

            User existingUser = await _repo.GetUserByEmail(user.Email);
            if (existingUser == null)
            {
                throw new Exception("Unauthorized user");
            }
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);


            ResponseLogInDTO resUser = new ResponseLogInDTO()
            {
                
                Email = existingUser.Email,
                Name = existingUser.FirstName + " " + existingUser.LastName

            };
            if (isPasswordCorrect)
            {
                string token=createToken(existingUser);
                resUser.Token = token;
                return resUser;

            }
            resUser.Token = null;
            return resUser;
           
        }


        private string createToken(User user)
        {
            // the token will include this user details
            var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Email, user.Email),
                };

            // the secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // creating the token
            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(3),
            signingCredentials: creds);


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }


    }
}
