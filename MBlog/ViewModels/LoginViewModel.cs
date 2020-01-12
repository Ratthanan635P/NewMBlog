using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows.Input;
using MBlog.CallApi.Dtos;
using MBlog.CallApi.Helpers;
using MBlog.CallApi.Models;

using MBlog.Helpers;

using MBlog.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MBlog.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties
        public Result<UserDto, ErrorModel> result { get; set; }


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
                ErrorMessageEmail = "Invalid Email.";
                IsErrorEmail = true;
                return;
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
                else if (NullValidate(Password) == false)
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
                                    Password = Password
                                };

                                result = await AuthService.Login(command);
                                if (result.StatusCode == Enums.StatusCode.Ok)
                                {
                                    // await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                                    App.Email = result.Success.Email;
                                    App.FullName = result.Success.FullName;
                                    App.UserId = result.Success.Id;
                                    App.AccessToken = result.Success.AccessToken;
                                    App.About = result.Success.About;
                                    App.ImagePath = result.Success.ImageProfilePath;
                                    App.Current.MainPage = new AppShell();
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
            catch (OperationCanceledException ex)
            {
                //await PopupNavigation.Instance.PushAsync(new ErrorPopup("ปิดปรับปรุงServer"));
                await Application.Current.MainPage.DisplayAlert("", "ปิดปรับปรุงServer", "OK");
                //Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            catch (TimeoutException ex)
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

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
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
