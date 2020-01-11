using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBlog.Api.Models;
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

	}
}