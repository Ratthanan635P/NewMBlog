using MBlog.CallApi.Helpers;
using MBlog.CallApi.Models;
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
	public class HomeViewModel:BaseViewModel
	{
		private Result<List<ProfileDto>, ErrorModel> DataYouMightLike { get; set; }
		private Result<List<BlogDto>, ErrorModel> BlogsHot { get; set; }
		private Result<List<BlogDto>, ErrorModel> BlogsLatest { get; set; }
		private Result<List<BlogDto>, ErrorModel> BlogsForYou { get; set; }
		private Result<List<TopicDto>, ErrorModel> HeadTopics { get; set; }

		const int RefreshDuration = 2;

		private ObservableCollection<ProfileDto> listYouMightLike;
		public ObservableCollection<ProfileDto> ListYouMightLike
		{
			get { return listYouMightLike; }
			set
			{
				if (!Equals(listYouMightLike, value))
				{
					listYouMightLike = value;
					OnPropertyChanged();
				}
			}
		}

		private ObservableCollection<BlogModel> listBlogLatest;
		public ObservableCollection<BlogModel> ListBlogLatest
		{
			get { return listBlogLatest; }
			set
			{
				if (!Equals(listBlogLatest, value))
				{
					listBlogLatest = value;
					OnPropertyChanged();
				}
			}
		}

		private ObservableCollection<BlogModel> listBlogHot;
		public ObservableCollection<BlogModel> ListBlogHot
		{
			get { return listBlogHot; }
			set
			{
				if (!Equals(listBlogHot, value))
				{
					listBlogHot = value;
					OnPropertyChanged();
				}
			}
		}

		private ObservableCollection<BlogModel> listBlogsForYou;
		public ObservableCollection<BlogModel> ListBlogsForYou
		{
			get { return listBlogsForYou; }
			set
			{
				if (!Equals(listBlogsForYou, value))
				{
					listBlogsForYou = value;
					OnPropertyChanged();
				}
			}
		}

		private ObservableCollection<TopicDto> listTopic;
		public ObservableCollection<TopicDto> ListTopic
		{
			get { return listTopic; }
			set
			{
				if (!Equals(listTopic, value))
				{
					listTopic = value;
					OnPropertyChanged();
				}
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

		public ICommand RefreshCommand => new Command(async () => await RefreshItemsAsync());
		public HomeViewModel()
		{
			GetYouMightLike();
			//GetBlogsHot();
			//GetBlogsLatest();
			//GetBlogsForYou();
			//GetTopicAll();
		}
		async Task RefreshItemsAsync()
		{
			IsRefreshing = true;
			GetYouMightLike();
			IsRefreshing = false;
		}
		public async void GetYouMightLike()
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

							DataYouMightLike = await BlogService.GetYouMightLike(App.UserId);
							if (DataYouMightLike.StatusCode == Enums.StatusCode.Ok)
							{
								ListYouMightLike = new ObservableCollection<ProfileDto>(DataYouMightLike.Success);
								workingStep = 100;
							}
							else
							{
								if (loopcheck <= maxRetry)
								{
									if (DataYouMightLike.StatusCode == Enums.StatusCode.Unauthorized)
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
							if (DataYouMightLike.StatusCode == Enums.StatusCode.BadRequest)
							{
							}			
							else if (DataYouMightLike.StatusCode == Enums.StatusCode.NotFound)
							{
															}
							else if (DataYouMightLike.StatusCode == Enums.StatusCode.InternalServerError)
							{
													}
							else
							{
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
		public async void GetBlogsHot()
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

							BlogsHot = await BlogService.GetBlogHot();
							if (BlogsHot.StatusCode == Enums.StatusCode.Ok)
							{
								List<BlogModel> blogModel = new List<BlogModel>();
								List<BlogDto> data = BlogsHot.Success;
								blogModel = data.Select(b => new BlogModel()
								{
									BookMarkVisible = true,
									IsLike = true,
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
									IsOn = true,
									IsOff = false
								}).ToList();
								ListBlogHot = new ObservableCollection<BlogModel>(blogModel);
								workingStep = 100;
							}
							else
							{
								if (loopcheck <= maxRetry)
								{
									if (BlogsHot.StatusCode == Enums.StatusCode.Unauthorized)
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
							if (BlogsHot.StatusCode == Enums.StatusCode.BadRequest)
							{
											}
							else if (BlogsHot.StatusCode == Enums.StatusCode.NotFound)
							{
											}
							else if (BlogsHot.StatusCode == Enums.StatusCode.InternalServerError)
							{
									}
							else
							{
										}
							workingStep++;
							break;
						default:
							internetCheck = false;
							break;
					}
				} while (internetCheck);
			}
			catch (OperationCanceledException)
			{
			}
			catch (TimeoutException)
			{
		    }
		}
		public async void GetBlogsLatest()
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

							BlogsLatest = await BlogService.GetBlogLatest();
							if (BlogsLatest.StatusCode == Enums.StatusCode.Ok)
							{
								List<BlogModel> blogModel = new List<BlogModel>();
								List<BlogDto> data = BlogsLatest.Success;
								blogModel = data.Select(b => new BlogModel()
								{
									BookMarkVisible = true,
									IsLike = true,
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
									IsOn = true,
									IsOff = false
								}).ToList();
								ListBlogLatest = new ObservableCollection<BlogModel>(blogModel);
								workingStep = 100;
							}
							else
							{
								if (loopcheck <= maxRetry)
								{
									if (BlogsLatest.StatusCode == Enums.StatusCode.Unauthorized)
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
							if (BlogsLatest.StatusCode == Enums.StatusCode.BadRequest)
							{
							}
							else if (BlogsLatest.StatusCode == Enums.StatusCode.NotFound)
							{
							}
							else if (BlogsLatest.StatusCode == Enums.StatusCode.InternalServerError)
							{
							}
							else
							{
							}
							workingStep++;
							break;
						default:
							internetCheck = false;
							break;
					}
				} while (internetCheck);
			}
			catch (OperationCanceledException)
			{
			}
			catch (TimeoutException)
			{
			}
		}
		public async void GetBlogsForYou()
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

							BlogsForYou = await BlogService.GetBlogForYou(App.UserId);
							if (BlogsForYou.StatusCode == Enums.StatusCode.Ok)
							{
								List<BlogModel> blogModel = new List<BlogModel>();
								List<BlogDto> data = BlogsForYou.Success;
								blogModel = data.Select(b => new BlogModel()
								{
									BookMarkVisible = true,
									IsLike = true,
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
									IsOn = true,
									IsOff = false
								}).ToList();
								ListBlogsForYou = new ObservableCollection<BlogModel>(blogModel);
								workingStep = 100;
							}
							else
							{
								if (loopcheck <= maxRetry)
								{
									if (BlogsForYou.StatusCode == Enums.StatusCode.Unauthorized)
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
							if (BlogsForYou.StatusCode == Enums.StatusCode.BadRequest)
							{
							}
							else if (BlogsForYou.StatusCode == Enums.StatusCode.NotFound)
							{
							}
							else if (BlogsForYou.StatusCode == Enums.StatusCode.InternalServerError)
							{
							}
							else
							{
							}
							workingStep++;
							break;
						default:
							internetCheck = false;
							break;
					}
				} while (internetCheck);
			}
			catch (OperationCanceledException)
			{
			}
			catch (TimeoutException)
			{
			}
		}
		public async void GetTopicAll()
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

							HeadTopics = await TopicService.GetAll();
							if (BlogsForYou.StatusCode == Enums.StatusCode.Ok)
							{
								List<TopicDto> blogModel = new List<TopicDto>();
								List<TopicDto> data = HeadTopics.Success;
								blogModel = data;
								ListTopic = new ObservableCollection<TopicDto>(blogModel);
								workingStep = 100;
							}
							else
							{
								if (loopcheck <= maxRetry)
								{
									if (HeadTopics.StatusCode == Enums.StatusCode.Unauthorized)
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
							if (HeadTopics.StatusCode == Enums.StatusCode.BadRequest)
							{
							}
							else if (HeadTopics.StatusCode == Enums.StatusCode.NotFound)
							{
							}
							else if (HeadTopics.StatusCode == Enums.StatusCode.InternalServerError)
							{
							}
							else
							{
							}
							workingStep++;
							break;
						default:
							internetCheck = false;
							break;
					}
				} while (internetCheck);
			}
			catch (OperationCanceledException)
			{
			}
			catch (TimeoutException)
			{
			}
		}
		//GetAllTopic
	}
}
