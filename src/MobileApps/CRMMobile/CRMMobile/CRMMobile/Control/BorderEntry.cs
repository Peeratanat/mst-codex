using CRMMobile.Helper;
using Xamarin.Forms;

namespace CRMMobile.Control
{
    public class BorderEntry : Entry
    {
        public static readonly BindableProperty LineColorProperty =
            BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(BorderEntry), Color.White);

        public static readonly BindableProperty ImageAlignmentProperty =
            BindableProperty.Create(nameof(ImageAlignment), typeof(ImageAlignment), typeof(BorderEntry), ImageAlignment.Left);

        public static readonly BindableProperty FocusColorProperty =
            BindableProperty.Create(nameof(FocusColor), typeof(Color), typeof(BorderEntry), Color.Default);

        public static readonly BindableProperty UnFocusColorProperty =
            BindableProperty.Create(nameof(FocusColor), typeof(Color), typeof(BorderEntry), Color.FromHex("#8992A7"));

        public static readonly BindableProperty RadiusProperty =
            BindableProperty.Create(nameof(Radius), typeof(int), typeof(BorderEntry), 18);

        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(nameof(Icon), typeof(FontIcons), typeof(BorderEntry));

        public static readonly BindableProperty IconColorProperty =
            BindableProperty.Create(nameof(IconColor), typeof(Color), typeof(BorderEntry), Color.Default);

        public static readonly BindableProperty ErrorColorProperty =
            BindableProperty.Create(nameof(ErrorColor), typeof(Color), typeof(BorderEntry), Color.Default);

        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(BorderEntry), false);

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

        public Color LineColor
        {
            get { return (Color)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
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

        public ImageAlignment ImageAlignment
        {
            get { return (ImageAlignment)GetValue(ImageAlignmentProperty); }
            set { SetValue(ImageAlignmentProperty, value); }
        }

        public FontIcons Icon
        {
            get { return (FontIcons)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public Color IconColor
        {
            get { return (Color)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }
    }

    public enum ImageAlignment
    {
        Left,
        Right
    }
}