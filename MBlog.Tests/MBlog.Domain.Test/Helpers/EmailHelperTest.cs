using System;
using System.Collections.Generic;
using MBlog.Domain.Helpers;
using Xunit;

namespace MBlog.Domain.Test.Helpers
{
    public class EmailHelperTest
    {
        [Theory]
        [InlineData("test@test.test")]
        [InlineData("ff13@gmail.com")]
        public void Test_Valid_Email(string email)
        {
            bool isEmail = EmailHelper.IsValidEmail(email);
            Assert.True(isEmail);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("@test.com")]
        [InlineData("test.com")]
        public void Test_Invalid_Email(string email)
        {
            bool isEmail = EmailHelper.IsValidEmail(email);
            Assert.False(isEmail);
        }

    }
}
