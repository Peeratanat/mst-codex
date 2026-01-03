using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Control
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RadioEntry : ContentView
    {
        public RadioEntry()
        {
            InitializeComponent();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            this.GestureRecognizers.Add(tapGestureRecognizer);
        }

        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
          "Color",
          typeof(Color),
          typeof(RadioEntry),
          Color.Blue,
          propertyChanged: OnColorPropertyChanged);

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly BindableProperty CheckedProperty = BindableProperty.Create("Checked", typeof(bool), typeof(RadioEntry), false, propertyChanged: OnCheckboxPropertyChanged);

        public bool Checked
        {
            get { return (bool)GetValue(CheckedProperty); }
            set { SetValue(CheckedProperty, value); }
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(propertyName: "Command",
            returnType: typeof(ICommand),
            declaringType: typeof(RadioEntry),
            defaultValue: null);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            propertyName: "CommandParameter",
            returnType: typeof(object),
            declaringType: typeof(RadioEntry),
            defaultValue: null);

        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(RadioEntry), string.Empty, propertyChanged: OnTextPropertyChanged);

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create("FontFamily", typeof(string), typeof(RadioEntry), string.Empty, propertyChanged: OnFontFamilyPropertyChanged);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ICommand cmd = Command;
            if (cmd != null && cmd.CanExecute(CommandParameter))
            {
                cmd.Execute(CommandParameter);
            }
            else
            {
                this.Checked = !this.Checked;
            }
        }

        public void Trigger()
        {
            InnerCheck.IsVisible = this.Checked;
        }

        private async static void OnCheckboxPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var checkbox = bindable as RadioEntry;
            var value = (bool)newValue;

            if (value)
            {
                await checkbox.InnerCheck.FadeTo(1, 50);
                checkbox.OuterCheck.BorderColor = checkbox.Color;
            }
            else
            {
                await checkbox.InnerCheck.FadeTo(0, 50);
                checkbox.OuterCheck.BorderColor = Color.FromHex("#BABABA");
            }

            if (checkbox != null)
                checkbox.InnerCheck.IsVisible = value;
        }

        private static void OnColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var checkbox = bindable as RadioEntry;
            var value = (Color)newValue;
            if (checkbox != null)
            {
                checkbox.InnerCheck.BackgroundColor = value;
            }
        }

        private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var checkbox = bindable as RadioEntry;
            var value = (string)newValue;
            if (checkbox != null)
            {
                checkbox.radiodesc.Text = value;

                if (string.IsNullOrEmpty(value))
                {
                    checkbox.radiodesc.IsVisible = false;
                }
                else
                {
                    checkbox.radiodesc.IsVisible = true;
                }
            }
        }

        private static void OnFontFamilyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var checkbox = bindable as RadioEntry;
            var value = (string)newValue;
            if (checkbox != null)
            {
                checkbox.radiodesc.FontFamily = value;
            }
        }
    }
}