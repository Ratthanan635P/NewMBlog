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

	public class FollowingRepository : BaseRepository, IFollowingRepository
	{
		private readonly MBlogContext _context;

		public FollowingRepository(MBlogContext context) : base(context)
		{
			_context = context;
			if (_context.ChangeTracker != null)
			{
				_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			}
		}

		public List<Following> GetDataFollowerByUserId(int Userid)
		{
			var result = _context.Followings.Where(x=>x.FollowerId==Userid).ToList();
			return result;
		}

		public List<Following> GetDataFollowingByUserId(int Userid)
		{
			var result = _context.Followings.Where(x => x.FollowingId == Userid).ToList();
			return result;
		}

		public int GetFollowerByUserId(int Userid)
		{
			var result = _context.Followings.Where(x => x.FollowerId == Userid&&x.IsDelete==false).Count();
			return result;
		}

		public int GetFollowingByUserId(int Userid)
		{
			var result = _context.Followings.Where(x => x.FollowingId == Userid && x.IsDelete == false).Count();
			return result;
		}
	}
}
