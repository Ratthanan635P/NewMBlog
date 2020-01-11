using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBlog.CallApi.Models
{
	public class BlogCommand
	{
		public int OwnerId { get; set; }
		public string Title { get; set; }
		public string Detail { get; set; }
		public byte[] ImageHead { get; set; }
		public string ImagePath { get; set; }
		public int TopicId { get; set; }

	}
}
