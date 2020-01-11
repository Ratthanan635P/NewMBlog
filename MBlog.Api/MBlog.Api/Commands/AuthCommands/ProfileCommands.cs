using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBlog.Api.Commands.AuthCommands
{
	public class ProfileCommands
	{
		public string Email { get; set; }		
		public string FullName { get; set; }
		public string About { get; set; }		
		public byte[] ImageProfile { get; set; }
		public string ImageProfilePath { get; set; }
	}
}
