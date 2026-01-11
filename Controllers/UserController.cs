using ChineseAuctionAPI.DTO;
using ChineseAuctionAPI.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static ChineseAuctionAPI.DTO.UserDTO;

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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ReadUserDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
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

            return Ok(user);
        }

<<<<<<< HEAD
     
        [HttpPost("logIn")]
        public async Task<IActionResult> LogInUser([FromBody] LogInDTO logInDTO)
=======

        [HttpPost("LogIn")]
        public async Task<ActionResult<ResponseUserDTO>> LogInUser([FromBody] LogInDTO logInDTO)
>>>>>>> e72b92878823898f83683f46179bd59d07ddba7e
        {
           

            var validationResult = await _logInValidator.ValidateAsync(logInDTO);

            if (!validationResult.IsValid)
            {

                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var user = await _userService.LogInUser(logInDTO);

            if (user == null || user.Token.Equals(""))
                return Unauthorized(new { message = "Invalid username or password" });

            return Ok(user);
        }

        [HttpGet("me")]
        public async Task<ActionResult<ResponseUserDTO>> Me()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Unauthorized(new { message = "No Authorization header found" });
            }
            

            var userEmail = User.FindFirstValue("Email");
            if (userEmail==null)
            {
                return Unauthorized(new { message = "Token is not valid" });
            }
            var user = await _userService.Me(userEmail);
            if (user == null || user.Token.Equals(""))
                return Unauthorized(new { message = "Invalid username or password" });
            return Ok(user);
        }
    }
}