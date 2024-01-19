using Microsoft.AspNetCore.Mvc;
using Motorcycle.Application.Users.Models.Request;
using Motorcycle.Application.Users.Services;

namespace motorcycle_tcc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(UserRequest request)
        {
            var user = await _userService.CreateUserAsync(request);
            return Ok(user);
        }

        [HttpPost("auth")]
        public async Task<IActionResult> AuthAsync(UserAuthRequest request)
        {
            try
            {
                var authResult = await _userService.Login(request);

                if (authResult)
                {
                    return Ok(new { Message = "Authentication successful." });
                }
                else
                {
                    return Unauthorized(new { Message = "Authentication failed. Invalid credentials." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred during the authentication process." });
            }
        }


        [Route("name")]
        [HttpPut]
        public async Task<IActionResult> ChangeNameAsync(UserNameRequest request)
        {
            await _userService.UpdateUser(request);
            return Ok();
        }

        [Route("password")]
        [HttpPut]
        public async Task<IActionResult> ChangePasswordAsync(UserPasswordRequest request)
        {
            await _userService.UpdatePassword(request);
            return Ok();
        }

        [Route("forgotten-password")]
        [HttpPost]
        public async Task<IActionResult> ForgottenPasswordAsync(string email)
        {
            await _userService.ForgottenPasswordAsync(email);
            return Ok();
        }
    }
}
