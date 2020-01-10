﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBlog.Api.Helpers;
using MBlog.Api.Models;
using MBlog.Domain.Helpers;
using MBlog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MBlog.Api.Controller
{

	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class AuthenController : ControllerBase
	{
		private IAuthService _userService;
		private ValidateMethods validateMethods;
		public AuthenController(IAuthService userService)
		{
			_userService = userService;
			validateMethods = new ValidateMethods();
		}

		[AllowAnonymous]
		[HttpPost("LogIn")]
		public IActionResult LogInUser([FromBody]AuthenticateModel model)
		{
			string error = "";
			if (string.IsNullOrEmpty(model.Email))
			{
				error = " Email is null";
				return BadRequest(new { message = error });
			}
			if (string.IsNullOrEmpty(model.Password))
			{
				error = " Password is null";
				return BadRequest(new { message = error });
			}
			if (!((validateMethods.CheckRegEx_UserName(model.Email)) && (model.Email.Length > validateMethods.LengthEmail)))
			{
				error = " Email is Invalid!";
				return BadRequest(new { message = error });
			}
			if (!((validateMethods.CheckRegEx_Password(model.Password)) && (model.Password.Length > validateMethods.LengthPassword)))
			{
				if (error != "")
				{
					error = " Email and Password is Invalid! ";
					return BadRequest(new { message = error });
				}
				else
				{
					error = " Password is Invalid!";
					return BadRequest(new { message = error });
				}
			}

			//Call Api Check email and Password

			if (error == "")
			{
				try
				{
					var user = _userService.Login(model.Email, model.Password);
					if (user.ErrorMessage != "PASS")
						return BadRequest(new { message = user.ErrorMessage });
					return Ok(user);
				}
				catch (Exception ex)
				{
					error = ex.Message;
				}
				return BadRequest(new { message = error });
			}
			else
			{
				return BadRequest(new { message = error });

			}
			
		}
		[AllowAnonymous]
		[HttpPost("Register")]
		public IActionResult RegisterUser([FromBody]AuthenticateModel model)
		{
			string error = "";
			if (string.IsNullOrEmpty(model.Email))
			{
				error = " Email is null";
				return BadRequest(new { message = error });
			}
			if (string.IsNullOrEmpty(model.Password))
			{
				error = " Password is null";
				return BadRequest(new { message = error });
			}
			if (!((validateMethods.CheckRegEx_UserName(model.Email)) && (model.Email.Length > validateMethods.LengthEmail)))
			{
				error = " Email is Invalid!";
				return BadRequest(new { message = error });
			}
			if (!((validateMethods.CheckRegEx_Password(model.Password)) && (model.Password.Length > validateMethods.LengthPassword)))
			{
				if (error != "")
				{
					error = " Email and Password is Invalid! ";
					return BadRequest(new { message = error });
				}
				else
				{
					error = " Password is Invalid!";
					return BadRequest(new { message = error });
				}
			}

			//Call Api Check email and Password

			if (error == "")
			{
				try
				{				
						var user = _userService.RegisterUser(model.Email, model.Password);
					if (user == null)
						return BadRequest(new { message = user });
					return Ok(user);
				}
				catch (Exception ex)
				{
					error = ex.Message;
				}
				return BadRequest(new { message = error });
			}
			else
			{
				return BadRequest(new { message = error });

			}

		}
		[AllowAnonymous]
		//[Authorize(Roles = "User")]
		[HttpGet("ForgotPassword")]
		public IActionResult ForgotPassword(string email)
		{
			string error = "";
			if (string.IsNullOrEmpty(email))
			{
				error = " Email is null";
				return BadRequest(new { message = error });
			}
			if (!((validateMethods.CheckRegEx_UserName(email)) && (email.Length > validateMethods.LengthEmail)))
			{
				error = " Email is Invalid!";
				return BadRequest(new { message = error });
			}

			//Call Api Check email and Password

			if (error == "")
			{
				try
				{
					var user = _userService.ForgotPassword(email);
					if (user != "Success")
						return BadRequest(new { message = user });
					return Ok(user);
				}
				catch (Exception ex)
				{
					error = ex.Message;
				}
				return BadRequest(new { message = error });
			}
			else
			{
				return BadRequest(new { message = error });

			}
			///return Ok(email);
		}
		//[AllowAnonymous]
		[Authorize(Roles = "User")]
		[HttpGet("GetData")]
		public IActionResult GetUser(string email)
		{
			string error = "";
			if (string.IsNullOrEmpty(email))
			{
				error = " Email is null";
				return BadRequest(new { message = error });
			}
			if (!((validateMethods.CheckRegEx_UserName(email)) && (email.Length > validateMethods.LengthEmail)))
			{
				error = " Email is Invalid!";
				return BadRequest(new { message = error });
			}

			//Call Api Check email and Password

			if (error == "")
			{
				try
				{
					var user = _userService.GetDataUser(email);
					if (user == null)
					{
						user.ErrorMessage = "Account not found!";
						return BadRequest(new { message = user.ErrorMessage });
					}
					return Ok(user);
				}
				catch (Exception ex)
				{
					error = ex.Message;
				}
				return BadRequest(new { message = error });
			}
			else
			{
				return BadRequest(new { message = error });

			}
			///return Ok(email);
		}
	}

}