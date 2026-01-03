using Xamarin.Forms;

namespace CRMMobile.Control
{
    public class BorderEditor : Editor
    {
        public static readonly BindableProperty RadiusProperty =
           BindableProperty.Create(nameof(Radius), typeof(int), typeof(BorderEditor), 18);

        public static readonly BindableProperty FocusColorProperty =
            BindableProperty.Create(nameof(FocusColor), typeof(Color), typeof(BorderEditor), Color.Default);

        public static readonly BindableProperty UnFocusColorProperty =
            BindableProperty.Create(nameof(FocusColor), typeof(Color), typeof(BorderEditor), Color.FromHex("#8992A7"));

        public static readonly BindableProperty ErrorColorProperty =
          BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(BorderEditor), Color.Default);

        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(BorderEditor), false);

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