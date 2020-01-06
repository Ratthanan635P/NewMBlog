using System;
using Xamarin.Forms;

namespace MBlog.Navigators
{
    public class ViewNavigator
    {
        public Command PopNav => new Command(() => Application.Current.MainPage.Navigation.PopAsync());

        public Command NavToRegisterPage => new Command(() => Application.Current.MainPage.Navigation.PushAsync(new Views.RegisterPage()));
    }
}
