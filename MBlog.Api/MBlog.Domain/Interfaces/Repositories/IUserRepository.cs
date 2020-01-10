using System;
using System.Threading;
using System.Threading.Tasks;
using MBlog.Domain.Entities.MBlogEntities;

namespace MBlog.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository
    {
        User GetByEmail(string email);
        
    }
}
