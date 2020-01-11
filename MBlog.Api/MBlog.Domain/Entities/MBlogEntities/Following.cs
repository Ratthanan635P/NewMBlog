using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Entities.MBlogEntities
{
	public class Following:BaseEntity
	{
		public int FollowingId { get; set; }
		public User FollowingUser { get; set; }
		public int FollowerId { get; set; }
		public User Follower { get; set; }
	}
}
