using System;
using System.Net.Mail;

namespace MBlog.Domain.Helpers
{
    public static class EmailHelper
    {
        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress _ = new MailAddress(emailaddress);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
