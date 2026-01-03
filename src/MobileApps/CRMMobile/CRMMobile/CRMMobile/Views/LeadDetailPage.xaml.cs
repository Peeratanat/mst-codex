using CRMMobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadDetailPage : ContentPage
    {
        private double TranslationPoint;
        private int OldIndex = 0;
        private LeadDetailViewModel vm;

        public LeadDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm = this.BindingContext as LeadDetailViewModel;
            vm.OnPositionChanged -= Vm_OnPositionChanged;
            vm.OnPositionChanged += Vm_OnPositionChanged;
            
        }

        private void Carousel_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.OnPositionChanged -= Vm_OnPositionChanged;
        }

        private void Vm_OnPositionChanged(object sender, System.EventArgs e)
        {
            TapGestureRecognizer_Tapped(null, null);
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (OldIndex == 0 && OldIndex == vm.Position)
                return;

            var selectColumn = this.HeaderTab.Children[vm.Position];
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
    }
}