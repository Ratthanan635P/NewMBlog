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
				return Ok(myBlog);
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

	}
}