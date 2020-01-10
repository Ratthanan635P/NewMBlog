using MBlog.CallApi.Helpers;
using MBlog.CallApi.Models;
using MBlog.Helpers;
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
        public Result<SuccessModel, ErrorModel> result { get; set; }
        protected int retry = 1;
        protected int maxRetry = 3;

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
        private string password;
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
            ClearErrorMessage();
            if (!EmailHelper.IsValidEmail(email))
            {
                ErrorMessageEmail = "Invalid Email.";
                IsErrorEmail = true;
                return;
            }
            if (NewPassword != ConfirmPassword)
            {
                ErrorMessagePassword = "Password and ConfirmPassword not same!";
                IsErrorPassword = true;
                return;
            }
            else
            {
                password = NewPassword;
            }
            IsBusy = true;
            await Task.Delay(1000);
            try
            {
                //LoadingLottie = true;
                if (NullValidate(Email) == false)
                {
                    ErrorMessageEmail = "Please enter E-mail";
                    IsErrorEmail = true;
                }
                else if (!EmailHelper.IsValidEmail(email))
                {
                    ErrorMessageEmail = "E-mail is invalid";
                    IsErrorEmail = true;
                }
                else if (NullValidate(password) == false)
                {
                    ErrorMessagePassword = "Please enter Password";
                    IsErrorPassword = true;
                }
                else
                {
                    var checkNet = true;
                    int workingStep = 1;
                    retry = 1;
                    int loopcheck = 0;

                    bool internetCheck = true;
                    do
                    {
                        switch (workingStep)
                        {
                            case 1://check internet
                                checkNet = CheckingInternet();
                                if (checkNet == true)
                                {

                                    workingStep = 10;
                                }
                                else
                                {
                                    workingStep = 2;
                                }
                                break;
                            case 2://delay
                                await Task.Delay(300);
                                workingStep = 3;
                                break;
                            case 3://action result
                                bool istryAgain = await Application.Current.MainPage.DisplayAlert("", "No Internet", "Try Again", "Cancel");
                                if (istryAgain)
                                {
                                    workingStep = 1;
                                }
                                else
                                {
                                    internetCheck = false;
                                }
                                break;
                            case 10://call api
                                loopcheck++;
                                LoginCommandModel command = new LoginCommandModel()
                                {
                                    Email = Email,
                                    Password = password
                                };

                                result = await AuthService.Register(command);
                                if (result.StatusCode == Enums.StatusCode.Ok)
                                {
                                    await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                                    workingStep = 100;
                                }
                                else
                                {
                                    if (loopcheck <= maxRetry)
                                    {
                                        if (result.StatusCode == Enums.StatusCode.Unauthorized)
                                        {
                                            //	await GetToken();
                                        }
                                        else
                                        {
                                            workingStep++;
                                        }
                                    }
                                    else
                                    {
                                        internetCheck = false;
                                    }
                                }
                                break;
                            case 11://
                                await Task.Delay(300);
                                workingStep++;
                                break;
                            case 12://

                                if (result.StatusCode == Enums.StatusCode.BadRequest)
                                {
                                    //await PopupNavigation.Instance.PushAsync(new ErrorPopup(resultHistory.Error.ErrorMessage));
                                    //await Application.Current.MainPage.DisplayAlert("", resultHistory.Error.ErrorMessage.ToString(), "OK");
                                }
                                else if (result.StatusCode == Enums.StatusCode.NotFound)
                                {
                                    //await PopupNavigation.Instance.PushAsync(new ErrorPopup(resultHistory.Error.ErrorMessage));
                                    //await Application.Current.MainPage.DisplayAlert("", resultHistory.Error.ErrorMessage.ToString(), "OK");
                                }
                                else if (result.StatusCode == Enums.StatusCode.InternalServerError)
                                {
                                    //await PopupNavigation.Instance.PushAsync(new ErrorPopup(resultHistory.Error.ErrorMessage));
                                    //await Application.Current.MainPage.DisplayAlert("", resultHistory.Error.ErrorMessage.ToString(), "OK");
                                }
                                else
                                {
                                    //await PopupNavigation.Instance.PushAsync(new ErrorPopup(resultHistory.Error.ErrorMessage));
                                    //await Application.Current.MainPage.DisplayAlert("", resultHistory.Error.ErrorMessage.ToString(), "OK");
                                }
                                workingStep++;
                                break;
                            case 13://
                                internetCheck = false;
                                break;
                            case 100://
                                internetCheck = false;
                                break;
                            default:
                                internetCheck = false;
                                break;
                        }
                    } while (internetCheck);
                }
            }
            catch (OperationCanceledException)
            {
                //await PopupNavigation.Instance.PushAsync(new ErrorPopup("ปิดปรับปรุงServer"));
                await Application.Current.MainPage.DisplayAlert("", "ปิดปรับปรุงServer", "OK");
                //Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            catch (TimeoutException)
            {
                //await PopupNavigation.Instance.PushAsync(new ErrorPopup("กรุณาลองใหม่อีกครั้ง"));
                await Application.Current.MainPage.DisplayAlert("", "กรุณาลองใหม่อีกครั้ง", "OK");
                //Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            catch (Exception ex)
            {
                //await PopupNavigation.Instance.PushAsync(new ErrorPopup("กรุณาลองใหม่อีกครั้ง"));
                await Application.Current.MainPage.DisplayAlert("", "กรุณาลองใหม่อีกครั้ง", "OK");
            }
            //Call Api
            //Home
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
