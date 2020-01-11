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
			var result = _context.Followings.Include(x => x.FollowingUser).Where(x=>x.FollowingId==Userid).ToList();
			return result;
		}

		public List<User> GetDataFollowingByUserId(int Userid)
		{
			//var result = _context.Followings.Include(x => x.FollowingUser).Where(x => x.FollowerId == Userid).ToList();
			var data = _context.Users
				.Join(_context.Followings.Where(u => u.FollowerId==Userid),
				 u => u.Id,
				 f => f.FollowingId,
			(u, f) => new User
			{
				About = u.About,
				Email = u.Email,
				FullName = u.FullName,
				Id = u.Id,
				ImageProfile = u.ImageProfile,
				ImageProfilePath =u.ImageProfilePath,

			}
		).ToList();
			return data;
		}

		public Following GetFollowByUserId(int unSubUserId, int myUserId)
		{
			var result = _context.Followings.Include(x => x.FollowingUser).Include(x => x.Follower).Where(x => x.FollowingId == unSubUserId&&x.FollowerId==myUserId).FirstOrDefault();
			return result;
		}

		public int GetFollowerByUserId(int Userid)
		{
			var result = _context.Followings.Include(x=>x.Follower).Where(x => x.FollowerId == Userid&&x.IsDelete==false).Count();
			return result;
		}

		public int GetFollowingByUserId(int Userid)
		{
			var result = _context.Followings.Where(x => x.FollowingId == Userid && x.IsDelete == false).Count();
			return result;
		}
	}
}
