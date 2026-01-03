using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using CRMMobile.Control;
using CRMMobile.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderEditor), typeof(CustomEditorRenderer))]
namespace CRMMobile.Droid.Renderer
{
    public class CustomEditorRenderer : EditorRenderer
    {
        private GradientDrawable drawable;
        BorderEditor element;

        public CustomEditorRenderer(Context context)
            : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;

            element = Element as BorderEditor;
            Control.Gravity = GravityFlags.CenterVertical;
            //Control.SetLines(1);
            Control.SetPadding(25, 1, 25, 1);
            Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            drawable = new GradientDrawable();
            drawable.SetCornerRadius(element.Radius);
            drawable.SetStroke(2, element.UnFocusColor.ToAndroid());
            drawable.SetColor(Android.Graphics.Color.White);

            Control.Background.SetColorFilter(element.FocusColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
            SetBackgroundDrawable(drawable);

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == BorderEditor.IsFocusedProperty.PropertyName)
            {
                if (drawable == null)
                    return;
                element = Element as BorderEditor;
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

            if (e.PropertyName == BorderEditor.IsValidProperty.PropertyName)
            {
                if (drawable == null)
                    return;
                element = Element as BorderEditor;
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
