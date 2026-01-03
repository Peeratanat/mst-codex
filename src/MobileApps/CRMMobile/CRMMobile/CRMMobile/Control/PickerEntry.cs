using Xamarin.Forms;

namespace CRMMobile.Control
{
    public class PickerEntry : Picker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(PickerEntry), string.Empty);

        public static readonly BindableProperty FocusColorProperty =
            BindableProperty.Create(nameof(FocusColor), typeof(Color), typeof(BorderEntry), Color.Default);

        public static readonly BindableProperty UnFocusColorProperty =
            BindableProperty.Create(nameof(FocusColor), typeof(Color), typeof(BorderEntry), Color.FromHex("#bababa"));

        public static readonly BindableProperty RadiusProperty =
            BindableProperty.Create(nameof(Radius), typeof(int), typeof(BorderEntry), 18);

        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(BorderEntry), false);

        public static readonly BindableProperty ErrorColorProperty =
            BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(BorderEntry), Color.Default);

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }

        public Color ErrorColor
        {
            get { return (Color)GetValue(ErrorColorProperty); }
            set { SetValue(ErrorColorProperty, value); }
        }

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public Color FocusColor
        {
            get { return (Color)GetValue(FocusColorProperty); }
            set { SetValue(FocusColorProperty, value); }
        }

        public Color UnFocusColor
        {
            get { return (Color)GetValue(UnFocusColorProperty); }
            set { SetValue(UnFocusColorProperty, value); }
        }

        public int Radius
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }
    }
}