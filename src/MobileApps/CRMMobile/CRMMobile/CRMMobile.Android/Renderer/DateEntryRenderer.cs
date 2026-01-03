using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using CRMMobile.Control;
using CRMMobile.Droid.Renderer;
using CRMMobile.Helper;
using JoanZapata.XamarinIconify;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.DatePicker), typeof(DateEntryRenderer))]
namespace CRMMobile.Droid.Renderer
{
    public class DateEntryRenderer : DatePickerRenderer
    {
        public DateEntryRenderer(Context context) : base(context)
        {
        }

        Xamarin.Forms.DatePicker element;
        GradientDrawable drawable;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            element = (Xamarin.Forms.DatePicker)this.Element;

            if (Control != null && this.Element != null)
            {
                IconDrawable icon = new IconDrawable(this.Context, FontIcons.calendar)
                    .WithColor(Xamarin.Forms.Color.FromHex("#8992A7").ToAndroid())
                    .WithSizePx(40);
                Control.SetCompoundDrawablesWithIntrinsicBounds(null, null, icon, null);
                Control.Background = AddPickerStyles(element);
            }
        }

        public LayerDrawable AddPickerStyles(Xamarin.Forms.DatePicker elements)
        {
            Control.SetPadding(25, 1, 25, 1);
            drawable = new GradientDrawable();
            drawable.SetCornerRadius(5);
            drawable.SetStroke(2, Xamarin.Forms.Color.FromHex("#8992A7").ToAndroid());
            drawable.SetColor(Android.Graphics.Color.White);


            ShapeDrawable border = new ShapeDrawable();
            border.Paint.Color = Android.Graphics.Color.Transparent;
            border.SetPadding(25, 1, 25, 1);
            border.Paint.SetStyle(Paint.Style.Stroke);


            Drawable[] layers = { drawable, border };

            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            //if (e.PropertyName == CRMMobile.Control.SFDatePicker.IsValidProperty.PropertyName)
            //{
            //    var _element = Element as Control.SFDatePicker;
            //    if (!_element.IsValid)
            //    {
            //        drawable.SetStroke(2, _element.ErrorColor.ToAndroid());
            //    }
            //    else
            //    {
            //        drawable.SetStroke(2, Xamarin.Forms.Color.FromHex("#8992A7").ToAndroid());
            //    }
            //}
        }
    }
}