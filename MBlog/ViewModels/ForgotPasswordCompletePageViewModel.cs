using MBlog.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MBlog.ViewModels
{
	public class ForgotPasswordCompletePageViewModel:BaseViewModel
	{
		public ICommand RegisterCommand { get; set; }
		public ICommand SendEmailCommand { get; set; }
		public ICommand ForgotCommand { get; set; }
		public ICommand BackPageCommand { get; set; }
		public ForgotPasswordCompletePageViewModel()
		{
			ForgotCommand = new Command(GotoForgotPage);
			RegisterCommand = new Command(GotoRegisterPage);
			BackPageCommand = new Command(BackPage);
		}
		private async void GotoForgotPage()
		{
			await App.Current.MainPage.Navigation.PushAsync(new ForgotPasswordPage());
		}
		private async void GotoRegisterPage()
		{
			await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
		}
		private async void BackPage()
		{
			await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
		}
		private async void GotoPage()
		{
			await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
		}
	}
}
