using System;
using MBlog.Domain.Helpers;

namespace MBlog.Domain.Entities.MBlogEntities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; } = false;
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public DateTime? UpdateDateTime { get; set; }
    }
}
