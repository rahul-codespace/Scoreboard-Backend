using Microsoft.AspNetCore.Identity;
using Scoreboard.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Scoreboard.Repository.Auths
{
    /// <summary>
    /// Provides authentication and user management functionality.
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Registers a new user with the provided password.
        /// </summary>
        /// <param name="user">The user model.</param>
        /// <param name="password">User's password.</param>
        /// <returns>Returns an IdentityResult indicating the outcome of the registration process.</returns>
        Task<IdentityResult> Register(ScoreboardUser user, string password);

        /// <summary>
        /// Logs in a user with the provided email and password.
        /// </summary>
        /// <param name="email">User's email address.</param>
        /// <param name="password">User's password.</param>
        /// <param name="rememberMe">Indicates whether to remember the user's login.</param>
        /// <returns>Returns a JWT security token upon successful login; otherwise, returns null.</returns>
        Task<JwtSecurityToken> Login(string email, string password, bool rememberMe);

        /// <summary>
        /// Logs out the currently authenticated user.
        /// </summary>
        Task Logout();

        /// <summary>
        /// Retrieves a user by their email address.
        /// </summary>
        /// <param name="email">User's email address.</param>
        /// <returns>Returns the user if found; otherwise, returns null.</returns>
        Task<ScoreboardUser> GetUser(string email);

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="user">The user whose password should be changed.</param>
        /// <param name="currentPassword">The current password of the user.</param>
        /// <param name="newPassword">The new password to set for the user.</param>
        /// <returns>Returns an IdentityResult indicating the outcome of the password change process.</returns>
        Task<IdentityResult> ChangePassword(ScoreboardUser user, string currentPassword, string newPassword);

        /// <summary>
        /// Updates user information.
        /// </summary>
        /// <param name="user">The user model to update.</param>
        /// <returns>Returns an IdentityResult indicating the outcome of the update process.</returns>
        Task<IdentityResult> UpdateUser(ScoreboardUser user);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="user">The user to delete.</param>
        /// <returns>Returns an IdentityResult indicating the outcome of the user deletion process.</returns>
        Task<IdentityResult> DeleteUser(ScoreboardUser user);

        /// <summary>
        /// Generates a password reset token for a user.
        /// </summary>
        /// <param name="user">The user for whom to generate the reset token.</param>
        /// <returns>Returns the password reset token.</returns>
        Task<string> GenerateResetToken(ScoreboardUser user);
    }
}
