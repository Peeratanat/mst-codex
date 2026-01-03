using IO.Swagger.Api;
using IO.Swagger.Model;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading.Tasks;

namespace CRMMobile.ViewModels
{
    public class LeadDeletePopupViewModel : ViewModelBase
    {
        public LeadDeletePopupViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            ConfirmDeleteLeadCommand = new DelegateCommand(async () =>
             {
                 await Task.Run(() => LeadsApi.DeleteLead(Id));
                 await DisplayAlert("สำเร็จ", "ลบ Lead สำเร็จ", "ปิด", async () =>
                 {
                     await NavigatedBack();
                     NavigationParameters param = new NavigationParameters();
                     param.Add("Refresh", true);
                     await NavigatedBack(param);
                 });
             });
        }

        public DelegateCommand ConfirmDeleteLeadCommand { get; set; }

        [Unity.Dependency]
        public ILeadsApi LeadsApi { get; set; }

        private string fullName;

        public string FullName
        {
            get => fullName;
            set { SetProperty(ref fullName, value); }
        }

        private string telePhone;

        public string TelePhone
        {
            get => telePhone;
            set { SetProperty(ref telePhone, value); }
        }

        public Guid Id { get; set; }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.TryGetValue(KnownNavigationParameters.XamlParam, out LeadListDTO value))
            {
                // do something with fooObject
                FullName = value.FirstName + " " + value.LastName;
                TelePhone = value.PhoneNumber;
                Id = value.Id.Value;
            }
        }

        private void DeleleteLeadAsync()
        {
            //await Task.Run(()=> LeadsApi.DeleteLead(Id));
            //DisplayAlert
        }
    }
}