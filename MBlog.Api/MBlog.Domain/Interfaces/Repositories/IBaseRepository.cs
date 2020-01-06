using System;
using System.Threading.Tasks;

namespace MBlog.Domain.Interfaces.Repositories
{
    public interface IBaseRepository
    {
        void Add<TEntity>(TEntity entity);
        void Update<TEntity>(TEntity entity);
        void Remove<TEntity>(TEntity entity);

        void SaveChange();
        Task SaveChangeAsync();
    }
}
