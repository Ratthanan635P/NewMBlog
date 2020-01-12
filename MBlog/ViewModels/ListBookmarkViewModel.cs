using MBlog.CallApi.Helpers;
using MBlog.CallApi.Models;
using MBlog.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MBlog.ViewModels
{
	public class ListBookmarkViewModel:BaseViewModel
	{
		private Result<List<BlogDto>, ErrorModel> result { get; set; }
		private Result<SuccessModel, ErrorModel> resultUnFavo { get; set; }
		const int RefreshDuration = 2;
		private ObservableCollection<BlogModel> listFavorite;
		public ObservableCollection<BlogModel> ListFavorite
		{
			get { return listFavorite; }
			set
			{
				if (!Equals(listFavorite, value))
				{
					listFavorite = value;
					OnPropertyChanged();
				}
			}
		}
		//private ObservableCollection<BlogDto> listSubscrtest;
		//public ObservableCollection<BlogDto> ListSubscrtest
		//{
		//	get { return listSubscrtest; }
		//	set
		//	{
		//		if (!Equals(listSubscrtest, value))
		//		{
		//			listSubscrtest = value;
		//			OnPropertyChanged();
		//		}
		//	}
		//}

		public Command BookmakCommand { get; set; }
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

		public ListBookmarkViewModel()
		{
			GetFavorite();

			BookmakCommand = new Command<BlogModel>(async (data) => await OnSelectedBookMark(data));
		}
		private async Task OnSelectedBookMark(BlogModel data)
		{
			bool response = await App.Current.MainPage.DisplayAlert("ยืนยัน", "คุณต้องการยกเลิกรายการโปรด \n ใช่ หรือ ไม่", "ใช่", "ไม่ใช่");
			if (response)
			{
				UnFavorite(data);
			}
			//await App.Current.MainPage.Navigation.PushAsync(new FollowPage());
		}
		async Task RefreshItemsAsync()
		{
			IsRefreshing = true;
			await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
			GetFavorite();
			IsRefreshing = false;
		}
		public async void GetFavorite()
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

							result = await BlogService.GetFavorites(App.UserId);
							if (result.StatusCode == Enums.StatusCode.Ok)
							{
								List<BlogModel> blogModel = new List<BlogModel>();
								List<BlogDto> data = result.Success;
								blogModel = data.Select(b => new BlogModel(){
									BookMarkVisible = true,
									IsLike = true,
									Createtime=b.Createtime,
									Detail=b.Detail,
									Id=b.Id,
									ImageHead=b.ImageHead,
									ImagePath=b.ImagePath,
									Owner=b.Owner,
									OwnerId=b.OwnerId,
									Title=b.Title,
									Topic=b.Topic,
									TopicId=b.TopicId,
									IsOn=true,
									IsOff=false
								}).ToList();
								ListFavorite = new ObservableCollection<BlogModel>(blogModel);
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
		public async void UnFavorite(BlogModel data)
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

							resultUnFavo = await BlogService.UnFavorite(data.Id, App.UserId);
							if (resultUnFavo.StatusCode == Enums.StatusCode.Ok)
							{
								//ListSubscr = new ObservableCollection<ProfileDto>(resultUnsub.Success);
								GetFavorite();
								workingStep = 100;
							}
							else
							{
								if (loopcheck <= maxRetry)
								{
									if (resultUnFavo.StatusCode == Enums.StatusCode.Unauthorized)
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
							if (resultUnFavo.StatusCode == Enums.StatusCode.BadRequest)
							{
								//await App.Current.MainPage.Navigation.PopAsync();
								//await PopupNavigation.Instance.PushAsync(new ErrorPopup("SOMETHING WRONG"));

								//await App.Current.MainPage.DisplayAlert("WARNING", "SOMETHING WRONG", "OK");
							}
							else if (resultUnFavo.StatusCode == Enums.StatusCode.NotFound)
							{
								//await PopupNavigation.Instance.PushAsync(new ErrorPopup(result.Error.ErrorMessage));

								//await Application.Current.MainPage.DisplayAlert("", result.Error.ErrorMessage.ToString(), "OK");

							}
							else if (resultUnFavo.StatusCode == Enums.StatusCode.InternalServerError)
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
