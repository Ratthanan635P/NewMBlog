using MBlog.Models;
using MBlog.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MBlog.ViewModels
{
	public class ListTitleViewModel:BaseViewModel
    {
        //public ObservableCollection<string> Items { get; set; }	

        public Command FollowCommand { get; set; }
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
        }
}
