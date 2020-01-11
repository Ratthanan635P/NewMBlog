using MBlog.Domain.Entities.MBlogEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Interfaces.Repositories
{
	public interface IBlogRepository:IBaseRepository
	{
		List<Blog> GetBlogsByUserId(int UserId);
	}
}
