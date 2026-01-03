using CRMMobile.Views.Popup;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Threading.Tasks;

namespace CRMMobile.Services
{
    public interface IDialogService
    {
        Task DisplayAlertAsync(string title, string text, Action onClose = null);

        Task ConfirmAlertAsync(string title, string text, string cancel, string ok, Action onSayYes = null, Action onSayNo = null);

        Task DisplayPopup(PopupPage page);

        Task DisplayCompletePopup(string message = null, Action action = null);

        Task CloseCurrentPopup();

        Task CloseAllPopup();
    }

    public class DialogService : IDialogService
    {
        public Task CloseAllPopup()
        {
            throw new NotImplementedException();
        }

        public async Task CloseCurrentPopup()
        {
            await Prism.PrismApplicationBase.Current.MainPage.Navigation.PopPopupAsync();
        }

        public async Task ConfirmAlertAsync(string title, string text, string cancel, string ok, Action onSayYes = null, Action onSayNo = null)
        {
            ConfirmPopup popup = new ConfirmPopup(title, text, cancel, ok);
            await Prism.PrismApplicationBase.Current.MainPage.Navigation.PushPopupAsync(popup);

            popup.OnConfirmEvent += (o, e) =>
            {
                if (e)
                {
                    onSayYes?.Invoke();
                }
                else
                {
                    onSayNo?.Invoke();
                }
            };
        }

        public async Task DisplayAlertAsync(string title, string text, Action onClose = null)
        {
            AlertPopup popup = new AlertPopup(title, text, onClose);
            await Prism.PrismApplicationBase.Current.MainPage.Navigation.PushPopupAsync(popup);
        }

        public async Task DisplayCompletePopup(string message = null, Action action = null)
        {
            CompletePopup popup = new CompletePopup(message, action);
            await Prism.PrismApplicationBase.Current.MainPage.Navigation.PushPopupAsync(popup);
        }

        public async Task DisplayPopup(PopupPage page)
        {
            await Prism.PrismApplicationBase.Current.MainPage.Navigation.PushPopupAsync(page);
        }
    }
}