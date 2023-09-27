using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scoreboard.Contracts.Auths;
using Scoreboard.Contracts.Emails;
using Scoreboard.Contracts.Students;
using Scoreboard.Domain.Models;
using Scoreboard.Repository.Auths;
using Scoreboard.Service.Canvas.Students;
using Scoreboard.Service.Email;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Scoreboard.Api.Controllers.Auths
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IEmailServices _emailService;
        private readonly IStudentAppServices _studentAppServices;
        public AuthController(IAuthRepository authRepository, IEmailServices emailServices, IStudentAppServices studentAppServices)
        {
            _authRepository = authRepository;
            _emailService = emailServices;
            _studentAppServices = studentAppServices;
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
        [HttpPost("register-student")]
        public async Task<IActionResult> RegisterStudent(RegisterStudentDto input)
        {
            var result = await _studentAppServices.RegisterStudent(input);

            if (result is Student student)
            {
                return Ok(student);
            }

            return BadRequest(result);


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
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _authRepository.GetUser(email);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var resetToken = await _authRepository.GenerateResetToken(user);
            var resetLink = Url.Action(nameof(ResetPassword), "Auth", new { token = resetToken, email = user.Email }, Request.Scheme);

            _emailService.SendPasswordResetEmail(user.Email, resetLink!);

            return Ok("Password reset request successful, please check your email for further instructions");
        }

        [HttpGet("reset-password")]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordDto { Token = token, Email = email };
            return Ok(model);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _authRepository.GetUser(resetPasswordDto.Email);
            if (user == null)
            {
                return BadRequest($"User not found with {resetPasswordDto.Email}");
            }

            var resetPasswordResult = await _authRepository.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (resetPasswordResult.Succeeded)
            {
                _emailService.SendEmail(new MessageDto(new List<string> { user.Email }, "Password Changed Successfully", "Password Changed Successfully"));
                return Ok();
            }

            resetPasswordResult.Errors.ToList().ForEach(error => ModelState.AddModelError(error.Code, error.Description));
            return BadRequest(ModelState);
        }

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
