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
	public class FavoriteRepository : BaseRepository, IFavoriteRepository
	{
		private readonly MBlogContext _context;

		public FavoriteRepository(MBlogContext context) : base(context)
		{
			_context = context;
			if (_context.ChangeTracker != null)
			{
				_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			}
		}		
		public List<Favorite> GetByUserId(int Userid)
		{
			var results = _context.Favorites.Where(F=>F.UserId==Userid).ToList();
			return results;
	    }

		public List<Blog> GetDataFavoritesByUserId(int Userid)
		{
			var data = _context.Blogs.Include(u=>u.Owner)
				.Join(_context.Favorites.Where(f => f.UserId == Userid),
				 b => b.Id,
				 f => f.BlogId,
			(b, f) => b
		).ToList();
			return data;
		}

		public Favorite GetFavoritesByUserId(int blogId, int myUserId)
		{
			var result = _context.Favorites.Include(x => x.User).Where(x => x.Id == blogId && x.UserId == myUserId).FirstOrDefault();
			return result;
		}

	}
}
