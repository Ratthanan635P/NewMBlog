﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBlog.CallApi.Models
{
	public class MyBlogs
	{
		public int Posts { get; set; }
		public int Followings { get; set; }
		public int Followers { get; set; }
		public List<BlogModel> Blogs { get; set; }
	}
}
