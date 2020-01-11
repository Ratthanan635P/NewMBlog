using MBlog.Domain.Entities.MBlogEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Interfaces.Repositories
{
	public interface IFollowingRepository : IBaseRepository
	{
		List<Following> GetDataFollowerByUserId(int Userid);
		List<Following> GetDataFollowingByUserId(int Userid);
		int GetFollowerByUserId(int Userid);
		int GetFollowingByUserId(int Userid);
	}
}
