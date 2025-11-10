using doctors.services.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace doctors.services.impelemtion
{
    public class Emailservis : IEmail
    {
        public async Task SendEmailAsync(string email, string verificationCode)
        {
            var fromEmail = "youssefraslan00000@gmail.com";
            var password = "mynemwodkpjoxuqm";

            var msg = new MailMessage();
            msg.IsBodyHtml = true;
            msg.From = new MailAddress(fromEmail);
            msg.To.Add(email);
            msg.Subject = "Verify Your Email";
            msg.Body = $@"
            <h3>Email Verification</h3>
            <p>Your verification code is: <strong>{verificationCode}</strong></p>
            <p>This code will expire in 10 minutes.</p>
        ";

            using var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail, password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            await smtpClient.SendMailAsync(msg);
        }
    }
}
