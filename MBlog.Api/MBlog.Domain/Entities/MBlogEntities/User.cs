using MBlog.Domain.Helpers;
using System;

namespace MBlog.Domain.Entities.MBlogEntities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
		public string Password { get; set; }
		public string Salt { get; set; }
		public string RefeshToken { get; set; }
		public string AccessToken { get; set; }
		public string FullName { get; set; }
		public string About { get; set; }
		public Enums.Status ActiveStatus { get; set; }
		public Enums.Roles Role { get; set; }
	}
}
