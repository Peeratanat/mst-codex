using CRMMobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpportunityActivityView : ContentView
    {
        public OpportunityActivityView(OpportunityDetailViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = vm;
        }
    }
}