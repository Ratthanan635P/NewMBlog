using MBlog.CallApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MBlog.CallApi.Service.Interfaces
{
	public interface IBlogService:IBaseService
	{
		Task<Result<SuccessModel, ErrorModel>> CreateBlog(BlogCommand blog);//post
		Task<Result<MyBlogs, ErrorModel>> GetMyBlog(int userId);//Get
		Task<Result<List<ProfileDto>, ErrorModel>> GetSubscribes(int userId);//get
		Task<Result<SuccessModel, ErrorModel>> Subscribes(int targetUser, int userId);//get
		Task<Result<SuccessModel, ErrorModel>> UnSubscribes(int targetUser, int userId);//get
		Task<Result<SuccessModel, ErrorModel>> Favorite(int blogId, int userId);//get
		Task<Result<SuccessModel, ErrorModel>> UnFavorite(int blogId, int userId);//get
		Task<Result<List<BlogDto>, ErrorModel>> GetFavorites(int userId);//get
	}
}
