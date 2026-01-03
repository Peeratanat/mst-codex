using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollapseContactOpportunityLayout : ContentView
    {
        private bool _collapsed;
        private double OriginalHeight;

        public CollapseContactOpportunityLayout()
        {
            InitializeComponent();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            MyContent.SizeChanged += Layout_SizeChanged;
        }

        private void Layout_SizeChanged(object sender, EventArgs e)
        {
            OriginalHeight = MyContent.Bounds.Height;
            MyContent.SizeChanged -= Layout_SizeChanged;
        }

        private async void Rotate_Clicked(object sender, EventArgs e)
        {
            var rec = new Rectangle(MyContent.Bounds.X, MyContent.Bounds.Y, MyContent.Bounds.Width, OriginalHeight);
            if (_collapsed)
            {
                MyContent.IsVisible = true;
                await Task.WhenAll(new List<Task> {
                    MyContent.FadeTo(1,250),
                    MyContent.LayoutTo(rec, 500, Easing.CubicOut),
                    MyButton.RotateTo(0, 500, Easing.SpringOut),
                });
                _collapsed = false;
            }
            else
            {
                await Task.WhenAll(new List<Task> {
                    MyContent.LayoutTo(new Rectangle(MyContent.Bounds.X, MyContent.Bounds.Y, MyContent.Bounds.Width, 0), 500, Easing.CubicIn),
                    MyButton.RotateTo(180, 500, Easing.SpringOut),
                });
                //MyButton.Rotation = 0;
                MyContent.IsVisible = false;
                await MyContent.FadeTo(0, 250);
                _collapsed = true;
            }
        }

        public static readonly BindableProperty EditCommandProperty = BindableProperty.Create(nameof(EditCommand), typeof(ICommand), typeof(CollapseContactOpportunityLayout), null);

        public ICommand EditCommand
        {
            get { return (ICommand)GetValue(EditCommandProperty); }
            set { SetValue(EditCommandProperty, value); }
        }

        public void Handle_Edit(object sender, EventArgs e)
        {
            var commandParameter = this.BindingContext;
            if (!EditCommand.CanExecute(commandParameter))
                return;

            EditCommand.Execute(commandParameter);
        }
    }
}