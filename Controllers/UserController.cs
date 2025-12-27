using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using Microsoft.AspNetCore.Mvc;
using static ChineseAuctionAPI.DTO.UserDTO;
namespace ChineseAuctionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> AddUser(SignInDTO signIn)
        {
            await _userService.AddUser(signIn);
            return StatusCode(201);
        }
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogInUser(LogInDTO logInDTO)
        {
            await _userService.LogInUser(logInDTO);
            return StatusCode(201);
        }
    }
}
