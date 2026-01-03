using System;
using CRMMobile.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CustomNavigationPageRenderer))]
namespace CRMMobile.iOS.Renderer
{
    public class CustomNavigationPageRenderer : NavigationRenderer
    {
        public CustomNavigationPageRenderer()
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if(e.NewElement != null)
            {
                var att = new UITextAttributes();
                att.Font = UIFont.FromName("AP-Bold",17);
                UINavigationBar.Appearance.SetTitleTextAttributes(att);
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationBar.BarTintColor = UIColor.FromRGB(201, 32, 40);
            NavigationBar.BarStyle = UIBarStyle.Black;
            NavigationBar.TintColor = UIColor.White;
        }
    }
}
