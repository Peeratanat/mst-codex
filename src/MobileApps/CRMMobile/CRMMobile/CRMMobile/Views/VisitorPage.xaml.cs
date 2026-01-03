using CRMMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitorPage : ContentPage
    {
        private bool isOpen;
        private VisitorViewModel vm;

        public VisitorPage()
        {
            InitializeComponent();
            ProjectList.OnTextFilterChanged += ProjectList_OnTextFilterChanged;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            vm = this.BindingContext as VisitorViewModel;
            vm.OnOpenCloseFilterEvent += Vm_CloseFilterEvent;
        }

        private async void ProjectList_OnTextFilterChanged(object sender, TextChangedEventArgs e)
        {
            await vm.GetProjects(e.NewTextValue);
        }

        private void Vm_CloseFilterEvent(object sender, bool e)
        {
            //isOpen = e;
            OpenOrClose(e);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (vm != null)
            {
                vm.OnOpenCloseFilterEvent -= Vm_CloseFilterEvent;
                vm.OnOpenCloseFilterEvent += Vm_CloseFilterEvent;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (vm != null)
                vm.OnOpenCloseFilterEvent -= Vm_CloseFilterEvent;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private async void Filter(object sender, EventArgs e)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            OpenOrClose(!isOpen);
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private async void OnSearch()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            var vm = this.BindingContext as VisitorViewModel;
            // OpenOrClose();
            vm.Filter();
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private async void OnClear()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            var vm = this.BindingContext as VisitorViewModel;
            vm.ClearFilter();
        }

        private async void OpenOrClose(bool open)
        {
            if (open)
            {
                isOpen = open;
                var originalHeigth = 540.0;
                var width = Application.Current.MainPage.Width;
                if (originalHeigth > Application.Current.MainPage.Height - 150)
                {
                    originalHeigth = Application.Current.MainPage.Height - 150;
                }
                if (SearchArea.Bounds.Width.Equals(-1))
                    width = SearchArea.Bounds.Width;

                FilterButton.TextColor = Color.White;
                FilterButton.BorderColor = Color.FromHex("#AD0008");
                FilterButton.BackgroundColor = Color.FromHex("#C92028");
                AbsoluteLayout.SetLayoutBounds(SearchArea, new Rectangle(0, 75, 1, originalHeigth));
                AbsoluteLayout.SetLayoutFlags(SearchArea, AbsoluteLayoutFlags.WidthProportional);
                await Task.WhenAll(new List<Task>() {
                        SearchArea.LayoutTo(new Rectangle(0, SearchArea.Bounds.Y, width, originalHeigth), 250, Easing.CubicOut),
                        SearchDetail.FadeTo(1, 250, Easing.CubicOut),
                });
            }
            else
            {
                isOpen = open;
                AbsoluteLayout.SetLayoutBounds(SearchArea, new Rectangle(0, 75, 1, 0));
                AbsoluteLayout.SetLayoutFlags(SearchArea, AbsoluteLayoutFlags.WidthProportional);
                FilterButton.TextColor = Color.FromHex("#454F63");
                FilterButton.BorderColor = Color.Transparent;
                FilterButton.BackgroundColor = Color.Transparent;
                await Task.WhenAll(new List<Task>() {
                        SearchDetail.FadeTo(0, 100, Easing.CubicInOut),
                        SearchArea.LayoutTo(new Rectangle(0, SearchArea.Bounds.Y, SearchArea.Bounds.Width, 0), 100, Easing.CubicInOut),
                 });
            }
        }
    }
}