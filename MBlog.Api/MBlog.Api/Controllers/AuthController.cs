using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBlog.Api.Commands.AuthCommands;
using MBlog.Api.Helpers;
using MBlog.Api.Models;
using MBlog.Domain.Dtos;
using MBlog.Domain.Helpers;
using MBlog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MBlog.Api.Controller
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class AuthController : ControllerBase
	{
		private IAuthService _authService;
		private ValidateMethods validateMethods;
		private SuccessModel SuccessModel;
		private ErrorModel ErrorModel;
		public AuthController(IAuthService userService)
		{
			_authService = userService;
			validateMethods = new ValidateMethods();
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
		[HttpPost("Login")]
		[ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
		public IActionResult LogInUser(LoginCommandModel cmd)
		{
			try
			{
				UserDto userDto = _authService.Login(cmd.Email.ToLower(), cmd.Password);
				if (userDto != null)
				{
					return Ok(userDto);
				}
				else
				{
					ErrorModel.ErrorCode = "400";
					ErrorModel.ErrorMessage = "User or Password invalid.";

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
		[HttpPost("Register")]
		[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status200OK)]
		public IActionResult RegisterUser(LoginCommandModel model)
		{
			try
			{
				string isSuccess = _authService.Register(model.Email.ToLower(), model.Password);
				if (isSuccess== "Success")
				{
					SuccessModel.SuccessCode = "200";
					SuccessModel.SuccessMessage = "Register Completed.";

					return Ok(SuccessModel);
				}
				else
				{
					ErrorModel.ErrorCode = "400";
					ErrorModel.ErrorMessage = "This User is Exist.";

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
		//[Authorize(Roles = "User")]
		[HttpGet("ForgotPassword")]
		public IActionResult ForgotPassword(string email)
		{
			try
			{
				var isSuccess = _authService.ForgotPassword(email.ToLower());
				if (isSuccess)
				{
					SuccessModel.SuccessCode = "200";
					SuccessModel.SuccessMessage = "Password was Send!";
					return Ok(SuccessModel);
				}
				else
				{
					ErrorModel.ErrorCode = "400";
					ErrorModel.ErrorMessage = "User or Password invalid.";

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
		[HttpPost("Update")]
		[ProducesResponseType(typeof(ErrorModel), StatusCodes.Status200OK)]
		public IActionResult UpdateUser(ProfileCommands model)
		{
			try
			{
				ProfileCommand profile = new ProfileCommand() {
					Email = model.Email,
					FullName = model.FullName,
					About = model.About,
					ImageProfile = model.ImageProfile,
					ImageProfilePath = model.ImageProfilePath
				};
				bool isSuccess = _authService.UpdateProfile(profile);
				if (isSuccess)
				{
					SuccessModel.SuccessCode = "200";
					SuccessModel.SuccessMessage = "Update Completed.";

					return Ok(SuccessModel);
				}
				else
				{
					ErrorModel.ErrorCode = "400";
					ErrorModel.ErrorMessage = "Not found";
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
		//[Authorize(Roles = "User")]
		[HttpGet("GetData")]
		public IActionResult GetUser(string email)
		{
			

			//Call Api Check email and Password

			if (EmailHelper.IsValidEmail(email))
			{
				try
				{
					var user = _authService.GetDataUser(email);					
					return Ok(user);
				}
				catch (Exception ex)
				{
					ErrorModel.ErrorMessage = ex.Message;
					ErrorModel.ErrorCode = "500";

					return StatusCode(500, ErrorModel);
				}
				
			}
			else
			{
				ErrorModel.ErrorCode = "400";
				ErrorModel.ErrorMessage = "Email inValid";
				return BadRequest(ErrorModel);
			}
			
		}
	}

}