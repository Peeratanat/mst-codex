using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollapseOpportunityLayout : ContentView
    {
        private bool _collapsed;
        private double OriginalHeight;

        public CollapseOpportunityLayout()
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
                MyContent.IsVisible = false;
                await MyContent.FadeTo(0, 250);
                _collapsed = true;
            }
        }

        public static readonly BindableProperty CreateRevisitCommandProperty = BindableProperty.Create(nameof(CreateRevisitCommand), typeof(ICommand), typeof(CollapseOpportunityLayout), null);

        public ICommand CreateRevisitCommand
        {
            get { return (ICommand)GetValue(CreateRevisitCommandProperty); }
            set { SetValue(CreateRevisitCommandProperty, value); }
        }

        public static readonly BindableProperty CreateActivityWalkCommandProperty = BindableProperty.Create(nameof(CreateActivityWalkCommand), typeof(ICommand), typeof(CollapseOpportunityLayout), null);

        public ICommand CreateActivityWalkCommand
        {
            get { return (ICommand)GetValue(CreateActivityWalkCommandProperty); }
            set { SetValue(CreateActivityWalkCommandProperty, value); }
        }

        public static readonly BindableProperty EditCommandProperty = BindableProperty.Create(nameof(EditCommand), typeof(ICommand), typeof(CollapseOpportunityLayout), null);

        public ICommand EditCommand
        {
            get { return (ICommand)GetValue(EditCommandProperty); }
            set { SetValue(EditCommandProperty, value); }
        }

        public static readonly BindableProperty RemoveCommandProperty = BindableProperty.Create(nameof(RemoveCommand), typeof(ICommand), typeof(CollapseOpportunityLayout), null);

        public ICommand RemoveCommand
        {
            get { return (ICommand)GetValue(RemoveCommandProperty); }
            set { SetValue(RemoveCommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
           BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CollapseOpportunityLayout), null);

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public void Handle_Edit(object sender, EventArgs e)
        {
            if (!EditCommand.CanExecute(CommandParameter))
                return;

            EditCommand.Execute(CommandParameter);
        }

        public void Handle_Remove(object sender, EventArgs e)
        {
            if (!RemoveCommand.CanExecute(CommandParameter))
                return;
            RemoveCommand.Execute(CommandParameter);
        }

        public void Handle_CreateActivity(object sender, EventArgs e)
        {
            if (CreateActivityWalkCommand == null)
                return;

            if (!CreateActivityWalkCommand.CanExecute(CommandParameter))
                return;

            CreateActivityWalkCommand?.Execute(CommandParameter);
        }

        public void Handle_CreateRevisit(object sender, EventArgs e)
        {
            if (CreateRevisitCommand == null)
                return;
            if (!CreateRevisitCommand.CanExecute(CommandParameter))
                return;
            CreateRevisitCommand.Execute(CommandParameter);
        }

        public void Handle_RevisitCount(object sender, EventArgs e)
        {
        }
    }
}