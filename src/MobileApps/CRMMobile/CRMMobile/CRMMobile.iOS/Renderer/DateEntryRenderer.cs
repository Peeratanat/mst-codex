using System.ComponentModel;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using CRMMobile.Control;
using CRMMobile.Helper;
using CRMMobile.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Xamarin.Forms.DatePicker), typeof(DateEntryRenderer))]
namespace CRMMobile.iOS.Renderer
{
    public class DateEntryRenderer : DatePickerRenderer
    {
        DatePicker element;
        float borderWith = .6f;
        int radius = 5;
        private string borderName = "BorderRect";
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var element = (DatePicker)this.Element;

                if (Control != null && this.Element != null)
                {
                    var icon = new UILabel()
                    {
                        Frame = new CGRect(0, 0, 45, 45)
                    };


                    icon.Text = ((char)FontIcons.calendar).ToString();
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
                        BorderColor = Xamarin.Forms.Color.FromHex("#8992A7").ToCGColor(),
                        CornerRadius = radius - 2
                    };
                    Control.Layer.AddSublayer(bottomBorder);

                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
           
            if (e.PropertyName == "Height")
            {
                element = Element as DatePicker;
                var layer = Control.Layer.Sublayers.Where(t => t.Name == borderName).FirstOrDefault();
                layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                layer.BorderWidth = borderWith;
                layer.BorderColor = Xamarin.Forms.Color.FromHex("#8992A7").ToCGColor();
                layer.CornerRadius = radius - 2;

            }
            if (e.PropertyName == DatePicker.IsFocusedProperty.PropertyName)
            {
                element = Element as DatePicker;
                if (element.IsFocused)
                {
                    var layer = Control.Layer.Sublayers.Where(t => t.Name == borderName).FirstOrDefault();

                    layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                    layer.BorderWidth = borderWith;
                    layer.BorderColor = Xamarin.Forms.Color.FromHex("#8992A7").ToCGColor();
                    layer.CornerRadius = radius - 2;

                }
                else
                {
                    var layer = Control.Layer.Sublayers.Where(t => t.Name == borderName).FirstOrDefault();

                    layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                    layer.BorderWidth = borderWith;
                    layer.BorderColor = Xamarin.Forms.Color.FromHex("#8992A7").ToCGColor();
                    layer.CornerRadius = radius - 2;

                }
            }
            if (e.PropertyName == "IsValid")
            {
                //var _element = Element as SFDatePicker;
               
                //if (!_element.IsValid)
                //{
                   
                //    var layer = Control.Layer.Sublayers.Where(t => t.Name == borderName).FirstOrDefault();

                //    layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                //    layer.BorderWidth = borderWith;
                //    layer.BorderColor = Xamarin.Forms.Color.FromHex("#C92028").ToCGColor();
                //    layer.CornerRadius = radius - 2;
                //}
                //else
                //{
                //    var layer = Control.Layer.Sublayers.Where(t => t.Name == borderName).FirstOrDefault();

                //    layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                //    layer.BorderWidth = borderWith;
                //    layer.BorderColor = Xamarin.Forms.Color.FromHex("#8992A7").ToCGColor();
                //    layer.CornerRadius = radius - 2;
                //}

            }
        }
    }
}
