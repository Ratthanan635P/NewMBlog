using System;
using System.Threading.Tasks;
using MBlog.Data.Contexts;
using MBlog.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MBlog.Data.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly MBlogContext _context;

        public BaseRepository(MBlogContext context)
        {
            _context = context;
        }

        public void Add<TEntity>(TEntity entity)
        {
            _context.Add(entity);
        }

        public void Remove<TEntity>(TEntity entity)
        {
            _context.Remove(entity);
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public Task SaveChangeAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Update<TEntity>(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
