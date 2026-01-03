using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;

namespace CRMMobile.Views.Popup
{
    public partial class LeadDeletePopup : PopupPage
    {
        public LeadDeletePopup(string name, string telNo)
        {
            InitializeComponent();
            Fullname.Text = name;
            Tel.Text = telNo;
        }

        public event EventHandler<bool> OnConfirm;

        public async void OnOkTapped(object sender, EventArgs e)
        {
            OnConfirm?.Invoke(this, true);
            await Navigation.PopPopupAsync();
        }

        public async void OnCancelTapped(object sender, EventArgs e)
        {
            OnConfirm?.Invoke(this, false);
            await Navigation.PopPopupAsync();
        }
    }
}