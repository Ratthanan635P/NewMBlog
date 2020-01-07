
using MBlog.Views;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MBlog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Shell
    {
        public MainPage()
        {
            InitializeComponent();
           
                ShellSection shell_section1 = new ShellSection
                {
                    Title = "ออเดอร์",
                    Icon = "ic_menu.png",
                    IsTabStop = true
                };
                shell_section1.Items.Add(new ShellContent() { Content = new FollowPage() });            
                flyoutItemAdd.Items.Add(shell_section1);
            
        }

        private void Exit_Clicked(object sender, EventArgs e)
        {
           // Application.Current.MainPage = new LoginPage();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
           // MessagingCenter.Send(this, "Hi");
        }
    }
}
