using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace KanbanBoard.Utility
{
    public class EmailService : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
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

            await smtpClient.SendMailAsync(mMessage);
        }
    }
}
