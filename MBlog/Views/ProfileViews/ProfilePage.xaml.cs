using MBlog.Models;
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
	public partial class ProfilePage : ContentPage
	{
		
        public ProfilePage()
		{
			InitializeComponent();           
		}

		private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		private void Button_Clicked(object sender, EventArgs e)
		{
			Page A = new EditProfilePage();
			Navigation.PushAsync(A);
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (App.Current.MainPage.Navigation.NavigationStack.Count >= 2)
			{
				for (int i = 1; i < App.Current.MainPage.Navigation.NavigationStack.Count - 1; i++)
				{
					App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[i]);
				}
			}
		}
	}

}
