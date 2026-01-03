using System;
using System.ComponentModel;
using CRMMobile.Control;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using RectangleF = CoreGraphics.CGRect;


namespace CRMMobile.iOS.Renderer
{
    
    public class CalendarPickerRenderer : CalendarRendererBase<UITextField>
    {
        public CalendarPickerRenderer()
        {
        }
    }

    internal class NoCaretField : UITextField
    {
        public NoCaretField() : base(new RectangleF())
        {
            SpellCheckingType = UITextSpellCheckingType.No;
            AutocorrectionType = UITextAutocorrectionType.No;
            AutocapitalizationType = UITextAutocapitalizationType.None;
        }


        public override RectangleF GetCaretRectForPosition(UITextPosition position)
        {
            return new RectangleF();
        }
    }

    public abstract class CalendarRendererBase<TControl> : ViewRenderer<PickerCalendar, TControl>
        where TControl : UITextField
    {

    }



}
