using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Unity;

namespace CRMMobile.ViewModels
{
    public class ContactViewModel : ViewModelBase
    {
        public ContactViewModel(INavigationService navigationService, IUnityContainer container)
            : base(navigationService)
        {
            //ContactsApi = container.Resolve<IContactsApi>();
            Init();
        }

        #region Commands

        public DelegateCommand DisplaySearchcommand { get; set; }
        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand ClearCommand { get; private set; }
        public DelegateCommand LoadMoreCommand { get; private set; }
        public DelegateCommand<object> NavigateToOpportunityTapCommand { get; private set; }
        public DelegateCommand<object> CreateOpportunityCommand { get; set; }
        #endregion Commands

        #region

        [Unity.Dependency]
        public IContactsApi ContactsApi { get; set; }

        #endregion

        //public event EventHandler<bool> CloseFilterEvent;

        #region Property
        public bool canDisplayList;

        private bool isDisplaySearch;

        public bool IsDisplaySearch
        {
            get => isDisplaySearch;
            set
            {
                SetProperty(ref isDisplaySearch, value);
                RaisePropertyChanged(nameof(IsNotDisplaySearch));
            }
        }

        public bool IsNotDisplaySearch { get => !IsDisplaySearch && canDisplayList; }

        public ObservableCollection<ContactListDTO> contacts;

        public ObservableCollection<ContactListDTO> Contacts {
            get => contacts;
            set { SetProperty(ref contacts, value); }
        }

        private bool displayEmptyView;

        public bool DisplayEmptyView
        {
            get => displayEmptyView;
            set { SetProperty(ref displayEmptyView, value); }
        }

        //Filter Property
        private string contactNo;

        public string ContactNo
        {
            get => contactNo;
            set { SetProperty(ref contactNo, value); }
        }

        private string firstNameTH;

        public string FirstNameTH
        {
            get => firstNameTH;
            set { SetProperty(ref firstNameTH, value); }
        }

        private string lastNameTH;

        public string LastNameTH
        {
            get => lastNameTH;
            set { SetProperty(ref lastNameTH, value); }
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get => phoneNumber;
            set { SetProperty(ref phoneNumber, value); }
        }

        #endregion

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            DisplayEmptyView = false;
            if (Contacts != null && Contacts.Count > 0) { Contacts.Clear(); }
            await GetContacts(contactNo:this.ContactNo,
                firstNameTH:this.FirstNameTH,
                lastNameTH:this.LastNameTH,
                phoneNumber:this.PhoneNumber,
                page:PageIndex,
                pageSize:PageSize);
            canDisplayList = true;
            RaisePropertyChanged(nameof(IsNotDisplaySearch));
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            canDisplayList = false;
            RaisePropertyChanged(nameof(IsNotDisplaySearch));
        }

        public async Task GetContacts(string contactNo =  null, string firstNameTH = null, string lastNameTH = null, string phoneNumber =null, DateTime? createdDateFrom =null, DateTime? createdDateTo = null, DateTime? updatedDateFrom = null, DateTime? updatedDateTo = null, string citizenIdentityNo = null, int? page = 0, int? pageSize =10, string sortBy = null , bool? ascending = null)
        {
            try
            {
                DisplayEmptyView = false;
                IsBusy = true;
                var result = await Run(() => ContactsApi.GetContactList(contactNo, firstNameTH, lastNameTH, phoneNumber, createdDateFrom, createdDateTo, updatedDateFrom, updatedDateTo, citizenIdentityNo, page, pageSize, sortBy, ascending));
                //var result = await Run(() => ContactsApi.GetContactList(null, firstNameTH, lastNameTH, phoneNumber, null, null, null, null, null, PageIndex, PageSize, null, null));

                if (result == null || result.Count == 0)
                    DisplayEmptyView = true;
                foreach (var item in result) { Contacts.Add(item); }
                IsBusy = false;
            }
            catch (ApiException e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        //public async Task Search()
        //{
        //    try
        //    {
        //        IsDisplaySearch = false;
        //        IsBusy = true;
        //        PageIndex = 1;
        //        if (Contacts.Count > 0)
        //            Contacts.Clear();

        //        var result = await Run(() => ContactsApi.GetContactList(null, firstNameTH, lastNameTH, phoneNumber, null, null, null, null, null, PageIndex, PageSize, null, null));

        //        if (Contacts.Count > 0)
        //            Contacts.Clear();
        //        foreach (var item in result) { Contacts.Add(item); }
        //    }
        //    catch (Exception e) { HandleException(e); }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        public void Init()
        {
            Contacts = new ObservableCollection<ContactListDTO>();
            DisplaySearchcommand = new DelegateCommand(() => IsDisplaySearch = !IsDisplaySearch);
            NavigateToOpportunityTapCommand = new DelegateCommand<object>(NavigateOpportunityTab);
            CreateOpportunityCommand = new DelegateCommand<object>(CreateOpportunuty);
            LoadMoreCommand = new DelegateCommand(async () => { await GetContacts(page:++PageIndex); });
            SearchCommand = new DelegateCommand(async () => {

                IsDisplaySearch = false;
                PageIndex = 1;
                if (Contacts.Count > 0)
                    Contacts.Clear();
                await GetContacts(contactNo: this.ContactNo,
                firstNameTH: this.FirstNameTH,
                lastNameTH: this.LastNameTH,
                phoneNumber: this.PhoneNumber,
                page: PageIndex,
                pageSize: PageSize);
                //await Search();

            });

            ClearCommand = new DelegateCommand(async () =>
            {
                bool canSearch = !string.IsNullOrEmpty(FirstNameTH)
                || !string.IsNullOrEmpty(LastNameTH)
                || !string.IsNullOrEmpty(PhoneNumber);

                if (!string.IsNullOrEmpty(FirstNameTH))
                    FirstNameTH = null;

                if (!string.IsNullOrEmpty(LastNameTH))
                    LastNameTH = null;

                if (!string.IsNullOrEmpty(PhoneNumber))
                    PhoneNumber = null;

            });
        }

        public async void NavigateOpportunityTab(object obj)
        {
            var contact = obj as ContactListDTO;
            NavigationParameters param = new NavigationParameters();
            param.Add("TabIndex", 3);
            param.Add("ContactID", contact.Id);
            await Navigate("ContactDetail", param);
        }

        public async void CreateOpportunuty(object obj)
        {
            var contact = obj as ContactListDTO;
            NavigationParameters param = new NavigationParameters();
            param.Add("ContactId", contact.Id);
            await Navigate("OpportunityForm", param);
        }
    }
}