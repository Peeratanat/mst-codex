using Android.Content;
using Android.Graphics;
using Android.Widget;
using CRMMobile.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationBarRenderer))]
namespace CRMMobile.Droid.Renderer
{
    public class CustomNavigationBarRenderer : NavigationPageRenderer
    {
        // private Android.Support.V7.Widget.Toolbar toolbar;

        public CustomNavigationBarRenderer(Context context) : base(context)
        {
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            for (int index = 0; index < toolbar.ChildCount; index++)
            {
                if (toolbar.GetChildAt(index) is TextView)
                {
                    var title = toolbar.GetChildAt(index) as TextView;
                    float toolbarCenter = toolbar.MeasuredWidth / 2;
                    float titleCenter = title.MeasuredWidth / 2;
                    Typeface tf = Typeface.CreateFromAsset(Context.Assets, "AP-Bold.ttf");
                    title.Typeface = tf;
                    title.SetTextColor(Android.Graphics.Color.White);
                    title.SetX(toolbarCenter - titleCenter);
                }
            }
        }
    }
}