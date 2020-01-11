﻿using MBlog.Domain.Entities.MBlogEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Dtos
{
	public class BlogDto 
	{
		public int OwnerId { get; set; }
		public ProfileDto Owner { get; set; }
		public string Title { get; set; }
		public string Detail { get; set; }
		public byte[] ImageHead { get; set; }
		public string ImagePath { get; set; }
		public int TopicId { get; set; }
		public TopicDto Topic { get; set; }
	}
}
