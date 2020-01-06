using System;
using MBlog.Domain.Helpers;

namespace MBlog.Domain.Entities.MBlogEntities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreateDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
