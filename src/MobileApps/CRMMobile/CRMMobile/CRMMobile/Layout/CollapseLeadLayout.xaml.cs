using IO.Swagger.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollapseLeadLayout : ContentView
    {
        private bool _collapsed;
        private double OriginalHeight;

        public CollapseLeadLayout()
        {
            InitializeComponent();
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
                MyContent.IsVisible = false;
                await MyContent.FadeTo(0, 250);
                _collapsed = true;
            }
        }

        public static readonly BindableProperty LeadQualiflyCommandProperty = BindableProperty.Create(nameof(LeadQualiflyCommand), typeof(ICommand), typeof(CollapseLeadLayout), null);

        public static readonly BindableProperty LeadDisQualifyCommandProperty = BindableProperty.Create(nameof(LeadDisQualifyCommand), typeof(ICommand), typeof(CollapseLeadLayout), null);

        public static readonly BindableProperty RemoveLeadCommandProperty = BindableProperty.Create(nameof(RemoveLeadCommand), typeof(ICommand), typeof(CollapseLeadLayout), null);

        public ICommand LeadQualiflyCommand
        {
            get { return (ICommand)GetValue(LeadQualiflyCommandProperty); }
            set { SetValue(LeadQualiflyCommandProperty, value); }
        }

        public ICommand LeadDisQualifyCommand
        {
            get { return (ICommand)GetValue(LeadDisQualifyCommandProperty); }
            set { SetValue(LeadDisQualifyCommandProperty, value); }
        }

        public ICommand RemoveLeadCommand
        {
            get { return (ICommand)GetValue(RemoveLeadCommandProperty); }
            set { SetValue(RemoveLeadCommandProperty, value); }
        }

        public void LeadQualifly(object sender, EventArgs e)
        {
            var commandParameter = this.BindingContext as LeadListDTO;
            if (!LeadQualiflyCommand.CanExecute(commandParameter.Id))
                return;

            LeadQualiflyCommand.Execute(commandParameter.Id);
        }

        public void LeadDisQualify(object sender, EventArgs e)
        {
            var commandParameter = this.BindingContext;
            if (!LeadDisQualifyCommand.CanExecute(commandParameter))
                return;

            LeadDisQualifyCommand.Execute(commandParameter);
        }

        public void RemoveLead(object sender, EventArgs e)
        {
            var commandParameter = this.BindingContext;
            if (!RemoveLeadCommand.CanExecute(commandParameter))
                return;

            RemoveLeadCommand.Execute(commandParameter);
        }
    }
}