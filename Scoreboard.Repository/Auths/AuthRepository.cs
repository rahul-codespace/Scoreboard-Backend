using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Scoreboard.Data.Context;
using Scoreboard.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Scoreboard.Repository.Auths
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ScoreboardDbContext _context;
        private readonly UserManager<ScoreboardUser> _userManager;
        private readonly SignInManager<ScoreboardUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthRepository(ScoreboardDbContext context, UserManager<ScoreboardUser> userManager, SignInManager<ScoreboardUser> signInManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> Register(ScoreboardUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<JwtSecurityToken> Login(string email, string password, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user,password))
            {
                return null;
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            authClaims.AddRange((await _userManager.GetRolesAsync(user)).Select(role => new Claim(ClaimTypes.Role, role)));
            var expires = rememberMe ? DateTime.Now.AddDays(15) : DateTime.Now.AddDays(1);
            return GenerateTokenOptions(authClaims, expires);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ScoreboardUser> GetUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<IdentityResult> ChangePassword(ScoreboardUser user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result;
        }

        public async Task<IdentityResult> UpdateUser(ScoreboardUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult> DeleteUser(ScoreboardUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<string> GenerateResetToken(ScoreboardUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;
        }

        private JwtSecurityToken GenerateTokenOptions(List<Claim> authClaims, DateTime expires)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!);
            var tokenOptions = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        claims: authClaims,
                        expires: expires,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
            return tokenOptions;
        }

        public async Task<IdentityRole<int>> GetRole(string role)
        {
            var result = await _context.Roles.FirstOrDefaultAsync(r => r.Name == role);
            return result;
        }

        public async Task<IdentityResult> AddRoleToUser(ScoreboardUser user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result;
        }
    }
}
