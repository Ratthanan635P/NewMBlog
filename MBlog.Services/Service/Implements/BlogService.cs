using MBlog.CallApi.Models;
using MBlog.CallApi.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MBlog.CallApi.Service.Implements
{
	public class BlogService : BaseService,IBlogService
	{
		public async Task<Result<SuccessModel, ErrorModel>> CreateBlog(BlogCommand blog)//post
		{
			Uri url = new Uri(BaseUriUser, $"/Blog/CreateBlog");

			Result<SuccessModel, ErrorModel> result = await PostMethodAsync<SuccessModel, ErrorModel>(url, blog);

			return result;
		}

		public async Task<Result<SuccessModel, ErrorModel>> Favorite(int blogId, int userId)
		{
			Uri url = new Uri(BaseUriUser, $"/Blog/Favorite?blogId={blogId}&userId={userId}");

			Result<SuccessModel, ErrorModel> result = await GetMethodAsync<SuccessModel, ErrorModel>(url);

			return result;
		}

		public async Task<Result<List<BlogDto>, ErrorModel>> GetFavorites(int userId)
		{
			Uri url = new Uri(BaseUriUser, $"/Blog/GetFavorites?userId={userId}");

			Result<List<BlogDto>, ErrorModel> result = await GetMethodAsync<List<BlogDto>, ErrorModel>(url);

			return result;
		}

		public async Task<Result<MyBlogs, ErrorModel>> GetMyBlog(int userId)
		{
			Uri url = new Uri(BaseUriUser, $"/Blog/GetMyBlog?userId={userId}");

			Result<MyBlogs, ErrorModel> result = await GetMethodAsync<MyBlogs, ErrorModel>(url);

			return result;
		}

		public async Task<Result<List<ProfileDto>, ErrorModel>> GetSubscribes(int userId)
		{
			Uri url = new Uri(BaseUriUser, $"/Blog/GetSubscribes?userId={userId}");

			Result<List<ProfileDto>, ErrorModel> result = await GetMethodAsync<List<ProfileDto>, ErrorModel>(url);

			return result;
		}

		public async Task<Result<MyBlogs, ErrorModel>> GetTargetBlog(int targetId, int userId)
		{
			Uri url = new Uri(BaseUriUser, $"/Blog/GetFollowBlog?targetId={targetId}&userId={userId}");

			Result<MyBlogs, ErrorModel> result = await GetMethodAsync<MyBlogs, ErrorModel>(url);

			return result;
		}

		public async Task<Result<SuccessModel, ErrorModel>> Subscribes(int targetUser, int userId)
		{
			Uri url = new Uri(BaseUriUser, $"/Blog/Subscribes?targetUser={targetUser}&userId={userId}");

			Result<SuccessModel, ErrorModel> result = await GetMethodAsync<SuccessModel, ErrorModel>(url);

			return result;
		}

		public async Task<Result<SuccessModel, ErrorModel>> UnFavorite(int blogId, int userId)
		{
			Uri url = new Uri(BaseUriUser, $"/Blog/UnFavorite?blogId={blogId}&userId={userId}");

			Result<SuccessModel, ErrorModel> result = await GetMethodAsync<SuccessModel, ErrorModel>(url);

			return result;
		}

		public async Task<Result<SuccessModel, ErrorModel>> UnSubscribes(int targetUser, int userId)
		{
			Uri url = new Uri(BaseUriUser, $"/Blog/UnSubscribes?targetUser={targetUser}&userId={userId}");

			Result<SuccessModel, ErrorModel> result = await GetMethodAsync<SuccessModel, ErrorModel>(url);

			return result;
		}
	}
}
