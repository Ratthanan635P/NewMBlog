using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MBlog
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public string UserName { get; set; }

        public AppShell()
        {
            UserName = "DAMRONGDET LALITLAGSAMANONT";
            InitializeComponent();           
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}
