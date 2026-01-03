using CRMMobile.Models;
using CRMMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRMMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : MasterDetailPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var vm = (MenuViewModel)BindingContext;

            vm.SelectedMenuItem = e.SelectedItem as MainMenuItem;
            foreach (var item in vm.MenuItems)
            {
                if (item == vm.SelectedMenuItem)
                {
                    item.SelectedColorItem = Color.FromHex("#C92028");
                }
                else
                {
                    item.SelectedColorItem = Color.FromHex("#00ffffff");
                }
            }
        }
    }
}