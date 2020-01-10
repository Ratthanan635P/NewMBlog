
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MBlog.Api.Commands.AuthCommands
{
    public class LoginCommandModel
    {
        private const string EmailPattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        [Required]
        [RegularExpression(EmailPattern, ErrorMessage = "Invalid email pattern.")]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        //[Required]
        //public Enums.Role Role { get; set; }
    }
}
