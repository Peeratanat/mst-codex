using System;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace CRMMobile.Droid.Renderer
{
    public class CustomContentPageRenderer :PageRenderer 
    {
        public CustomContentPageRenderer(Context context):base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
        }
    }
}
