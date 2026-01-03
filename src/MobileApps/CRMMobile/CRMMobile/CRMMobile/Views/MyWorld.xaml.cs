using CRMMobile.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;

namespace CRMMobile.Views
{
    public partial class MyWorld : ContentPage
    {
        private bool isOpen;
        private MyWorldViewModel vm;

        public MyWorld()
        {
            InitializeComponent();
        }

        private void ProjectList_OnSelectItemChanged(object sender, object e)
        {
            if (vm.FilterCommand.CanExecute())
                vm.FilterCommand.Execute();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm = this.BindingContext as MyWorldViewModel;
            vm.CloseFilterEvent += Vm_CloseFilterEvent;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.CloseFilterEvent -= Vm_CloseFilterEvent;
        }

        private void Vm_CloseFilterEvent(object sender, bool e)
        {
            isOpen = true;
            OpenOrClose();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            //onSettingLayout();
        }

        private void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            
            // Perform required operation after examining e.Value
        }

        private async void Projects_OnTextFilterChanged(object sender, TextChangedEventArgs e)
        {
            vm = this.BindingContext as MyWorldViewModel;
            await vm.GetProjects(e.NewTextValue);
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private async void Filter(object sender, EventArgs e)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            OpenOrClose();
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private async void OpenOrClose()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            if (!isOpen)
            {
                
                var heigth = Application.Current.MainPage.Height - FilterHeader.Bounds.Height;
                double newHeigth = 0;
                if (ActivitiesList.ItemsSource == null)
                {
                    ActivitiesList.HeightRequest = 0;
                }
                else
                {
                    var list = ActivitiesList.ItemsSource.Cast<object>().ToList();
                    var row = list.Count() / 2.0;
                    ActivitiesList.HeightRequest = row * 60;
                }

                if (Math.Abs(FilterDetailContainer.Height) <= 1)
                    newHeigth = 500 + ActivitiesList.HeightRequest;
                else
                    newHeigth = FilterDetailContainer.Height;


                if (newHeigth > heigth)
                    newHeigth = heigth - 50;
                
                
                isOpen = true;
                FilterButton.TextColor = Color.White;
                FilterButton.BorderColor = Color.FromHex("#AD0008");
                FilterButton.BackgroundColor = Color.FromHex("#C92028");
                AbsoluteLayout.SetLayoutBounds(FilterDetail, new Rectangle(0, 0, 1, newHeigth));
                AbsoluteLayout.SetLayoutFlags(FilterDetail, AbsoluteLayoutFlags.WidthProportional);
            }
            else
            {
                isOpen = false;
                FilterButton.TextColor = Color.FromHex("#454F63");
                FilterButton.BorderColor = Color.Transparent;
                FilterButton.BackgroundColor = Color.Transparent;
                AbsoluteLayout.SetLayoutBounds(FilterDetail, new Rectangle(0, 0, 1, 0));
                AbsoluteLayout.SetLayoutFlags(FilterDetail, AbsoluteLayoutFlags.WidthProportional);
                
            }
        }
    }
}