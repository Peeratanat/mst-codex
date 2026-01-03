using CRMMobile.Helper;
using CRMMobile.Models;
using IO.Swagger.Api;
using IO.Swagger.Model;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CRMMobile.ViewModels
{
    public class MyWorldViewModel : ViewModelBase
    {
        public MyWorldViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            MyWorlds = new ObservableCollection<MyworldModel>();
            FilterCommand = new DelegateCommand(async () => await Filter());
            DetailCommand = new DelegateCommand<object>(Detail);
            ClearCommand = new DelegateCommand(ClearFilter);
            OnlyOverDueCommand = new DelegateCommand(() => OnlyOverdue = !OnlyOverdue);
            LoadMoreCommand = new DelegateCommand(LoadMore);
        }

        [Unity.Dependency]
        public IActivitiesApi ActivitiesApi { get; set; }

        [Unity.Dependency]
        public IProjectsApi ProjectsApi { get; set; }

        [Unity.Dependency]
        public IMasterCentersApi MasterCentersApi { get; set; }

        #region Filter

        public event EventHandler<bool> CloseFilterEvent;
        private string customerName;

        public string CustomerName
        {
            get => customerName;
            set { SetProperty(ref customerName, value); }
        }

        private string telephone;

        public string Telephone
        {
            get => telephone;
            set { SetProperty(ref telephone, value); }
        }

        private SortBy sortBy;

        public SortBy SortBy
        {
            get => sortBy;
            set { SetProperty(ref sortBy, value); }
        }

        private DateTime? dateFrom;

        public DateTime? DateFrom
        {
            get => dateFrom;
            set { SetProperty(ref dateFrom, value); }
        }

        private DateTime? dateTo;

        public DateTime? DateTo
        {
            get => dateTo;
            set { SetProperty(ref dateTo, value); }
        }

        private MasterCenterDropdownDTO activityTaskStatusSelected;

        public MasterCenterDropdownDTO ActivityTaskStatusSelected
        {
            get => activityTaskStatusSelected;
            set { SetProperty(ref activityTaskStatusSelected, value); }
        }

        private bool onlyOverdue;

        public bool OnlyOverdue
        {
            get => onlyOverdue;
            set { SetProperty(ref onlyOverdue, value); }
        }

        private CheckBoxGroup leadTopic;

        public CheckBoxGroup LeadTopic
        {
            get => leadTopic;
            set { SetProperty(ref leadTopic, value); }
        }

        private CheckBoxGroup activityTasList;

        public CheckBoxGroup ActivityTaskList
        {
            get => activityTasList;
            set { SetProperty(ref activityTasList, value); }
        }

        private bool displayEmptyView;

        public bool DisplayEmptyView
        {
            get => displayEmptyView;
            set { SetProperty(ref displayEmptyView, value); }
        }


        public DelegateCommand OnlyOverDueCommand { get; set; }

        #endregion Filter

        public DelegateCommand FilterCommand { get; set; }
        public DelegateCommand LoadMoreCommand { get; set; }
        public DelegateCommand ClearCommand { get; set; }
        public DelegateCommand<object> DetailCommand { get; set; }

        private List<ProjectDTO> projects;

        public List<ProjectDTO> Projects
        {
            get => projects;
            set { SetProperty(ref projects, value); }
        }

        private ProjectDTO project;

        public ProjectDTO Project
        {
            get => project;
            set { SetProperty(ref project, value); }
        }

        private ObservableCollection<MyworldModel> myWorlds;

        public ObservableCollection<MyworldModel> MyWorlds
        {
            get => myWorlds;
            set { SetProperty(ref myWorlds, value); }
        }

        private List<MasterCenterDropdownDTO> activityTaskOverdueStatusList;

        public List<MasterCenterDropdownDTO> ActivityTaskOverdueStatusList
        {
            get => activityTaskOverdueStatusList;
            set { SetProperty(ref activityTaskOverdueStatusList, value); }
        }

        private List<MasterCenterDropdownDTO> activityTaskStatusList;

        public List<MasterCenterDropdownDTO> ActivityTaskStatusList
        {
            get => activityTaskStatusList;
            set { SetProperty(ref activityTaskStatusList, value); }
        }

        private List<SortBy> sorts;

        public List<SortBy> Sorts
        {
            get => sorts;
            set { SetProperty(ref sorts, value); }
        }

        public bool IsEnabled { get; set; }

        public async Task GetActivities(string activityTaskTopicKey = null,
            string activityTaskTopicKeys = null, string leadTypeKey = null,
            string activityTaskTypeKey = null, string activityTaskTypeKeys = null,
            Guid? projectID = null, string projectIDs = null,
            string fullName = null, string phoneNumber = null,
            DateTime? dueDateFrom = null, DateTime? dueDateTo = null,
            string overdueStatusKey = null, Guid? ownerID = null,
            string activityTaskStatusKey = null, int? page = 1, int? pageSize = 10,
            string sortBy = null, bool? ascending = null)
        {
            DisplayEmptyView = false;
            var activityListDTOs = await Run(() => ActivitiesApi.GetActivityList(activityTaskTopicKey, activityTaskTopicKeys,
                leadTypeKey, activityTaskTypeKey,
                activityTaskTypeKeys, projectID,
                projectIDs, fullName, phoneNumber,
                dueDateFrom, dueDateTo, overdueStatusKey,
                ownerID, activityTaskStatusKey, page,
                pageSize, sortBy, ascending));

            if(activityListDTOs == null || activityListDTOs.Count == 0)
            {
                DisplayEmptyView = true;
                return;
            }

            foreach (var item in activityListDTOs)
            {
                var myworld = new MyworldModel() { Activity = item };
                MyWorlds.Add(myworld);
            }
        }

        public async Task Initialize()
        {
            try
            {
                IsEnabled = false;
                IsBusy = true;
                DateTo = null;
                DateFrom = null;
                IsEnabled = true;
                IsBusy = true;
                await Task.Run(() =>
                {
                    Sorts = new List<SortBy>()
                    {
                       new SortBy(){ Name = "หัวข้อ",Value="ActivityTaskTopic" },
                       new SortBy(){ Name = "ประเภท",Value="LeadType" },
                       new SortBy(){ Name = "ประเภท Activity",Value="ActivityTaskType" },
                       new SortBy(){ Name = "โครงการ",Value="Project" },
                       new SortBy(){ Name = "ชื่อลูกค้า",Value="FirstName" },
                       new SortBy(){ Name = "นามสกุลลูกค้า",Value="LastName" },
                       new SortBy(){ Name = "เบอร์โทรศัพท์",Value="PhoneNumber" },
                       new SortBy(){ Name = "Overdue (วัน)",Value="OverdueDays" },
                       new SortBy(){ Name = "LC ผู้ดูแล",Value="Owner" },
                       new SortBy(){ Name = "สถานะ",Value="ActivityTaskStatus" },
                       new SortBy(){ Name = "วันที่ต้องทำ",Value="DueDate" },
                    };
                });
                
                await GetLeadTopic();
                await GetActivityTaskType();
                await GetProjects();
                await GetActivityTaskOverdueStatusList();
                await GetActivityTaskStatusList();
                await GetActivities(pageSize: 10);
                IsBusy = false;
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public async Task GetActivityTaskOverdueStatusList()
        {
            ActivityTaskOverdueStatusList = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(MasterCenterKey.ActivityTaskOverdueStatus, null));
        }

        public async Task GetActivityTaskStatusList()
        {
            ActivityTaskStatusList = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(MasterCenterKey.ActivityTaskStatus, null));
        }

        public async Task GetProjects(string name = null)
        {
            Projects = await Run(() => ProjectsApi.GetProjectDropdown(name));
            if (string.IsNullOrEmpty(name))
                Projects.Insert(0, new ProjectDTO() { ProjectNameTH = "ทั้งหมด" });
        }

        public async Task GetActivityTaskType()
        {
            var ActivityTaskType = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(MasterCenterKey.ActivityTaskType, null));
            ActivityTaskList = new CheckBoxGroup() { IsMultiple = true };
            ActivityTaskList.AddCheckboxValue(ActivityTaskType);
        }

        public async Task GetLeadTopic()
        {
            var activityTaskTopicList = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(MasterCenterKey.ActivityTaskTopic, null));
            LeadTopic = new CheckBoxGroup() { IsMultiple = true };
            foreach (var item in activityTaskTopicList)
            {
                LeadTopic.AddValue(item);
            }
        }

        public void Clear()
        {
            while (MyWorlds?.Count > 0)
            {
                MyWorlds.RemoveAt(0);
            }
        }

        public async Task Filter()
        {
            CloseFilterEvent?.Invoke(this, false);
            Clear();

            string leaTopicSelected = null;
            foreach (var task in LeadTopic.Checkboxs.Where(t => t.IsCheck).ToList())
            {
                if (!string.IsNullOrEmpty(leaTopicSelected))
                    leaTopicSelected += ",";

                var leadTopicTmp = task.Value as MasterCenterDropdownDTO;
                leaTopicSelected += $"{leadTopicTmp.Key}";
            }

            string activityTypeListSelected = null;
            foreach (var task in ActivityTaskList.Checkboxs.Where(t => t.IsCheck).ToList())
            {
                if (!string.IsNullOrEmpty(activityTypeListSelected))
                    activityTypeListSelected += ",";
                var _task = task.Value as MasterCenterDropdownDTO;
                activityTypeListSelected += $"{_task.Key},";
            }

            try
            {
                IsBusy = true;
                await GetActivities(
               dueDateFrom: this.DateFrom,
               dueDateTo: this.DateTo,
               activityTaskTopicKeys: leaTopicSelected,
               activityTaskTypeKeys: activityTypeListSelected,
               activityTaskStatusKey: this.ActivityTaskStatusSelected?.Key,
               sortBy: this.sortBy?.Value,
               fullName: this.CustomerName,
               phoneNumber: this.telephone,
               projectID: this.Project?.Id,
               overdueStatusKey: this.OnlyOverdue ? ActivityTaskOverdueStatus.Overdue : null,
               page: 1);
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await Initialize();
        }

        public async void Detail(object obj)
        {
            var activity = (obj as MyworldModel).Activity;

            if (activity.ActivityTaskTopic.Key.Equals(ActivityTaskTopic.Lead))
            {
                NavigationParameters param = new NavigationParameters();
                param.Add("ActivityId", activity.LeadActivity.Id);
                param.Add("IsCompleted", activity.LeadActivity.IsCompleted);
                param.Add("LeadId", activity.LeadActivity.LeadID);

                await Navigate("LeadActivityForm", param);
            }
            else if (activity.ActivityTaskTopic.Key.Equals(ActivityTaskTopic.Walk))
            {
                NavigationParameters param = new NavigationParameters();
                param.Add("ActivityId", activity.OpportunityActivity.Id);
                param.Add("IsCompleted", activity.OpportunityActivity.IsCompleted);
                param.Add("OpportunityId", activity.OpportunityActivity.OpportunityID);
                await Navigate("OpportunityActivityForm", param);
            }
            else
            {
                NavigationParameters param = new NavigationParameters();
                param.Add("ActivityId", activity.RevisitActivity.Id);
                param.Add("IsCompleted", activity.RevisitActivity.IsCompleted);
                param.Add("OpportunityId", activity.RevisitActivity.OpportunityID);
                await Navigate("OpportunityRevisitForm", param);
            }
        }

        public async void ClearFilter()
        {
            this.Telephone = null;
            this.DateTo = this.DateFrom = null;
            this.SortBy = null;
            this.Project = null;
            this.CustomerName = null;
            this.OnlyOverdue = false;
            this.ActivityTaskStatusSelected = null;

            LeadTopic.Clear();
            ActivityTaskList.Clear();
        }

        public async void LoadMore()
        {
            try
            {
                MasterCenterDropdownDTO _leacTopic = null;
                var _tmp = LeadTopic.Checkboxs.FirstOrDefault(t => t.IsCheck);
                if (_tmp != null)
                    _leacTopic = _tmp.Value as MasterCenterDropdownDTO;

                MasterCenterDropdownDTO _type = null;
                var _tmp2 = ActivityTaskList.Checkboxs.FirstOrDefault(t => t.IsCheck);
                if (_tmp2 != null)
                    _type = _tmp2.Value as MasterCenterDropdownDTO;

                IsBusy = true;
                await GetActivities(
                dueDateFrom: this.DateFrom,
                dueDateTo: this.DateTo,
                activityTaskTopicKey: _leacTopic?.Key,
                activityTaskTypeKey: _type?.Key,
                activityTaskStatusKey: this.ActivityTaskStatusSelected?.Key,
                sortBy: this.sortBy?.Value,
                fullName: this.CustomerName,
                phoneNumber: this.telephone,
                projectID: this.Project?.Id,
                overdueStatusKey: this.OnlyOverdue ? ActivityTaskOverdueStatus.Overdue : null,
                page: PageIndex += 1);
            }
            catch (Exception e) { HandleException(e); }
            finally { IsBusy = false; }
        }
    }
}