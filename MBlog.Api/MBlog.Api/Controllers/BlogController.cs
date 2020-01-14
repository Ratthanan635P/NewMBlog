using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MBlog.Api.Commands.AuthCommands;
using MBlog.Api.Models;
using MBlog.Domain.Dtos;
using MBlog.Domain.Helpers;
using MBlog.Domain.Interfaces.Services;
using MBlog.IoC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MBlog.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
		private ITopicService _topicService;
		private IBlogService _blogService;
		private readonly IMapper _mapper;
		private SuccessModel SuccessModel;
		private ErrorModel ErrorModel;
		public BlogController(ITopicService topicService,IBlogService blogService, IMapper mapper)
		{
			_topicService = topicService;
			_blogService = blogService;
			_mapper = mapper;
			SuccessModel = new SuccessModel();
			ErrorModel = new ErrorModel();
		}
		[AllowAnonymous]
		[HttpGet("test")]
		public string Test()
		{
	       return "server is running";
		}
		[HttpPost("CreateBlog")]
		public IActionResult CreateBlog(BlogCommand blog)
		{
			try
			{
				if (blog != null)
				{
					
					BlogDto data = _mapper.Map<BlogDto>(blog);
					var iSuccess = _blogService.AddBlog(data);
					if (iSuccess)
					{
						SuccessModel.SuccessMessage = "Success";
						SuccessModel.SuccessCode = "200";
						return Ok(SuccessModel);
					}
					else
					{
						ErrorModel.ErrorMessage = "Update fail";
						ErrorModel.ErrorCode = "400";
						return StatusCode(400, ErrorModel);
					}
				}
				else
				{
					ErrorModel.ErrorMessage = "Data fail";
					ErrorModel.ErrorCode = "400";
					return StatusCode(400, ErrorModel);
				}
			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";
				return StatusCode(500, ErrorModel);
			}
		}
		[HttpGet("GetMyBlog")]
		public IActionResult GetMyBlog(int userId)
		{
			try
			{
				var myBlog = _blogService.GetBlogByUserId(userId);
				var posts = myBlog.Count();
				var followers = _blogService.GetFollowingByUserId(userId);
				var followings = _blogService.GetFollowerByUserId(userId);
				var favorites = _blogService.GetFavoritesByUserId(userId);
				MyBlogs myBlogDetail = new MyBlogs();
				myBlogDetail.Blogs = myBlog.Select(b=>new BlogModel()
				{
					BookMarkVisible=true,
					Createtime=b.Createtime,
					Detail=b.Detail,
					Id=b.Id,
					ImageHead=b.ImageHead,
					ImagePath=b.ImagePath,
					IsLike=false,
					IsOff=false,
					IsOn=true,
					Owner=b.Owner,
					OwnerId=b.OwnerId,
					Title=b.Title,
					Topic=b.Topic,
					TopicId=b.TopicId
				}).ToList();
				myBlogDetail.Followers = followers;
				myBlogDetail.Followings = followings;
				myBlogDetail.Posts = posts;
				return Ok(myBlogDetail);
			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";

				return StatusCode(500, ErrorModel);
			}
		}
		[HttpGet("GetFollowBlog")]
		public IActionResult GetTargetBlog(int targetId,int userId)
		{
			try
			{
				var myBlog = _blogService.GetBlogByUserId(targetId);
				var posts = myBlog.Count();
				var followers = _blogService.GetFollowingByUserId(targetId);
				var followings = _blogService.GetFollowerByUserId(targetId);
				var favo = _blogService.GetFavoritesByUserId(userId);
				MyBlogs myBlogDetail = new MyBlogs();
				//var myBlog = _blogService.(userId);
				myBlogDetail.Blogs = myBlog.Select(b => new BlogModel()
				{
					BookMarkVisible = true,
					Createtime = b.Createtime,
					Detail = b.Detail,
					Id = b.Id,
					ImageHead = b.ImageHead,
					ImagePath = b.ImagePath,
					IsLike = false,
					IsOff = true,
					IsOn = false,
					Owner = b.Owner,
					OwnerId = b.OwnerId,
					Title = b.Title,
					Topic = b.Topic,
					TopicId = b.TopicId
				}).ToList();
				
				foreach (var item in favo)
				{
					for (int i = 0; i < myBlogDetail.Blogs.Count; i++)
					{
						if ((item.Id == myBlogDetail.Blogs[i].Id))
						{
							myBlogDetail.Blogs[i].IsLike = true;
							myBlogDetail.Blogs[i].IsOn = true;
							myBlogDetail.Blogs[i].IsOff = false;
						}
					}
					
				}
				myBlogDetail.Followers = followers;
				myBlogDetail.Followings = followings;
				myBlogDetail.Posts = posts;
				return Ok(myBlogDetail);
			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";

				return StatusCode(500, ErrorModel);
			}
		}
		[HttpGet("GetSubscribes")]
		public IActionResult GetSubscribes(int userId)
		{			
			   try
				{
					var user = _blogService.GetSubscribesByUserId(userId);
					return Ok(user);
				}
				catch (Exception ex)
				{
					ErrorModel.ErrorMessage = ex.Message;
					ErrorModel.ErrorCode = "500";

					return StatusCode(500, ErrorModel);
				}					
		}
		[HttpGet("Subscribes")]
		public IActionResult Subscribes(int targetUser,int userId)
		{
			try
			{
				var user = _blogService.SubscribesByUserId(targetUser,userId);
				if (user)
				{
					SuccessModel.SuccessMessage = "Success";
					SuccessModel.SuccessCode = "200";
					return Ok(SuccessModel);
				}
				else
				{
					ErrorModel.ErrorMessage = "Update fail";
					ErrorModel.ErrorCode = "400";
					return StatusCode(400, ErrorModel);
				}
			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";
				return StatusCode(500, ErrorModel);
			}
		}
		[HttpGet("UnSubscribes")]
		public IActionResult UnSubscribes(int targetUser, int userId)
		{
			try
			{
				var user = _blogService.UnSubscribesByUserId(targetUser, userId);
				if (user)
				{
					SuccessModel.SuccessMessage = "Success";
					SuccessModel.SuccessCode = "200";
					return Ok(SuccessModel);
				}
				else
				{
					ErrorModel.ErrorMessage = "Update fail";
					ErrorModel.ErrorCode = "400";
					return StatusCode(400, ErrorModel);
				}
			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";
				return StatusCode(500, ErrorModel);
			}
		}
		[HttpGet("Favorite")]
		public IActionResult Favorite(int blogId, int userId)
		{
			try
			{
				var user = _blogService.FavoritesByUserId(blogId, userId);
				if (user)
				{
					SuccessModel.SuccessMessage = "Success";
					SuccessModel.SuccessCode = "200";
					return Ok(SuccessModel);
				}
				else
				{
					ErrorModel.ErrorMessage = "Update fail";
					ErrorModel.ErrorCode = "400";
					return StatusCode(400, ErrorModel);
				}
			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";
				return StatusCode(500, ErrorModel);
			}
		}
		[HttpGet("UnFavorite")]
		public IActionResult UnFavorite(int blogId, int userId)
		{
			try
			{
				var user = _blogService.UnFavoritesByUserId(blogId, userId);
				if (user)
				{
					SuccessModel.SuccessMessage = "Success";
					SuccessModel.SuccessCode = "200";
					return Ok(SuccessModel);
				}
				else
				{
					ErrorModel.ErrorMessage = "Update fail";
					ErrorModel.ErrorCode = "400";
					return StatusCode(400, ErrorModel);
				}
			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";
				return StatusCode(500, ErrorModel);
			}
		}
		[HttpGet("GetFavorites")]
		public IActionResult GetFavorites(int userId)
		{
			try
			{
				var user = _blogService.GetFavoritesByUserId(userId);
				return Ok(user);
			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";

				return StatusCode(500, ErrorModel);
			}
		}
		[HttpGet("GetBlogHot")]//TODO
		public IActionResult GetBlogHot()
		{
			int userId = 1;
			try
			{
				var user = _blogService.GetFavoritesByUserId(userId);
				return Ok(user.Take(4));
			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";

				return StatusCode(500, ErrorModel);
			}
		}
		[HttpGet("GetBlogForYou")]//TODO
		public IActionResult GetBlogForYou(int userId)
		{
			//int userId = 1;
			try
			{
				var user = _blogService.GetFavoritesByUserId(userId);
				return Ok(user.Take(3));
			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";

				return StatusCode(500, ErrorModel);
			}
		}
		[HttpGet("GetBlogLatest")]//TODO
		public IActionResult GetBlogLatest()
		{
			int userId = 1;
			try
			{
				var user = _blogService.GetFavoritesByUserId(userId);
				return Ok(user.Take(4));
			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";

				return StatusCode(500, ErrorModel);
			}
		}
		[HttpGet("GetYouMightLike")]//TODO
		public IActionResult GetYouMightLike(int userId)
		{
			try
			{
				var user = _blogService.GetSubscribesByUserId(userId);
				return Ok(user.Take(3));
			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";

				return StatusCode(500, ErrorModel);
			}
		}
		[HttpGet("GetBlogByTopic")]//TODO
		public IActionResult GetBlogByTopicId(int TopicId)
		{
			int userId = TopicId;
			try
			{
				var user = _blogService.GetFavoritesByUserId(userId);
				return Ok(user.Take(4));
			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";

				return StatusCode(500, ErrorModel);
			}
		}

	}
}