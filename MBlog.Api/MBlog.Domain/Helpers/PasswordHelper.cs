using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MBlog.Domain.Helpers
{
    public static class PasswordHelper
    {
        private const int startIndex = 13;
        private const int saltSize = 20;
        private const int passwordMinLength = 6;

        public static string CreatePasswordHashed(string password)
        {
            var salt = GetSalt();
            string hashed = HashPassword(password, salt);

            return hashed;
        }

        private static string HashPassword(string password, byte[] salt)
        {
            string strSalt = Convert.ToBase64String(salt);

            if (strSalt.Length > saltSize)
            {
                strSalt = strSalt.Substring(0, saltSize);
                salt = Convert.FromBase64String(strSalt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            hashed = hashed.Insert(startIndex, strSalt);


            return hashed;
        }

        private static byte[] GetSalt()
        {
            byte[] salt = new byte[15];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public static bool ValidatePassword(string password, string passwordHashed)
        {
            //string strSalt = passwordHashed.Substring(startIndex, saltSize);
            //var salt = Convert.FromBase64String(strSalt);
            //string hashed = HashPassword(password, salt);

            //return hashed == passwordHashed;
            return password == passwordHashed;
        }

        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < passwordMinLength)
            {
                return false;
            }

            string strongPasswordPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{" + passwordMinLength + ",})";

            return Regex.Match(password, strongPasswordPattern).Success;
        }
    }
}
