using MBlog.Domain.Entities.MBlogEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Data.Configurations.MBlog
{
	public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ConfigureBase();

            builder
               .HasOne(s => s.User)
               .WithMany(s => s.Favorites)
               .HasForeignKey(s => s.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
