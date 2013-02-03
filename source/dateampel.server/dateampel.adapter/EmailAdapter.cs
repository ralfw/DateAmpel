using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace dateampel.adapter
{
    public interface IEmailAdapter
    {
        
    }

    public class YahooEmailAdapter
    {
        public void Send(string receiverEmail, string betreff, string text)
        {
            var client = new SmtpClient("smtp.mail.yahoo.de");
            var from = new MailAddress("dateampel@yahoo.de", "Die Date Ampel");
            var to = new MailAddress(receiverEmail);
            using (var message = new MailMessage(from, to))
            {
                message.Subject = betreff;
                message.SubjectEncoding = Encoding.UTF8;

                message.Body = text;
                message.BodyEncoding = Encoding.UTF8;

                client.SendAsync(message, null);
            }
        }
    }
}
