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
    public class VisitorViewModel : ViewModelBase
    {
        public VisitorViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            DetailCommand = new DelegateCommand<object>(Detail);
            VisitorLists = new ObservableCollection<VisitorListDTO>();
            VisitByCheckBox = new CheckBoxGroup();
            VisitStatusByCheckBox = new CheckBoxGroup();

            FilterCommand = new DelegateCommand(Filter);
            ClearCommand = new DelegateCommand(ClearFilter);
            LoadMoreCommand = new DelegateCommand(LoadMore);
            //Init();
        }

        [Unity.Dependency]
        public IVisitorsApi VisitorsApi { get; set; }

        [Unity.Dependency]
        public IMasterCentersApi MasterCentersApi { get; set; }

        [Unity.Dependency]
        public IProjectsApi ProjectsApi { get; set; }

        public event EventHandler<bool> OnOpenCloseFilterEvent;

        private bool displayEmptyView;

        public bool DisplayEmptyView
        {
            get => displayEmptyView;
            set { SetProperty(ref displayEmptyView, value); }
        }

        public ObservableCollection<VisitorListDTO> visitorLists;

        public ObservableCollection<VisitorListDTO> VisitorLists
        {
            get => visitorLists;
            set { SetProperty(ref visitorLists, value); }
        }

        private CheckBoxGroup visitByCheckBox;

        public CheckBoxGroup VisitByCheckBox
        {
            get { return visitByCheckBox; }
            set { SetProperty(ref visitByCheckBox, value); }
        }

        private CheckBoxGroup visitStatusCheckBox;

        public CheckBoxGroup VisitStatusByCheckBox
        {
            get { return visitStatusCheckBox; }
            set { SetProperty(ref visitStatusCheckBox, value); }
        }

        private SortBy sort;

        public SortBy Sort
        {
            get { return sort; }
            set { SetProperty(ref sort, value); }
        }

        private List<SortBy> sorts;

        public List<SortBy> Sorts
        {
            get { return sorts; }
            set { SetProperty(ref sorts, value); }
        }

        private MasterCenterDropdownDTO vehicle;

        public MasterCenterDropdownDTO Vehicle
        {
            get { return vehicle; }
            set { SetProperty(ref vehicle, value); }
        }

        private List<MasterCenterDropdownDTO> vehicles;

        public List<MasterCenterDropdownDTO> Vehicles
        {
            get { return vehicles; }
            set { SetProperty(ref vehicles, value); }
        }

        private ProjectDTO project;

        public ProjectDTO Project
        {
            get => project;
            set { SetProperty(ref project, value); }
        }

        private List<ProjectDTO> projects;

        public List<ProjectDTO> Projects
        {
            get => projects;
            set { SetProperty(ref projects, value); }
        }

        private DateTime? visitDateInFrom;

        public DateTime? VisitDateInFrom
        {
            get { return visitDateInFrom; }
            set { SetProperty(ref visitDateInFrom, value); }
        }

        private DateTime? visitDateInTo;

        public DateTime? VisitDateInTo
        {
            get { return visitDateInTo; }
            set { SetProperty(ref visitDateInTo, value); }
        }

        private VisitorProjectDTO visitorProjectDTO;

        public VisitorProjectDTO VisitorProjectDTO
        {
            get { return visitorProjectDTO; }
            set { SetProperty(ref visitorProjectDTO, value); }
        }

        public VisitorListDTO SelectedVisior { get; set; }

        public DelegateCommand<object> DetailCommand { get; set; }
        public DelegateCommand FilterCommand { get; private set; }
        public DelegateCommand LoadMoreCommand { get; private set; }
        public DelegateCommand ClearCommand { get; private set; }

        public async Task Init()
        {
            await Task.Run(() =>
            {
                //Sort By
                Sorts = new List<SortBy>()
                {
                   new SortBy(){ Name = "เลขที่รับ",Value="ReceiveNumber" },
                   new SortBy(){ Name = "รหัสลูกค้า",Value="ContactNo" },
                   new SortBy(){ Name = "ชื่อ - นามสกุล",Value="FullName" },
                   new SortBy(){ Name = "เบอร์โทรศัพท์",Value="PhoneNumber" },
                   new SortBy(){ Name = "ลักษณะการเดินทาง",Value="VisitBy" },
                   new SortBy(){ Name = "สถานะ Walk",Value="WalkStatus" },
                   new SortBy(){ Name = "รายละเอียด",Value="VehicleDescription" },
                   new SortBy(){ Name = "ผู้ดูแล",Value="Owner" },
                   new SortBy(){ Name = "เวลาเข้า",Value="VisitDateIn" },
                };

                var visitStatus = new List<string>() {
                    "ลูกค้า",
                    "ไม่ใช่ลูกค้า"
                };
                VisitStatusByCheckBox.AddCheckboxValue(visitStatus, 0);
                VisitDateInFrom = VisitDateInTo = DateTime.Now;
            });
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            DisplayEmptyView = false;
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                try
                {
                    IsBusy = true;
                    await Init();
                    await GetProjects();
                    await GetVihicle();
                    await GetVisitBy();

                    OnOpenCloseFilterEvent.Invoke(this, true);
                }
                catch (Exception e)
                {
                    HandleException(e);
                }
                finally { IsBusy = false; }
            }
            if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                if (SelectedVisior != null)
                    UpdateVisitorInVisitorList();
            }
        }

        private async Task<List<VisitorListDTO>> GetVisitor(string receiveNumber = null, string contactNo = null,
            string fullName = null, string phoneNumber = null,
            Guid? ownerID = null, string vehicleDescription = null,
            Guid? projectID = null, string visitByKey = null,
            string vehicleKey = null, bool? isContact = null,
            DateTime? _visitDateInFrom = null, DateTime? _visitDateInTo = null,
            int? page = 1, int? pageSize = 10, string sortBy = null,
            bool? ascending = null)
        {
            IsBusy = true;
            var result = await Run(() => VisitorsApi.GetVisitorList(receiveNumber, contactNo,
                fullName, phoneNumber, ownerID, vehicleDescription,
                projectID, visitByKey, vehicleKey, isContact,
                _visitDateInFrom, _visitDateInTo, page, pageSize,
                sortBy, ascending));
            VisitorProjectDTO = await Run(() => VisitorsApi.GetVisitorProject(receiveNumber, contactNo, fullName, phoneNumber, ownerID, vehicleDescription, projectID, visitByKey, vehicleKey, isContact, visitDateInFrom, visitDateInTo));
            return result;
        }

        private async void Detail(object obj)
        {
            SelectedVisior = (VisitorListDTO)obj;
            NavigationParameters param = new NavigationParameters();
            param.Add("Id", SelectedVisior.Id);
            await Navigate("VisitorDetail", param);
        }

        public async Task GetProjects(string name = null)
        {
            Projects = await Run(() => ProjectsApi.GetProjectDropdown(name));
        }

        private async Task GetVihicle()
        {
            Vehicles = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(MasterCenterKey.Vehicle, null));
            Vehicles.Insert(0, new MasterCenterDropdownDTO() { Name = "ทั้งหมด" });
            Vehicle = Vehicles[0];
        }

        private async Task GetVisitBy()
        {
            var visitByList = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(MasterCenterKey.VisitBy, null));
            VisitByCheckBox.AddCheckboxValue(visitByList, 1);
        }

        public async void Filter()
        {
            try
            {
                OnOpenCloseFilterEvent?.Invoke(this, false);
                Clear();

                var isContact = VisitStatusByCheckBox.Checkboxs.FirstOrDefault(t => t.IsCheck);
                var isContactValue = isContact.IsCheck;
                var visitBy = VisitByCheckBox.Checkboxs.FirstOrDefault(t => t.IsCheck);
                var _visitByValue = visitBy.Value as MasterCenterDropdownDTO;

                if (VisitDateInFrom != null)
                {
                    TimeSpan ts = new TimeSpan(0, 0, 0);
                    VisitDateInFrom = VisitDateInFrom.Value.Date + ts;
                }

                if (VisitDateInTo != null)
                {
                    TimeSpan ts = new TimeSpan(23, 59, 59);
                    VisitDateInTo = VisitDateInTo.Value.Date + ts;
                }

                if (isContact.Value.ToString() == "ไม่ใช่ลูกค้า")
                {
                    isContactValue = false;
                }

                var list = await GetVisitor(projectID: this.project?.Id, vehicleKey: this.Vehicle?.Key,
                    visitByKey: _visitByValue?.Key, isContact: isContactValue,
                    _visitDateInFrom: VisitDateInFrom, _visitDateInTo: VisitDateInTo,
                    sortBy: Sort?.Value, page: 1, pageSize: 10);

                if (list == null || list.Count == 0)
                    DisplayEmptyView = true;

                foreach (var item in list)
                {
                    VisitorLists.Add(item);
                }
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public void Clear()
        {
            while (VisitorLists?.Count > 0)
            {
                VisitorLists.RemoveAt(0);
            }
        }

        public void ClearFilter()
        {
            this.Project = null;
            this.VisitByCheckBox.Clear();
            this.VisitByCheckBox.SetDefault(1);
            this.VisitStatusByCheckBox.Clear();
            this.VisitStatusByCheckBox.SetDefault(0);
            this.Sort = null;
            this.Vehicle = Vehicles[0];
            this.VisitDateInFrom = this.VisitDateInTo = DateTime.Now;
        }

        public async void LoadMore()
        {
            try
            {
                IsBusy = true;
                var isContact = VisitStatusByCheckBox.Checkboxs.FirstOrDefault(t => t.IsCheck);
                var isContactValue = isContact.IsCheck;
                var visitBy = VisitByCheckBox.Checkboxs.FirstOrDefault(t => t.IsCheck);
                var _visitByValue = visitBy.Value as MasterCenterDropdownDTO;

                if (VisitDateInFrom != null)
                {
                    TimeSpan ts = new TimeSpan(0, 0, 0);
                    VisitDateInFrom = VisitDateInFrom.Value.Date + ts;
                }

                if (VisitDateInTo != null)
                {
                    TimeSpan ts = new TimeSpan(23, 59, 59);
                    VisitDateInTo = VisitDateInTo.Value.Date + ts;
                }

                if (isContact.Value.ToString() == "ไม่ใช่ลูกค้า")
                {
                    isContactValue = false;
                }

                var list = await GetVisitor(projectID: this.project?.Id, vehicleKey: this.Vehicle?.Key,
                visitByKey: _visitByValue?.Key, isContact: isContactValue,
                _visitDateInFrom: VisitDateInFrom, _visitDateInTo: VisitDateInTo,
                sortBy: Sort?.Value, page: ++PageIndex, pageSize: 10);

                if (list == null || list.Count == 0)
                    DisplayEmptyView = false;

                foreach (var item in list)
                {
                    VisitorLists.Add(item);
                }
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public async void UpdateVisitorInVisitorList()
        {
            // SelectedVisior
            var newVisitor = await Run(() => VisitorsApi.GetVisitor(SelectedVisior?.Id));
            var visitorInList = VisitorLists.FirstOrDefault(t => t.Id == newVisitor.Id);
            var index = VisitorLists.IndexOf(visitorInList);
            if (visitorInList != null)
            {
                // visitorInList;
                //visitorInList.FirstNameTH = "TEST JAA";
                VisitorLists[index] = new VisitorListDTO()
                {
                    FirstNameTH = newVisitor.FirstNameTH,
                    LastNameTH = newVisitor.LastNameTH,
                    Id = newVisitor.Id,
                    Contact = newVisitor.Contact,
                    Owner = newVisitor.Owner,
                    PhoneNumber = newVisitor.PhoneNumber,
                    ReceiveNumber = newVisitor.ReceiveNumber,
                    VehicleDescription = newVisitor.VehicleDescription,
                    VisitBy = newVisitor.VisitBy,
                    VisitDateIn = newVisitor.VisitDateIn,
                    VisitDateOut = newVisitor.VisitDateOut,
                    WalkStatus = newVisitor.WalkStatus
                };
            }
        }
    }
}