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
	}
}
