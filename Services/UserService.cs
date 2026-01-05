using AutoMapper;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Models;
using ChineseAuctionAPI.Models.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static ChineseAuctionAPI.DTO.UserDTO;


namespace ChineseAuctionAPI.Services
{
    public class UserService : IUserService
    {

        private const string Location = "UserService";
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepo repo, IMapper mapper, IConfiguration configuration)
        {
            _repo = repo;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<IEnumerable<ReadUserDTO>> GetAllUsers()
        {

            var users = await _repo.GetAllUsers();
            if(users == null || !users.Any())
            {
                return Enumerable.Empty<ReadUserDTO>();
            }
            return _mapper.Map<IEnumerable<ReadUserDTO>>(users);
        }


        public async Task<ReadUserDTO> GetUserById(int id)
        {
            var user= await _repo.GetUserById(id);
            if (user == null)
            {
                throw new ErrorResponse(404, "GetUserById", "User not found.", $"No user exists with the Id: {id}.", "GET", Location);
            }
            return _mapper.Map<ReadUserDTO>(user);
        }


        public async Task<ResponseUserDTO> AddUser(SignInDTO user)
        {
            var existingUser = await _repo.GetUserByEmail(user.Email);
            if (existingUser != null)
            {
                throw new ErrorResponse(
                    statusCode: 409, func: "AddUser",message: "Unable to complete registration with these details",
                    detailedMessage: $"Registration failed: Email {user.Email} already exists in the system.",
                    method: "POST", Location
                );
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Password = passwordHash;

            User userEntity = _mapper.Map<User>(user);
            if (userEntity == null) {
                throw new ErrorResponse(500, "AddUser", "Internal Server Error", "AutoMapper failed to map SignInDTO to User entity.", "POST", Location);
            }
            userEntity.Role = "User";

            await _repo.AddUser(userEntity);
            if (userEntity.Id == 0)
            {
                throw new ErrorResponse(500, "AddUser", "Failed to save user", "Database insert operation returned no ID.", "POST", Location);
            }

            string token = CreateToken(userEntity);
            if (string.IsNullOrEmpty(token)) {
                throw new ErrorResponse(500, "AddUser", "Internal Server Error", "JWT Token generation returned null or empty.", "POST", Location);
            }

            ResponseUserDTO resUser = new ResponseUserDTO()
            {
                Token = token,
                Email = userEntity.Email,
                Name = userEntity.FirstName + " " + userEntity.LastName

            };
            return resUser;

        }

        public async Task<ResponseUserDTO> LogInUser(LogInDTO user)
        {

            User existingUser = await _repo.GetUserByEmail(user.Email);

           
            if (existingUser == null)
            {
                throw new ErrorResponse(401, "LogInUser", "Invalid email or password", "user not found", "POST",Location );
            }

            
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);

            if (!isPasswordCorrect)
            {
                throw new ErrorResponse(401, "LogInUser", "Invalid email or password", "Password validation failed", "POST", Location);
            }

            return new ResponseUserDTO
            {
                Email = existingUser.Email,
                Name = $"{existingUser.FirstName} {existingUser.LastName}",
                Token = CreateToken(existingUser)
            };

        }


        private string CreateToken(User user)
        {
            // the token will include this user details
            var claims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Email, user.Email),
                    new (ClaimTypes.Name, user.FirstName),
                    new(ClaimTypes.Role, user.Role)
                };

            // the secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // creating the token
            var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds);


            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }


    }
}
