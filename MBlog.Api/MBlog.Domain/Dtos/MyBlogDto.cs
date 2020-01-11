using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Dtos
{
	public class MyBlogDto
	{
		public int Following { get; set; }
		public int Follower { get; set; }
		public int Posting { get; set; }
		public List<BlogDto> Blogs { get; set; }
	}
}
