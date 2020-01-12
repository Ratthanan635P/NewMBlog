using MBlog.CallApi.Helpers;
using MBlog.CallApi.Models;
using MBlog.Helpers;
using MBlog.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MBlog.ViewModels
{
	public class ForgotPasswordPageViewModel:BaseViewModel
	{
        public Result<SuccessModel, ErrorModel> result { get; set; }

        private string email;
        public string Email
        {
            get { return email; }
            set {
           
                SetProperty(ref email, value); 
                }
        }
        //private string password;
        //public string Password
        //{
        //    get { return password; }
        //    set
        //    {

        //        SetProperty(ref password, value, onChanged: SendEmailCommand.CanExecuteChanged);
        //    }
        //}

        public ICommand RegisterCommand { get; set; }
		public ICommand SendEmailCommand { get;}
		public ICommand ForgotCommand { get; set; }
		public ICommand BackPageCommand { get; set; }
		public ForgotPasswordPageViewModel()
		{
			ForgotCommand = new Command(GotoForgotPage);
			RegisterCommand = new Command(GotoRegisterPage);
			BackPageCommand = new Command(BackPage);
			SendEmailCommand=new Command(async () => await SendEmailPage()); 

        }
        private bool CanEmail()
        {
            if (string.IsNullOrEmpty(Email))
            {
                return false;
            }

            return true;
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
			await App.Current.MainPage.Navigation.PopAsync();
		}
	
        private async Task SendEmailPage()
        {
            ClearErrorMessage();
            if (!EmailHelper.IsValidEmail(Email))
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
                                

                                result = await AuthService.ForgotPassword(Email);
                                if (result.StatusCode == Enums.StatusCode.Ok)
                                {
                                     await App.Current.MainPage.Navigation.PushAsync(new ForgotPasswordCompletePage());
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
    }
}
