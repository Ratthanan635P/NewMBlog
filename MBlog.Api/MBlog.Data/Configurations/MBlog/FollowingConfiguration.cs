using MBlog.Domain.Entities.MBlogEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Data.Configurations.MBlog
{
	public class FollowingConfiguration : IEntityTypeConfiguration<Following>
    {
        public void Configure(EntityTypeBuilder<Following> builder)
        {
            builder.ConfigureBase();
            //builder.Property(entity => entity.)
            //      .HasMaxLength(100);

        }
    }
}
