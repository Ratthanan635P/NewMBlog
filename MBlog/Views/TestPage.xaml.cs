using Plugin.Media;
using Plugin.Media.Abstractions;
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
	public partial class TestPage : ContentPage
	{
		
		public TestPage()
		{
			InitializeComponent();
		}

		private void Button_Clicked_1(object sender, EventArgs e)
		{
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				DisplayAlert("No Camera", ":( No camera available.", "OK");
				return;
			}

			var file = CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
			{
				Directory = "Test",
				SaveToAlbum = true,
				CompressionQuality = 75,
				CustomPhotoSize = 50,
				PhotoSize = PhotoSize.MaxWidthHeight,
				MaxWidthHeight = 2000,
				DefaultCamera = CameraDevice.Front
			});

			if (file == null)
				return;

			//DisplayAlert("File Location", file., "OK");

			//image.Source = ImageSource.FromStream(() =>
			//{
			//	var stream = file.GetStream();
			//	file.Dispose();
			//	return stream;
			//});
		}
	  }
	}