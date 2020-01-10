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
	public class ListTitleViewModel:BaseViewModel
    {
        //public ObservableCollection<string> Items { get; set; }	
        const int RefreshDuration = 2;
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
        public ListTitleViewModel()
		{
            //LoadingForyou = true;
            //

            Task.Run(async () => await MockDatatest());
            
            //LoadingForyou = false;
            FollowCommand = new Command<DataTest>(OnSelectedFollowView);

        }
        private async void OnSelectedFollowView(DataTest data)
        {
            //await App.Current.MainPage.DisplayAlert("", data.Title +"_", "OK");
            await App.Current.MainPage.Navigation.PushAsync(new FollowPage());
        }
        void AddItems()
        {
            for (int i = 0; i < 4; i++)
            {
                ListData.Add(new DataTest
                {
                    BookmarkShow=false,
                    Detail="Testgfgsdfgh fhfhf",
                    Title="Hello"+i,
                });
            }
        }

        async Task RefreshItemsAsync()
        {
            IsRefreshing = true;
            await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
            AddItems();
            IsRefreshing = false;
        }
    }
}
