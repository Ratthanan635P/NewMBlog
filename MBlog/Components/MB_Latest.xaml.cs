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
	public partial class MB_Latest : ContentView
	{
		public static readonly BindableProperty TxTitleProperty =
						  BindableProperty.Create(nameof(TxTitle),
												  typeof(string),
												  typeof(MB_Latest),
												  string.Empty);

		public string TxTitle
		{
			get { return (string)GetValue(TxTitleProperty); }
			set { SetValue(TxTitleProperty, value); }
		}
		public static readonly BindableProperty TxNameProperty =
						  BindableProperty.Create(nameof(TxName),
												  typeof(string),
												  typeof(MB_Latest),
												  string.Empty);
		public string TxName
		{
			get { return (string)GetValue(TxNameProperty); }
			set { SetValue(TxNameProperty, value); }
		}
		public static readonly BindableProperty TxTimeAgoProperty =
								  BindableProperty.Create(nameof(TxTimeAgo),
														  typeof(string),
														  typeof(MB_Latest),
														  string.Empty);
		public string TxTimeAgo
		{
			get { return (string)GetValue(TxTimeAgoProperty); }
			set { SetValue(TxTimeAgoProperty, value); }
		}
		public static readonly BindableProperty ImageTitleProperty =
						  BindableProperty.Create(nameof(ImageTitle),
												  typeof(ImageSource),
												  typeof(MB_Latest)
												  );
		public ImageSource ImageTitle
		{
			get { return (ImageSource)GetValue(ImageTitleProperty); }
			set { SetValue(ImageTitleProperty, value); }
		}
		public MB_Latest()
		{
			InitializeComponent();
		}
	}
}