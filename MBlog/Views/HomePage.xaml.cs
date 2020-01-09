using MBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBlog.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			BindingContext = new ListTitleViewModel();
		}

		private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			Navigation.PushAsync( new ProfilePage());
		}
	}
}