using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MBlog.Views
{
    public partial class LoginPage : ContentPage
    {

#if DEBUG
        public LoginPage()
        {
            InitializeComponent();
        }
#else
        public LoginPage()
        {
            InitializeComponent();
            InitialViewForAnimations();
        }

        private void InitialViewForAnimations()
        {
            logoImage.Opacity = 0;
            usernameEntry.Opacity = 0;
            passwordEntry.Opacity = 0;
            loginButton.Opacity = 0;
            loginFormStackLayout.TranslationY = 50;

            tokenSource = new CancellationTokenSource();
        }

        private CancellationTokenSource tokenSource;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RunViewAnimation(tokenSource.Token);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            tokenSource.Cancel();
        }

        private void RunViewAnimation(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                await logoImage.FadeTo(1, 500);
                await Task.Delay(500);
                await loginFormStackLayout.TranslateTo(0, -50, 1000);
                Task.WaitAll(usernameEntry.FadeTo(1, 300),
                             passwordEntry.FadeTo(1, 300),
                             loginButton.FadeTo(1, 300));
            }, cancellationToken);
        }

#endif
    }
}
