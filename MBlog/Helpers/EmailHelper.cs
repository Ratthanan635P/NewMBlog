using System;
using System.Net.Mail;

namespace MBlog.Helpers
{
    public static class EmailHelper
    {
        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
