using CRMMobile.Helper;
using CRMMobile.Views;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRMMobile.ViewModels
{
    public class ContactDetailViewModel : ViewModelBase
    {
        public ContactDetailViewModel(INavigationService navigationService) : base(navigationService)
        {
            initCommand();
            ContactAddressies = new ObservableCollection<ContactAddressDTO>();
        }

        #region API

        [Unity.Dependency]
        public IContactsApi ContactsApi { get; set; }

        [Unity.Dependency]
        public IOpportunitiesApi OpportunitiesApi { get; set; }

        #endregion API

        public event EventHandler<int> PositionChanged;

        private List<DataTemplate> template;

        public List<DataTemplate> Template
        {
            get { return template; }
            set { template = value; RaisePropertyChanged("Template"); }
        }

        private int position;

        public int Position
        {
            get { return position; }
            set { position = value; RaisePropertyChanged("Position"); }
        }

        public bool IsPerson
        {
            get
            {
                return Contact.ContactType.Key.Equals(ContactType.Personal) ? true : false;
            }
        }

        public bool IsCorperate { get => !IsPerson; }

        public bool IsDisplayContentHeader { get; set; }
        public bool IsDisplayContent { get; set; }
        public Command<object> MyCommand { protected set; get; }

        #region Contract General View

        public Guid ContactId { get; set; }
        public DelegateCommand<object> OnEditInfoCommand { get; set; }

        private ContactDTO contact;

        public ContactDTO Contact
        {
            get => contact;
            set { SetProperty(ref contact, value); }
        }

        private List<OpportunityListDTO> opportunityLists;

        public List<OpportunityListDTO> OpportunitiesList
        {
            get => opportunityLists;
            set { SetProperty(ref opportunityLists, value); }
        }

        public string EmailDisplay { get; set; }
        public string MobileTelNo { get; set; }
        public string HomeTelNo { get; set; }
        public string OfficeTelNo { get; set; }
        public string ForiegnTelNo { get; set; }

        public async Task ContactGeneralViewInit(Guid id)
        {
            var result = await Run(() => ContactsApi.GetContact(id));
            if (result == null)
                return;
            Contact = result;

            RaisePropertyChanged(nameof(IsPerson));
            RaisePropertyChanged(nameof(IsCorperate));
            EmailDisplay = string.Empty;
            foreach (var item in Contact.ContactEmails.OrderBy(t => t.IsMain))
            {
                EmailDisplay += item.Email;
                if (item.IsMain.HasValue && item.IsMain.Value)
                    EmailDisplay += "(อิเมล์หลัก)";

                if (Contact.ContactEmails.Count() == 1)
                    continue;
                EmailDisplay += ", ";
            }
            RaisePropertyChanged(nameof(EmailDisplay));

            MobileTelNo = HomeTelNo = OfficeTelNo = ForiegnTelNo = string.Empty;
            foreach (var item in Contact.ContactPhones.OrderBy(t => t.IsMain))
            {
                if (item.PhoneType.Key.Equals(PhoneType.MobileTelNo))
                {
                    if (!string.IsNullOrEmpty(MobileTelNo))
                        MobileTelNo += ", ";

                    MobileTelNo += item.PhoneNumber;
                    if (item.IsMain.HasValue && item.IsMain.Value)
                        MobileTelNo += "(เบอร์หลัก)";
                }
                else if (item.PhoneType.Key.Equals(PhoneType.HomeTelNo))
                {
                    if (!string.IsNullOrEmpty(HomeTelNo))
                        HomeTelNo += ", ";

                    HomeTelNo += item.PhoneNumber;
                    if (item.IsMain.HasValue && item.IsMain.Value)
                        HomeTelNo += "(เบอร์หลัก)";
                }
                else if (item.PhoneType.Key.Equals(PhoneType.OfficeTelNo))
                {
                    if (!string.IsNullOrEmpty(OfficeTelNo))
                        OfficeTelNo += ", ";
                    OfficeTelNo += item.PhoneNumber;
                    if (!string.IsNullOrEmpty(item.PhoneNumberExt))
                        OfficeTelNo += " ต่อ " + item.PhoneNumberExt;

                    if (item.IsMain.HasValue && item.IsMain.Value)
                        OfficeTelNo += "(เบอร์หลัก)";
                }
                else if (item.PhoneType.Key.Equals(PhoneType.ForiegnTelNo))
                {
                    if (!string.IsNullOrEmpty(ForiegnTelNo))
                        ForiegnTelNo += ", ";
                    ForiegnTelNo += item.PhoneNumber;
                    if (item.IsMain.HasValue && item.IsMain.Value)
                        ForiegnTelNo += "(เบอร์หลัก)";
                }
            }
            RaisePropertyChanged(nameof(MobileTelNo));
            RaisePropertyChanged(nameof(ForiegnTelNo));
            RaisePropertyChanged(nameof(OfficeTelNo));
            RaisePropertyChanged(nameof(HomeTelNo));
            RaisePropertyChanged(nameof(Contact));
        }

        #endregion Contract General View

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                try
                {
                    IsBusy = true;
                    await ContactGeneralViewInit(ContactId);
                    await GetContactAddressies();
                    await GetOpportunities(contactID: ContactId);
                }
                catch (Exception e) { HandleException(e); }
                finally { IsBusy = false; }
                return;
            }

            if (parameters.TryGetValue(KnownNavigationParameters.XamlParam, out Guid? contractId))
            {
                // do something with fooObject
                ContactId = contractId.Value;
                IsBusy = true;
                try
                {
                    IsBusy = true;
                    await ContactGeneralViewInit(contractId.Value);
                    await GetContactAddressies();
                    await GetOpportunities(contactID: ContactId);
                }
                catch (Exception e) { HandleException(e); }
                finally { IsBusy = false; }
            }

            if (parameters.TryGetValue("ContactID", out Guid? contactID))
            {
                ContactId = contactID.Value;
                IsBusy = true;
                try
                {
                    IsBusy = true;
                    await ContactGeneralViewInit(ContactId);
                    await GetContactAddressies();
                    await GetOpportunities(contactID: ContactId);
                }
                catch (Exception e) { HandleException(e); }
                finally { IsBusy = false; }
            }

            IsDisplayContentHeader = true;
            IsDisplayContent = true;
            RaisePropertyChanged(nameof(IsDisplayContentHeader));
            RaisePropertyChanged(nameof(IsDisplayContent));
            await Task.Run(() =>
            {
                Template = new List<DataTemplate>()
                        {
                            new DataTemplate(()=> new ContactTimelineView() { BindingContext = this}),
                            new DataTemplate(()=> new ContactGeneralView() { BindingContext = this}),
                            new DataTemplate(()=> new ContactAddressView() { BindingContext = this}),
                            new DataTemplate(()=> new ContactOpportunityView() { BindingContext = this}),
                        };
            });

            if (parameters.TryGetValue("TabIndex", out int tabIndex))
            {
                Position = tabIndex;
                PositionChanged?.Invoke(this, tabIndex);
                if (MyCommand.CanExecute(tabIndex))
                {
                    MyCommand.Execute(tabIndex);
                }
            }
        }

        #region ContactAddress

        public DelegateCommand AddContactAddressCommand { get; set; }
        public DelegateCommand AddContactAddressHomeCommand { get; set; }
        public DelegateCommand AddContactAddressWorkCommand { get; set; }
        public DelegateCommand AddContactAddressCitiyzenCommand { get; set; }
        public DelegateCommand<object> EditContactAddressCommand { get; set; }
        public DelegateCommand<object> RemoveContactAddressCommand { get; set; }
        public ObservableCollection<ContactAddressDTO> ContactAddressies { get; set; }
        public bool CanCreateContactHome { get { return Address.HomeAddress == null; } }
        public bool CanCreateContactCitizen { get { return Address.CitizenAddress == null; } }
        public bool CanCreateContactWork { get { return Address.WorkAddress == null; } }
        private AddressDTO address;

        public AddressDTO Address
        {
            get => address;
            set { SetProperty(ref address, value); }
        }

        #endregion ContactAddress

        public async Task GetContactAddressies()
        {
            var result = await Run(() => ContactsApi.GetContactAddressList(ContactId));
            if (result != null)
                Address = result;

            RaisePropertyChanged(nameof(CanCreateContactWork));
            RaisePropertyChanged(nameof(CanCreateContactHome));
            RaisePropertyChanged(nameof(CanCreateContactCitizen));
        }

        public async Task RemoveContactAddress(Guid? contactAddressId)
        {
            await ConfirmPopup("แจ้งเตือน", "คุณต้องการลบที่อยู่หรือไม่ ", "ตกลง", "ยกเลิก", async () =>
            {
                try
                {
                    IsBusy = true;
                    await RunWithoutReturn(() => ContactsApi.DeleteContactAddress(contact.Id, contactAddressId));
                    IsBusy = false;

                    await DisplaySuccessPopup();
                    await GetContactAddressies();
                }
                catch (ApiException e) { HandleException(e); }
                finally { IsBusy = false; }
            });
        }

        private async Task GetOpportunities(Guid? projectID = null, DateTime? arriveDateFrom = null, DateTime? arriveDateTo = null,
            Guid? contactID = null, string contactNo = null, string fullName = null,
            string phoneNumber = null, string salesOpportunityKey = null,
            Guid? ownerID = null, string statusQuestionaireKey = null,
            DateTime? updatedDateFrom = null, DateTime? updatedDateTo = null,
            string excludeIDs = null, int? page = 1, int? pageSize = 10,
            string sortBy = null, bool? ascending = null)
        {
            OpportunitiesList = await Run(() => OpportunitiesApi.GetOpportunityList(projectID, arriveDateFrom, arriveDateTo, contactID,
                contactNo, fullName, phoneNumber, salesOpportunityKey, ownerID, statusQuestionaireKey, updatedDateFrom, updatedDateTo, excludeIDs, page, pageSize, sortBy, ascending));
        }

        public void initCommand()
        {
            MyCommand = new Command<object>(value =>
            {
                Position = Convert.ToInt32(value);
            });

            OnEditInfoCommand = new DelegateCommand<object>(async (Id) =>
            {
                var _id = Id as Guid?;
                NavigationParameters param = new NavigationParameters();
                param.Add("ContactId", _id.Value);
                await Navigate("ContactForm", param);
            });

            AddContactAddressCommand = new DelegateCommand(async () =>
            {
                NavigationParameters param = new NavigationParameters();
                param.Add("ContactId", Contact.Id);
                param.Add("ContactAddress", Address.ContactAddress);
                param.Add("ContactHomeAddress", Address.HomeAddress);
                param.Add("ContactCitizenAddress", Address.CitizenAddress);
                param.Add("ContactTypeKey", ContactAddressType.ContactAddress);
                await Navigate("ContactAddressFrom", param);
            });

            AddContactAddressCitiyzenCommand = new DelegateCommand(async () =>
            {
                NavigationParameters param = new NavigationParameters();
                param.Add("ContactId", Contact.Id);
                param.Add("ContactTypeKey", ContactAddressType.CitizenAddress);
                await Navigate("ContactAddressFrom", param);
            });

            AddContactAddressHomeCommand = new DelegateCommand(async () =>
            {
                NavigationParameters param = new NavigationParameters();
                param.Add("ContactId", Contact.Id);
                param.Add("ContactTypeKey", ContactAddressType.Home);
                await Navigate("ContactAddressFrom", param);
            });

            AddContactAddressWorkCommand = new DelegateCommand(async () =>
            {
                NavigationParameters param = new NavigationParameters();
                param.Add("ContactId", Contact.Id);
                param.Add("ContactTypeKey", ContactAddressType.ContactAddress);
                await Navigate("ContactAddressFrom", param);
            });

            EditContactAddressCommand = new DelegateCommand<object>(async (obj) =>
            {
                var _contact = obj as ContactAddressDTO;
                NavigationParameters param = new NavigationParameters();
                if (_contact.ContactAddressType.Key.Equals(ContactAddressType.ContactAddress))
                {
                    param.Add("ContactTypeKey", ContactAddressType.ContactAddress);
                    param.Add("ContactAddress", Address.ContactAddress);
                    param.Add("ContactHomeAddress", Address.HomeAddress);
                    param.Add("ContactCitizenAddress", Address.CitizenAddress);
                }
                else if (_contact.ContactAddressType.Key.Equals(ContactAddressType.Home))
                {
                    param.Add("ContactTypeKey", ContactAddressType.Home);
                }
                else if (_contact.ContactAddressType.Key.Equals(ContactAddressType.CitizenAddress))
                {
                    param.Add("ContactTypeKey", ContactAddressType.CitizenAddress);
                    param.Add("ContactAddress", Address.ContactAddress);
                    param.Add("ContactHomeAddress", Address.HomeAddress);
                }
                else if (_contact.ContactAddressType.Key.Equals(ContactAddressType.OfficeWarking))
                {
                    param.Add("ContactTypeKey", ContactAddressType.OfficeWarking);
                }

                param.Add("ContactId", Contact.Id);
                param.Add("AddressId", _contact.Id);
                await Navigate("ContactAddressFrom", param);
            });

            RemoveContactAddressCommand = new DelegateCommand<object>(async (obj) =>
            {
                var addressId = obj as Guid?;
                await RemoveContactAddress(addressId);
            });

            CreateOpportunityCommand = new DelegateCommand(CreateOpportunuty);
            EditOpportunityCommand = new DelegateCommand<object>(EditOpportunity);
        }

        #region Opportunity

        public DelegateCommand CreateOpportunityCommand { get; set; }
        public DelegateCommand<object> EditOpportunityCommand { get; set; }

        public async void CreateOpportunuty()
        {
            NavigationParameters param = new NavigationParameters();
            param.Add("ContactId", Contact.Id);
            await Navigate("OpportunityForm", param);
        }

        public async void EditOpportunity(object obj)
        {
            if (obj == null) return;

            var opport = obj as OpportunityListDTO;
            NavigationParameters param = new NavigationParameters();
            param.Add("OpportunityId", opport.Id);
            await Navigate("OpportunityDetail", param);
        }

        #endregion Opportunity
    }
}