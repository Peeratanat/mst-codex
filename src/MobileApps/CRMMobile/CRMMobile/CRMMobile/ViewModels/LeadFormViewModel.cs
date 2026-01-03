using CRMMobile.Validations;
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
    public class LeadFormViewModel : ViewModelBase
    {
        public LeadFormViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            CanSubmmit = true;
            CreateCommand = new DelegateCommand(CreateLead);
            FilterProjectCommand = new DelegateCommand<string>(async (obj) => await GetProjects(obj));
            Advertisements = new List<MasterCenterDropdownDTO>();
            Projects = new List<ProjectDTO>();
            InitValidation();
        }

        #region Command

        public DelegateCommand CreateCommand { get; }
        public DelegateCommand<string> FilterProjectCommand { get; private set; }

        #endregion Command

        #region Service

        [Unity.Dependency]
        public ILeadsApi LeadsApi { get; set; }

        [Unity.Dependency]
        public IMasterCentersApi MasterCentersApi { get; set; }

        [Unity.Dependency]
        public IProjectsApi ProjectsApi { get; set; }

        #endregion Service

        public const string LeadTypeKeyfilter = "C";
        public const string LeadTypeKey = "LeadType";
        public const string AdvertisementKey = "Advertisement";

        private ValidationObject<ProjectDTO> project;

        public ValidationObject<ProjectDTO> Project
        {
            get
            {
                return project;
            }
            set
            {
                SetProperty(ref project, value);
            }
        }

        private ValidationObject<MasterCenterDropdownDTO> advertisement;

        public ValidationObject<MasterCenterDropdownDTO> Advertisement
        {
            get { return advertisement; }
            set { SetProperty(ref advertisement, value); }
        }

        private ValidationObject<string> firstName;

        public ValidationObject<string> FirstName
        {
            get { return firstName; }
            set
            {
                SetProperty(ref firstName, value);
            }
        }

        private ValidationObject<string> lastName;

        public ValidationObject<string> LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }

        private ValidationObject<string> email;

        public ValidationObject<string> Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private ValidationObject<string> phoneNumber;

        public ValidationObject<string> PhoneNumber
        {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }

        private ValidationObject<string> telephone;

        public ValidationObject<string> Telephone
        {
            get { return telephone; }
            set { SetProperty(ref telephone, value); }
        }

        private string visitZone;

        public string VisitZone
        {
            get { return visitZone; }
            set { SetProperty(ref visitZone, value); }
        }

        private string advertisementId;

        public string AdvertisementId
        {
            get { return advertisementId; }
            set { SetProperty(ref advertisementId, value); }
        }

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { SetProperty(ref remark, value); }
        }

        private bool canSubmmit;

        public bool CanSubmmit
        {
            get { return canSubmmit; }
            set { SetProperty(ref canSubmmit, value); }
        }

        private List<ProjectDTO> projects;

        public List<ProjectDTO> Projects
        {
            get => projects;
            set { SetProperty(ref projects, value); }
        }

        private List<MasterCenterDropdownDTO> advertisements;

        public List<MasterCenterDropdownDTO> Advertisements
        {
            get => advertisements;
            set { SetProperty(ref advertisements, value); }
        }

        public ObservableCollection<string> Lists { get; set; }
        public MasterCenterDropdownDTO LeadType { get; set; }

        public async void CreateLead()
        {
            if (!Validate())
                return;

            LeadDTO newLead = new LeadDTO()
            {
                Project = new ProjectDTO()
                {
                    Id = project.Value.Id,
                    IsActive = false
                },
                FirstName = this.firstName.Value,
                LastName = this.lastName.Value,
                Telephone = this.telephone.Value,
                PhoneNumber = this.phoneNumber.Value,
                VisitZone = this.visitZone,
                Remark = this.remark,
                Email = this.Email.Value,
                Advertisement = Advertisement.Value,
                LeadType = new MasterCenterDropdownDTO() { Id = LeadType.Id },
            };

            try
            {
                var result = await Run(() => LeadsApi.CreateLead(newLead));
                CanSubmmit = false;
                await DisplaySuccessPopup(async () =>
                {
                    NavigationParameters param = new NavigationParameters();
                    param.Add("Refresh", true);
                    await NavigatedBack(param);
                });
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        public async Task Init()
        {
            await GetProjects();
            Advertisements = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(AdvertisementKey, null));
            var lead = MasterCentersApi.GetMasterCenterDropdownList(LeadTypeKey, null);
            LeadType = lead.FirstOrDefault(t => t.Key == LeadTypeKeyfilter);
            RaisePropertyChanged(nameof(LeadType));
        }

        public void InitValidation()
        {
            FirstName = new ValidationObject<string>();
            FirstName.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกนามสกุลด้วยภาษาไทยหรืออังกฤษ", IsThai = true, IsEng = true });
            FirstName.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณาระบุชื่อ" });

            LastName = new ValidationObject<string>();
            LastName.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกนามสกุลด้วยภาษาไทยหรืออังกฤษ", IsThai = true, IsEng = true });
            LastName.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณาระบุนามสกุล" });

            PhoneNumber = new ValidationObject<string>();
            PhoneNumber.Validations.Add(new IsNumberRule<string>() { ValidationMessage = "กรุณาระบุตัวเลข 0-9" });
            PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณาระบุเบอร์โทรศัพท์" });

            Telephone = new ValidationObject<string>();
            Telephone.Validations.Add(new IsNumberRule<string>() { ValidationMessage = "กรุณาระบุตัวเลข 0-9" });

            Project = new ValidationObject<ProjectDTO>();
            Project.Validations.Add(new IsObjectNull<ProjectDTO>() { ValidationMessage = "กรุณาระบุโครงการ" });
            Project.ValueChanged += (s, e) =>
            {
                Project.Validate();
            };

            Email = new ValidationObject<string>();
            Email.Validations.Add(new EmailRule<string>() { ValidationMessage = "กรุณากรอกอิเมลล์ให้ถูกต้อง" });

            Email.ValueChanged += (s,e)=> {
                if (!string.IsNullOrEmpty(Email.Value))
                {
                    Email.Validate();
                }
                else
                {
                    Email.IsValid = true;

                }
            };
            
            Advertisement = new ValidationObject<MasterCenterDropdownDTO>();
            Advertisement.Validations.Add(new IsObjectNull<MasterCenterDropdownDTO>() { ValidationMessage = "กรุณาระบุสื่อ" });
        }

        public bool Validate()
        {
            bool isValidProject = project.Validate();
            bool isValidFirstname = firstName.Validate();
           
            bool isValidLastname = lastName.Validate();
            bool isValidPhoneNo = PhoneNumber.Validate();
            return isValidProject && isValidFirstname && isValidLastname && isValidPhoneNo;
        }

        public async Task GetProjects(string name = null)
        {
            Projects = await Run(() => ProjectsApi.GetProjectDropdown(name));
        }

        public async override void OnNavigatedModeNew(INavigationParameters parameters)
        {
            base.OnNavigatedModeNew(parameters);
            await Init();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
    }
}