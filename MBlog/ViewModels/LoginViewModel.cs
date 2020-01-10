using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Input;
using MBlog.Helpers;
using MBlog.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MBlog.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties

        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value, onChanged: LoginCommand.ChangeCanExecute); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value, onChanged: LoginCommand.ChangeCanExecute); }
        }

        #endregion

        #region Commands

        public Command LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand ForgotCommand { get; set; }

        #endregion

        #region Constructors

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login(), CanLogin);
            ForgotCommand = new Command(GotoForgotPage);
            RegisterCommand = new Command(GotoRegisterPage);
        }

        #endregion

        #region Methods

        private async Task Login()
        {
            ClearErrorMessage();
            if (!EmailHelper.IsValidEmail(email))
            {
                ErrorMessage = "Invalid Email.";
                return;
            }

            IsBusy = true;
            await Task.Delay(2000);
            //Call Api
            //Home
            IsBusy = false;
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Password);
        }
        private async void GotoForgotPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ForgotPasswordPage());
        }
        private async void GotoRegisterPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
        #endregion


    }
}
