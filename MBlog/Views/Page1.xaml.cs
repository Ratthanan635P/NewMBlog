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
	public partial class Page1 : ContentPage
	{
		public Page1()
		{
			InitializeComponent();
		}

		private void BtnTitle_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ListTitlePage());
		}

		private void BtnSubscribe_Clicked(object sender, EventArgs e)
		{			
				Navigation.PushAsync(new ListSubscribePage());
		}

		private void BtnLatest_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ListLatestPage());
		}

		private void BtnHot_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ListHotBlogPage());
		}

		private void BtnProfile_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new ProfilePage());
		}

		private void BtnFollow_Clicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new FollowPage());
		}
	}
}