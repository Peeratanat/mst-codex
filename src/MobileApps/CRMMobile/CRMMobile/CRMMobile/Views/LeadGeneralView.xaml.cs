using CRMMobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeadGeneralView : ContentView
    {
        private LeadDetailViewModel _vm;

        public LeadGeneralView(LeadDetailViewModel vm)
        {
            InitializeComponent();
            this.BindingContext = _vm = vm;
        }

        public void HandleAdvertisementChanged(object sender, EventArgs args)
        {
        }
    }
}