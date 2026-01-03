using CRMMobile.Control;
using IO.Swagger.Api;
using IO.Swagger.Model;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRMMobile.Views.Popup
{
    public partial class PopupAutoComplete : PopupPage
    {
        public IMasterCentersApi MasterCentersApi { get; set; }
        public IProjectsApi ProjectsApi { get; set; }
        public EventHandler<EventArgs> TextChangedEvent { get; set; }
        public EventHandler<ItemTappedEventArgs> OnSelectItemEvent { get; set; }

        public IList<MasterCenterDropdownDTO> _itemSource { get; set; }
        public Object SelectItemItem { get; set; }
        private string Key;
        private APIType APIType;

        public PopupAutoComplete(string key, APIType aPIType = APIType.MasterCenter)
        {
            InitializeComponent();
            Key = key;
            APIType = aPIType;
            if (APIType == APIType.Proejct)
            {
                ProjectsApi = DependencyService.Resolve<IProjectsApi>();
                listview.IsVisible = false;
            }
            else
            {
                MasterCentersApi = DependencyService.Resolve<IMasterCentersApi>();
                project.IsVisible = false;
            }

            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await Filter(Key, string.Empty);
        }

        private async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            SelectItemItem = e.Item;
            OnSelectItemEvent?.Invoke(this, e);
            await Navigation.PopPopupAsync();
        }

        private void Handle_BackgroundClicked(object sender, System.EventArgs e)
        {
            TextChangedEvent?.Invoke(this, EventArgs.Empty);
        }

        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return false;
        }

        public async void ClosedClicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }

        public async void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.OldTextValue) && string.IsNullOrEmpty(e.NewTextValue))
                return;

            if (e.NewTextValue.Length >= 3)
                await Filter(Key, e.NewTextValue);

            if (e.NewTextValue.Length == 0)
                await Filter(Key, string.Empty);
        }

        public async Task Filter(string key, string filter)
        {
            try
            {
                if (APIType == APIType.Proejct)
                {
                    IsBusy2.IsRunning = true;
                    var result2 = await Task.Run(() => ProjectsApi.GetProjectDropdown(filter));
                    project.ItemsSource = result2;
                }
                else
                {
                    IsBusy.IsRunning = true;
                    var result = await Task.Run(() => MasterCentersApi.GetMasterCenterDropdownList(key, filter));
                    listview.ItemsSource = result;
                }
            }
            catch (Exception) { }
            finally { IsBusy.IsRunning = IsBusy2.IsRunning = false; }
        }
    }
}