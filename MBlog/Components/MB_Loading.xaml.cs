using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MBlog.Components
{
    public partial class MB_Loading : ContentView
    {
        public static readonly BindableProperty IsPlayingProperty =
                                    BindableProperty.Create(nameof(IsPlaying),
                                                            typeof(bool),
                                                            typeof(MB_Loading),
                                                            default(bool));

        public bool IsPlaying
        {
            get { return (bool)GetValue(IsPlayingProperty); }
            set { SetValue(IsPlayingProperty, value); }
        }

        public MB_Loading()
        {
            InitializeComponent();
        }
    }
}
