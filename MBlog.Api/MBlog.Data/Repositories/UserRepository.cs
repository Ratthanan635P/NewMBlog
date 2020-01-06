using System;
using System.Threading.Tasks;
using MBlog.Data.Contexts;
using MBlog.Domain.Entities.MBlogEntities;
using MBlog.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Primitives;
using System.Threading;

namespace MBlog.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly MBlogContext _context;

        public UserRepository(MBlogContext context) : base(context)
        {
            _context = context;
            if (_context.ChangeTracker != null)
            {
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
        }

        public User GetByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            return user;
        }
    }
}
