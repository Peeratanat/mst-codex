using Android.Content;
using CRMMobile.Control;
using CRMMobile.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PickerFilter), typeof(PickerFilterRenderer))]
namespace CRMMobile.Droid.Renderer
{
    public class PickerFilterRenderer : BorderEntryRenderer
    {
        public PickerFilterRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                Control.ShowSoftInputOnFocus = false;
            }
        }
    }
}
