using System;
using MBlog.Domain.Entities.MBlogEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MBlog.Data.Configurations.MBlog
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ConfigureBase();

            builder.Property(entity => entity.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(entity => entity.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasAlternateKey(entity => entity.Email);
        }
    }
}
