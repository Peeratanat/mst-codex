
using CoreAnimation;
using CoreGraphics;
using CRMMobile.Control;
using CRMMobile.Helper;
using CRMMobile.iOS.Renderer;
using System.ComponentModel;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PickerEntry), typeof(PickerEntryRenderer))]
namespace CRMMobile.iOS.Renderer
{
    public class PickerEntryRenderer : PickerRenderer
    {
        PickerEntry element;
        float borderWith = .6f;
        private string borderName = "BorderRect";
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (PickerEntry)this.Element;

            if (Control != null && this.Element != null)
            {
                var icon = new UILabel()
                {
                    Frame = new CGRect(0, 0, 45, 45)
                };


                icon.Text = ((char)FontIcons.expand).ToString();
                icon.Font = UIFont.FromName("icomoon", 20);
                icon.TextAlignment = UITextAlignment.Center;
                icon.TextColor = Xamarin.Forms.Color.FromHex("#8992A7").ToUIColor();

                UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, 45, 45));
                objLeftView.AddSubview(icon);
                Control.RightViewMode = UIKit.UITextFieldViewMode.Always;
                Control.RightView = objLeftView;
                CALayer bottomBorder = new CALayer
                {
                    Name = borderName,
                    Frame = new CGRect(0, 0, Element.Width, Element.Height),
                    BorderWidth = borderWith,
                    BorderColor = element.UnFocusColor.ToCGColor(),
                    CornerRadius = element.Radius - 2
                };
                Control.Layer.AddSublayer(bottomBorder);

            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == PickerEntry.HeightProperty.PropertyName)
            {
                var layer = Control.Layer.Sublayers.Where(t => t.Name == borderName).FirstOrDefault();
                layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                layer.BorderWidth = borderWith;
                layer.BorderColor = element.UnFocusColor.ToCGColor();
                layer.CornerRadius = element.Radius - 2;

            }
            if (e.PropertyName == PickerEntry.IsFocusedProperty.PropertyName)
            {

                element = Element as PickerEntry;
                if (element.IsFocused)
                {
                    var layer = Control.Layer.Sublayers.Where(t => t.Name == borderName).FirstOrDefault();

                    layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                    layer.BorderWidth = borderWith;
                    layer.BorderColor = element.FocusColor.ToCGColor();
                    layer.CornerRadius = element.Radius - 2;
                   
                }
                else
                {
                    var layer = Control.Layer.Sublayers.Where(t => t.Name == borderName).FirstOrDefault();

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
                    var layer = Control.Layer.Sublayers.Where(t => t.Name == borderName).FirstOrDefault();

                    layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                    layer.BorderWidth = borderWith;
                    layer.BorderColor = element.ErrorColor.ToCGColor();
                    layer.CornerRadius = element.Radius - 2;
                }
                else
                {
                    var layer = Control.Layer.Sublayers.Where(t => t.Name == borderName).FirstOrDefault();

                    layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                    layer.BorderWidth = borderWith;
                    layer.BorderColor = element.UnFocusColor.ToCGColor();
                    layer.CornerRadius = element.Radius - 2;
                }
               
            }
        }
    }
}