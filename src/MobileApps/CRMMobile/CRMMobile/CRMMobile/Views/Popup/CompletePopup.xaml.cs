using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;

namespace CRMMobile.Views.Popup
{
    public partial class CompletePopup : PopupPage
    {
        private Action Action;
        private const string defaultMessage = "บันทึกข้อมูลเสร็จสิ้น";

        public CompletePopup()
        {
            InitializeComponent();
            Message.Text = defaultMessage;
        }

        public CompletePopup(string message, Action action = null)
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(message))
                Message.Text = defaultMessage;
            else
                Message.Text = message;
            Action = action;
        }

        public async void CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        public void Handle_BackgroundClicked(object sender, EventArgs e)
        {
            //Close();
        }

        protected override bool OnBackButtonPressed()
        {
            Close();
            return base.OnBackButtonPressed();
        }

        protected override bool OnBackgroundClicked()
        {
            Close();
            return false;
        }

        public async void Close()
        {
            await Prism.PrismApplicationBase.Current.MainPage.Navigation.PopPopupAsync();
            Action?.Invoke();
        }
    }
}