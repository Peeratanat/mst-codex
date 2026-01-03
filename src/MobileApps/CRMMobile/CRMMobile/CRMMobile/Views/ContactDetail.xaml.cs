using CRMMobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetail : ContentPage
    {
        private double TranslationPoint;
        private int OldIndex = 0;
        private ContactDetailViewModel vm;

        public ContactDetail()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm = this.BindingContext as ContactDetailViewModel;
            vm.PositionChanged += Vm_PositionChanged;
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (OldIndex == 0 && OldIndex == vm.Position)
                return;

            var selectColumn = this.HeaderTab.Children[vm.Position];
            //var gridWith = selectColumn.Width;
            if (vm.Position > OldIndex)
            {
                var index = vm.Position - OldIndex;
                TranslationPoint += (selectColumn.Width * index);
            }
            else
            {
                var index = OldIndex - vm.Position;
                TranslationPoint -= (selectColumn.Width * index);
            }

            await UnderlineTab.TranslateTo(TranslationPoint, 0, 100);
            OldIndex = vm.Position;
        }

        private void Vm_PositionChanged(object sender, int e)
        {
            TapGestureRecognizer_Tapped(null, EventArgs.Empty);
        }
    }
}