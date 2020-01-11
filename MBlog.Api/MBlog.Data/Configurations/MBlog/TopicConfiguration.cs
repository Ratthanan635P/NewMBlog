using MBlog.Domain.Entities.MBlogEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Data.Configurations.MBlog
{
	public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ConfigureBase();
            builder.Property(entity => entity.TopicName)
                  .HasMaxLength(50);
            builder.ConfigureBase();
            builder.Property(entity => entity.TopicDetail)
                  .HasMaxLength(300);

        }
    }
}
