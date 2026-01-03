using CoreGraphics;
using CRMMobile.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(CustomeFrameRenderer))]
namespace CRMMobile.iOS.Renderer
{
    public class CustomeFrameRenderer : FrameRenderer
    {
        public CustomeFrameRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
                return;


            Layer.CornerRadius = Element.CornerRadius;

            if (Element.HasShadow)
            {
                Layer.BorderColor = Element.BorderColor.ToCGColor();
                Layer.MasksToBounds = false;
                Layer.ShadowOffset = new CGSize(1, 1);
                Layer.ShadowRadius = 2;
                Layer.ShadowOpacity = 0.4f;
            }


        }

        //public override void Draw(CGRect rect)
        //{
        //    base.Draw(rect);
        //    Layer.ShadowRadius = 1.6f;
        //    //Layer.BorderColor = UIColor.Red.CGColor;
        //    Layer.ShadowColor = UIColor.Gray.CGColor;
        //    Layer.ShadowOffset = new CGSize(0, 1.0f);
        //    Layer.ShadowOpacity = 0.80f;
        //    //Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
        //    Layer.MasksToBounds = false;
        //}
    }
}
