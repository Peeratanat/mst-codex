using CRMMobile.Models;
using CRMMobile.Views.Popup;
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
using Unity;

namespace CRMMobile.ViewModels
{
    public class LeadViewModel : ViewModelBase
    {
        public LeadViewModel(INavigationService navigationService, IUnityContainer container)
            : base(navigationService)
        {
            Leads = new ObservableCollection<LeadListDTO>();
            LeadTypes = new ObservableCollection<MasterCenterDropdownDTO>();
            Projects = new List<ProjectDTO>();
            Onwers = new ObservableCollection<UserListDTO>();

            LeadQualiflyCommand = new DelegateCommand<object>(async (value) => await Qualifly(value));
            LeadDisQualifyCommand = new DelegateCommand<object>((obj) => DisQualifly(obj));
            RemoveLeadCommand = new DelegateCommand<object>((obj) => RemoveLead(obj));
            FilterProjectCommand = new DelegateCommand<string>(async (obj) => await GetProjects(obj));
            FilterOwnerCommand = new DelegateCommand<string>(async (obj) => await GetOwner(obj));

            SearchCommand = new DelegateCommand(async () =>
            {
                PageIndex = 1;
                Leads.Clear();
                await Search();
                ClosedPopup?.Invoke(this, EventArgs.Empty);
                IsDisplaySearch = false;
            });

            ClearCommand = new DelegateCommand(() =>
            {
                FirstName = string.Empty;
                LastName = string.Empty;
                TelNo = string.Empty;
                LeadType = null;
                Project = null;
                Onwer = null;
            });

            LoadMoreCommand = new DelegateCommand(async () =>
            {
                PageIndex += 1;
                await Search();
            });
            
        }

        #region

        [Unity.Dependency]
        public ILeadsApi LeadsApi { get; set; }

        [Unity.Dependency]
        public IMasterCentersApi MasterCentersApi { get; set; }

        [Unity.Dependency]
        public IProjectsApi ProjectsApi { get; set; }

        [Unity.Dependency]
        public IUsersApi UsersApi { get; set; }

        #endregion

        public event EventHandler<EventArgs> ClosedPopup;

        #region Commands

        public DelegateCommand SearchCommand { get; private set; }
        public DelegateCommand ClearCommand { get; private set; }
        public DelegateCommand LoadMoreCommand { get; private set; }
        public DelegateCommand<object> LeadQualiflyCommand { get; private set; }
        public DelegateCommand<object> LeadDisQualifyCommand { get; private set; }
        public DelegateCommand<object> RemoveLeadCommand { get; private set; }
        public DelegateCommand<string> FilterProjectCommand { get; private set; }
        public DelegateCommand<string> FilterOwnerCommand { get; private set; }
        #endregion

        #region property

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

        private bool isLoadMore;

        public bool IsLoadMore
        {
            get => isLoadMore;
            set { SetProperty(ref isLoadMore, value); }
        }

        public bool IsNotDisplaySearch { get => !IsDisplaySearch && canDisplayList; }

        private ObservableCollection<LeadListDTO> leads;

        public ObservableCollection<LeadListDTO> Leads
        {
            get => leads;
            set { SetProperty(ref leads, value); }
        }

        public ObservableCollection<MasterCenterDropdownDTO> LeadTypes { get; private set; }

        private ObservableCollection<UserListDTO> onwers;

        public ObservableCollection<UserListDTO> Onwers
        {
            get => onwers;
            set { SetProperty(ref onwers, value); }
        }

        private List<ProjectDTO> projects;

        public List<ProjectDTO> Projects
        {
            get => projects;
            set { SetProperty(ref projects, value); }
        }

        private string firstName;

        public string FirstName
        {
            get => firstName;
            set { SetProperty(ref firstName, value); }
        }

        private string lastName;

        public string LastName
        {
            get => lastName;
            set { SetProperty(ref lastName, value); }
        }

        private string telNo;

        public string TelNo
        {
            get => telNo;
            set { SetProperty(ref telNo, value); }
        }

        private MasterCenterDropdownDTO leadType;

        public MasterCenterDropdownDTO LeadType
        {
            get => leadType;
            set { SetProperty(ref leadType, value); }
        }

        private ProjectDTO project;

        public ProjectDTO Project
        {
            get => project;
            set { SetProperty(ref project, value); }
        }

        private UserListDTO onwer;

        public UserListDTO Onwer
        {
            get => onwer;
            set { SetProperty(ref onwer, value); }
        }

        private bool displayEmptyView;

        public bool DisplayEmptyView
        {
            get => displayEmptyView;
            set { SetProperty(ref displayEmptyView, value); }
        }

        #endregion

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            DisplayEmptyView = false;
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                await Init();
                await GetLeadList();
            }

            if (parameters.TryGetValue("Refresh", out bool value))
            {
                if (value)
                {
                    Leads.Clear();
                    await GetLeadList();
                }
            }

            if (parameters.TryGetValue("RemoveLeadGuid", out Guid leadId))
            {
                var item = Leads.FirstOrDefault(t => t.Id == leadId);
                Leads.Remove(item);
            }

            if(parameters.TryGetValue("UpdateLeadId",out Guid updateLeadId))
            {
                await GetLead(updateLeadId);
            }

