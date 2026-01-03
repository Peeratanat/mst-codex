using Android.Graphics;
using Android.Widget;
using CRMMobile.Droid;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(UnderlineEffect), "UnderlineEffect")]
namespace CRMMobile.Droid
{
    public class UnderlineEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            SetUnderline(true);
        }

        protected override void OnDetached()
        {
            SetUnderline(false);

        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            if (args.PropertyName == Label.TextProperty.PropertyName || args.PropertyName == Label.FormattedTextProperty.PropertyName)
            {
                SetUnderline(true);
            }
        }
        private void SetUnderline(bool underlined)
        {
            try
            {
                var textView = (TextView)Control;
                if (underlined)
                {
                    textView.PaintFlags |= PaintFlags.UnderlineText;
                }
                else
                {
                    textView.PaintFlags &= ~PaintFlags.UnderlineText;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot underline Label. Error: ", ex.Message);
            }
        }
    }
}