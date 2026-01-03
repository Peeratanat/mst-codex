using Xamarin.Forms;

namespace CRMMobile.Layout
{
    public partial class ContactPersonLayout : ContentView
    {
        public ContactPersonLayout()
        {
            InitializeComponent();
            //PrefixAutoComplete.OnTextFilterChanged += PrefixAutoComplete_OnTextFilterChanged;
            //PrefixsEngPicker.OnTextFilterChanged += PrefixsEngPicker_OnTextFilterChanged;
            //NationsPicker.OnTextFilterChanged += NationsPicker_OnTextFilterChanged;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private async void NationsPicker_OnTextFilterChanged(object sender, TextChangedEventArgs e)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            //var vm = this.BindingContext as ContactFormViewModel;
            //var result = await vm.GetMasterCenterData(MasterCenterKey.National, e.NewTextValue);
            //vm.Nations = new System.Collections.ObjectModel.ObservableCollection<IO.Swagger.Model.MasterCenterDropdownDTO>(result);
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private async void PrefixsEngPicker_OnTextFilterChanged(object sender, TextChangedEventArgs e)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            //var vm = this.BindingContext as ContactFormViewModel;
            //var result = await vm.GetMasterCenterData(MasterCenterKey.ContactTitleEN, e.NewTextValue);
            //vm.PrefixsEng = new System.Collections.ObjectModel.ObservableCollection<IO.Swagger.Model.MasterCenterDropdownDTO>(result);
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private async void PrefixAutoComplete_OnTextFilterChanged(object sender, TextChangedEventArgs e)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            //var vm = this.BindingContext as ContactFormViewModel;
            //var result= await vm.GetMasterCenterData(MasterCenterKey.ContactTitleTH, e.NewTextValue);
            //vm.PrefixsThai = new System.Collections.ObjectModel.ObservableCollection<IO.Swagger.Model.MasterCenterDropdownDTO>(result);
        }
    }
}