            canDisplayList = true;
            RaisePropertyChanged(nameof(IsNotDisplaySearch));
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            canDisplayList = false;
            RaisePropertyChanged(nameof(IsNotDisplaySearch));
        }

        public async Task Init()
        {
            try
            {
                IsBusy = true;
                await GetProjects(null);
                await GetOwner();
                await GetLeadType();
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public async Task Search()
        {

            var leadTypeKey = LeadType?.Key;
            var projectId = Project?.Id;
            var leadStatus = Enum.GetName(typeof(LeadStatus), LeadStatus.InProgress);

            IsBusy = true;
            var result = await Run(() => LeadsApi.GetLeadList(firstName, lastName, telNo, leadTypeKey,
                    Onwer?.Id, null, projectId, null, null, null, PageIndex, PageSize, "CreatedDate", false));

            IsBusy = false;
            // IsDisplaySearch = false;

            if (result == null || result.Count == 0)
            {
                DisplayEmptyView = true;
                return;
            }

            foreach (var item in result) { Leads.Add(item); }
        }

        public async Task Qualifly(object obj)
        {
            var leadId = (Guid?)obj;

            IsBusy = true;
            var qualifly = await Run(() => LeadsApi.GetLeadQualify(leadId));
            IsBusy = false;

            if (qualifly == null || qualifly.Count == 0)
            {
                await ConfirmPopup("แจ้งเตือน", $"คุณต้อง Qualifly หรือไม่", "ตกลง", "ยกเลิก", async () =>
                {
                    try
                    {
                        IsBusy = true;
                        await Run(() => LeadsApi.SubmitQualify(leadId, null));
                        IsBusy = false;
                        var item = Leads.FirstOrDefault(t => t.Id == leadId);
                        Leads.Remove(item);
                        await DisplaySuccessPopup();
                    }
                    catch (ApiException e) { HandleException(e); }
                    finally { IsBusy = false; }
                });
            }
            else
            {
                NavigationParameters param = new NavigationParameters();
                param.Add("LeadQualifyDTO", qualifly);
                param.Add("LeadId", leadId);
                await Navigate("QualifyContactPopup", param);
            }
        }

        public async void DisQualifly(object obj)
        {
            try
            {
                var lead = (LeadListDTO)obj;
                if (!await CheckConnection())
                    return;

                var popup = new DisQualifyPopup(lead.FirstName + " " + lead.LastName, lead.PhoneNumber);
                await DialogService.DisplayPopup(popup);
                popup.OnConfirm += async (s, e) =>
                {
                    if (!e)
                        return;

                    await Run(() => LeadsApi.UnSubmitQualify(lead.Id));
                    await DisplaySuccessPopup();
                    await DialogService.CloseCurrentPopup();
                    Leads.Remove(lead);
                };
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public async Task GetLeadList(string firstName = null, string lastName = null, string phoneNumber = null, string leadTypeKey = null, Guid? ownerID = null, string leadStatusKey = null, Guid? projectID = null, DateTime? createdDateFrom = null, DateTime? createdDateTo = null, string excludeIDs = null, int? page = null, int? pageSize = null, string sortBy = null, bool? ascending = null)
        {
            IsBusy = true;
            var result = await Run(() => LeadsApi.GetLeadList(firstName, lastName, phoneNumber, leadTypeKey, ownerID, leadStatusKey, projectID, createdDateFrom, createdDateTo, excludeIDs, PageIndex, PageSize, "CreatedDate", false));
            IsBusy = false;
            if (result == null)
            {
                DisplayEmptyView = true;
                return;
            }

            foreach (var item in result) { Leads.Add(item); }
        }

        public async Task GetLead(Guid id)
        {
            
            var result = await Run(() => LeadsApi.GetLead(id));
            var lead = Leads.FirstOrDefault(t => t.Id == id);
            var index = Leads.IndexOf(lead);

            if (result == null)
                return;

            //lead.FirstName = "TEST JAA";
            Leads[index] = new LeadListDTO()
            {
                Advertisement = result.Advertisement,
                CreatedDate = result.CreatedDate,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Id = result.Id,
                LeadType = result.LeadType,
                Owner = result.Owner,
                PhoneNumber = result.PhoneNumber,
                Project = result.Project,
                Remark = result.Remark

            };
            //RaisePropertyChanged(nameof(Leads));
        }
        public async void RemoveLead(object obj)
        {
            try
            {
                var lead = (LeadListDTO)obj;
                if (!await CheckConnection())
                    return;

                var popup = new LeadDeletePopup(lead.FirstName + " " + lead.LastName, lead.PhoneNumber);
                await DialogService.DisplayPopup(popup);
                popup.OnConfirm += async (s, e) =>
                {
                    if (!e)
                        return;

                    await RunWithoutReturn(() => LeadsApi.DeleteLead(lead.Id));
                    Leads.Remove(lead);
                };
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public async Task GetOwner(string filter = null)
        {
            var user = await Run(() => UsersApi.GetUserDropdownList(filter));
            Onwers = new ObservableCollection<UserListDTO>(user);
            Onwers.Insert(0, new UserListDTO() { Id = null, FirstName = "ทั้งหมด" });
        }

        public async Task GetProjects(string name = null)
        {
            Projects = await Run(() => ProjectsApi.GetProjectDropdown(name));
        }

        public async Task GetLeadType()
        {
            var result = await Run(() => MasterCentersApi.GetMasterCenterDropdownList("LeadType", null));
            IsBusy = false;
            if (result == null)
                return;

            foreach (var item in result) { LeadTypes.Add(item); }
        }

    }
}
