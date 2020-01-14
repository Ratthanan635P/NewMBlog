using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace MBlog.Domain.Services
{
    public class CenterService
    {
        private Random random = new Random();         
        public void SendNewPassword(string email, string NewPassword)
        {
            var message = new System.Net.Mail.MailMessage();

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtpm.csloxinfo.com");
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("student@enixer.net");
            mail.To.Add(email);
            mail.Subject = "MBlog System<Auto>";
            mail.Body = "NewPassword :" + NewPassword;
            mail.Body += "<br /> ";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("student@enixer.net", "Gg123456789");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
