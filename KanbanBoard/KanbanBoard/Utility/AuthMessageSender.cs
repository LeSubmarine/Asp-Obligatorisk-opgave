using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using KanbanBoard.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace KanbanBoard.Utility
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public AuthMessageSender(IOptions<SMSoptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public SMSoptions Options { get; }  // set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage mMessage = new MailMessage();
            mMessage.To.Add(email);
            mMessage.Subject = subject;
            mMessage.IsBodyHtml = true;
            mMessage.Body = htmlMessage;
            mMessage.From = new MailAddress("clashitclashitbruv@gmail.com");
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            // smtp 
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("clashitclashitbruv@gmail.com", "ClashIt123123");

            smtpClient.SendMailAsync(mMessage);
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            ASPSMS.SMS SMSSender = new ASPSMS.SMS();

            SMSSender.Userkey = Options.SMSAccountIdentification;
            SMSSender.Password = Options.SMSAccountPassword;
            SMSSender.Originator = Options.SMSAccountFrom;

            SMSSender.AddRecipient(number);
            SMSSender.MessageData = message;

            SMSSender.SendTextSMS();

            return Task.FromResult(0);
        }
    }
    
}
