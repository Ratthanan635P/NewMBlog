using System;
using Android.Content;
using MBlog.Droid.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomGlobalEntry))]
namespace MBlog.Droid.CustomRenderers
{
    public class CustomGlobalEntry : EntryRenderer
    {
        public CustomGlobalEntry(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Android.Graphics.Color backgroundColor = Android.Graphics.Color.White;
                if (Element is Entry entry)
                {
                    backgroundColor = entry.BackgroundColor.ToAndroid();
                }
                Control.SetBackgroundColor(backgroundColor);
            }
        }
    }
}
