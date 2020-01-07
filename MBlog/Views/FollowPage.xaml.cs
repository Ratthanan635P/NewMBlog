﻿using MBlog.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBlog.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FollowPage : ContentPage
	{
		
        public FollowPage()
		{
			InitializeComponent();           
		}

		private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}
	}

}