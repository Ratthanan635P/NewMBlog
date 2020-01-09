﻿using MBlog.Models;
using MBlog.ViewModels;
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
	public partial class ListSubscribePage : ContentPage
	{		
        public ListSubscribePage()
		{
			InitializeComponent();           
		}

		private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ProfilePage());
		}
		protected override async void OnAppearing()
		{
			base.OnAppearing();
			Loading.IsVisible = true;
			Loading.IsPlaying = true;

			await Task.Delay(2000);
			BindingContext = new ListTitleViewModel();
			Loading.IsVisible = false;
			Loading.IsPlaying = false;
		}
	}

}
