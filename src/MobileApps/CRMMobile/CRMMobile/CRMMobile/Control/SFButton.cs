using CRMMobile.Helper;
using Xamarin.Forms;

namespace CRMMobile.Control
{
    public class SFButton : Button
    {
        public SFButton()
        {
            Padding = new Thickness(8, 0, 8, 0);
        }

        public static readonly BindableProperty IconAlignmentProperty =
            BindableProperty.Create(nameof(IconAlignment), typeof(ImageAlignment), typeof(BorderEntry),
                ImageAlignment.Left);

        public static readonly BindableProperty IconProperty =
            BindableProperty.Create(nameof(Icon), typeof(FontIcons), typeof(BorderEntry));

        public static readonly BindableProperty IconColorProperty =
            BindableProperty.Create(nameof(IconColor), typeof(Color), typeof(BorderEntry), Color.Default);

        public ImageAlignment IconAlignment
        {
            get { return (ImageAlignment)GetValue(IconAlignmentProperty); }
            set { SetValue(IconAlignmentProperty, value); }
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
}