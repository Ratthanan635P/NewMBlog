using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

using System.Text;
using MBlog.Domain.Entities.MBlogEntities;
using Microsoft.Extensions.Logging;

namespace MBlog.Data.Contexts
{
    public class MBlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(config => config.AddConsole());
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(loggerFactory).UseSqlServer("Server=localhost;Database=MBlogV2DB;Trusted_Connection=True;Integrated Security = false;User Id =sa;Password=yourStrong(!)Password");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
