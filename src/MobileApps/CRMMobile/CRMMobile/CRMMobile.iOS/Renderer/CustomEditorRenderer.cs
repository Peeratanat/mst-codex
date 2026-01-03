using System;
using System.ComponentModel;
using CRMMobile.Control;
using CRMMobile.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderEditor), typeof(CustomEditorRenderer))]
namespace CRMMobile.iOS.Renderer
{
    public class CustomEditorRenderer : EditorRenderer
    {
        BorderEditor borderEditor;
        float borderWith = .6f;
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            if(Control != null)
            {
                borderEditor = Element as BorderEditor;
                Control.Layer.CornerRadius = borderEditor.Radius - 2;
                Control.Layer.BorderColor =  borderEditor.UnFocusColor.ToCGColor();
                Control.Layer.BorderWidth = borderWith;

                if (Element.BackgroundColor == Color.Default)
                {
                    Control.BackgroundColor = UIColor.White;
                }
                else
                {
                    Control.BackgroundColor = Element.BackgroundColor.ToUIColor();
                }                    

                if (Element.TextColor == Color.Default)
                {
                    this.Control.TextColor = UIColor.Black;
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(e.PropertyName == BorderEditor.TextColorProperty.PropertyName)
            {
                Control.TextColor = Element.TextColor.ToUIColor();
            }
            if (e.PropertyName == BorderEntry.IsFocusedProperty.PropertyName)
            {

                borderEditor = Element as BorderEditor;
                if (borderEditor.IsFocused)
                {
                    Control.Layer.CornerRadius = borderEditor.Radius - 2;
                    Control.Layer.BorderColor = borderEditor.FocusColor.ToCGColor();
                    Control.Layer.BorderWidth = borderWith;
                   
                }
                else
                {
                    Control.Layer.CornerRadius = borderEditor.Radius - 2;
                    Control.Layer.BorderColor = borderEditor.UnFocusColor.ToCGColor();
                    Control.Layer.BorderWidth = borderWith;
                   
                }
            }
            if (e.PropertyName == BorderEntry.IsValidProperty.PropertyName)
            {
                //if (!element.IsValid)
                //{
                //    var layer = Control.Layer.Sublayers.Where(t => t.Name.Equals(borderName)).FirstOrDefault();

                //    layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                //    layer.BorderWidth = borderWith;
                //    layer.BorderColor = element.ErrorColor.ToCGColor();
                //    layer.CornerRadius = element.Radius - 2;
                //}
                //else
                //{
                //    var layer = Control.Layer.Sublayers.Where(t => t.Name.Equals(borderName)).FirstOrDefault();

                //    layer.Frame = new CGRect(0, 0, Element.Width, Element.Height);
                //    layer.BorderWidth = borderWith;
                //    layer.BorderColor = element.UnFocusColor.ToCGColor();
                //    layer.CornerRadius = element.Radius - 2;
                //}
            }
        }

    }
}
