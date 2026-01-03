using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using CRMMobile.Control;
using CRMMobile.Droid.Renderer;
using JoanZapata.XamarinIconify;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderEntry), typeof(BorderEntryRenderer))]
namespace CRMMobile.Droid.Renderer
{
    public class BorderEntryRenderer : EntryRenderer
    {
        private GradientDrawable drawable;
        BorderEntry element;
        //private Context con

        public BorderEntryRenderer(Context context)
            : base(context)
        { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;

            Control.Gravity = GravityFlags.CenterVertical;
            Control.SetLines(1);
            Control.SetPadding(25, 1, 25, 1);
            Control.SetBackgroundColor(Android.Graphics.Color.Transparent);

            element = (BorderEntry)this.Element;
            IconDrawable icon = null;

            var editText = this.Control;
            if (element.Icon != 0)
            {
                icon = new IconDrawable(this.Context, element.Icon).WithColor(element.IconColor.ToAndroid()).WithSizePx(50);
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(icon, null, null, null);
                        break;
                    case ImageAlignment.Right:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, icon, null);
                        break;
                }
            }

            editText.CompoundDrawablePadding = 25;
            Control.Background.SetColorFilter(element.LineColor.ToAndroid(), PorterDuff.Mode.SrcAtop);

            drawable = new GradientDrawable();
            drawable.SetCornerRadius(element.Radius);
            drawable.SetStroke(2, element.UnFocusColor.ToAndroid());
            drawable.SetColor(Android.Graphics.Color.White);
#pragma warning disable CS0618 // 'View.SetBackgroundDrawable(Drawable)' is obsolete: 'deprecated'
            SetBackgroundDrawable(drawable);
#pragma warning restore CS0618 // 'View.SetBackgroundDrawable(Drawable)' is obsolete: 'deprecated'

            if(Element is SFDatePicker)
            {
                Control.ShowSoftInputOnFocus = false;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == BorderEntry.IsFocusedProperty.PropertyName)
            {
                if (drawable == null)
                    return;
                element = Element as BorderEntry;
                if (element.IsFocused)
                {
                    drawable.SetStroke(2, element.FocusColor.ToAndroid());
                }
                else
                {
                    drawable.SetStroke(2, element.UnFocusColor.ToAndroid());
                }
#pragma warning disable CS0618 // 'View.SetBackgroundDrawable(Drawable)' is obsolete: 'deprecated'
                SetBackgroundDrawable(drawable);
#pragma warning restore CS0618 // 'View.SetBackgroundDrawable(Drawable)' is obsolete: 'deprecated'
            }

            if (e.PropertyName == BorderEntry.IsValidProperty.PropertyName)
            {
                if (drawable == null)
                    return;
                element = Element as BorderEntry;
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