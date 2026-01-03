using CRMMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadPage : ContentPage
    {
        private bool Opening;
        private LeadViewModel vm;

        public LeadPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm = this.BindingContext as LeadViewModel;
            vm.ClosedPopup -= Vm_ClosedPopup;
            vm.ClosedPopup += Vm_ClosedPopup;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.ClosedPopup -= Vm_ClosedPopup;
        }

        private void Vm_ClosedPopup(object sender, EventArgs e)
        {
            OpenPopup(false);
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        public async void Tapped(object sender, EventArgs e)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {

            vm.IsDisplaySearch = Opening = !Opening;
            OpenPopup(Opening);
        }

        public async void OpenPopup(bool isOpen)
        {
            if (isOpen)
            {
                await Filter.FadeTo(1, 250);
                AbsoluteLayout.SetLayoutBounds(Filter, new Rectangle(0, 0, 1, 425));
                AbsoluteLayout.SetLayoutFlags(Filter, AbsoluteLayoutFlags.WidthProportional);
            }
            else
            {
                await Filter.FadeTo(0, 250);
                AbsoluteLayout.SetLayoutBounds(Filter, new Rectangle(0, 0, 1, 0));
                AbsoluteLayout.SetLayoutFlags(Filter, AbsoluteLayoutFlags.WidthProportional);
            }
        }
    }
}