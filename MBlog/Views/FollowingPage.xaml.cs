using MBlog.CallApi.Models;
using MBlog.Models;
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
	public partial class FollowingPage : ContentPage
	{
		
        public FollowingPage(ProfileDto profile)
		{
			InitializeComponent();
			BindingContext = new FollowingViewModel(profile); 
		}

		private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			//Navigation.PushAsync( new EditFollowPage());
		}
	}

}
