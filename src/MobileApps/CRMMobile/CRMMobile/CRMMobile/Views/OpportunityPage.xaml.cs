using CRMMobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpportunityPage : ContentPage
    {
        private bool Opening;
        //private bool isOpen;
        private OpportunityViewModel vm;

        public OpportunityPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm = this.BindingContext as OpportunityViewModel;
            vm.CloseFilterEvent -= Vm_CloseFilterEvent;
            vm.CloseFilterEvent += Vm_CloseFilterEvent;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.CloseFilterEvent -= Vm_CloseFilterEvent;
        }

        private async void Projects_OnTextFilterChanged(object sender, TextChangedEventArgs e)
        {
            await vm.GetProjects(e.NewTextValue);
        }

        private void Vm_CloseFilterEvent(object sender, bool e)
        {

            //isOpen = true;
            OpenOrClose(e);
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private async void Filter(object sender, EventArgs e)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Opening = !Opening;
            OpenOrClose(Opening);
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private async void OpenOrClose(bool isOpen)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            if (isOpen)
            {
                //isOpen = true;
                var heigth = Application.Current.MainPage.Height - 66;
                double newHeigth = 0;

                if (Math.Abs(FilterDetailContainer.Height) <= 1)
                    newHeigth = 440;
                else
                    newHeigth = FilterDetailContainer.Height;

                if (newHeigth > heigth)
                    newHeigth = heigth - 45;
               

                FilterButton.TextColor = Color.White;
                Header.Text = "เลือกข้อมูลที่ต้องการค้นหา";
                FilterFrame.BackgroundColor = Color.FromHex("#C92028");
                AbsoluteLayout.SetLayoutBounds(FilterDetail, new Rectangle(0, 66, 1, newHeigth));
                AbsoluteLayout.SetLayoutFlags(FilterDetail, AbsoluteLayoutFlags.WidthProportional);
            }
            else
            {
                //isOpen = false;
                Header.Text = "Opportunity";
                AbsoluteLayout.SetLayoutBounds(FilterDetail, new Rectangle(0, 130, 1, 0));
                AbsoluteLayout.SetLayoutFlags(FilterDetail, AbsoluteLayoutFlags.WidthProportional);
                FilterButton.TextColor = Color.FromHex("#454F63");
                FilterFrame.BackgroundColor = Color.Transparent;
            }
        }
    }
}