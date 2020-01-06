using System;
using System.Collections.Generic;
using MBlog.Domain.Entities.MBlogEntities;
using MBlog.Domain.Helpers;

namespace MBlog.Domain.Test
{
    public class DataTest
    {
        public static List<User> Users { get => GenerateUser(); }

        private static List<User> GenerateUser()
        {
            var users = new List<User>();

            User user = new User()
            {
                Id = 1,
                Email = "test@test.test",
                Password = PasswordHelper.CreatePasswordHashed("Gg1234")
            };
            users.Add(user);

            return users;
        }

    }
}
