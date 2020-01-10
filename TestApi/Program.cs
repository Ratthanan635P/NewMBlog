using MBlog.Services.Implements;
using MBlog.Services.Interfaces;
using System;

namespace TestApi
{
	class Program
	{
		public IAuthService authService = new AuthService();
		static void Main(string[] args)
		{
			//Console.WriteLine("Hello World!");
			
		}
	}
}
