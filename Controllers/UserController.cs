using Microsoft.AspNetCore.Mvc;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
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
        public async Task<IActionResult> AddUser([FromBody] SignInDTO signIn)
        {
            try
            {
                string token = await _userService.AddUser(signIn);
                return CreatedAtAction(nameof(AddUser), new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

     
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogInUser([FromBody] LogInDTO logInDTO)
        {
            string token = await _userService.LogInUser(logInDTO);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "שם משתמש או סיסמה שגויים" });
            }

            return Ok(new
            {
                token = token,
                message = "התחברת בהצלחה!"
            });
        }
    }
}