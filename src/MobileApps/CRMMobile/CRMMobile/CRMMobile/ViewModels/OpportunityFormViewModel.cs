using CRMMobile.Helper;
using CRMMobile.Validations;
using IO.Swagger.Api;
using IO.Swagger.Model;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMMobile.ViewModels
{
    public class OpportunityFormViewModel : ViewModelBase
    {
        public OpportunityFormViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            InitValidation();
            SubmitCommand = new DelegateCommand(async () => await Submmit());
        }

        [Unity.Dependency]
        public IProjectsApi ProjectsApi { get; set; }

        [Unity.Dependency]
        public IMasterCentersApi MasterCentersApi { get; set; }

        [Unity.Dependency]
        public IOpportunitiesApi OpportunitiesApi { get; set; }

        [Unity.Dependency]
        public IContactsApi ContactsApi { get; set; }

        public Guid ContactId { get; set; }

        public DelegateCommand SubmitCommand { get; set; }

        public ContactDTO Contact { get; set; }
        public Guid? Id { get; set; }

        private ValidationObject<DateTime?> arriveDate;

        public ValidationObject<DateTime?> ArriveDate
        {
            get => arriveDate;
            set { SetProperty(ref arriveDate, value); }
        }

        private List<ProjectDTO> projects;

        public List<ProjectDTO> Projects
        {
            get => projects;
            set { SetProperty(ref projects, value); }
        }

        private ValidationObject<ProjectDTO> project;

        public ValidationObject<ProjectDTO> Project
        {
            get => project;
            set { SetProperty(ref project, value); }
        }

        private ValidationObject<MasterCenterDropdownDTO> estimateSalesOpportunity;

        public ValidationObject<MasterCenterDropdownDTO> EstimateSalesOpportunity
        {
            get => estimateSalesOpportunity;
            set { SetProperty(ref estimateSalesOpportunity, value); }
        }

        private string interestedProduct;

        public string InterestedProduct
        {
            get => interestedProduct;
            set { SetProperty(ref interestedProduct, value); }
        }

        private string interestedProduct2;

        public string InterestedProduct2
        {
            get => interestedProduct2;
            set { SetProperty(ref interestedProduct2, value); }
        }

        private string interestedProduct3;

        public string InterestedProduct3
        {
            get => interestedProduct3;
            set { SetProperty(ref interestedProduct3, value); }
        }

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

        private ValidationObject<MasterCenterDropdownDTO> salesOpportunity;

        public ValidationObject<MasterCenterDropdownDTO> SalesOpportunity
        {
            get => salesOpportunity;
            set { SetProperty(ref salesOpportunity, value); }
        }

        private bool isEnabled;

        public bool IsEnabled
        {
            get => isEnabled;
            set { SetProperty(ref isEnabled, value); }
        }

        private ContactPhoneDTO phoneNumber;

        public ContactPhoneDTO PhoneNumber
        {
            get => phoneNumber;
            set { SetProperty(ref phoneNumber, value); }
        }

        private ContactPhoneDTO homeNumber;

        public ContactPhoneDTO HomeNumber
        {
            get => homeNumber;
            set { SetProperty(ref homeNumber, value); }
        }

        public string fullName;

        public string FullName
        {
            get => fullName;
            set { SetProperty(ref fullName, value); }
        }

        public async Task GetProjects(string name = null)
        {
            Projects = await Run(() => ProjectsApi.GetProjectDropdown(name));
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue("ContactId", out Guid _contactId))
            {
                ContactId = _contactId;
            }

            IsEnabled = true;
            try
            {
                IsBusy = true;
                await GetContact();
                await GetProjects();
                await GetEstimateSalesOpportunity();
                await GetSalesOpportunitys();
            }
            catch (Exception e) { HandleException(e); }
            finally { IsBusy = false; }
        }

        public OpportunityDTO CreateRequest()
        {
            Project.Value.IsActive = false;
            return new OpportunityDTO()
            {
                ArriveDate = this.ArriveDate.Value,
                Project = this.Project?.Value,
                EstimateSalesOpportunity = EstimateSalesOpportunity.Value,
                SalesOpportunity = SalesOpportunity.Value,
                InterestedProduct1 = this.InterestedProduct,
                InterestedProduct2 = this.InterestedProduct2,
                InterestedProduct3 = this.InterestedProduct3,
                ProductQTY = 0,

                Contact = new ContactListDTO() { Id = this.Contact.Id, OpportunityCount = 0 }
            };
        }

        public void SetUpDefault(OpportunityDTO opportunity)
        {
            this.ArriveDate.Value = opportunity.ArriveDate;
            this.Project.Value = opportunity.Project;
            this.EstimateSalesOpportunity.Value = opportunity.EstimateSalesOpportunity;
            this.SalesOpportunity.Value = opportunity.SalesOpportunity;
            this.InterestedProduct = opportunity.InterestedProduct1;
            this.InterestedProduct2 = opportunity.InterestedProduct2;
            this.InterestedProduct3 = opportunity.InterestedProduct3;
            this.Id = opportunity.Id;
        }

        public async Task Submmit()
        {
            try
            {
                if (!Validate())
                    return;
                IsBusy = true;

                var request = CreateRequest();
                if (Id.HasValue)
                {
                    var result = await Run(() => OpportunitiesApi.EditOpportunity(this.Id, request));
                    SetUpDefault(result);
                }
                else
                {
                    var result = await Run(() => OpportunitiesApi.CreateOpportunity(request, null));
                    SetUpDefault(result);
                }

                await DisplaySuccessPopup();
                await Task.Delay(500);
                await DialogService.CloseCurrentPopup();
                await NavigatedBack();
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; IsEnabled = true; }
        }

        public bool Validate()
        {
            bool estimateSalesOpportunityValid = EstimateSalesOpportunity.Validate();
            bool arriveValid = ArriveDate.Validate();
            bool projectValid = Project.Validate();
            //bool saleOpportunityValid = SalesOpportunity.Validate();
            return estimateSalesOpportunityValid && arriveValid && projectValid;
        }

        public void InitValidation()
        {
            Project = new ValidationObject<ProjectDTO>();
            Project.Validations.Add(new IsObjectNull<ProjectDTO>() { ValidationMessage = "กรุณาเลือกโครงการ" });

            EstimateSalesOpportunity = new ValidationObject<MasterCenterDropdownDTO>();
            EstimateSalesOpportunity.Validations.Add(new IsObjectNull<MasterCenterDropdownDTO>());
            SalesOpportunity = new ValidationObject<MasterCenterDropdownDTO>();
            SalesOpportunity.Validations.Add(new IsObjectNull<MasterCenterDropdownDTO>());

            ArriveDate = new ValidationObject<DateTime?>();
            ArriveDate.Value = null;
            ArriveDate.Validations.Add(new IsObjectNull<DateTime?>());
        }

        public async Task GetEstimateSalesOpportunity()
        {
            EstimateSalesOpportunitys = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(OpportunityKey.EstimateSalesOpportunity, null));
        }

        public async Task GetSalesOpportunitys()
        {
            SalesOpportunitys = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(OpportunityKey.SalesOpportunity, null));
        }

        public async Task GetContact()
        {
            var result = await Run(() => ContactsApi.GetContact(ContactId));
            if (result != null)
            {
                Contact = result;
                FullName = Contact.FirstNameTH + " " + Contact.LastNameTH;
                if (Contact.ContactPhones != null)
                {
                    if (Contact.ContactPhones.Any(t => t.PhoneType.Key.Equals(PhoneType.MobileTelNo)))
                    {
                        PhoneNumber = Contact.ContactPhones.FirstOrDefault(t => t.PhoneType.Key.Equals(PhoneType.MobileTelNo));
                    }
                    if (Contact.ContactPhones.Any(t => t.PhoneType.Key.Equals(PhoneType.HomeTelNo)))
                    {
                        HomeNumber = Contact.ContactPhones.FirstOrDefault(t => t.PhoneType.Key.Equals(PhoneType.HomeTelNo));
                    }
                }
            }
        }
    }
}