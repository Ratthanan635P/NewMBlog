﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBlog.Components
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MB_BookmarkBlog : ContentView
	{
		public static readonly BindableProperty TxTitleProperty =
						  BindableProperty.Create(nameof(TxTitle),
												  typeof(string),
												  typeof(MB_BookmarkBlog),
												  string.Empty);

		public string TxTitle
		{
			get { return (string)GetValue(TxTitleProperty); }
			set { SetValue(TxTitleProperty, value); }
		}
		public static readonly BindableProperty TxDetailProperty =
						  BindableProperty.Create(nameof(TxDetail),
												  typeof(string),
												  typeof(MB_BookmarkBlog),
												  string.Empty);

		public string TxDetail
		{
			get { return (string)GetValue(TxDetailProperty); }
			set { SetValue(TxDetailProperty, value); }
		}
		public static readonly BindableProperty TxNameProperty =
						  BindableProperty.Create(nameof(TxName),
												  typeof(string),
												  typeof(MB_BookmarkBlog),
												  string.Empty);
		public string TxName
		{
			get { return (string)GetValue(TxNameProperty); }
			set { SetValue(TxNameProperty, value); }
		}
		public static readonly BindableProperty TxTimeAgoProperty =
								  BindableProperty.Create(nameof(TxTimeAgo),
														  typeof(string),
														  typeof(MB_BookmarkBlog),
														  string.Empty);
		public string TxTimeAgo
		{
			get { return (string)GetValue(TxTimeAgoProperty); }
			set { SetValue(TxTimeAgoProperty, value); }
		}
		public static readonly BindableProperty ImageTitleProperty =
						  BindableProperty.Create(nameof(ImageTitle),
												  typeof(ImageSource),
												  typeof(MB_BookmarkBlog)
												  );
		public ImageSource ImageTitle
		{
			get { return (ImageSource)GetValue(ImageTitleProperty); }
			set { SetValue(ImageTitleProperty, value); }
		}


		public static readonly BindableProperty IsLikeProperty =
						  BindableProperty.Create(nameof(IsLike),
												  typeof(bool),
												  typeof(MB_BookmarkBlog),
												  defaultBindingMode: BindingMode.TwoWay
												  );
		public bool IsLike
		{
			get { return (bool)GetValue(IsLikeProperty); }
			set
			{
				SetValue(IsLikeProperty, value);
				SetBookMark();
				OnPropertyChanged();
			}
				
		}

		public static readonly BindableProperty BookMarkVisibleProperty =
						  BindableProperty.Create(nameof(BookMarkVisible),
												  typeof(bool),
												  typeof(MB_BookmarkBlog)
												  );
		public bool BookMarkVisible
		{
			get { return (bool)GetValue(BookMarkVisibleProperty); }
			set { SetValue(BookMarkVisibleProperty, value); }
		}


		public static readonly BindableProperty IsOnProperty =
						  BindableProperty.Create(nameof(IsOn),
												  typeof(bool),
												  typeof(MB_BookmarkBlog)
												  );
		public bool IsOn
		{
			get { return (bool)GetValue(IsOnProperty); }
			set { SetValue(IsOnProperty, value);
				OnPropertyChanged();
			}
		}
		//private bool isOn;
		//public bool IsOn
		//{
		//	get { return isOn; }
		//	set
		//	{
		//		isOn = value;
		//		OnPropertyChanged();
		//	}
		//}
		public static readonly BindableProperty IsOffProperty =
						  BindableProperty.Create(nameof(IsOff),
												  typeof(bool),
												  typeof(MB_BookmarkBlog)
												  );
		public bool IsOff
		{
			get { return (bool)GetValue(IsOffProperty); }
			set
			{
				SetValue(IsOffProperty, value);
				OnPropertyChanged();
			}
		}
		//private bool isOff;
		//public bool IsOff
		//{
		//	get { return isOff; }
		//	set
		//	{
		//		isOff = value;
		//		OnPropertyChanged();
		//	}
		//}
		public static readonly BindableProperty BookmarkBtnProperty =
						  BindableProperty.Create(nameof(BookmarkBtn),
												  typeof(Command),
												  typeof(MB_BookmarkBlog)
												 );
		public Command BookmarkBtn
		{
			get { return (Command)GetValue(BookmarkBtnProperty); }
			set { SetValue(BookmarkBtnProperty, value); }
		}
		public static readonly BindableProperty BookmarkBtnParameterProperty =
						  BindableProperty.Create(nameof(BookmarkBtnParameter),
												  typeof(object),
												  typeof(MB_BookmarkBlog)
												 );
		public object BookmarkBtnParameter
		{
			get { return (object)GetValue(BookmarkBtnParameterProperty); }
			set { SetValue(BookmarkBtnParameterProperty, value); }
		}
		public MB_BookmarkBlog()
		{
			InitializeComponent();
			if (IsLike == false)
			{
				IsOn = false;
				IsOff = true;
			}
			else
			{
				IsOn = true;
				IsOff = false;
			}
		}

		private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			ChangeBookMark();
		}
		private void ChangeBookMark()
		{
			if (IsLike == true)
			{
				IsOn = false;
				IsOff = true;
				IsLike = false;
			}
			else
			{
				IsOn = true;
				IsOff = false;
				IsLike = true;
			}
		}
		private void SetBookMark()
		{
			if (IsLike == true)
			{
				IsOn = false;
				IsOff = true;
			}
			else
			{
				IsOn = true;
				IsOff = false;
			}
		}
	}
}