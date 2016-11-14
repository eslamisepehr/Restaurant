using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Restaurant.Helper
{
    public class Email
    {
        public string SendMail(string To, string Subject, string Message)
        {
            try
            {
                SmtpClient SmtpServer = new SmtpClient("mail.eslamisepehr.com");
                MailMessage mail = new MailMessage();
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("[Email]", "[password]");
                mail.From = new MailAddress("[Email]");
                mail.To.Add(To);
                mail.Subject = Subject;
                mail.Body = Message;
                SmtpServer.Send(mail);

                return "Send";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }
    }
}