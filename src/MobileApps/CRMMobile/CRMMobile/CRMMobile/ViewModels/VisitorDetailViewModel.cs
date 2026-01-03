using CRMMobile.Helper;
using CRMMobile.Views;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRMMobile.ViewModels
{
    public class VisitorDetailViewModel : ViewModelBase
    {
        public VisitorDetailViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            MyCommand = new Command<object>(value =>
            {
                Position = Convert.ToInt32(value);
            });

            UpdateStatusCommand = new DelegateCommand(async () => await UpdateContactStatus());
            ActionCommand = new DelegateCommand(async () => await Action());
            Action2Command = new DelegateCommand(async () => await Action2());
            Template = new List<DataTemplate>()
            {
                new DataTemplate(()=> new VisitorGeneralView() { BindingContext = this}),
                new DataTemplate(()=> new VisitorHistoryView() { BindingContext = this}),
            };
        }

        [Unity.Dependency]
        public IVisitorsApi VisitorsApi { get; set; }

        [Unity.Dependency]
        public IOpportunitiesApi OpportunitiesApi { get; set; }

        [Unity.Dependency]
        public IContactsApi ContactsApi { get; set; }

        [Unity.Dependency]
        public IMasterCentersApi MasterCentersApi { get; set; }

        public Guid? Id { get; set; }

        private int position;

        public int Position
        {
            get { return position; }
            set { position = value; RaisePropertyChanged("Position"); }
        }

        public Command<object> MyCommand { protected set; get; }
        public DelegateCommand UpdateStatusCommand { protected set; get; }
        public DelegateCommand ActionCommand { protected set; get; }
        public DelegateCommand Action2Command { protected set; get; }

        private List<DataTemplate> template;

        public List<DataTemplate> Template
        {
            get { return template; }
            set { template = value; RaisePropertyChanged("Template"); }
        }

        private List<MasterCenterDropdownDTO> contactStatus;

        public List<MasterCenterDropdownDTO> ContactStatus
        {
            get => contactStatus;
            set { SetProperty(ref contactStatus, value); }
        }

        private MasterCenterDropdownDTO contactStatusSelected;

        public MasterCenterDropdownDTO ContactStatusSelected
        {
            get => contactStatusSelected;
            set { SetProperty(ref contactStatusSelected, value); }
        }

        private VisitorDTO visitor;

        public VisitorDTO Visitor
        {
            get => visitor;
            set { SetProperty(ref visitor, value); }
        }

        public int VisitorHistoriesCount { get; set; }
        private List<VisitorHistoryDTO> visitorHistories;

        public List<VisitorHistoryDTO> VisitorHistories
        {
            get => visitorHistories;
            set { SetProperty(ref visitorHistories, value); }
        }

        public int VisitorPurchaseHistoriesCount { get; set; }
        private List<VisitorPurchaseHistoryDTO> visitorPurchaseHistories;

        public List<VisitorPurchaseHistoryDTO> VisitorPurchaseHistories
        {
            get => visitorPurchaseHistories;
            set { SetProperty(ref visitorPurchaseHistories, value); }
        }

        public int VisitorQuestionnaireHistoriesCount { get; set; }
        private List<VisitorQuestionnaireHistoryDTO> visitorQuestionnaireHistories;

        public List<VisitorQuestionnaireHistoryDTO> VisitorQuestionnaireHistories
        {
            get => visitorQuestionnaireHistories;
            set { SetProperty(ref visitorQuestionnaireHistories, value); }
        }

        public int LeadListCount { get; set; }

        private List<LeadListDTO> leadList;

        public List<LeadListDTO> LeadList
        {
            get => leadList;
            set { SetProperty(ref leadList, value); }
        }

        public bool ActionButtonVisible { get; set; }
        public bool ActionButton2Visible { get; set; }
        public string ActionButtonTitle { get; set; }
        public string ActionButton2Title { get; set; }

        //public string ActionButtonTitle { get; set; }
        public FontIcons ActionButton2Icon { get; set; }

        public bool IsWelCome { get; set; }
        public bool IsSaveOpportunity { get; set; }
        public bool IsContact { get; set; }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                Id = (Guid?)parameters["Id"];
            }
            try
            {
                IsBusy = true;
                await GetContactStatus();
                await GetVisitorDetail();
                await GetVisitorHistory();
            }
            catch (ApiException e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public async Task GetContactStatus()
        {
            ContactStatus = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(MasterCenterKey.ContactStatus, null));
        }

        public async Task GetVisitorDetail()
        {
            Visitor = await Run(() => VisitorsApi.GetVisitor(Id));

            if (Visitor.IsContact == true)
            {
                Visitor.FirstNameTH = Visitor.Contact.FirstNameTH;
                Visitor.LastNameTH = Visitor.Contact.LastNameTH;
            }

            RaisePropertyChanged(nameof(Visitor));
            ContactStatusSelected = Visitor.ContactStatus;
            SetActionButton(Visitor.IsContact ?? false, Visitor.IsSavedOpportunity ?? false, Visitor.IsWelcome ?? false);
        }

        public async Task GetVisitorHistory()
        {
            await RunWithoutReturn(() =>
            {
                VisitorHistories = VisitorsApi.GetVisitorHistory(Id, null, null);
                LeadList = VisitorsApi.GetVisitorAdvertisement(Id, null, null);
            });

            VisitorHistoriesCount = VisitorHistories != null ? VisitorHistories.Count : 0;
            VisitorPurchaseHistoriesCount = VisitorPurchaseHistories != null ? VisitorPurchaseHistories.Count : 0;
            VisitorQuestionnaireHistoriesCount = VisitorQuestionnaireHistories != null ? VisitorQuestionnaireHistories.Count : 0;
            LeadListCount = LeadList != null ? LeadList.Count : 0;
            RaisePropertyChanged(nameof(VisitorHistoriesCount));
            RaisePropertyChanged(nameof(VisitorPurchaseHistoriesCount));
            RaisePropertyChanged(nameof(VisitorQuestionnaireHistoriesCount));
            RaisePropertyChanged(nameof(LeadListCount));
        }

        private async Task UpdateContactStatus()
        {
            if (ContactStatusSelected == null)
            {
                await DialogService.DisplayAlertAsync("แจ้งเตือน", "กรุณาเลือกสถานะ", null);
                return;
            }

            try
            {
                IsBusy = true;
                Visitor.ContactStatus = ContactStatusSelected;
                await Run(() => VisitorsApi.EditVisitorType(Id, Visitor));
                await DisplaySuccessPopup();
                await GetVisitorDetail();
            }
            catch (Exception e) { HandleException(e); }
            finally { IsBusy = false; }
        }

        private void SetActionButton(bool isContact, bool isSaveOpportunity, bool isWelcome)
        {
            IsWelCome = isWelcome;
            IsSaveOpportunity = isSaveOpportunity;
            IsContact = isContact;

            if (IsWelCome)
            {
                ActionButtonTitle = "บันทึกต้อนรับลูกค้า";
            }
            else
            {
                ActionButtonTitle = "ยกเลิกต้อนรับลูกค้า";
            }

            RaisePropertyChanged(nameof(ActionButtonTitle));

            if (!IsContact)
            {
                ActionButton2Title = "เพิ่มข้อมูลลูกค้าได้";
                ActionButton2Visible = true;
                ActionButton2Icon = FontIcons.plussquare;
            }
            else
            {
                if (!IsSaveOpportunity)
                {
                    ActionButton2Title = "บันทึก Opportunity";
                    ActionButton2Icon = FontIcons.opportunity;
                    ActionButton2Visible = true;
                }
                else { ActionButton2Visible = false; }
            }
            RaisePropertyChanged(nameof(ActionButton2Icon));
            RaisePropertyChanged(nameof(ActionButtonTitle));
            RaisePropertyChanged(nameof(ActionButton2Title));
            RaisePropertyChanged(nameof(ActionButton2Visible));
        }

        private async Task CreateOpportunity()
        {
            try
            {
                IsBusy = true;
                var opport = new OpportunityDTO()
                {
                    ProductQTY = 0,
                };
                await Run(() => OpportunitiesApi.CreateOpportunity(opport, Visitor.Id.Value));
                await GetVisitorDetail();
                await DisplaySuccessPopup();
            }
            catch (ApiException e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public async Task CreateContact()
        {
            NavigationParameters param = new NavigationParameters();
            param.Add("VisitorId", Id);
            await Navigate("ContactForm", param);
        }

        public async Task EditContact()
        {
            NavigationParameters param = new NavigationParameters();
            param.Add("VisitorId", Id);
            param.Add("ContactId", Visitor.Contact.Id);
            await Navigate("ContactForm", param);
        }

        private async Task Action()
        {
            try
            {
                IsBusy = true;
                await Run(() => VisitorsApi.SubmitVisitorWelcome(Id, new VisitorWelcomeInput() { IsWelcome = !this.IsWelCome }));
                await GetVisitorDetail();
                await DisplaySuccessPopup();
            }
            catch (ApiException e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        private async Task Action2()
        {
            if (!IsContact)
            {
                await CreateContact();
            }
            else
            {
                if (!IsSaveOpportunity)
                {
                    await CreateOpportunity();
                }
            }
        }
    }
}