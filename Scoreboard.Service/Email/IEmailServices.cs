using Scoreboard.Contracts.Emails;

namespace Scoreboard.Service.Email
{
    /// <summary>
    /// Interface for sending email messages.
    /// </summary>
    public interface IEmailServices
    {
        /// <summary>
        /// Sends an email message.
        /// </summary>
        /// <param name="emailDto">The email message to be sent.</param>
        void SendEmail(MessageDto emailDto);

        /// <summary>
        /// Sends a password reset email to the specified recipient email address.
        /// </summary>
        /// <param name="recipientEmail">The email address of the password reset recipient.</param>
        /// <param name="resetLink">The link to reset the password.</param>
        void SendPasswordResetEmail(string recipientEmail, string resetLink);
    }
}