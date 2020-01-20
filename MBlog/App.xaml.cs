
using MBlog.CallApi.Service.Implements;
using MBlog.Views;
using MBlog.Views.LogInViews;
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
            DependencyService.Register<TopicService>();
            InitializeComponent();
            UserId = 1;
            Email = "jenggig@gmail.com";
            FullName = "Ratthanan Poulai";
            About="คนว่างงาน ที่รวยที่สุดในโลก 5555555+";
            //MainPage = new NavigationPage(new ListBookmarkPage());
            MainPage = new NavigationPage(new LoginPage());
            //MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app startsSS
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
