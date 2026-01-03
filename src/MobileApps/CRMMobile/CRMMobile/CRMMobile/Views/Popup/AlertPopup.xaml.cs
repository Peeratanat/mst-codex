using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;

namespace CRMMobile.Views.Popup
{
    public partial class AlertPopup : PopupPage
    {
        public EventHandler<EventArgs> OnCloseClicked { get; set; }
        public Action OnClose;

        public AlertPopup(string title, string text, Action onClose)
        {
            InitializeComponent();
            LabelTitle.Text = title;
            LabelText.Text = text;

            OnClose = onClose;
        }

        public async void CloseClicked(object sender, EventArgs e)
        {
            OnCloseClicked?.Invoke(this, e);
            OnClose?.Invoke();
            await Prism.PrismApplicationBase.Current.MainPage.Navigation.PopPopupAsync();
        }
    }
}