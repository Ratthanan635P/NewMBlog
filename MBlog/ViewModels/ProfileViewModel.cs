using MBlog.CallApi.Dtos;
using MBlog.CallApi.Helpers;
using MBlog.CallApi.Models;
using MBlog.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MBlog.ViewModels
{
	public class ProfileViewModel : BaseViewModel
	{

        public Result<SuccessModel, ErrorModel> result { get; set; }
        public Result<UserDto, ErrorModel> userData { get; set; }
        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged();
            }
        }
        private string about;
        public string About
        {
            get { return about; }
            set
            {
                about = value;
                OnPropertyChanged();
            }
        }
        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                imagePath = value;
                OnPropertyChanged();
            }
        }
        public Command SaveCommand { get; set; }
        public ProfileViewModel()
        {
            FullName = App.FullName;
            About = App.About;
            ImagePath = App.ImagePath;
            //Task.Run(()=> GetHome());
            SaveCommand = new Command(async () => await SaveData());
        }
        public async Task GetHome()
        {
            try
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

                            userData = await AuthService.GetUser(App.Email);
                            if (userData.StatusCode == Enums.StatusCode.Ok)
                            {
                                App.FullName= userData.Success.FullName;
                                App.About = userData.Success.About;
                                App.ImagePath = userData.Success.ImageProfilePath;

                                FullName = App.FullName;
                                About = App.About;
                                ImagePath = App.ImagePath;
                                workingStep = 100;
                            }
                            else
                            {
                                if (loopcheck <= maxRetry)
                                {
                                    if (userData.StatusCode == Enums.StatusCode.Unauthorized)
                                    {
                                       // await GetToken();
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
                            if (userData.StatusCode == Enums.StatusCode.BadRequest)
                            {
                                //await App.Current.MainPage.Navigation.PopAsync();
                                //await PopupNavigation.Instance.PushAsync(new ErrorPopup("SOMETHING WRONG"));

                                //await App.Current.MainPage.DisplayAlert("WARNING", "SOMETHING WRONG", "OK");
                            }
                            else if (userData.StatusCode == Enums.StatusCode.NotFound)
                            {
                              //  await PopupNavigation.Instance.PushAsync(new ErrorPopup(result.Error.ErrorMessage));

                                //await Application.Current.MainPage.DisplayAlert("", result.Error.ErrorMessage.ToString(), "OK");

                            }
                            else if (userData.StatusCode == Enums.StatusCode.InternalServerError)
                            {
                              //  await PopupNavigation.Instance.PushAsync(new ErrorPopup(result.Error.ErrorMessage));

                                //await Application.Current.MainPage.DisplayAlert("", result.Error.ErrorMessage.ToString(), "OK");
                            }
                            else
                            {
                              //  await PopupNavigation.Instance.PushAsync(new ErrorPopup(result.Error.ErrorMessage));

                                //await Application.Current.MainPage.DisplayAlert("", result.Error.ErrorMessage.ToString(), "OK");
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
            catch (OperationCanceledException)
            {
              //  await PopupNavigation.Instance.PushAsync(new ErrorPopup("ปิดปรับปรุงServer"));

                //await Application.Current.MainPage.DisplayAlert("", "ปิดปรับปรุงServer", "OK");
                //Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            catch (TimeoutException)
            {
              //  await PopupNavigation.Instance.PushAsync(new ErrorPopup("ปิดปรับปรุงServer"));

                //await Application.Current.MainPage.DisplayAlert("", "กรุณาลองใหม่อีกครั้ง", "OK");
                //Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
        private async Task SaveData()
        {
            ClearErrorMessage();
            IsBusy = true;
            await Task.Delay(200);
            try
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
                            ProfileCommands profile = new ProfileCommands()
                            {
                                Email=App.Email,
                                FullName=FullName,
                                About=About,
                                ImageProfilePath= "https://cdn.pixabay.com/photo/2018/04/05/05/51/duck-3291946_640.png"
                            };
                                result = await AuthService.Update(profile);
                                if (result.StatusCode == Enums.StatusCode.Ok)
                                {
                                 await GetHome();
                                // await App.Current.MainPage.Navigation.PushAsync(new LoginPage());

                                //App.Current.MainPage = new AppShell();
                                 await  App.Current.MainPage.DisplayAlert("", "Save Complete", "OK");
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
               // }
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
