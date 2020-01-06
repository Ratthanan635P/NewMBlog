using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MBlog.Domain.Helpers;
using Xunit;

namespace MBlog.Domain.Test.Helpers
{
    public class PasswordHelperTest
    {
        public static IEnumerable<object[]> GetPasswordData()
        {

            var data = new List<object[]>()
            {
                new object[] { "Gg1234", true },
                new object[] { "gG1234", true },
                new object[] { "Gg123456789", true },
                new object[] { "g1234G", true },
                new object[] { "", false },
                new object[] { "123", false },
                new object[] { "123456", false },
                new object[] { "1234567", false },
                new object[] { "asd", false },
                new object[] { "asdfghjkl", false },
                new object[] { "asdfgh", false },
                new object[] { "ASD", false },
                new object[] { "ASDFGH", false },
                new object[] { "ASDFGHJKL", false },
                new object[] { "ASD123", false },
                new object[] { "ASD1234", false },
            };

            return data;
        }

        [Theory]
        [MemberData(nameof(GetPasswordData))]
        public void Test_Hash_And_Validate_Password(string password, bool _)
        {
            string hashedPassword = PasswordHelper.CreatePasswordHashed(password);
            bool isValid = PasswordHelper.ValidatePassword(password, hashedPassword);
            Assert.True(isValid);
        }

        [Theory]
        [MemberData(nameof(GetPasswordData))]
        public void Test_Valid_Strong_Password(string password, bool answer)
        {
            bool isValid = PasswordHelper.IsValidPassword(password);
            Assert.Equal(isValid, answer);
        }

        [Theory]
        [MemberData(nameof(GetPasswordData))]
        public void Test_Invalid_Strong_Password(string password, bool answer)
        {
            bool isValid = PasswordHelper.IsValidPassword(password);
            Assert.Equal(isValid, answer);
        }

    }
}
