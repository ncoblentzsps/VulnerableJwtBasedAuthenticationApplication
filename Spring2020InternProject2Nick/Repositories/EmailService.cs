using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Spring2020InternProject2Nick.Models;

namespace Spring2020InternProject2Nick.Repositories
{
    public class EmailService : IEmailService
    {
        IConfiguration _configuration;
        private SmtpClient _client;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new SmtpClient(_configuration["Email:Host"], Convert.ToInt32(_configuration["Email:Port"]));
            _client.Credentials = new NetworkCredential(_configuration["Email:Username"], _configuration["Email:Password"]);
            _client.EnableSsl = true;            
        }
        public void EmailTwoFactorCode(HRUser user)
        {
            string message = $"Hello {user.FirstName} {user.LastName},\n\nYour code is: {user.TwoFactorCode}";
            SendEmailAsync(user.Email, "Two Factor Code", message);
        }
        public Task SendEmailAsync(string recipient, string subject, string message)
        {
            using (MailMessage mailMessage = new MailMessage(_configuration["Email:From"], recipient, subject, message))
            {                                
                
                try
                {                    
                    _client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: {0}", ex.ToString());
                }
            }
            return null;
        }

    }
}
