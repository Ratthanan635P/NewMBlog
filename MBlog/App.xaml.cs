﻿
using MBlog.CallApi.Service.Implements;
using MBlog.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBlog
{
    public partial class App : Application
    {
        public static int UserId { get; set; }
        public static string Email { get; set; }
        public static string FullName { get; set; }
        public static string About { get; set; }
        public static string ImagePath { get; set; }
        public static string AccessToken { get; set; }
        public App()
        {
            DependencyService.Register<AuthService>();
            DependencyService.Register<BlogService>();
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
           // MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
