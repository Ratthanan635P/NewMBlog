using MBlog.Domain.Entities.MBlogEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Interfaces.Repositories
{
	public interface IFavoriteRepository:IBaseRepository
	{
		List<Favorite> GetByUserId(int Userid);
	}
}
