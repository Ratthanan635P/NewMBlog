using MBlog.Data.Contexts;
using MBlog.Domain.Entities.MBlogEntities;
using MBlog.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBlog.Data.Repositories
{
	public class BlogRepository:BaseRepository, IBlogRepository
	{
		private readonly MBlogContext _context;

		public BlogRepository(MBlogContext context) : base(context)
		{
			_context = context;
			if (_context.ChangeTracker != null)
			{
				_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			}
		}

		public List<Blog> GetBlogsByUserId(int UserId)
		{
			//throw new NotImplementedException();
			var blog = _context.Blogs.Include(x=>x.Owner).Where(b=>b.OwnerId==UserId).ToList();
			return blog;
		}
	}
}
