using MBlog.CallApi.Dtos;
using MBlog.CallApi.Helpers;
using MBlog.CallApi.Models;
using MBlog.Helpers;
using MBlog.Models;
using MBlog.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MBlog.ViewModels
{
	public class FollowingViewModel : BaseViewModel
	{

        public Result<SuccessModel, ErrorModel> resultFavo { get; set; }
        public Result<UserDto, ErrorModel> userData { get; set; }
        private Result<MyBlogs, ErrorModel> result { get; set; }
        private Result<SuccessModel, ErrorModel> resultUnFavo { get; set; }
		private Result<SuccessModel, ErrorModel> resultUnsub { get; set; }
		const int RefreshDuration = 2;

        private ObservableCollection<CallApi.Models.BlogModel> listBlog;
        public ObservableCollection<CallApi.Models.BlogModel> ListBlog
        {
            get { return listBlog; }
            set
            {
                if (!Equals(listBlog, value))
                {
                    listBlog = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public string posts;
        public string Posts
        {
            get { return posts; }
            set
            {
                posts = value;
                OnPropertyChanged();
            }
        }

        public string followings;
        public string Followings
        {
            get { return followings; }
            set
            {
                followings = value;
                OnPropertyChanged();
            }
        }

        public string followers;
        public string Followers
        {
            get { return followers; }
            set
            {
                followers = value;
                OnPropertyChanged();
            }
        }
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
		public Command BookmakCommand { get; set; }
		public Command FollowPageCommand { get; set; }

        public ProfileDto Profile { get; set; }
        public Command SaveCommand { get; set; }
        public FollowingViewModel(ProfileDto profile)
        {

            Profile = profile;
            FullName = Profile.FullName;
            About = Profile.About;
            ImagePath = Profile.ImageProfilePath;
            GetBlog();
            BookmakCommand = new Command<BlogModel>(async (data) => await OnSelectedBookMark(data));
			FollowPageCommand = new Command(UnSubscribe);

		}
		private async Task OnSelectedBookMark(BlogModel data)
		{
			
			if (data.IsLike)
			{
				UnFavorite(data);
			}
			else
			{
				Favorite(data);
			}
			//await App.Current.MainPage.Navigation.PushAsync(new FollowPage());
		}
		async Task RefreshItemsAsync()
		{
			IsRefreshing = true;
			await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
			GetBlog();
			IsRefreshing = false;
		}
		public async void Favorite(BlogModel data)
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

							resultFavo = await BlogService.Favorite(data.Id,App.UserId);
							if (result.StatusCode == Enums.StatusCode.Ok)
							{
								GetBlog();
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
								GetBlog();
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
		public async void UnSubscribe()
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

							resultUnsub = await BlogService.UnSubscribes(Profile.Id, App.UserId);
							if (resultUnsub.StatusCode == Enums.StatusCode.Ok)
							{
								//ListSubscr = new ObservableCollection<ProfileDto>(resultUnsub.Success);
								//await App.Current.MainPage.Navigation.RemovePage();
								await App.Current.MainPage.Navigation.PushAsync(new FollowPage(this));								

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
		public async void GetBlog()
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
                            result = await BlogService.GetTargetBlog(Profile.Id,App.UserId);
                            if (result.StatusCode == Enums.StatusCode.Ok)
                            {
                                List<BlogModel> blogModel = new List<BlogModel>();
                                MyBlogs data = result.Success;
                                blogModel = data.Blogs.Select(b => new BlogModel()
                                {
                                    BookMarkVisible = b.BookMarkVisible,
                                    IsLike = b.IsLike,
                                    Createtime = b.Createtime,
                                    Detail = b.Detail,
                                    Id = b.Id,
                                    ImageHead = b.ImageHead,
                                    ImagePath = b.ImagePath,
                                    Owner = b.Owner,
                                    OwnerId = b.OwnerId,
                                    Title = b.Title,
                                    Topic = b.Topic,
                                    TopicId = b.TopicId,
                                    IsOn = b.IsOn,
                                    IsOff = b.IsOff
                                }).ToList();
                                ListBlog = new ObservableCollection<BlogModel>(blogModel);
                                Posts = data.Posts + " " + "Posts";
                                Followers = data.Followers + " " + "Followers";
                                Followings = data.Followings + " " + "Followings";
                                workingStep = 100;
                                loopcheck = 0;
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
    }
}
