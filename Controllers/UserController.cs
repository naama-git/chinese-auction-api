using Microsoft.AspNetCore.Mvc;
using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using static ChineseAuctionAPI.DTO.UserDTO;
using FluentValidation;

namespace ChineseAuctionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<SignInDTO> _signInValidator;
        private readonly IValidator<LogInDTO> _logInValidator;

        public UserController(IUserService userService, IValidator<SignInDTO> signInValidator, IValidator<LogInDTO> logInValidator)
        {
            _userService = userService;
            _signInValidator = signInValidator;
            _logInValidator = logInValidator;

        }

        [HttpGet]
        public async Task<ActionResult<List<ReadUserDTO>>> GetAllUsers() { 
            var users=await _userService.GetAllUsers();
            return Ok(users);
        }


        [HttpPost("register")]
        public async Task<IActionResult> AddUser([FromBody] SignInDTO signIn)
        {

                var validationResult = await _signInValidator.ValidateAsync(signIn);

                if (!validationResult.IsValid)
                { 
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                }
                var user = await _userService.AddUser(signIn);

                
                if (user == null || string.IsNullOrEmpty(user.Token))
                {
                  
                    return BadRequest(new { message = "Registration failed. The details provided may be invalid or already in use." });
                }

                return Ok(new
                {
                    user,
                    token = user.Token,
                    message = "User registered and logged in successfully!"
                });
        }

     
        [HttpPost("logIn")]
        public async Task<IActionResult> LogInUser([FromBody] LogInDTO logInDTO)
        {
            var authHeader = Request.Headers["Authorization"].ToString();
           
           var validationResult = await _logInValidator.ValidateAsync(logInDTO);

                if (!validationResult.IsValid)
                {

                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                }
            
            var user = await _userService.LogInUser(logInDTO);

            if (user==null || user.Token.Equals(""))
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(new { 
                user , 
                token = user.Token, 
                message = "Logged in successfully!" 
            });
        }
    }
}