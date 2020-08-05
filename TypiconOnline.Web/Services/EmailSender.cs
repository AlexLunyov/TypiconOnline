using MimeKit;
using MailKit.Net.Smtp;
using System;
using TypiconOnline.AppServices.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Security;

namespace TypiconOnline.Web.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "admin@typicon.online"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.CheckCertificateRevocation = true;
                    await client.ConnectAsync("typicon.online", 25, SecureSocketOptions.None);
                }
                catch (SslHandshakeException ex)
                {
                    client.CheckCertificateRevocation = false;
                    await client.ConnectAsync("typicon.online", 25, false);
                }
                
                await client.AuthenticateAsync("admin@typicon.online", "&Q.n*8qc");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
