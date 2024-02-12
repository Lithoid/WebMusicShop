using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Helpers;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace Services
{
    public class SendGridEmail:ISendGridEmail
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SendGridEmail> _logger;
        public SendGridEmail(IConfiguration config, ILogger<SendGridEmail> logger)
        {
            _config = config;
            _logger = logger;

        }

        public async Task SendEmailAsync(string toEmail, string subject, string message) { 
            await Execute( subject, message, toEmail);
        }

        private async Task Execute( string subject, string message, string toEmail)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("larue.prosacco0@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = message };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("larue.prosacco0@ethereal.email", "pcsKTWmxN31k3X375W");
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }



            /*
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("zhekacheter@gmail.com"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            var dummy = response.StatusCode;
            var dummy2 = response.Headers;
            _logger.LogInformation(response.IsSuccessStatusCode
                                   ? $"Email to {toEmail} queued successfully!"
                                   : $"Failure Email to {toEmail}");
            */
        }

    }
}
