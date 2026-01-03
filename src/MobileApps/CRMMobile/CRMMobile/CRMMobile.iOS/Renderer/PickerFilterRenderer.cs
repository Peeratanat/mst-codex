using CRMMobile.Control;
using CRMMobile.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(PickerFilter), typeof(PickerFilterRenderer))]
namespace CRMMobile.iOS.Renderer
{
    public class PickerFilterRenderer : BorderEntryRenderer
    {
        
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
                this.Control.InputView = new UIKit.UIView();

        }


    }
}
