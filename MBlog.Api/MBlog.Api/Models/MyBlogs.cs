using MBlog.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBlog.Api.Models
{
	public class MyBlogs
	{
		public int Posts { get; set; }
		public int Followings { get; set; }
		public int Followers { get; set; }
		public List<BlogDto> Blogs { get; set; }
	}
}
