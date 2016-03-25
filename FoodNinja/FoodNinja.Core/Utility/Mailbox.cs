using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FoodNinja.Core.Utility
{
    public static class Mailbox
    {
        public static void SendEmail(string to, string from, string subject, string message)
        {
            var client = new SmtpClient();
            client.Credentials = new NetworkCredential("cwsoftwareinc@gmail.com", "@b9kyu04_cw");
            client.Port = Convert.ToInt32(587);
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";

            var email = new MailMessage();

            email.To.Add(new MailAddress(to));
            email.From = new MailAddress(from);
            email.Subject = subject;
            email.Body = message;
            email.IsBodyHtml = true;
            email.BodyEncoding = Encoding.UTF8;

            client.Send(email);
        }
    }
}
