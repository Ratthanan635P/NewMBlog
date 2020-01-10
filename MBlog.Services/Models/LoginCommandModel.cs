
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBlog.CallApi.Models
{
    public class LoginCommandModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
