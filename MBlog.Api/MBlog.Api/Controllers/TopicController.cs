using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBlog.Api.Models;
using MBlog.Domain.Dtos;
using MBlog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MBlog.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
		private ITopicService _topicService;
		private SuccessModel SuccessModel;
		private ErrorModel ErrorModel;
		public TopicController(ITopicService topicService)
		{
			_topicService = topicService;
			SuccessModel = new SuccessModel();
			ErrorModel = new ErrorModel();
		}
		[AllowAnonymous]
		[HttpGet("test")]
		public string Test()
		{
			return "server is running";
		}
		[AllowAnonymous]
		[HttpGet("GetAll")]
		//[ProducesResponseType(typeof(TopicDto), StatusCodes.Status200OK)]
		public IActionResult GetAll()
		{
			try
			{
				var topicDtos = _topicService.GetAll();
				if (topicDtos != null)
				{
					return Ok(topicDtos);
				}
				else
				{
					ErrorModel.ErrorCode = "400";
					ErrorModel.ErrorMessage = "Not Found";

					return BadRequest(ErrorModel);
				}

			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";

				return StatusCode(500, ErrorModel);
			}
		}
		[AllowAnonymous]
		[HttpPost("AddTopic")]
		//[ProducesResponseType(typeof(TopicDto), StatusCodes.Status200OK)]
		public IActionResult AddTopic(TopicDto topicDto)
		{
			try
			{
				var topicDtos = _topicService.Add(topicDto);
				if (topicDtos)
				{
					SuccessModel.SuccessCode = "200";
					SuccessModel.SuccessMessage = "Success";
					return Ok(SuccessModel);
				}
				else
				{
					ErrorModel.ErrorCode = "400";
					ErrorModel.ErrorMessage = "Add Fail";
					return BadRequest(ErrorModel);
				}

			}
			catch (Exception ex)
			{
				ErrorModel.ErrorMessage = ex.Message;
				ErrorModel.ErrorCode = "500";

				return StatusCode(500, ErrorModel);
			}
		}
		//[AllowAnonymous]
		//[HttpPost("Register")]
		//[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status200OK)]
		//public IActionResult RegisterUser(LoginCommandModel model)
		//{
		//	try
		//	{
		//		string isSuccess = _authService.Register(model.Email.ToLower(), model.Password);
		//		if (isSuccess == "Success")
		//		{
		//			SuccessModel.SuccessCode = "200";
		//			SuccessModel.SuccessMessage = "Register Completed.";

		//			return Ok(SuccessModel);
		//		}
		//		else
		//		{
		//			ErrorModel.ErrorCode = "400";
		//			ErrorModel.ErrorMessage = "This User is Exist.";

		//			return BadRequest(ErrorModel);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		ErrorModel.ErrorMessage = ex.Message;
		//		ErrorModel.ErrorCode = "500";

		//		return StatusCode(500, ErrorModel);
		//	}
		//}
		//[AllowAnonymous]
		////[Authorize(Roles = "User")]
		//[HttpGet("ForgotPassword")]
		//public IActionResult ForgotPassword(string email)
		//{
		//	try
		//	{
		//		var isSuccess = _authService.ForgotPassword(email.ToLower());
		//		if (isSuccess)
		//		{
		//			SuccessModel.SuccessCode = "200";
		//			SuccessModel.SuccessMessage = "Password was Send!";
		//			return Ok(SuccessModel);
		//		}
		//		else
		//		{
		//			ErrorModel.ErrorCode = "400";
		//			ErrorModel.ErrorMessage = "User or Password invalid.";

		//			return BadRequest(ErrorModel);
		//		}

		//	}
		//	catch (Exception ex)
		//	{
		//		ErrorModel.ErrorMessage = ex.Message;
		//		ErrorModel.ErrorCode = "500";

		//		return StatusCode(500, ErrorModel);
		//	}
		//}
	}
}