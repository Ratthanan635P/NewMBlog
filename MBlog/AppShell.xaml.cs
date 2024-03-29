﻿using MBlog.Views;
using MBlog.Views.LogInViews;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MBlog
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public string UserName { get; set; }

        public AppShell()
        {
            UserName = App.FullName;
            InitializeComponent();           
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Email = "";
            App.FullName = "";
            App.AccessToken = "";
            App.ImagePath = "";
            App.About = "";
            App.Current.MainPage = new NavigationPage( new LoginPage());
        }
    }
}
