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
            
                var user = await _userService.AddUser(signIn);
               
                 if (string.IsNullOrEmpty(user.Token))
                 {
                    return Unauthorized(new { message ="Unauthorized user" });
                 }
                
                 return Ok(new
                   {
                     user,
                      token = user.Token,
                      message = "You Logged In successfully!"
                   });   
            
           
        }

     
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogInUser([FromBody] LogInDTO logInDTO)
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            Console.WriteLine($"Received Authorization Header: {authHeader}");
            
            var user = await _userService.LogInUser(logInDTO);

            if (string.IsNullOrEmpty(user.Token))
            {
                return Unauthorized(new { message = "שם משתמש או סיסמה שגויים" });
            }

            return Ok(new
            {
                 user,
                token = user.Token,
                message = "You Logged In successfully!"
            });
        }
    }
}