using IO.Swagger.Api;
using IO.Swagger.Model;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CRMMobile.ViewModels
{
    public class OpportunityViewModel : ViewModelBase
    {
        public OpportunityViewModel(INavigationService navigationService) : base(navigationService)
        {
            OpportunitiesList = new ObservableCollection<OpportunityListDTO>();
            EditCommand = new DelegateCommand<object>((obj) => OnEdit(obj));
            RemoveCommand = new DelegateCommand<object>((obj) => OnRemove(obj));
            CreateActivityCommand = new DelegateCommand<object>(CreateActivityWalk);
            CreateRevisitCommand = new DelegateCommand<object>(CreateActivityRevisit);
        }

        [Unity.Dependency]
        public IOpportunitiesApi OpportunitiesApi { get; set; }

        [Unity.Dependency]
        public IProjectsApi ProjectsApi { get; set; }

        public event EventHandler<bool> CloseFilterEvent;

        public DelegateCommand<object> EditCommand { get; set; }
        public DelegateCommand<object> RemoveCommand { get; set; }
        public DelegateCommand<object> CreateRevisitCommand { get; set; }
        public DelegateCommand<object> CreateActivityCommand { get; set; }

        public DelegateCommand LoadMoreCommand => new DelegateCommand(async () =>
        {
            try
            {
                IsBusy = true;
                await GetOpportunities(fullName: this.firstName + " " + this.lastName, phoneNumber: this.Telephone, projectID: projectsSelected?.Id, page: PageIndex += 1, sortBy: "UpdatedDate", ascending: false);
            }
            catch (Exception e) { IsBusy = false; }
        });

        public DelegateCommand Searchcommand => new DelegateCommand(async () =>
        {
            CloseFilterEvent?.Invoke(this, false);
            ClearOpportunities();
            try
            {
                IsBusy = true;
                await GetOpportunities(fullName: this.firstName + " " + this.lastName, phoneNumber: this.Telephone, projectID: projectsSelected?.Id, sortBy: "UpdatedDate", ascending: false);
            }
            catch (Exception e) { IsBusy = false; }
            finally { IsBusy = false; }
        });

        public DelegateCommand Clearcommand => new DelegateCommand(async () =>
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            FirstName = LastName = Telephone = null;
            ProjectsSelected = null;
        });

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

        private string telephone;

        public string Telephone
        {
            get => telephone;
            set { SetProperty(ref telephone, value); }
        }

        private List<ProjectDTO> projects;

        public List<ProjectDTO> Projects
        {
            get => projects;
            set { SetProperty(ref projects, value); }
        }

        private ProjectDTO projectsSelected;

        public ProjectDTO ProjectsSelected
        {
            get => projectsSelected;
            set { SetProperty(ref projectsSelected, value); }
        }

        private ObservableCollection<OpportunityListDTO> opportunityLists;

        public ObservableCollection<OpportunityListDTO> OpportunitiesList
        {
            get => opportunityLists;
            set { SetProperty(ref opportunityLists, value); }
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                try
                {
                    IsBusy = true;
                    await GetOpportunities(sortBy: "UpdatedDate", ascending: false);
                    await GetProjects();
                }
                catch (Exception e)
                {
                    HandleException(e);
                }
                finally { IsBusy = false; }
            }
            else if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                if (Searchcommand.CanExecute()) { Searchcommand.Execute(); }
            }
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public async Task GetProjects(string name = null)
        {
            Projects = await Run(() => ProjectsApi.GetProjectDropdown(name));
        }

        public void ClearOpportunities()
        {
            while (OpportunitiesList?.Count > 0)
            {
                OpportunitiesList.RemoveAt(0);
            }
        }

        private async Task GetOpportunities(Guid? projectID = null, DateTime? arriveDateFrom = null,
            DateTime? arriveDateTo = null, Guid? contactID = null,
            string contactNo = null, string fullName = null,
            string phoneNumber = null, string salesOpportunityKey = null,
            Guid? ownerID = null, string statusQuestionaireKey = null,
            DateTime? updatedDateFrom = null, DateTime? updatedDateTo = null, string excludeIDs = null,
            int? page = 1, int? pageSize = 5,
            string sortBy = null, bool? ascending = null)
        {
            //if (OpportunitiesList != null && OpportunitiesList.Count > 0)
            //{
            //    while (OpportunitiesList.Count > 0) { OpportunitiesList.RemoveAt(0); }
            //}
            var result = await Run(() => OpportunitiesApi.GetOpportunityList(projectID, arriveDateFrom, arriveDateTo, contactID,
                contactNo, fullName, phoneNumber, salesOpportunityKey, ownerID, statusQuestionaireKey, updatedDateFrom, updatedDateTo, excludeIDs, page, pageSize, sortBy, ascending));

            foreach (var item in result) { OpportunitiesList.Add(item); }
        }

        public async void OnEdit(object obj)
        {
            var id = (Guid?)obj;
            NavigationParameters param = new NavigationParameters();
            param.Add("OpportunityId", id);
            await Navigate("OpportunityDetail", param);
        }

        public async void OnRemove(object obj)
        {
            var id = (Guid?)obj;
            if (!id.HasValue)
                return;

            try
            {
                IsBusy = true;
                await ConfirmPopup("แจ้งเตือน", "คุณต้องการลบข้อมูล Opportunity หรือไม่", "ตกลง", "ยกเลิก", async () =>
                {
                    await RunWithoutReturn(() => OpportunitiesApi.DeleteOpportunity(id));
                    await DisplaySuccessPopup();
                    await Task.Delay(500);
                    await DialogService.CloseCurrentPopup();
                    if (Searchcommand.CanExecute()) { Searchcommand.Execute(); }
                });
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        private async void CreateActivityWalk(object obj)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("OpportunityId", (Guid?)obj);
            await Navigate("OpportunityActivityForm", parameters);
        }

        private async void CreateActivityRevisit(object obj)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("OpportunityId", (Guid?)obj);
            await Navigate("OpportunityRevisitForm", parameters);
        }
    }
}