using CRMMobile.Helper;
using CRMMobile.Validations;
using CRMMobile.Views.Popup;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMMobile.ViewModels
{
    public class ContactAddressFormViewModel : ViewModelBase
    {
        public ContactAddressFormViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            InitValidation();
            SubmitCommand = new DelegateCommand(async () => await Submit());
            CopyToEngCommand = new DelegateCommand(CopyToEng);
            CopyContacrAddressPopupCommand = new DelegateCommand(DisplayContacrAddressPopup);
            CopyFromCitizenAddressCommand = new DelegateCommand(CopyFromCitizenAddress);
            CopyFromHomeAddressCommand = new DelegateCommand(CopyFromHomeAddress);
        }

        [Unity.Dependency]
        public IProjectsApi ProjectsApi { get; set; }

        [Unity.Dependency]
        public IContactsApi ContactsApi { get; set; }

        [Unity.Dependency]
        public ICountriesApi CountriesApi { get; set; }

        [Unity.Dependency]
        public IProvincesApi ProvincesApi { get; set; }

        [Unity.Dependency]
        public IDistrictsApi DistrictsApi { get; set; }

        [Unity.Dependency]
        public ISubDistrictsApi SubDistrictsApi { get; set; }

        [Unity.Dependency]
        public IMasterCentersApi MasterCentersApi { get; set; }

  

        #region Commands

        public DelegateCommand SubmitCommand { get; set; }
        public DelegateCommand CopyToEngCommand { get; set; }
        public DelegateCommand CopyContacrAddressPopupCommand { get; set; }
        public DelegateCommand CopyFromHomeAddressCommand { get; set; }
        public DelegateCommand CopyFromCitizenAddressCommand { get; set; }

        #endregion Commands

        #region Property

        public Guid? ContactId { get; set; }
        public Guid? Id { get; set; }
        public bool DisplayCopyContactAddress { get; set; }
        public bool DisplayCopyHomeAddress { get; set; }
        public bool DisplayCopyCitizenAddress { get; set; }
        public bool DisplayProject { get; set; }
        public string ContentTitle { get; set; }

        public FormMode Mode
        {
            get
            {
                if (Id.HasValue)
                    return FormMode.Edit;
                else
                    return FormMode.Create;
            }
        }

        public MasterCenterDropdownDTO ContactTypeAddress { get; set; }
        public string ContactTypeKey { get; set; }
        public List<ContactAddressDTO> ContactAddress { get; set; }
        public ContactAddressDTO HomeAddress { get; set; }
        public ContactAddressDTO OfficeAddress { get; set; }
        public ContactAddressDTO CitizenAddress { get; set; }

        private ValidationObject<ProjectDTO> project;

        public ValidationObject<ProjectDTO> Project
        {
            get => project;
            set { SetProperty(ref project, value); }
        }

        private ValidationObject<string> addressNo;

        public ValidationObject<string> AddressNo
        {
            get => addressNo;
            set { SetProperty(ref addressNo, value);  }
        }

        private ValidationObject<string> moo;

        public ValidationObject<string> Moo
        {
            get => moo;
            set { SetProperty(ref moo, value); MooEN = value; }
        }

        private ValidationObject<string> vaillage;

        public ValidationObject<string> Vaillage
        {
            get => vaillage;
            set { SetProperty(ref vaillage, value); }
        }

        private ValidationObject<string> soi;

        public ValidationObject<string> Soi
        {
            get => soi;
            set { SetProperty(ref soi, value); }
        }

        private ValidationObject<string> street;

        public ValidationObject<string> Street
        {
            get => street;
            set { SetProperty(ref street, value); }
        }

        private ValidationObject<CountryDTO> country;

        public ValidationObject<CountryDTO> Country
        {
            get => country;
            set { SetProperty(ref country, value); }
        }

        private ValidationObject<ProvinceListDTO> province;

        public ValidationObject<ProvinceListDTO> Province
        {
            get => province;
            set { SetProperty(ref province, value); }
        }

        private ValidationObject<SubDistrictListDTO> subDistrict;

        public ValidationObject<SubDistrictListDTO> SubDistrict
        {
            get => subDistrict;
            set { SetProperty(ref subDistrict, value); }
        }

        private ValidationObject<DistrictListDTO> district;

        public ValidationObject<DistrictListDTO> District
        {
            get => district;
            set { SetProperty(ref district, value); }
        }

        private ValidationObject<string> postcode;

        public ValidationObject<string> Postcode
        {
            get => postcode;
            set { SetProperty(ref postcode, value); }
        }

        //EN
        private ValidationObject<string> addressNoEN;

        public ValidationObject<string> AddressNoEN
        {
            get => addressNoEN;
            set { SetProperty(ref addressNoEN, value); }
        }

        private ValidationObject<string> mooEN;

        public ValidationObject<string> MooEN
        {
            get => mooEN;
            set { SetProperty(ref mooEN, value); }
        }

        private ValidationObject<string> vaillageEN;

        public ValidationObject<string> VaillageEN
        {
            get => vaillageEN;
            set { SetProperty(ref vaillageEN, value); }
        }

        private ValidationObject<string> soiEN;

        public ValidationObject<string> SoiEN
        {
            get => soiEN;
            set { SetProperty(ref soiEN, value); }
        }

        private ValidationObject<string> streetEN;

        public ValidationObject<string> StreetEN
        {
            get => streetEN;
            set { SetProperty(ref streetEN, value); }
        }

        private ValidationObject<string> countryEN;

        public ValidationObject<string> CountryEN
        {
            get => countryEN;
            set { SetProperty(ref countryEN, value); }
        }

        private ValidationObject<ProvinceListDTO> provinceEN;

        public ValidationObject<ProvinceListDTO> ProvinceEN
        {
            get => provinceEN;
            set { SetProperty(ref provinceEN, value); }
        }

        private ValidationObject<DistrictListDTO> districtEN;

        public ValidationObject<DistrictListDTO> DistrictEN
        {
            get => districtEN;
            set { SetProperty(ref districtEN, value); }
        }

        private ValidationObject<SubDistrictListDTO> subDistrictEN;

        public ValidationObject<SubDistrictListDTO> SubDistrictEN
        {
            get => subDistrictEN;
            set { SetProperty(ref subDistrictEN, value); }
        }

        private ValidationObject<string> postcodeEN;

        public ValidationObject<string> PostcodeEN
        {
            get => postcodeEN;
            set { SetProperty(ref postcodeEN, value); }
        }

        private bool isThaiCountry;

        public bool IsThaiCountry
        {
            get => isThaiCountry;
            set
            {
                SetProperty(ref isThaiCountry, value);
            }
        }

        // Foriegn
        private string foreignProvince;

        public string ForeignProvince
        {
            get => foreignProvince;
            set
            {
                SetProperty(ref foreignProvince, value);
            }
        }

        private string foreignDistrict;

        public string ForeignDistrict
        {
            get => foreignDistrict;
            set
            {
                SetProperty(ref foreignDistrict, value);
            }
        }

        private string foreignSubDistrict;

        public string ForeignSubDistrict
        {
            get => foreignSubDistrict;
            set
            {
                SetProperty(ref foreignSubDistrict, value);
            }
        }

        #endregion Property

        private List<CountryDTO> countries;
        public List<CountryDTO> Countries { get => countries; set { SetProperty(ref countries, value); } }
        private List<ProvinceListDTO> provinces;
        public List<ProvinceListDTO> Provinces { get => provinces; set { SetProperty(ref provinces, value); } }
        private List<DistrictListDTO> districts;
        public List<DistrictListDTO> Districts { get => districts; set { SetProperty(ref districts, value); } }
        private List<ProjectDTO> projects;

        public List<ProjectDTO> Projects
        {
            get => projects;
            set { SetProperty(ref projects, value); }
        }

        private List<SubDistrictListDTO> subDistricts;

        public List<SubDistrictListDTO> SubDistricts
        {
            get => subDistricts;
            set { SetProperty(ref subDistricts, value); }
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue("ContactId", out Guid? _contactId))
                ContactId = _contactId;

            if (parameters.TryGetValue("AddressId", out Guid? _addressId))
                Id = _addressId;


            if (parameters.TryGetValue("ContactTypeKey", out string _contactType))
            {
                ContactTypeKey = _contactType;
                if (ContactTypeKey.Equals(ContactAddressType.ContactAddress))
                {
                    DisplayCopyContactAddress = DisplayCopyCitizenAddress = DisplayCopyHomeAddress = DisplayProject = true;
                    RaisePropertyChanged(nameof(DisplayCopyHomeAddress));
                    RaisePropertyChanged(nameof(DisplayCopyContactAddress));
                    RaisePropertyChanged(nameof(DisplayCopyCitizenAddress));
                }
                else if (ContactTypeKey.Equals(ContactAddressType.CitizenAddress))
                {
                    DisplayCopyContactAddress = DisplayCopyHomeAddress = true;
                    RaisePropertyChanged(nameof(DisplayCopyHomeAddress));
                    RaisePropertyChanged(nameof(DisplayCopyContactAddress));
                }
            }


            if (parameters.TryGetValue("ContactAddress", out List<ContactAddressDTO> _contactAddress))
                ContactAddress = _contactAddress;

            if (parameters.TryGetValue("ContactHomeAddress", out ContactAddressDTO _contactHomeAddress))
                HomeAddress = _contactHomeAddress;
           
            if (parameters.TryGetValue("ContactCitizenAddress", out ContactAddressDTO _citizenAddress))
                CitizenAddress = _citizenAddress;
            
            if (parameters.TryGetValue("ContactOfficeAddress", out ContactAddressDTO _officeAddress))
                OfficeAddress = _officeAddress;

            if (Mode == FormMode.Edit)
                await GetContactAddressData();

            if (parameters.GetNavigationMode() != NavigationMode.New)
                return;

            if (Mode == FormMode.Create && ContactTypeKey.Equals(ContactAddressType.ContactAddress))
            {
                Title = "เพิ่มที่อยู่ที่ติดต่อได้";
                ContentTitle = "ที่อยู่ที่ติดต่อได้";              
            }
            else if (Mode == FormMode.Edit && ContactTypeKey.Equals(ContactAddressType.ContactAddress))
            {
                ContentTitle = "ที่อยู่ที่ติดต่อได้";
                Title = "แก้ไขที่อยู่ที่ติดต่อได้";
            }               
            else if (Mode == FormMode.Edit && ContactTypeKey.Equals(ContactAddressType.CitizenAddress))
            {
                Title = "แก้ไขที่อยู่ตามสัญญา (บัตรประชาชน)";
                ContentTitle = "ที่อยู่ตามสัญญา (บัตรประชาชน)";
            }
            else if (Mode == FormMode.Edit && ContactTypeKey.Equals(ContactAddressType.Home))
            {
                Title = "แก้ไขที่อยู่ตามฝ่ายโอน (ทะเบียนบ้าน)";
                ContentTitle = "ที่อยู่ตามฝ่ายโอน (ทะเบียนบ้าน)";
            }
            else if (Mode == FormMode.Edit && ContactTypeKey.Equals(ContactAddressType.OfficeWarking))
            {
                Title = "แก้ไขที่อยู่ที่ทำงาน";
                ContentTitle = "ที่อยู่ที่ทำงาน";
            }

            RaisePropertyChanged(nameof(ContentTitle));
            RaisePropertyChanged(nameof(DisplayProject));

            try
            {
                await GetContactAddressType(_contactType);
                await GetProjects();
                await GetCountries();
                await SetThaiDefault();
                await GetProvince();
            }
            catch (Exception e) { HandleException(e); }
        }

        private void InitValidation()
        {
            Project = new ValidationObject<ProjectDTO>();
            Project.Validations.Add(new IsObjectNull<ProjectDTO>() { ValidationMessage = "กรุณาระบุโครงการ" });
            Project.ValueChanged += (s, e) =>
            {
                Project.Validate();
            };

            //TH
            AddressNo = new ValidationObject<string>();
            Moo = new ValidationObject<string>();
            Vaillage = new ValidationObject<string>();
            Soi = new ValidationObject<string>();
            Street = new ValidationObject<string>();
            Country = new ValidationObject<CountryDTO>();
            Country.PropertyChanged += Country_PropertyChanged;
            Province = new ValidationObject<ProvinceListDTO>();
            //Province.ValueChanged += (s, e) =>
            //{
            //    District.Value = null;
            //    SubDistrict.Value = null;
            //    Postcode.Value = null;

            //    DistrictEN.Value = null;
            //    SubDistrictEN.Value = null;

            //};
            District = new ValidationObject<DistrictListDTO>();
            SubDistrict = new ValidationObject<SubDistrictListDTO>();
            Postcode = new ValidationObject<string>();

            //EN
            AddressNoEN = new ValidationObject<string>();
            MooEN = new ValidationObject<string>();
            VaillageEN = new ValidationObject<string>();
            SoiEN = new ValidationObject<string>();
            StreetEN = new ValidationObject<string>();
            CountryEN = new ValidationObject<string>();
            ProvinceEN = new ValidationObject<ProvinceListDTO>();
            //ProvinceEN.ValueChanged += (s, e) =>
            //{
            //    District.Value = null;
            //    SubDistrict.Value = null;
            //    Postcode.Value = null;

            //    DistrictEN.Value = null;
            //    SubDistrictEN.Value = null;
            //};
            DistrictEN = new ValidationObject<DistrictListDTO>();
            SubDistrictEN = new ValidationObject<SubDistrictListDTO>();
            PostcodeEN = new ValidationObject<string>();
        }

        private void Country_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Country.Value == null)
                return;

            if (Country.Value.Code.Equals(NationalType.ThaiNationCode))
                IsThaiCountry = true;
            else
                IsThaiCountry = false;
        }

        public async Task GetProjects(string name = null)
        {
            Projects = await Run(() => ProjectsApi.GetProjectDropdown(name));
        }

        public async Task GetCountries(string code = null, string nameTH = null, string nameEN = null, string updatedBy = null, DateTime? updatedFrom = null, DateTime? updatedTo = null)
        {
            Countries = await Run(() => CountriesApi.GetCountryDropdownList(code, nameTH, nameEN, updatedBy, updatedFrom, updatedTo));
        }

        public async Task GetProvince(string name = null)
        {
            Provinces = await Run(() => ProvincesApi.GetProvinceDropdownList(name));
        }

        public async Task GetDistrict(string name = null)
        {
            Districts = await Run(() => DistrictsApi.GetDistrictDropdownList(name, province.Value.Id));
        }

        public async Task GetSubDistrict(string name = null)
        {
            SubDistricts = await Run(() => SubDistrictsApi.GetSubDistrictDropdownList(name, district.Value.Id));
        }

        public async Task SetThaiDefault()
        {
            var _thai = await Run(() => CountriesApi.GetCountryDropdownList(null, "thailand", null, null, null, null));
            Country.Value = _thai.FirstOrDefault(t => t.Code.Equals(NationalType.ThaiNationCode));
        }

        public async Task GetContactAddressType(string type)
        {
            var result2 = await Run(() => MasterCentersApi.GetMasterCenterDropdownList("ContactAddressType", null));
            ContactTypeAddress = result2.FirstOrDefault(t => t.Key == type);
        }

        //public async Task GetPostcode()
        //{
        //    await Run(()=> PostCode)
        //}

        public async Task Submit()
        {
            if (!Validate())
                return;

            if (!await CheckConnection())
                return;

            try
            {
                IsBusy = true;
                var request = CreateRequeset();
                if (Mode == FormMode.Create)
                    await Run(() => ContactsApi.CreateContactAddress(ContactId, request));
                else
                    await Run(() => ContactsApi.EditContactAddress(ContactId, Id, request));

                IsBusy = false;
                await DisplaySuccessPopup(async () => { await NavigatedBack(); });
                await Task.Delay(1000);
                await DialogService.CloseCurrentPopup();
                await NavigatedBack();

            }
            catch (ApiException e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public bool Validate()
        {
            if (!ContactTypeAddress.Key.Equals(ContactAddressType.ContactAddress))
                return true;

            bool isProject = Project.Validate();
            return isProject;
        }

        public ContactAddressDTO CreateRequeset()
        {
            Project.Value.IsActive = false;
            return new ContactAddressDTO()
            {
                ContactID = ContactId ?? null,
                Id = Id ?? null,
                HouseNoTH = AddressNo.Value,
                MooTH = Moo.Value,
                VillageTH = vaillage.Value,
                SoiTH = soi.Value,
                RoadTH = street.Value,
                Country = Country.Value,
                Province = Province.Value,
                District = District.Value,
                SubDistrict = SubDistrict.Value,
                PostalCode = Postcode.Value,

                HouseNoEN = AddressNo.Value,
                MooEN = Moo.Value,
                VillageEN = VaillageEN.Value,
                SoiEN = SoiEN.Value,
                RoadEN = StreetEN.Value,
                ForeignProvince = ForeignProvince,
                ForeignSubDistrict = ForeignSubDistrict,
                ForeignDistrict = ForeignDistrict,
                Project = Project.Value,
                ContactAddressType = this.ContactTypeAddress,
                
                  
            };
        }

        public void CopyToEng()
        {
            //AddressNoEN.Value = AddressNo.Value;
            //MooEN.Value = Moo.Value;
            PostcodeEN.Value = Postcode.Value;
        }

        public bool CanSelectProvince()
        {
            if (Country.Value == null)
            {
                return false;
            }
            if (!Country.Value.Code.Equals(NationalType.ThaiNationCode))
            {
                return false;
            }
            else { return true; }
        }

        public bool CanSelectedDistrict()
        {
            if (!CanSelectProvince())
            {
                return false;
            }
            if (Province.Value == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CanSelectSubDistrict()
        {
            if (!CanSelectedDistrict())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task GetContactAddressData()
        {
            var result = await Run(() => ContactsApi.GetContactAddress(ContactId, Id));
            SetDefaultAddress(result);
        }

        public void SetDefaultAddress(ContactAddressDTO addressDTO)
        {
            ContactTypeAddress = addressDTO?.ContactAddressType;
            Project.Value = addressDTO?.Project;
            Id = addressDTO?.Id;
            AddressNo.Value = addressDTO?.HouseNoTH;
            //AddressNoEN.Value = addressDTO.HouseNoTH;
            Moo.Value = addressDTO?.MooTH;
            //MooEN.Value = addressDTO.MooEN;
            Vaillage.Value = addressDTO?.VillageTH;
            VaillageEN.Value = addressDTO?.VillageEN;
            Soi.Value = addressDTO?.SoiTH;
            SoiEN.Value = addressDTO?.SoiEN;
            Street.Value = addressDTO?.RoadTH;
            StreetEN.Value = addressDTO?.RoadEN;
            SubDistrict.Value = addressDTO?.SubDistrict;
            District.Value = addressDTO?.District;
            Province.Value = addressDTO?.Province;
            Country.Value = addressDTO?.Country;

            ForeignDistrict = addressDTO?.ForeignDistrict;
            ForeignProvince = addressDTO?.ForeignProvince;
            foreignSubDistrict = addressDTO?.ForeignSubDistrict;
            Postcode.Value = addressDTO?.PostalCode;
            //PostcodeEN.Value = addressNo.Value;
        }

        public async void DisplayContacrAddressPopup()
        {
            var popup = new ContactAddressPopup(ContactAddress);
            popup.OnConfirmEvent += Popup_OnConfirmEvent;
            await DialogService.DisplayPopup(popup);
        }

        public async void CopyFromCitizenAddress()
        {
            SetDefaultAddress(CitizenAddress);
        }

        public async void CopyFromHomeAddress()
        {
            SetDefaultAddress(HomeAddress);
        }

        private void Popup_OnConfirmEvent(object sender, Models.CheckBoxModel e)
        {
            var contact = e.Value as ContactAddressDTO;
            contact.Id = null;
            contact.Project = null;
            SetDefaultAddress(contact);
            DialogService.CloseCurrentPopup();
        }
    }
}
