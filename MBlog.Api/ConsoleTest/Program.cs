using System;
using System.Linq;
using MBlog.Data.Contexts;
using MBlog.Data.Repositories;
using MBlog.Domain.Entities;
using MBlog.Domain.Helpers;
using MBlog.Domain.Interfaces.Repositories;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = PasswordHelper.IsValidPassword("Gg1823456789");

        }
    }
}
