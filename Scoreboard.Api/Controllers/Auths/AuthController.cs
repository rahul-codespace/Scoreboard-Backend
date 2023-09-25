using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Auths;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Auths;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Scoreboard.Api.Controllers.Auths
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterUserDto input)
        {
            var user = new ScoreboardUser
            {
                Name = input.Name,
                Email = input.Email,
                UserName = input.Email
            };
            var result = await _authRepository.Register(user, input.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginUserDto input)
        {
            var token = await _authRepository.Login(input.Email, input.Password, input.RememberMe);
            if (token == null)
            {
                return BadRequest("Invalid Credentials");
            }
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> LogoutAsync()
        {
            await _authRepository.Logout();
            return Ok();
        }

        //[HttpGet]
        //public IActionResult ResetPassword(string token, string email)
        //{
        //    var model = new ResetPasswordDto { Token = token, Email = email };
        //    return Ok(model);
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ActionResult> ResetPasswordAsync(ResetPasswordDto input)
        //{
        //    var user = await _authRepository.GetUser(input.Email);
        //    if (user == null)
        //    {
        //        return BadRequest("Invalid email");
        //    }
        //    var result = await _authRepository.ChangePassword(user, input.Token, input.Password);
        //    if (!result.Succeeded)
        //    {
        //        return BadRequest(result.Errors);
        //    }
        //    return Ok();
        //}

        [Authorize]
        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto input)
        {
            var user = await _authRepository.GetUser(User.FindFirstValue(ClaimTypes.Email)!);
            if (user == null)
            {
                return BadRequest("Invalid email");
            }
            var result = await _authRepository.ChangePassword(user, input.CurrentPassword, input.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok();
        }
    }
}
