using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Entities.MBlogEntities
{
	public class Blog:BaseEntity
	{
		public int OwnerId { get; set; }
		public User Owner { get; set; }
		public string Title { get; set; }
		public string Detail { get; set; }
		public byte[] ImageHead { get; set; }
		public string ImagePath { get; set; }
		public int TopicId { get; set; }
		public Topic Topic { get; set; }
	}
}
