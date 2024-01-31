using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace TestWebApplication.Data
{
    public class EmailService
    {
        public static async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "dmtrsedov.loc@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("dmtrsedov.loc@mail.ru", "az2dUWjLB9ABv5myHSbS");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
