using Scoreboard.Contracts.Emails;

namespace Scoreboard.Service.Email
{
    public interface IEmailServices
    {
        void SendEmail(MessageDto email);
        void SendPasswordResetEmail(string recipientEmail, string resetLink);
    }
}