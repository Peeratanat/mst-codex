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
    public class LeadDetailViewModel : ViewModelBase
    {
        public LeadDetailViewModel(INavigationService navigationService) : base(navigationService)
        {
            InitValidation();

            MyCommand = new Command<object>(value =>
            {
                var _newPosition = Convert.ToInt32(value);
                Position = _newPosition;
                if (Position == 0)
                    CanDisplay = true;
                else
                    CanDisplay = false;

                OnPositionChanged?.Invoke(this, EventArgs.Empty);
            });

            SubmitCommand = new DelegateCommand(Submit);
            EditCommand = new DelegateCommand<object>(Edit);
            RemoveCommand = new DelegateCommand<object>(async (value) => await RemoveActivity(value));

            LeadActivityListDTOs = new ObservableCollection<LeadActivityListDTO>();
            Advertisements = new ObservableCollection<MasterCenterDropdownDTO>();
            LeadActivityListDTOsComplete = new ObservableCollection<LeadActivityListDTO>();
        }

        [Unity.Dependency]
        public ILeadsApi LeadsApi { get; set; }

        [Unity.Dependency]
        public IMasterCentersApi MasterCentersApi { get; set; }

        public event EventHandler<EventArgs> OnPositionChanged;

        public Command<object> MyCommand { protected set; get; }
        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand<object> EditCommand { get; private set; }
        public DelegateCommand<object> RemoveCommand { get; private set; }

        public bool IsSubmitted { get; set; }

        private bool canDisplay;

        public bool CanDisplay
        {
            get => canDisplay;
            set
            {
                canDisplay = value;
                RaisePropertyChanged("CanDisplay");
            }
        }

        private int position;

        public int Position
        {
            get { return position; }
            set
            {
                position = value;
                RaisePropertyChanged(nameof(Position));
            }
        }

        private List<DataTemplate> template;

        public List<DataTemplate> Template
        {
            get { return template; }
            set { template = value; RaisePropertyChanged("Template"); }
        }

        #region Activity

        public DelegateCommand CreateActivityCommand { get; set; }

        public ObservableCollection<LeadActivityListDTO> LeadActivityListDTOs { get; set; }

        public ObservableCollection<LeadActivityListDTO> LeadActivityListDTOsComplete { get; set; }

        #endregion Activity

        #region LeadInfo

        private LeadDTO leadDTO;

        public LeadDTO LeadDTO
        {
            get { return leadDTO; }
            set { SetProperty(ref leadDTO, value); }
        }

        public Guid LeadId { get; set; }

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

        private ValidationObject<string> email;

        public ValidationObject<string> Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private ValidationObject<MasterCenterDropdownDTO> advertisement;

        public ValidationObject<MasterCenterDropdownDTO> Advertisement
        {
            get { return advertisement; }
            set { SetProperty(ref advertisement, value); }
        }

        public bool DisplayUpcomming { get; set; }
        public bool DisplayCompleted { get; set; }
        public bool Displayline { get => DisplayCompleted && DisplayUpcomming; }

        #endregion LeadInfo

        public ObservableCollection<MasterCenterDropdownDTO> Advertisements { get; set; }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            CanDisplay = true;

            if (parameters.TryGetValue("RemoveLeadGuid", out Guid removeLeadGuid))
            {
                await NavigatedBack(parameters);
            }

            if (parameters.TryGetValue(KnownNavigationParameters.XamlParam, out Guid leadId))
            {
                IsBusy = true;
                LeadId = leadId;
                await LoadTemplate();
                await InitLeadInfo();
                await InitActivity();

                IsBusy = false;
            }
            if (parameters.TryGetValue("LeadActivityRefresh", out bool value))
            {
                if (value)
                {
                    IsBusy = true;
                    await InitActivity();
                    IsBusy = false;
                }
            }

            Telephone.ValueChanged -= Telephone_ValueChanged;
            Telephone.ValueChanged += Telephone_ValueChanged;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add("Refresh", IsSubmitted);
            base.OnNavigatedFrom(parameters);
        }

        public void InitValidation()
        {
            FirstName = new ValidationObject<string>();
            FirstName.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกนามสกุลด้วยภาษาไทยหรืออังกฤษ", IsThai = true, IsEng = true });
            FirstName.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณาระบุชื่อ" });

            LastName = new ValidationObject<string>();
            LastName.Validations.Add(new LanguageRule<string>() { ValidationMessage = "กรุณากรอกนามสกุลด้วยภาษาไทยหรืออังกฤษ", IsThai = true, IsEng = true });
            LastName.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณาระบุนามสกุล" });

            Email = new ValidationObject<string>();
            Email.Validations.Add(new EmailRule<string>() { ValidationMessage = "กรุณาระบุอิเมล์" });
            Email.ValueChanged += (s, e) => {
                if (!string.IsNullOrEmpty(Email.Value))
                    Email.Validate();
                else
                    Email.IsValid = true;
            };

            PhoneNumber = new ValidationObject<string>();
            PhoneNumber.Validations.Add(new IsNumberRule<string>() { ValidationMessage = "กรุณาระบุตัวเลข 0-9" });
            PhoneNumber.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "กรุณาระบุเบอร์โทรศัพท์" });

            Telephone = new ValidationObject<string>();
            Telephone.Validations.Add(new IsNumberRule<string>() { ValidationMessage = "กรุณาระบุตัวเลข 0-9" });

            Advertisement = new ValidationObject<MasterCenterDropdownDTO>();
            Advertisement.Validations.Add(new IsObjectNull<MasterCenterDropdownDTO>() { ValidationMessage = "กรุณาระบุสื่อ" });
        }

        private void Telephone_ValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Telephone.Value))
                Telephone.IsValid = true;
        }

        public bool Validate()
        {
            bool isValidFirstname = firstName.Validate();
            //bool isAdvertisement = advertisement.Validate();
            bool isValidLastname = lastName.Validate();
            bool isValidPhoneNo = PhoneNumber.Validate();
            //bool isValidEmail = Email.Validate();
            return isValidFirstname && isValidLastname && isValidPhoneNo;
        }

        private async Task InitLeadInfo()
        {
            var ads = await Task.Run(() => MasterCentersApi.GetMasterCenterDropdownList("Advertisement", null));
            foreach (var item in ads) { Advertisements.Add(item); }

            LeadDTO = await Task.Run(() => LeadsApi.GetLead(LeadId));
            LeadDTO.LeadScore = LeadDTO.LeadScore == null ? 0 : LeadDTO.LeadScore;
            FirstName.Value = leadDTO.FirstName;
            LastName.Value = leadDTO.LastName;
            PhoneNumber.Value = leadDTO.PhoneNumber;
            Telephone.Value = leadDTO.Telephone;
            Email.Value = leadDTO.Email;
            VisitZone = leadDTO.VisitZone;

            Remark = leadDTO.Remark;
            if (LeadDTO.Advertisement != null && LeadDTO.Advertisement.Id != null)
                Advertisement.Value = Advertisements.FirstOrDefault(t => t.Id == LeadDTO.Advertisement.Id);
        }

        private async Task InitActivity()
        {
            var leadActivityTemp = await Task.Run(() => LeadsApi.GetLeadActivityList(LeadDTO.Id));
            if (LeadActivityListDTOsComplete != null && LeadActivityListDTOsComplete.Count > 0)
                LeadActivityListDTOsComplete.Clear();

            if (LeadActivityListDTOs != null && LeadActivityListDTOs.Count > 0)
                LeadActivityListDTOs.Clear();
            foreach (var item in leadActivityTemp)
            {
                if (item.IsCompleted.Value)
                    LeadActivityListDTOsComplete.Add(item);
                else
                    LeadActivityListDTOs.Add(item);
            }

            UpdateDisplay();
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.

        private async Task LoadTemplate()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Template = new List<DataTemplate>()
                {
                     new DataTemplate(()=> new LeadGeneralView(this)),
                     new DataTemplate(()=> new LeadActivityView(this)),
                };
            });
        }

        private async void Edit(object obj)
        {
            NavigationParameters param = new NavigationParameters();
            var activity = obj as LeadActivityListDTO;
            if (activity.IsCompleted.Value)
                param.Add("IsCompleted", activity.IsCompleted);

            param.Add("ActivityId", activity.Id);
            param.Add("LeadId", LeadId);
            await NavigationService.NavigateAsync("LeadActivityForm", param);
        }

        private void CreateRequest()
        {
            LeadDTO.FirstName = this.firstName.Value;
            LeadDTO.LastName = this.lastName.Value;
            LeadDTO.Telephone = this.telephone.Value;
            LeadDTO.PhoneNumber = this.phoneNumber.Value;
            LeadDTO.VisitZone = this.visitZone;
            LeadDTO.Remark = this.remark;
            LeadDTO.Advertisement = Advertisement.Value;
            LeadDTO.Email = this.Email.Value;
        }

        private async void Submit()
        {
            try
            {
                if (!Validate())
                    return;

                if (!await CheckConnection())
                    return;

                
                IsBusy = true;
                CreateRequest();
                await Run(() => LeadsApi.EditLead(LeadId, LeadDTO));
                await DisplaySuccessPopup();
                await Task.Delay(450);
                await DialogService.CloseCurrentPopup();
                NavigationParameters param = new NavigationParameters();
                param.Add("UpdateLeadId", LeadId);
                await NavigatedBack(param);
                IsBusy = false;
                IsSubmitted = true;
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }

        }

        public async Task RemoveActivity(object obj)
        {
            var activity = obj as LeadActivityListDTO;
            if (activity == null)
                return;

            await ConfirmPopup(" ", $"คุณต้องการลบ [{activity.ActivityType.Name}] ใช่หรือไม่?", "ใช่", "ไม่ใช่", async () =>
            {
                try
                {
                    IsBusy = true;
                    await RunWithoutReturn(() => LeadsApi.DeleteLeadActivity(LeadId, activity.Id));
                    await DisplaySuccessPopup();
                    await InitActivity();
                }
                catch (ApiException e) { HandleException(e); }
                finally { IsBusy = false; }
            });
        }

        private void UpdateDisplay()
        {
            DisplayCompleted = LeadActivityListDTOsComplete.Count == 0 ? false : true;
            DisplayUpcomming = LeadActivityListDTOs.Count == 0 ? false : true;

            RaisePropertyChanged(nameof(DisplayUpcomming));
            RaisePropertyChanged(nameof(DisplayCompleted));
            RaisePropertyChanged(nameof(Displayline));
        }
    }

}