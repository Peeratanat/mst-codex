using CRMMobile.Views.Popup;
using IO.Swagger.Api;
using IO.Swagger.Model;
using Prism.Commands;
using Prism.Navigation;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRMMobile.ViewModels
{
    public class QualifyContactPopupViewModel : ViewModelBase
    {
        public QualifyContactPopupViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            LeadQualify = new ObservableCollection<LeadQualifyDTO>();
            QualifyLeadCommand = new DelegateCommand<object>(async (value) => await Qualify(value));
            QualifyWithoutContactCommand = new DelegateCommand(async () => await QualifyWithoutContact());
        }

        [Unity.Dependency]
        public ILeadsApi LeadsApi { get; set; }

        public IContactsApi ContactsApi { get; set; }

        public DelegateCommand<object> QualifyLeadCommand { get; set; }
        public DelegateCommand QualifyWithoutContactCommand { get; set; }

        public ObservableCollection<LeadQualifyDTO> LeadQualify { get; set; }
        public Guid? LeadId { get; set; }
        public bool isAlreadyQualify { get; set; }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue("LeadQualifyDTO", out List<LeadQualifyDTO> list))
            {
                LoadLeadQualifly(list);
            }
            if (parameters.TryGetValue("LeadId", out Guid? leadId))
            {
                LeadId = leadId;
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            if (isAlreadyQualify)
                parameters.Add("RemoveLeadGuid", LeadId);

            base.OnNavigatedFrom(parameters);
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        public async void LoadLeadQualifly(List<LeadQualifyDTO> list)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                foreach (var item in list)
                {
                    LeadQualify.Add(item);
                }
            });
        }

        private async Task Qualify(object obj)
        {
            var item = (LeadQualifyDTO)obj;
            QualifyLeadToContact confirmPopup = new QualifyLeadToContact(item);
            confirmPopup.onConfirm += async (o, e) =>
            {
                if (e)
                {
                    try
                    {
                        IsBusy = true;
                        await Run(() => LeadsApi.SubmitQualify(LeadId.Value, item.Contact.Id));
                        IsBusy = false;
                        await DialogService.CloseCurrentPopup();
                        await DisplaySuccessPopup();
                        isAlreadyQualify = true;
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                        isAlreadyQualify = false;
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                }
                else
                {
                    await Prism.PrismApplicationBase.Current.MainPage.Navigation.PopPopupAsync();
                }
            };

            await Prism.PrismApplicationBase.Current.MainPage.Navigation.PushPopupAsync(confirmPopup);
        }

        private async Task QualifyWithoutContact()
        {
            try
            {
                IsBusy = true;
                await Run(() => LeadsApi.SubmitQualify(LeadId.Value, null));
                IsBusy = false;
                await DialogService.CloseCurrentPopup();
                await DisplaySuccessPopup();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}