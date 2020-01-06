using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBlog.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MB_TitleBlog : ContentView
	{

		//   ImageTitle    // imageSource
		//TxTitle
		//TxDetail
		//TxButtom
		//ColorBtn   // color
		//CommandBtn //Command
		public static readonly BindableProperty TxTitleProperty =
						  BindableProperty.Create(nameof(TxTitle),
												  typeof(string),
												  typeof(MB_TitleBlog),
												  string.Empty);

		public string TxTitle
		{
			get { return (string)GetValue(TxTitleProperty); }
			set { SetValue(TxTitleProperty, value); }
		}
		public static readonly BindableProperty TxDetailProperty =
						  BindableProperty.Create(nameof(TxDetail),
												  typeof(string),
												  typeof(MB_TitleBlog),
												  string.Empty);

		public string TxDetail
		{
			get { return (string)GetValue(TxDetailProperty); }
			set { SetValue(TxDetailProperty, value); }
		}
		public static readonly BindableProperty TxButtomProperty =
						  BindableProperty.Create(nameof(TxButtom),
												  typeof(string),
												  typeof(MB_TitleBlog),
												  string.Empty);
		public string TxButtom
		{
			get { return (string)GetValue(TxButtomProperty); }
			set { SetValue(TxButtomProperty, value); }
		}

		public static readonly BindableProperty ImageTitleProperty =
						  BindableProperty.Create(nameof(ImageTitle),
												  typeof(ImageSource),
												  typeof(MB_TitleBlog)
												  );
		public ImageSource ImageTitle
		{
			get { return (ImageSource)GetValue(ImageTitleProperty); }
			set { SetValue(ImageTitleProperty, value); }
		}

		//public static readonly BindableProperty ColorBtnProperty =
		//				  BindableProperty.Create(nameof(ColorBtn),
		//										  typeof(Color),
		//										  typeof(MB_TitleBlog),
		//										  string.Empty);
		//public Color ColorBtn
		//{
		//	get { return (Color)GetValue(ColorBtnProperty); }
		//	set { SetValue(ColorBtnProperty, value); }
		//}
		//public static readonly BindableProperty CommandBtnProperty =
		//				  BindableProperty.Create(nameof(CommandBtn),
		//										  typeof(Command),
		//										  typeof(MB_TitleBlog),
		//										  string.Empty);
		//public Command CommandBtn
		//{
		//	get { return (Command)GetValue(CommandBtnProperty); }
		//	set { SetValue(CommandBtnProperty, value); }
		//}
		public MB_TitleBlog()
		{
			InitializeComponent();
		}
	}
}