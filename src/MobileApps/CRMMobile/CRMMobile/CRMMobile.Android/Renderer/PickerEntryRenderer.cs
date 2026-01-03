using Android.Content;
using Android.Graphics.Drawables;
using CRMMobile.Control;
using CRMMobile.Droid.Renderer;
using CRMMobile.Helper;
using JoanZapata.XamarinIconify;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PickerEntry), typeof(PickerEntryRenderer))]
namespace CRMMobile.Droid.Renderer
{
    public class PickerEntryRenderer : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer
    {
        public PickerEntryRenderer(Context context) : base(context)
        {
        }

        PickerEntry element;
        private GradientDrawable drawable;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (PickerEntry)this.Element;

            if (Control != null && this.Element != null)
            {
                IconDrawable icon = new IconDrawable(this.Context, FontIcons.expand)
                    .WithColor(Xamarin.Forms.Color.FromHex("#8992A7").ToAndroid())
                    .WithSizePx(40);
                Control.SetCompoundDrawablesWithIntrinsicBounds(null, null, icon, null);
                Control.Background = AddPickerStyles(element);
            }
        }

        public LayerDrawable AddPickerStyles(PickerEntry elements)
        {
            Control.SetPadding(25, 1, 25, 1);

            drawable = new GradientDrawable();
            drawable.SetCornerRadius(element.Radius);
            drawable.SetStroke(2, element.UnFocusColor.ToAndroid());
            drawable.SetColor(Android.Graphics.Color.White);
            Drawable[] layers = { drawable };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == BorderEntry.IsValidProperty.PropertyName)
            {
                element = Element as PickerEntry;
                if (!element.IsValid)
                {
                    drawable.SetStroke(2, element.ErrorColor.ToAndroid());
                }
                else
                {
                    drawable.SetStroke(2, element.UnFocusColor.ToAndroid());
                }
            }
        }
    }
}