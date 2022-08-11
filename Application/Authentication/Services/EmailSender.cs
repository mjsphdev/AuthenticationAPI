using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Application.Authentication.Services
{
    public static class EmailSender
    {
        public static async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mail = "apitrial123@gmail.com";
            var server = "smtp.gmail.com";
            var port = 587;
            var password = "xkzrcgghwamkexxy";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(mail);
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            message.Body = "<html><body> " + htmlMessage + " </body></html>";
            message.IsBodyHtml = true;

            using (SmtpClient client = new SmtpClient())
            {
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(mail, password);
                client.Host = server;
                client.Port = port;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                client.Send(message);
            }
        }
    }
}

