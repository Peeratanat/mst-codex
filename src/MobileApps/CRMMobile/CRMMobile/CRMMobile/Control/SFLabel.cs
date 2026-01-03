using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CRMMobile.Control
{
    public class SFLabel : Label
    {
        public SFLabel()
        {
            //this.Triggers.Add();
            //Margin = new Thickness();
            //AddTrigger();
        }

        
        public static readonly BindableProperty DisplayTextProperty =
            BindableProperty.Create(nameof(DisplayText), typeof(string), typeof(SFLabel), null, BindingMode.TwoWay , propertyChanged: OnDisplayTextChanged);

        public string DisplayText
        {
            get { return (string)GetValue(DisplayTextProperty); }
            set { SetValue(DisplayTextProperty, value); }
        }


        private static void OnDisplayTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
           // var _newValue = (string)newValue;
            var sFLabel = bindable as SFLabel;
            sFLabel.Text = newValue == null ? "-" : (string)newValue;
            //sFLabel.Text = "This SFLabel";
        }


    }
}