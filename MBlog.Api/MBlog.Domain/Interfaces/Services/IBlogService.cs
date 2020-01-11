using MBlog.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Interfaces.Services
{
	public interface IBlogService
	{
		bool AddBlog(BlogDto blogDto);
		List<BlogDto> GetBlogByUserId(int userId);
	}
}
