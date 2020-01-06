using System;
using System.Reflection;
using MBlog.Data.Configurations.MBlog;
using MBlog.Domain.Entities.MBlogEntities;
using Microsoft.EntityFrameworkCore;

namespace MBlog.Data.Contexts
{
    public class MBlogContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public MBlogContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=MBlogDb;Trusted_Connection=False;User ID=sa;Password=Gg123456789");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
