using CoreGraphics;
using CRMMobile.Control;
using CRMMobile.iOS.Renderer;
using Foundation;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SFButton), typeof(SFButtonRenderer))]
namespace CRMMobile.iOS.Renderer
{
    public class SFButtonRenderer : ButtonRenderer
    {
        SFButton sFButton;
        public SFButtonRenderer()
        {
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            sFButton = (SFButton)Element;

            if (sFButton.Icon != 0)
            {
                var size = new CGSize(20, 20);

                // Control.SemanticContentAttribute = UISemanticContentAttribute.ForceRightToLeft;
                var image = ImageHelper.ImageFromFont(((char)sFButton.Icon).ToString(), sFButton.IconColor.ToUIColor(), size);
                Control.SetImage(image, UIControlState.Normal);


                switch (sFButton.IconAlignment)
                {
                    case ImageAlignment.Left:
                        Control.ImageEdgeInsets = new UIEdgeInsets(5, -15, 5, 5);
                        break;
                    case ImageAlignment.Right:
                        Control.ImageEdgeInsets = new UIEdgeInsets(top: 5, left: ((nfloat)Element.Width - 35), bottom: 5, right: 5);
                        Control.TitleEdgeInsets = new UIEdgeInsets(top: 0, left: 0, bottom: 0, right: Control.ImageView.Frame.Width);
                        break;
                }
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
                return;

        }


    }


    public static class ImageHelper
    {
        public static UIImage ImageFromFont(string text, UIColor iconColor, CGSize iconSize, string fontName = "icomoon")
        {
            UIGraphics.BeginImageContextWithOptions(iconSize, false, 0);

            var textRect = new CGRect(CGPoint.Empty, iconSize);
            var path = UIBezierPath.FromRect(textRect);
            UIColor.Clear.SetFill();
            path.Fill();

            var font = UIFont.FromName(fontName, iconSize.Width);
            using (var label = new UILabel() { Text = text, Font = font })
            {
                GetFontSize(label, iconSize, 500, 5);
                font = label.Font;
            }
            iconColor.SetFill();
            using (var nativeString = new NSString(text))
            {
                nativeString.DrawString(textRect, new UIStringAttributes
                {
                    Font = font,
                    ForegroundColor = iconColor,
                    BackgroundColor = UIColor.Clear,
                    ParagraphStyle = new NSMutableParagraphStyle
                    {
                        Alignment = UITextAlignment.Center
                    }
                });
            }
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return image;
        }

        private static void GetFontSize(UILabel label, CGSize size, int maxFontSize, int minFontSize)
        {
            label.Frame = new CGRect(CGPoint.Empty, size);
            var fontSize = maxFontSize;
            var constraintSize = new CGSize(label.Frame.Width, nfloat.MaxValue);
            while (fontSize > minFontSize)
            {
                label.Font = UIFont.FromName(label.Font.Name, fontSize);
                using (var nativeString = new NSString(label.Text))
                {
                    var textRect = nativeString.GetBoundingRect(
                              constraintSize,
                              NSStringDrawingOptions.UsesFontLeading,
                              new UIStringAttributes { Font = label.Font },
                              null
                            );

                    if (textRect.Size.Height <= label.Frame.Height)
                    {
                        break;
                    }
                }

                fontSize -= 2;
            }
        }
    }
}
