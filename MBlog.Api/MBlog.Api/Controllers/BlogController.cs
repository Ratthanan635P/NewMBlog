using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBlog.Api.Models;
using MBlog.Domain.Helpers;
using MBlog.Domain.Interfaces.Services;
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
		private SuccessModel SuccessModel;
		private ErrorModel ErrorModel;
		public BlogController(ITopicService topicService,IBlogService blogService)
		{
			_topicService = topicService;
			_blogService = blogService;
			SuccessModel = new SuccessModel();
			ErrorModel = new ErrorModel();
		}
		[AllowAnonymous]
		[HttpGet("test")]
		public string Test()
		{
			return "server is running";
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

	}
}