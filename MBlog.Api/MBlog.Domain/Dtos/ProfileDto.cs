using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Dtos
{
	public class ProfileDto
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string FullName { get; set; }
		public string About { get; set; }
		public byte[] ImageProfile { get; set; }
		public string ImageProfilePath { get; set; }
		public bool Following { get; set; }
	}
}
