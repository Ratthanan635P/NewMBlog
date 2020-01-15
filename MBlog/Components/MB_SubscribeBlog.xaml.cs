using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBlog.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MB_SubscribeBlog : ContentView, INotifyPropertyChanged
	{

		//ImageTitle    // imageSource
		//TxTitle
		//TxDetail
		//TxButtom
		//ColorBtn		// color
		//CommandBtn	//Command
		public static readonly BindableProperty TxTitleProperty =
						  BindableProperty.Create(nameof(TxTitle),
												  typeof(string),
												  typeof(MB_SubscribeBlog),
												  string.Empty);

		public string TxTitle
		{
			get { return (string)GetValue(TxTitleProperty); }
			set { SetValue(TxTitleProperty, value); }
		}
		public static readonly BindableProperty TxDetailProperty =
						  BindableProperty.Create(nameof(TxDetail),
												  typeof(string),
												  typeof(MB_SubscribeBlog),
												  string.Empty);

		public string TxDetail
		{
			get { return (string)GetValue(TxDetailProperty); }
			set { SetValue(TxDetailProperty, value); }
		}

		//public static readonly BindableProperty TxButtomProperty =
		//				  BindableProperty.Create(nameof(TxButtom),
		//										  typeof(string),
		//										  typeof(MB_SubscribeBlog),
		//										  string.Empty);
		//public string TxButtom
		//{
		//	get { return (string)GetValue(TxButtomProperty); }
		//	set { SetValue(TxButtomProperty, value); }
		//}

		public static readonly BindableProperty FollowingProperty =
						  BindableProperty.Create(nameof(Following),
												  typeof(bool),
												  typeof(MB_SubscribeBlog)
												  );
		public bool Following
		{
			get { return (bool)GetValue(FollowingProperty); }
			set { SetValue(FollowingProperty, value);
				CheckButton();
			}
		}

		public static readonly BindableProperty ImageTitleProperty =
						  BindableProperty.Create(nameof(ImageTitle),
												  typeof(ImageSource),
												  typeof(MB_SubscribeBlog)
												  );
		public ImageSource ImageTitle
		{
			get { return (ImageSource)GetValue(ImageTitleProperty); }
			set { SetValue(ImageTitleProperty, value); }
		}

		//public static readonly BindableProperty ColorBtnProperty =
		//				  BindableProperty.Create(nameof(ColorBtn),
		//										  typeof(Color),
		//										  typeof(MB_TitleBlog)
		//										 );
		//public Color ColorBtn
		//{
		//	get { return (Color)GetValue(ColorBtnProperty); }
		//	set { SetValue(ColorBtnProperty, value); }
		//}
		public static readonly BindableProperty CommandBtnProperty =
						  BindableProperty.Create(nameof(CommandBtn),
												  typeof(Command),
												  typeof(MB_SubscribeBlog)
												 );
		public Command CommandBtn
		{
			get { return (Command)GetValue(CommandBtnProperty); }
			set { SetValue(CommandBtnProperty, value); }
		}
		public static readonly BindableProperty CommandBtnParameterProperty =
						  BindableProperty.Create(nameof(CommandBtnParameter),
												  typeof(object),
												  typeof(MB_SubscribeBlog)
												 );
		public object CommandBtnParameter
		{
			get { return (object)GetValue(CommandBtnParameterProperty); }
			set { SetValue(CommandBtnParameterProperty, value); }
		}

		private string txButtom;
		public string TxButtom
		{
			get { return txButtom; }
			set
			{
				txButtom = value;
				OnPropertyChanged();
			}
		}
		private Color colortxButtom;
		public Color ColorTxButtom
		{
			get { return colortxButtom; }
			set
			{
				colortxButtom = value;
				OnPropertyChanged();
			}
		}
		private Color colorBackgroudButtom;
		public Color ColorBackgroudButtom
		{
			get { return colorBackgroudButtom; }
			set
			{
				colorBackgroudButtom = value;
				OnPropertyChanged();
			}
		}
		//CommandBtnParameter
		public MB_SubscribeBlog()
		{
			InitializeComponent();
			CheckButton();
		}

		private void btnFollow_Clicked(object sender, EventArgs e)
		{
			CheckButton();
		}
		private void CheckButton()
		{
			if (Following)
			{
				TxButtom = "following";
				ColorTxButtom = Color.FromHex("#FFFFFF");
				ColorBackgroudButtom = Color.FromHex("#1A6B69");
				//btnFollow.BorderColor = Color.FromHex("#1A6B69");
				//Following = false;
			}
			else
			{
				TxButtom = "follow";
				ColorTxButtom = Color.FromHex("#1A6B69");
				ColorBackgroudButtom = Color.FromHex("#FFFFFF");
				//btnFollow.BorderColor = Color.FromHex("#1A6B69");	
				//Following = true;
			}
		}
	}
}