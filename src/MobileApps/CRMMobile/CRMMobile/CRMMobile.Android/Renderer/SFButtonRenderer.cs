using Android.Content;
using CRMMobile.Control;
using CRMMobile.Droid.Renderer;
using JoanZapata.XamarinIconify;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(SFButton), typeof(SFButtonRenderer))]
namespace CRMMobile.Droid.Renderer
{
    public class SFButtonRenderer : Xamarin.Forms.Platform.Android.ButtonRenderer
    {
        private SFButton sFButton;
        public SFButtonRenderer(Context Context)
            : base(Context)
        {
        }



        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                // Cleanup
            }

            if (e.NewElement == null)
                return;

            sFButton = (SFButton)e.NewElement;
            if (sFButton.Icon != 0)
            {

                IconDrawable icon = new IconDrawable(this.Context, sFButton.Icon)
                        .WithColor(sFButton.IconColor.ToAndroid())
                        .WithSizePx(40);


                Control.CompoundDrawablePadding = 25;
                switch (sFButton.IconAlignment)
                {
                    case ImageAlignment.Left:
                        Control.SetCompoundDrawablesWithIntrinsicBounds(icon, null, null, null);
                        break;
                    case ImageAlignment.Right:
                        Control.SetCompoundDrawablesWithIntrinsicBounds(null, null, icon, null);
                        break;
                }


            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

        }

    }
}
