using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Entities.MBlogEntities
{
	public class Topic : BaseEntity
	{
		public string TopicName { get; set; }
		public string TopicDetail { get; set; }
	}
	
}
