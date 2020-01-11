using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Entities.MBlogEntities
{
	public class Favorite:BaseEntity
	{
		public int BlogId { get; set; }
		public Blog Blog { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
	}
}
