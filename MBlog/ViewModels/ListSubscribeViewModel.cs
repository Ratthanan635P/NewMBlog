using MBlog.CallApi.Helpers;
using MBlog.CallApi.Models;
using MBlog.Models;
using MBlog.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MBlog.ViewModels
{
	public class ListSubscribeViewModel : BaseViewModel
	{
		private Result<List<ProfileDto>, ErrorModel> result { get; set; }
		private Result<SuccessModel, ErrorModel> resultUnsub { get; set; }
		const int RefreshDuration = 2;
		private ObservableCollection<ProfileDto> listSubscr;
		public ObservableCollection<ProfileDto> ListSubscr
		{
			get { return listSubscr; }
			set
			{
				if (!Equals(listSubscr, value))
				{
					listSubscr = value;
					OnPropertyChanged();
				}
			}
		}
		private ObservableCollection<ProfileDto> listSubscrtest;
		public ObservableCollection<ProfileDto> ListSubscrtest
		{
			get { return listSubscrtest; }
			set
			{
				if (!Equals(listSubscrtest, value))
				{
					listSubscrtest = value;
					OnPropertyChanged();
				}
			}
		}
		
		public Command FollowCommand { get; set; }
        public ICommand RefreshCommand => new Command(async () => await RefreshItemsAsync());
        bool isRefreshing;

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        public ListSubscribeViewModel()
        {
			GetSubscribe();

			FollowCommand = new Command<ProfileDto>(OnSelectedFollowView);
        }
        private async void OnSelectedFollowView(ProfileDto data)
        {
			bool response = await App.Current.MainPage.DisplayAlert("ยืนยัน", "คุณต้องการยกเลิกการติดตาม \n ใช่ หรือ ไม่", "ใช่", "ไม่ใช่");
			if(response)
			{
				UnSubscribe(data);
			}
			//await App.Current.MainPage.Navigation.PushAsync(new FollowPage());
		}
        async Task RefreshItemsAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
			GetSubscribe();
			IsRefreshing = false;
        }
		public async void GetSubscribe()
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

							 result = await BlogService.GetSubscribes(App.UserId);
							if (result.StatusCode == Enums.StatusCode.Ok)
							{
								ListSubscr = new ObservableCollection<ProfileDto>(result.Success);
								workingStep = 100;
							}
							else
							{
								if (loopcheck <= maxRetry)
								{
									if (result.StatusCode == Enums.StatusCode.Unauthorized)
									{
										//await GetToken();
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
								//await App.Current.MainPage.Navigation.PopAsync();
								//await PopupNavigation.Instance.PushAsync(new ErrorPopup("SOMETHING WRONG"));

								//await App.Current.MainPage.DisplayAlert("WARNING", "SOMETHING WRONG", "OK");
							}
							else if (result.StatusCode == Enums.StatusCode.NotFound)
							{
								//await PopupNavigation.Instance.PushAsync(new ErrorPopup(result.Error.ErrorMessage));

								//await Application.Current.MainPage.DisplayAlert("", result.Error.ErrorMessage.ToString(), "OK");

							}
							else if (result.StatusCode == Enums.StatusCode.InternalServerError)
							{
								//await PopupNavigation.Instance.PushAsync(new ErrorPopup(result.Error.ErrorMessage));

								//await Application.Current.MainPage.DisplayAlert("", result.Error.ErrorMessage.ToString(), "OK");
							}
							else
							{
								///await PopupNavigation.Instance.PushAsync(new ErrorPopup(result.Error.ErrorMessage));

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
				//await PopupNavigation.Instance.PushAsync(new ErrorPopup("ปิดปรับปรุงServer"));

				//await Application.Current.MainPage.DisplayAlert("", "ปิดปรับปรุงServer", "OK");
				//Application.Current.MainPage = new NavigationPage(new LoginPage());
			}
			catch (TimeoutException)
			{
				//await PopupNavigation.Instance.PushAsync(new ErrorPopup("ปิดปรับปรุงServer"));

				//await Application.Current.MainPage.DisplayAlert("", "กรุณาลองใหม่อีกครั้ง", "OK");
				//Application.Current.MainPage = new NavigationPage(new LoginPage());
			}
		}
		public async void UnSubscribe(ProfileDto data)
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

							resultUnsub = await BlogService.UnSubscribes(data.Id,App.UserId);
							if (resultUnsub.StatusCode == Enums.StatusCode.Ok)
							{
								//ListSubscr = new ObservableCollection<ProfileDto>(resultUnsub.Success);
								GetSubscribe();
								workingStep = 100;
							}
							else
							{
								if (loopcheck <= maxRetry)
								{
									if (resultUnsub.StatusCode == Enums.StatusCode.Unauthorized)
									{
										//await GetToken();
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
							if (resultUnsub.StatusCode == Enums.StatusCode.BadRequest)
							{
								//await App.Current.MainPage.Navigation.PopAsync();
								//await PopupNavigation.Instance.PushAsync(new ErrorPopup("SOMETHING WRONG"));

								//await App.Current.MainPage.DisplayAlert("WARNING", "SOMETHING WRONG", "OK");
							}
							else if (resultUnsub.StatusCode == Enums.StatusCode.NotFound)
							{
								//await PopupNavigation.Instance.PushAsync(new ErrorPopup(result.Error.ErrorMessage));

								//await Application.Current.MainPage.DisplayAlert("", result.Error.ErrorMessage.ToString(), "OK");

							}
							else if (resultUnsub.StatusCode == Enums.StatusCode.InternalServerError)
							{
								//await PopupNavigation.Instance.PushAsync(new ErrorPopup(result.Error.ErrorMessage));

								//await Application.Current.MainPage.DisplayAlert("", result.Error.ErrorMessage.ToString(), "OK");
							}
							else
							{
								///await PopupNavigation.Instance.PushAsync(new ErrorPopup(result.Error.ErrorMessage));

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
				//await PopupNavigation.Instance.PushAsync(new ErrorPopup("ปิดปรับปรุงServer"));

				//await Application.Current.MainPage.DisplayAlert("", "ปิดปรับปรุงServer", "OK");
				//Application.Current.MainPage = new NavigationPage(new LoginPage());
			}
			catch (TimeoutException)
			{
				//await PopupNavigation.Instance.PushAsync(new ErrorPopup("ปิดปรับปรุงServer"));

				//await Application.Current.MainPage.DisplayAlert("", "กรุณาลองใหม่อีกครั้ง", "OK");
				//Application.Current.MainPage = new NavigationPage(new LoginPage());
			}
		}
	}
}
