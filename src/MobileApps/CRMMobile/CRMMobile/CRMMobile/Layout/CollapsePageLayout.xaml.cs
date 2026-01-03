using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollapsePageLayout : ContentView
    {
        private bool _collapsed;
        private double OriginalHeight;

        public CollapsePageLayout()
        {
            InitializeComponent();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            MyContent.SizeChanged += Layout_SizeChanged;
        }

        public static readonly BindableProperty NavigateToOpportunityTapCommandProperty = BindableProperty.Create(nameof(NavigateToOpportunityTapCommand), typeof(ICommand), typeof(CollapsePageLayout), null);

        public ICommand NavigateToOpportunityTapCommand
        {
            get { return (ICommand)GetValue(NavigateToOpportunityTapCommandProperty); }
            set { SetValue(NavigateToOpportunityTapCommandProperty, value); }
        }

        public static readonly BindableProperty CreateOpportunityCommandProperty = BindableProperty.Create(nameof(CreateOpportunityCommand), typeof(ICommand), typeof(CollapsePageLayout), null);

        public ICommand CreateOpportunityCommand
        {
            get { return (ICommand)GetValue(CreateOpportunityCommandProperty); }
            set { SetValue(CreateOpportunityCommandProperty, value); }
        }

        private void Layout_SizeChanged(object sender, EventArgs e)
        {
            OriginalHeight = MyContent.Bounds.Height;
            MyContent.SizeChanged -= Layout_SizeChanged;
        }

        private async void Rotate_Clicked(object sender, EventArgs e)
        {
            if (_collapsed)
            {
                var rec = new Rectangle(MyContent.Bounds.X, MyContent.Bounds.Y, MyContent.Bounds.Width, OriginalHeight);
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
                    MyContent.FadeTo(0, 250),
                    MyContent.LayoutTo(new Rectangle(MyContent.Bounds.X, MyContent.Bounds.Y, MyContent.Bounds.Width, 0), 500, Easing.CubicIn),
                    MyButton.RotateTo(180, 500, Easing.SpringOut),
                });
                MyContent.IsVisible = false;

                _collapsed = true;
            }
        }

        public void NavigateToOpportunityTap(object sender, EventArgs e)
        {
            if (NavigateToOpportunityTapCommand.CanExecute(this.BindingContext))
            {
                NavigateToOpportunityTapCommand.Execute(this.BindingContext);
            }
        }

        public void CreateOpportunityTap(object sender, EventArgs e)
        {
            if (CreateOpportunityCommand.CanExecute(this.BindingContext))
            {
                CreateOpportunityCommand.Execute(this.BindingContext);
            }
        }
    }
}