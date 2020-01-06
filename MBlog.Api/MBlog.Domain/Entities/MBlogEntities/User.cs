using System;

namespace MBlog.Domain.Entities.MBlogEntities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
