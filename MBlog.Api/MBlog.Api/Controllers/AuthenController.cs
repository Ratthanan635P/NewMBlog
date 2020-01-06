using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MBlog.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MBlog.Api.Controllers
{
    [Route("[controller]")]
    public class AuthenController : Controller
    {
        private readonly IAuthService _authService;

        public AuthenController(IAuthService authService)
        {
            _authService = authService;
        }

        public Task<ActionResult> Login()
        {
            return null;
        }
    }
}
