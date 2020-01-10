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

        public string RandomRef(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            DateTime datenow = DateTime.Now;
            string stringdate = datenow.ToString("dd/MM/yy HH:mm:ss");
            string stringUni = stringdate.Substring(0, 2) + stringdate.Substring(3, 2) + stringdate.Substring(6, 2) + stringdate.Substring(9, 2) + stringdate.Substring(12, 2) + stringdate.Substring(15, 2);
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray())+stringUni;
        }

        public string RandomOpt(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public void EmailSent(string email, string refNo, string optNo)
        {
            var message = new System.Net.Mail.MailMessage();

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtpm.csloxinfo.com");
            mail.IsBodyHtml = true;
            mail.From = new MailAddress("student@enixer.net");
            mail.To.Add(email);
            mail.Subject = "Hackathon 1 OTP";
            mail.Body = "This is your OTP for login " + optNo;
            mail.Body += "<br />Reference number " + refNo;
            mail.Body += "<br />OTP Expire in 3 minutes.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("student@enixer.net", "Gg123456789");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        public bool PasswordAuth(string pwdInput, string pwdDb, string pwdSalt)
        {
            byte[] salt = Convert.FromBase64String(pwdSalt);

            var pbkdf2 = new Rfc2898DeriveBytes(pwdInput, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string pwdInputSalt = Convert.ToBase64String(hashBytes);

            if (pwdDb == pwdInputSalt)
                return true;
            else
                return false;
        }

        public string GeneratePassword(string password, string saltString)
        {
            byte[] salt = Convert.FromBase64String(saltString);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        public string GenerateSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            string res = Convert.ToBase64String(salt);
            return res;
        }

        public string GenAccountNo()
        {
            Random random = new Random();
            string letter = "0123456789";

            var genAccNo = new StringBuilder();

            for (int i = 1; i <= 10; i++)
            {
                var randomAccNo = letter[random.Next(0, letter.Length)];
                genAccNo.Append(randomAccNo);
            }

            return genAccNo.ToString();
        }

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
