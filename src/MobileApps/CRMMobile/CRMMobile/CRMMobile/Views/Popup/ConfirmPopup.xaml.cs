using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;

namespace CRMMobile.Views.Popup
{
    public partial class ConfirmPopup : PopupPage
    {
        public event EventHandler<bool> OnConfirmEvent;

        public ConfirmPopup(string title, string text, string acceptText, string cancelText)
        {
            InitializeComponent();
            LabelTitle.Text = title;
            LabelText.Text = text;
            AcceptButton.Text = acceptText;
            CancelButton.Text = cancelText;
        }

        public void OnAcceptClicked(object sender, EventArgs e)
        {
            OnConfirmEvent?.Invoke(this, true);
            CloseClicked(null, null);
        }

        public void OnCancelClicked(object sender, EventArgs e)
        {
            OnConfirmEvent?.Invoke(this, false);
            CloseClicked(null, null);
        }

        public async void CloseClicked(object sender, EventArgs e)
        {
            await Prism.PrismApplicationBase.Current.MainPage.Navigation.PopPopupAsync();
        }
    }
}