using Xamarin.Forms;

namespace CRMMobile.Control
{
    public class SFProgressBar : AbsoluteLayout
    {
        public Label label;
        public Rectangle rect = new Rectangle(0, 0, .1, 1);

        protected override void OnParentSet()
        {
            base.OnParentSet();

            label = new Label()
            {
                Text = "0/5",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#35ADED"),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };
            BackgroundColor = Color.FromHex("#E9F0F5");
            UpdateLayout(label, 0);
            Children.Add(label);
        }

        private void UpdateLayout(Label _label, double progress)
        {
            // 5/100
            var level = Progress / Maximum;
            var leveText = level * Maximum;
            _label.Text = $"{(int)Progress}/{Maximum}";
            rect.Width = level;

            AbsoluteLayout.SetLayoutBounds(_label, rect);
            AbsoluteLayout.SetLayoutFlags(label, AbsoluteLayoutFlags.SizeProportional);
        }

        public static readonly BindableProperty MaximumProperty = BindableProperty.Create(
          "Maximum",
          typeof(double),
          typeof(SFProgressBar),
          5.0);

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly BindableProperty ProgressProperty = BindableProperty.Create(
         "Progress",
         typeof(double),
         typeof(SFProgressBar),
         0.0,
         propertyChanged: OnProgressPropertyChanged);

        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        private static void OnProgressPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var sFProgress = bindable as SFProgressBar;
            var value = (double)newValue;

            if (sFProgress.label == null)
                return;

            sFProgress.UpdateLayout(sFProgress.label, value);
        }
    }
}