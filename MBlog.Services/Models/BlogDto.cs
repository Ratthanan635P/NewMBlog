
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.CallApi.Models
{
	public class BlogDto 
	{
		public int Id { get; set; }
		public int OwnerId { get; set; }
		public ProfileDto Owner { get; set; }
		public string Title { get; set; }
		public string Detail { get; set; }
		public byte[] ImageHead { get; set; }
		public string ImagePath { get; set; }
		public int TopicId { get; set; }
		public DateTime Createtime { get; set; }
		public TopicDto Topic { get; set; }
	}
}
