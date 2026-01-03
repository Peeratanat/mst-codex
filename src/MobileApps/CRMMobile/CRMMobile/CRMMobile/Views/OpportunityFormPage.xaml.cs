using CRMMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpportunityFormPage : ContentPage
    {
        public OpportunityFormPage()
        {
            InitializeComponent();
        }

        private async void Projects_OnTextFilterChanged(object sender, TextChangedEventArgs e)
        {
            var vm = this.BindingContext as OpportunityFormViewModel;
            await vm.GetProjects(e.NewTextValue);
        }
    }
}