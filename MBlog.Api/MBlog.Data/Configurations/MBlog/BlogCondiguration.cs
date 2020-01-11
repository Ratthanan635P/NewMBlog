using MBlog.Domain.Entities.MBlogEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Data.Configurations.MBlog
{
	public class BlogCondiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.ConfigureBase();

            builder
               .HasOne(s => s.Owner)
               .WithMany(s => s.Blogs)
               .HasForeignKey(s => s.OwnerId);
        }
    }
}
