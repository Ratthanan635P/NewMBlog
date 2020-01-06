using System;
using MBlog.Domain.Entities.MBlogEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MBlog.Data.Configurations.MBlog
{
    public static class EntityBaseConfiguration
    {
        public static void ConfigureBase<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CreateDateTime)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.UpdateDateTime)
                  .ValueGeneratedOnUpdate()
                  .HasDefaultValueSql("GETUTCDATE()");

        }
    }
}
