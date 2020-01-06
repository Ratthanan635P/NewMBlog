﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MBlog.Models
{
	public class DataTest
	{
		public string Title { get; set; }
		public DateTime Expire { get; set; }
		public string Detail { get; set; }
		public ImageSource Image { get; set; }
	}
}
