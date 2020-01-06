using MBlog.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MBlog.ViewModels
{
	public class ListTitleViewModel:BaseViewModel
    {
		//public ObservableCollection<string> Items { get; set; }	       
        public Command FollowCommand { get; set; }
        public ListTitleViewModel()
		{
            MockDatatest();
            FollowCommand = new Command<DataTest>(OnSelectedFollowView);

        }
        private async void OnSelectedFollowView(DataTest data)
        {
           await App.Current.MainPage.DisplayAlert("", data.Title +"_", "OK");
        }
           }
}
