using CRMMobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpportunityDetailPage : ContentPage
    {
        private double TranslationPoint;
        private int OldIndex = 0;

        public OpportunityDetailPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            var vm = this.BindingContext as OpportunityDetailViewModel;
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
    }
}