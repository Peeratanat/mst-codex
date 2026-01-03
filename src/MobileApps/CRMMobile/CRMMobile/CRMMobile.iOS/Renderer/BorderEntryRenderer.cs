using CoreAnimation;
using CoreGraphics;
using CRMMobile.Control;
using CRMMobile.iOS.Renderer;
using System;
using System.ComponentModel;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderEntry), typeof(BorderEntryRenderer))]
namespace CRMMobile.iOS.Renderer
{
    public class BorderEntryRenderer : EntryRenderer
    {
        private string borderName = "BorderRect";
        BorderEntry element;
        UITextField textField;
        float borderWith = .6f;
        public event EventHandler OnIconTouch;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;

            element = (BorderEntry)this.Element;
           

            textField = this.Control;
            var icon = new UILabel()
            {
                Frame = new CGRect(0, 0, 33, 33)
            };

            icon.Text = ((char)element.Icon).ToString();
            icon.Font = UIFont.FromName("icomoon", 15);
            icon.TextAlignment = UITextAlignment.Center;
            icon.TextColor = element.IconColor.ToUIColor();

            UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, 33, 33));
            objLeftView.AddSubview(icon);
            UITapGestureRecognizer tapGesture = new UITapGestureRecognizer(() =>
            {
                OnIconTouch?.Invoke(this, null);
                this.Element.Focus();
            });
            objLeftView.AddGestureRecognizer(tapGesture);

            if (element.Icon != 0)
            {
                switch (element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        textField.LeftViewMode = UITextFieldViewMode.Always;
                        textField.LeftView = objLeftView;
                        break;
                    case ImageAlignment.Right:
                        textField.RightViewMode = UITextFieldViewMode.Always;
                        textField.RightView = objLeftView;
                        break;
                }
            }
            Control.BackgroundColor = UIColor.White;
            Control.LeftView = new UIView(new CGRect(0, 0, 10, 0));
            Control.LeftViewMode = UITextFieldViewMode.Always;
            textField.BorderStyle = UITextBorderStyle.None;
            CALayer bottomBorder = new CALayer
            {
                Name = borderName,
                Frame = new CGRect(0, 0, Element.Width, Element.Height),
                BorderWidth = borderWith,
                BorderColor = element.UnFocusColor.ToCGColor(),
                CornerRadius = element.Radius - 2
            };

            

            Control.Layer.AddSublayer(bottomBorder);

            if (Element is SFDatePicker)
            {
                if (Control != null)
                {
                    Control.InputView = new UIKit.UIView();
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            
            if (e.PropertyName == BorderEntry.HeightProperty.PropertyName)
            {

                var layer = Control.Layer.Sublayers.Where(t => t.Name == borderName).FirstOrDefault();

                layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                layer.BorderWidth = borderWith;
                layer.BorderColor = element.UnFocusColor.ToCGColor();
                layer.CornerRadius = element.Radius - 2;

            }

            if (e.PropertyName == BorderEntry.IsFocusedProperty.PropertyName)
            {

                element = Element as BorderEntry;
                if (element.IsFocused)
                {
                    var layer = Control.Layer.Sublayers.Where(t => t.Name.Equals(borderName)).FirstOrDefault();

                    layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                    layer.BorderWidth = borderWith;
                    layer.BorderColor = element.FocusColor.ToCGColor();
                    layer.CornerRadius = element.Radius - 2;
                }
                else
                {
                    var layer = Control.Layer.Sublayers.Where(t => t.Name.Equals(borderName)).FirstOrDefault();

                    layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                    layer.BorderWidth = borderWith;
                    layer.BorderColor = element.UnFocusColor.ToCGColor();
                    layer.CornerRadius = element.Radius - 2;
                }
            }
            if (e.PropertyName == BorderEntry.IsValidProperty.PropertyName)
            {
                if (!element.IsValid)
                {
                    var layer = Control.Layer.Sublayers.Where(t => t.Name.Equals(borderName)).FirstOrDefault();

                    layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                    layer.BorderWidth = borderWith;
                    layer.BorderColor = element.ErrorColor.ToCGColor();
                    layer.CornerRadius = element.Radius - 2;
                }
                else
                {
                    var layer = Control.Layer.Sublayers.Where(t => t.Name.Equals(borderName)).FirstOrDefault();

                    layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                    layer.BorderWidth = borderWith;
                    layer.BorderColor = element.UnFocusColor.ToCGColor();
                    layer.CornerRadius = element.Radius - 2;
                }
            }

        }

    }
}
