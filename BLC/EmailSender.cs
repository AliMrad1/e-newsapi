using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BLC
{
    internal class EmailSender
    {
        private static readonly string subject = "verify your account";
        private IConfiguration _configuration;

        public EmailSender(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public void sendEmail(string toEmail, string verificationToken)
        {
            var smtpClient = new SmtpClient("smtp.office365.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_configuration["credentials:email"], _configuration["credentials:password"]),
                EnableSsl = true
            };

            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["credentials:email"]),
                Subject = subject,
                IsBodyHtml = true
            };

            string FullVerificationLink = $"http://localhost:5134/verify?token={verificationToken}";

            string body = $@"
                <html>
                <body>
                    <h1>Account Verification</h1>
                    <p>Thank you for registering an account. Please use the following verification token:</p>
                    <p><strong>{FullVerificationLink}</strong></p>
                    <p>Enter the token on the verification page to complete your account setup.</p>
                </body>
                </html>";

            mailMessage.Body = body;

            mailMessage.To.Add(new MailAddress(toEmail));

            // Send the email
            smtpClient.Send(mailMessage);
        }
    }
}
