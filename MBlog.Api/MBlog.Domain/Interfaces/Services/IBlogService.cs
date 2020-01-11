using MBlog.Domain.Dtos;
using MBlog.Domain.Entities.MBlogEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Interfaces.Services
{
	public interface IBlogService
	{
		bool AddBlog(BlogDto blogDto);
		List<BlogDto> GetBlogByUserId(int userId);

		List<ProfileDto> GetSubscribesByUserId(int userId);
		bool UnSubscribesByUserId(int unSubUserId, int myUserId);
		bool SubscribesByUserId(int unSubUserId, int myUserId);
		
	}
}
