using CRMMobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadActivityView : ContentView
    {
        public LeadActivityView(LeadDetailViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
    }
}