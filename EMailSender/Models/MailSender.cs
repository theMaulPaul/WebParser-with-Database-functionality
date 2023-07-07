using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace EMailSender.Models
{
    public class MailSender
    {
        private string Server;
        private int Port;
        private string Username;
        private string Password;

        public MailSender(string server, int port, string username, string password)
        {
            Server = server;
            Port = port;
            Username = username;
            Password = password;
        }

        public void Send(string fromMail, string toMail, string subject, string body, string attachment = null)
        {
            MailMessage msg = new MailMessage(fromMail, toMail);
            msg.Subject = subject;
            msg.Body = body;

            if (!string.IsNullOrEmpty(attachment))
            {
                Attachment atch = new Attachment(attachment, MediaTypeNames.Application.Octet);
                msg.Attachments.Add(atch);
            }

            SmtpClient smtpClient = new SmtpClient(Server, Port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(Username, Password);

            try
            {
                smtpClient.Send(msg);
                Console.WriteLine("Email has been sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine("There been an error while sending your email " + ex.Message);
            }
            finally { msg.Dispose(); }
        }
    }
}
