using MimeKit;
using Scoreboard.Contracts.Emails;
using MailKit.Net.Smtp;

namespace Scoreboard.Service.Email
{
    public class EmailServices : IEmailServices
    {
        private readonly EmailConfigurationDto _emailConfig;

        public EmailServices(EmailConfigurationDto emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendPasswordResetEmail(string recipientEmail, string resetLink)
        {
            var emailMessage = new MessageDto(new[] { recipientEmail }, "Forgot Password Link", resetLink);
            SendEmail(emailMessage);
        }

        public async void SendEmail(MessageDto emailDto)
        {
            var emailMessage = CreateEmailMessage(emailDto);
            MailSend(emailMessage);
        }

        private MimeMessage CreateEmailMessage(MessageDto message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(message.Subject, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            // Create the HTML part of the email
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message.Content; // Set the HTML content here

            // Attach the HTML part to the email message
            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }
        private async void MailSend(MimeMessage mailMessage)
        {
            var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.UserPassword);
                await client.SendAsync(mailMessage);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
