using CRMMobile.Helper;
using CRMMobile.Validations;
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
    public class OpportunityDetailViewModel : ViewModelBase
    {
        public OpportunityDetailViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            InitValidation();

            MyCommand = new Command<object>(value => Position = Convert.ToInt32(value));
            EditCommand = new DelegateCommand(Submit);

            EditActivityWalkCommand = new DelegateCommand<OpportunityActivityListDTO>(EditActivityWalk);
            RemoveActivityWalkCommand = new DelegateCommand<OpportunityActivityListDTO>(RemoveActivityWalk);
            CreateActivityWalkCommand = new DelegateCommand(CreateActivityWalk);

            CreateActivityRevisitCommand = new DelegateCommand(CreateActivityRevisit);
            EditActivityRevisitCommand = new DelegateCommand<RevisitActivityListDTO>(EditActivityRevisit);
            RemoveActivityRevisitCommand = new DelegateCommand<RevisitActivityListDTO>(RemoveActivityRevisit);

            ActivityListDTOsComplete = new ObservableCollection<OpportunityActivityListDTO>();
            ActivityListDTOs = new ObservableCollection<OpportunityActivityListDTO>();
            RevisitActivityList = new ObservableCollection<RevisitActivityListDTO>();
        }

        [Unity.Dependency]
        public IOpportunitiesApi OpportunitiesApi { get; set; }

        [Unity.Dependency]
        public IMasterCentersApi MasterCentersApi { get; set; }

        public Command<object> MyCommand { protected set; get; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand CreateActivityWalkCommand { get; set; }
        public DelegateCommand<OpportunityActivityListDTO> EditActivityWalkCommand { get; set; }
        public DelegateCommand<OpportunityActivityListDTO> RemoveActivityWalkCommand { get; set; }
        public DelegateCommand CreateActivityRevisitCommand { get; set; }
        public DelegateCommand<RevisitActivityListDTO> EditActivityRevisitCommand { get; set; }
        public DelegateCommand<RevisitActivityListDTO> RemoveActivityRevisitCommand { get; set; }

        public string DisplayFullName { get => OpportunityDTO?.Contact?.FirstNameTH + " " + OpportunityDTO?.Contact?.LastNameTH; }
        public string DisplayContact { get => string.IsNullOrEmpty(OpportunityDTO?.Contact?.ContactNo) ? " " : "(Contact ID : " + OpportunityDTO?.Contact.ContactNo + ")"; }
        public bool DisplayUpcomming { get; set; }
        public bool DisplayCompleted { get; set; }
        public bool Displayline { get => DisplayCompleted && DisplayUpcomming; }
        public Guid? Id { get; set; }

        private int position;

        public int Position
        {
            get { return position; }
            set { position = value; RaisePropertyChanged("Position"); }
        }

        private List<DataTemplate> template;

        public List<DataTemplate> Template
        {
            get { return template; }
            set { template = value; RaisePropertyChanged("Template"); }
        }

        public ObservableCollection<OpportunityActivityListDTO> ActivityListDTOs { get; set; }

        public ObservableCollection<OpportunityActivityListDTO> ActivityListDTOsComplete { get; set; }

        public ObservableCollection<RevisitActivityListDTO> RevisitActivityList { get; set; }

        private List<MasterCenterDropdownDTO> estimateSalesOpportunitys;

        public List<MasterCenterDropdownDTO> EstimateSalesOpportunitys
        {
            get => estimateSalesOpportunitys;
            set { SetProperty(ref estimateSalesOpportunitys, value); }
        }

        private List<MasterCenterDropdownDTO> salesOpportunitys;

        public List<MasterCenterDropdownDTO> SalesOpportunitys
        {
            get => salesOpportunitys;
            set { SetProperty(ref salesOpportunitys, value); }
        }

        private ValidationObject<MasterCenterDropdownDTO> estimateSalesOpportunity;

        public ValidationObject<MasterCenterDropdownDTO> EstimateSalesOpportunity
        {
            get => estimateSalesOpportunity;
            set { SetProperty(ref estimateSalesOpportunity, value); }
        }

        private ValidationObject<MasterCenterDropdownDTO> salesOpportunity;

        public ValidationObject<MasterCenterDropdownDTO> SalesOpportunity
        {
            get => salesOpportunity;
            set { SetProperty(ref salesOpportunity, value); }
        }

        private ValidationObject<DateTime?> arrivedDate;

        public ValidationObject<DateTime?> ArriveDate
        {
            get => arrivedDate;
            set { SetProperty(ref arrivedDate, value); }
        }

        private ValidationObject<string> interestedProduct1;

        public ValidationObject<string> InterestedProduct1
        {
            get => interestedProduct1;
            set { SetProperty(ref interestedProduct1, value); }
        }

        private ValidationObject<string> interestedProduct2;

        public ValidationObject<string> InterestedProduct2
        {
            get => interestedProduct2;
            set { SetProperty(ref interestedProduct2, value); }
        }

        private ValidationObject<string> interestedProduct3;

        public ValidationObject<string> InterestedProduct3
        {
            get => interestedProduct3;
            set { SetProperty(ref interestedProduct3, value); }
        }

        private OpportunityDTO opportunityDTO;

        public OpportunityDTO OpportunityDTO
        {
            get => opportunityDTO;
            set { SetProperty(ref opportunityDTO, value); }
        }

        private void InitValidation()
        {
            EstimateSalesOpportunity = new ValidationObject<MasterCenterDropdownDTO>();
            SalesOpportunity = new ValidationObject<MasterCenterDropdownDTO>();
            ArriveDate = new ValidationObject<DateTime?>();
            InterestedProduct1 = new ValidationObject<string>();
            InterestedProduct2 = new ValidationObject<string>();
            interestedProduct3 = new ValidationObject<string>();
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                await GetMasterData();
                await GetActivityWalk();
                await GetActivityRevisit();
            }
        }

        public async override void OnNavigatedModeNew(INavigationParameters parameters)
        {
            base.OnNavigatedModeNew(parameters);
            Id = parameters["OpportunityId"] as Guid?;
            await GetMasterData();
            await GetOpportunityDetail(Id);
            await GetActivityWalk();
            await GetActivityRevisit();
            Template = new List<DataTemplate>()
            {
                new DataTemplate(()=> new OpportunityContactView() { BindingContext = this}),
                new DataTemplate(()=> new OpportunityActivityView(this)),
                new DataTemplate(()=> new OpportunityRevisitView() { BindingContext = this}),
            };
        }

        public async Task GetOpportunityDetail(Guid? id)
        {
            try
            {
                IsBusy = true;
                OpportunityDTO = await Run(() => OpportunitiesApi.GetOpportunity(id));
                SetUpOpportunity(OpportunityDTO);
            }
            catch (ApiException e) { HandleException(e); }
            finally { IsBusy = false; }
        }

        public async Task GetMasterData()
        {
            try
            {
                IsBusy = true;
                EstimateSalesOpportunitys = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(OpportunityKey.EstimateSalesOpportunity, null));
                SalesOpportunitys = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(OpportunityKey.SalesOpportunity, null));
                EstimateSalesOpportunity.Value = EstimateSalesOpportunitys.FirstOrDefault(t => t.Key.Equals("2"));
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public void SetUpOpportunity(OpportunityDTO opportunityDTO)
        {
            ArriveDate.Value = opportunityDTO.ArriveDate;
            EstimateSalesOpportunity.Value = opportunityDTO.EstimateSalesOpportunity;
            SalesOpportunity.Value = opportunityDTO.SalesOpportunity;
            InterestedProduct1.Value = opportunityDTO.InterestedProduct1;
            InterestedProduct2.Value = opportunityDTO.InterestedProduct2;
            InterestedProduct3.Value = opportunityDTO.InterestedProduct3;
            RaisePropertyChanged(nameof(DisplayFullName));
            RaisePropertyChanged(nameof(DisplayContact));
        }

        public async void Submit()
        {
            await UpdateGenearaInfo();
        }

        public void CreateRequest()
        {
            OpportunityDTO.InterestedProduct1 = InterestedProduct1.Value;
            OpportunityDTO.InterestedProduct2 = InterestedProduct2.Value;
            OpportunityDTO.InterestedProduct3 = InterestedProduct3.Value;
            OpportunityDTO.ArriveDate = ArriveDate.Value;
            OpportunityDTO.EstimateSalesOpportunity = EstimateSalesOpportunity.Value;
            OpportunityDTO.SalesOpportunity = SalesOpportunity.Value;
        }

        public async Task UpdateGenearaInfo()
        {
            try
            {
                IsBusy = true;
                CreateRequest();
                OpportunityDTO = await Run(() => OpportunitiesApi.EditOpportunity(OpportunityDTO.Id, OpportunityDTO));
                await DisplaySuccessPopup();
                await Task.Delay(500);
                SetUpOpportunity(OpportunityDTO);
                IsBusy = false;
            }
            catch (ApiException e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public async Task GetActivityWalk()
        {
            var leadActivityTemp = await Run(() => OpportunitiesApi.GetOpportunityActivityList(OpportunityDTO.Id));

            if (ActivityListDTOsComplete != null && ActivityListDTOsComplete.Count > 0)
                ActivityListDTOsComplete.Clear();

            if (ActivityListDTOs != null && ActivityListDTOs.Count > 0)
                ActivityListDTOs.Clear();
            foreach (var item in leadActivityTemp)
            {
                if (item.IsCompleted.Value)
                    ActivityListDTOsComplete.Add(item);
                else
                    ActivityListDTOs.Add(item);
            }

            UpdateActivityListDisplay();
        }

        private void UpdateActivityListDisplay()
        {
            DisplayCompleted = ActivityListDTOsComplete.Count == 0 ? false : true;
            DisplayUpcomming = ActivityListDTOs.Count == 0 ? false : true;
            RaisePropertyChanged(nameof(DisplayUpcomming));
            RaisePropertyChanged(nameof(DisplayCompleted));
            RaisePropertyChanged(nameof(Displayline));
        }

        private async void CreateActivityWalk()
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("OpportunityId", OpportunityDTO.Id);
            await Navigate("OpportunityActivityForm", parameters);
        }

        private async void EditActivityWalk(OpportunityActivityListDTO opportunityActivity)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("OpportunityId", OpportunityDTO.Id);
            parameters.Add("ActivityId", opportunityActivity.Id);
            parameters.Add("IsCompleted", opportunityActivity.IsCompleted);
            await Navigate("OpportunityActivityForm", parameters);
        }

        private async void RemoveActivityWalk(OpportunityActivityListDTO opportunityActivity)
        {
            try
            {
                IsBusy = true;
                await ConfirmPopup("แจ้งเตือน", "คุณต้องการลบข้อมูล Activity หรือไม่", "ตกลง", "ยกเลิก", async () =>
                {
                    await RunWithoutReturn(() => OpportunitiesApi.DeleteOpportunityActivity(OpportunityDTO.Id, opportunityActivity.Id));
                    await DisplaySuccessPopup();
                    await Task.Delay(500);
                    await DialogService.CloseCurrentPopup();
                    await GetActivityWalk();
                });

                IsBusy = false;
            }
            catch (ApiException e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public async Task GetActivityRevisit()
        {
            var revisitActivityList = await Run(() => OpportunitiesApi.GetRevisitList(OpportunityDTO.Id));
            if (RevisitActivityList != null && RevisitActivityList.Count > 0)
                RevisitActivityList.Clear();

            foreach (var item in revisitActivityList)
            {
                RevisitActivityList.Add(item);
            }
        }

        private async void CreateActivityRevisit()
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("OpportunityId", OpportunityDTO.Id);
            await Navigate("OpportunityRevisitForm", parameters);
        }

        private async void EditActivityRevisit(RevisitActivityListDTO revisitActivity)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("OpportunityId", OpportunityDTO.Id);
            parameters.Add("ActivityId", revisitActivity.Id);
            parameters.Add("IsCompleted", revisitActivity.IsCompleted);
            await Navigate("OpportunityRevisitForm", parameters);
        }

        private async void RemoveActivityRevisit(RevisitActivityListDTO revisitActivity)
        {
            try
            {
                IsBusy = true;
                await ConfirmPopup("แจ้งเตือน", "คุณต้องการลบข้อมูล Revisit หรือไม่", "ตกลง", "ยกเลิก", async () =>
                {
                    await RunWithoutReturn(() => OpportunitiesApi.DeleteRevisitActivity(OpportunityDTO.Id, revisitActivity.Id));
                    await DisplaySuccessPopup();
                    await Task.Delay(500);
                    await DialogService.CloseCurrentPopup();
                    await GetActivityRevisit();
                });

                IsBusy = false;
            }
            catch (ApiException e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }
    }
}