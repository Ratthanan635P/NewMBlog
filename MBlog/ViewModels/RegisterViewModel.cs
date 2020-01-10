using MBlog.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MBlog.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {

        #region RegisterCommandProperties

        private string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value, onChanged: RegisterCommand.ChangeCanExecute); }
        }

        private string newPassword;
        public string NewPassword
        {
            get { return newPassword; }
            set { SetProperty(ref newPassword, value, onChanged: RegisterCommand.ChangeCanExecute); }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { SetProperty(ref confirmPassword, value, onChanged: RegisterCommand.ChangeCanExecute); }
        }

        #endregion

        #region Commands

        public Command RegisterCommand { get; }
        public Command ForgotCommand { get; set; }
        public Command BackPageCommand { get; set; }

        #endregion

        #region Constructor

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () => await Register(), CanRegister);
            ForgotCommand = new Command(GotoForgotPage);
            BackPageCommand = new Command(BackPage);
        }

        #endregion

        #region RegisterCommandMethod

        private bool CanRegister()
        {
            if (string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(NewPassword) ||
                string.IsNullOrEmpty(ConfirmPassword))
            {
                return false;
            }

            return true;
        }

        private async Task Register()
        {
            IsBusy = true;
            await Task.Delay(2000);
            //Call Api
            IsBusy = false;
        }
        private async void GotoForgotPage()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ForgotPasswordPage());
        }       
        private async void BackPage()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
        #endregion



    }
}
