using CRMMobile.Helper;
using CRMMobile.Models;
using CRMMobile.Validations;
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

namespace CRMMobile.ViewModels
{
    public class LeadActivityFormViewModel : ViewModelBase
    {
        public LeadActivityFormViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            InitValidation();
            SubmitCommand = new DelegateCommand(async () => await Submit());
            ConvetienTime = new ObservableCollection<MasterCenterDropdownDTO>();
            ActivityStatus = new ObservableCollection<RadioModel<LeadActivityStatusDTO>>();
        }

        #region Services

        [Unity.Dependency]
        public ILeadsApi LeadsApi { get; set; }

        [Unity.Dependency]
        public IMasterCentersApi MasterCentersApi { get; set; }

        #endregion Services

        #region Commands

        public DelegateCommand<object> CheckCommand { get; set; }
        public DelegateCommand SubmitCommand { get; set; }

        #endregion Commands

        public LeadActivityStatusDTO SelectedActivityStatuse { get; set; }
        public ObservableCollection<MasterCenterDropdownDTO> ConvetienTime { get; set; }
        public ObservableCollection<RadioModel<LeadActivityStatusDTO>> ActivityStatus { get; set; }
        public MasterCenterDropdownDTO ActivityType { get; set; }
        public bool IsEnabled => FormMode != FormMode.View;
        public Guid LeadId { get; set; }
        public Guid ActivityId { get; set; }
        public FormMode FormMode { get; set; }
        public bool IsRefreshLeadActivtiy { get; set; }
        public bool CanSubmit { get; set; }
        public bool DisplayFollowUpDueDate { get; set; }

        private ValidationObject<MasterCenterDropdownDTO> convertienTimeSelected;

        public ValidationObject<MasterCenterDropdownDTO> ConvertienTimeSelected
        {
            get => convertienTimeSelected;
            set { SetProperty(ref convertienTimeSelected, value); }
        }

        private ValidationObject<DateTime?> dueDate;

        public ValidationObject<DateTime?> DueDate
        {
            get { return dueDate; }
            set { SetProperty(ref dueDate, value); }
        }

        private ValidationObject<DateTime?> actualDate;

        public ValidationObject<DateTime?> ActualDate
        {
            get { return actualDate; }
            set { SetProperty(ref actualDate, value); }
        }

        private ValidationObject<DateTime?> appointmentDate;

        public ValidationObject<DateTime?> AppointmentDate
        {
            get { return appointmentDate; }
            set { SetProperty(ref appointmentDate, value); }
        }

        private RadioModel<LeadActivityStatusDTO> correctNumber;

        public RadioModel<LeadActivityStatusDTO> CorrectNumber
        {
            get { return correctNumber; }
            set { SetProperty(ref correctNumber, value); }
        }

        private bool appointmentDateEnable;

        public bool AppointmentDateEnable
        {
            get { return appointmentDateEnable; }
            set { SetProperty(ref appointmentDateEnable, value); }
        }

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { SetProperty(ref remark, value); }
        }

        private DateTime? createDate;

        public DateTime? CreateDate
        {
            get { return createDate; }
            set { SetProperty(ref createDate, value); }
        }

        private string createBy;

        public string CreateBy
        {
            get { return createBy; }
            set { SetProperty(ref createBy, value); }
        }

        private DateTime? updateDate;

        public DateTime? UpdateDate
        {
            get { return updateDate; }
            set { SetProperty(ref updateDate, value); }
        }

        private string updateBy;

        public string UpdateBy
        {
            get { return updateBy; }
            set { SetProperty(ref updateBy, value); }
        }

        private bool isEnableActualDate;

        public bool IsEnableActualDate
        {
            get => isEnableActualDate;
            set { SetProperty(ref isEnableActualDate, value); }
        }

        private DateTime? followUpDueDate;
        public DateTime? FollowUpDaueDate
        {
            get => followUpDueDate;
            set { SetProperty(ref followUpDueDate, value); }
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.TryGetValue("LeadId", out Guid value))
            {
                LeadId = value;
            }
            if (parameters.TryGetValue("IsCompleted", out bool iscompleted))
            {
            }
            if (parameters.TryGetValue("ActivityId", out Guid activityIdParam))
            {
                ActivityId = activityIdParam;
            }

            if (LeadId != Guid.Empty && ActivityId != Guid.Empty && iscompleted)
            {
                FormMode = FormMode.View;
                Title = "Activity";
            }
            else if (LeadId != Guid.Empty && ActivityId != Guid.Empty && !iscompleted)
            {
                FormMode = FormMode.Edit;
                Title = "แก้ไข Activity";
            }
            else
            {
                FormMode = FormMode.Create;
                Title = "สร้าง Activity";
            }

            RaisePropertyChanged(nameof(IsEnabled));
            if (parameters.GetNavigationMode() == NavigationMode.New)
                await Init();
           
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            parameters.Add("LeadActivityRefresh", IsRefreshLeadActivtiy);
            base.OnNavigatedFrom(parameters);
        }

        private void SetSelectRadio(object value)
        {
            var id = (Guid)value;
            foreach (var item in ActivityStatus)
            {
                if (item.Id == id)
                {
                    item.IsSelected = true;
                    AppointmentDateEnable = true;
                    SelectedActivityStatuse = item.Value;
                }
                else
                {
                    item.IsSelected = false;
                    AppointmentDateEnable = false;
                }

                if (item.Value.LeadActivityStatusType.Key.Equals(LeadActivityStatusType.CorrectNumber))
                {
                    if (item.IsSelected)
                        item.IsEnable = true;
                    else
                        item.IsEnable = false;
                }

                if (item.IsSelected && item.Value.LeadActivityFollowUpType.Key.Equals(LeadActivityFollowUpType.FollowUp))
                    item.IsVisible = true;
                else
                    item.IsVisible = false;

                
            }

        }

        public async Task Init()
        {
            if (!await CheckConnection())
                return;

            LeadActivityDTO leadActivityDTO = null;
            IsBusy = true;
            var result = await Run(() => MasterCentersApi.GetMasterCenterDropdownList("ConvenientTime", null));
            foreach (var item in result) { ConvetienTime.Add(item); }

            if (FormMode != FormMode.Create)
                leadActivityDTO = await Run(() => LeadsApi.GetLeadActivity(LeadId, ActivityId));
            else
                leadActivityDTO = await Run(() => LeadsApi.GetLeadActivityDraft(LeadId));

            IsBusy = false;

            if (FormMode != FormMode.Create)
            {
                AppointmentDate.Value = leadActivityDTO.AppointmentDate;
                DueDate.Value = leadActivityDTO.DueDate;
                ActualDate.Value = leadActivityDTO.ActualDate;
                Remark = leadActivityDTO.Description;
                UpdateDate = leadActivityDTO.UpdatedDate;
                UpdateBy = leadActivityDTO.UpdatedBy;
                CreateBy = leadActivityDTO.CreatedBy;
                CreateDate = leadActivityDTO.CreatedDate;

                if (result != null)
                    ConvertienTimeSelected.Value = result.FirstOrDefault(t => t.Id == leadActivityDTO?.ConvenientTime?.Id);
            }
            else
            {
                DueDate.Value = DateTime.Now;
            }
            ActivityType = leadActivityDTO.ActivityType;
            RaisePropertyChanged("ActivityType");
            IsBusy = true;
            await Task.Run(() => PrepareActivityStatuses(leadActivityDTO.ActivityStatuses));
            IsBusy = false;

            if (leadActivityDTO.SelectedActivityStatusID != null && leadActivityDTO.SelectedActivityStatusID != Guid.Empty)
            {
                SetSelectRadio(leadActivityDTO.SelectedActivityStatusID);
            }
        }

        public void PrepareActivityStatuses(List<LeadActivityStatusDTO> activityStatuses)
        {
            try
            {
                bool incorrectFirst = true;
                bool cannotAppointment = true;
                List<RadioModel<LeadActivityStatusDTO>> tmp = new List<RadioModel<LeadActivityStatusDTO>>();
                foreach (var item in activityStatuses)
                {
                    RadioModel<LeadActivityStatusDTO> radio = null;
                    if (item.LeadActivityStatusType.Key.Equals(LeadActivityStatusType.CorrectNumber))
                    {
                        radio = new RadioModel<LeadActivityStatusDTO>() { Value = item, Id = item.Id.Value, Descsription = $"{item.StatusDescription } ({item.LeadActivityFollowUpType.Name})" };
                        radio.Title = "เบอร์ถูกต้องนัดหมายได้";
                        radio.IsEnable = false;
                    }
                    else
                    {
                        radio = new RadioModel<LeadActivityStatusDTO>() { Value = item, Id = item.Id.Value, Descsription = $"{item.StatusDescription } ({item.LeadActivityFollowUpType.Name})" };

                        if (item.LeadActivityStatusType.Key.Equals(LeadActivityStatusType.IncorrectNumber) && incorrectFirst)
                        {
                            incorrectFirst = false;
                            radio.Title = "เบอร์ไม่ถูกต้อง";
                        }
                        else if (item.LeadActivityStatusType.Key.Equals(LeadActivityStatusType.CannotAppointment) && cannotAppointment)
                        {
                            cannotAppointment = false;
                            radio.Title = "เบอร์ถูกต้องนัดหมายไม่ได้";
                        }
                    }

                    tmp.Add(radio);
                }

                var disqualify = tmp.FirstOrDefault(t => t.Value.LeadActivityStatusType.Key.Equals(LeadActivityStatusType.CannotAppointment)
                     && !t.Value.LeadActivityFollowUpType.Key.Equals(LeadActivityFollowUpType.FollowUp));
                disqualify.RadioAttribute = RadioAttribute.Underline;
                ActivityStatus = new ObservableCollection<RadioModel<LeadActivityStatusDTO>>(tmp);
                RaisePropertyChanged(nameof(ActivityStatus));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public bool Validate()
        {
            if (SelectedActivityStatuse == null)
            {
                bool dueDateIsValid = DueDate.Validate();
                //bool convertienTimeSelectedIsValid = ConvertienTimeSelected.Validate();
                //bool actualDateIsValid = ActualDate.Validate();
                return dueDateIsValid;
            }

            if (SelectedActivityStatuse.LeadActivityStatusType.Key == LeadActivityStatusType.CorrectNumber)
            {
                bool dueDateIsValid = DueDate.Validate();
                //bool convertienTimeSelectedIsValid = ConvertienTimeSelected.Validate();
                //bool appointmentDateIsValid = AppointmentDate.Validate();
                bool actualDateIsValid = ActualDate.Validate();
                return dueDateIsValid && actualDateIsValid;
            }
            else
            {
                AppointmentDate.IsValid = true;
                bool dueDateIsValid = DueDate.Validate();
                //bool convertienTimeSelectedIsValid = ConvertienTimeSelected.Validate();
                bool actualDateIsValid = ActualDate.Validate();
                return dueDateIsValid && actualDateIsValid;
            }
        }

        public void InitValidation()
        {
            CheckCommand = new DelegateCommand<object>(SetSelectRadio);
            DueDate = new ValidationObject<DateTime?>();
            ActualDate = new ValidationObject<DateTime?>();
            AppointmentDate = new ValidationObject<DateTime?>();
            ConvertienTimeSelected = new ValidationObject<MasterCenterDropdownDTO>();

            DueDate.Validations.Add(new IsObjectNull<DateTime?>() { ValidationMessage = "Due Date has required" });
            ActualDate.Validations.Add(new IsObjectNull<DateTime?>() { ValidationMessage = "Actual Date has required" });
            AppointmentDate.Validations.Add(new IsObjectNull<DateTime?>() { ValidationMessage = "Appointment Date has required" });
            ConvertienTimeSelected.Validations.Add(new IsObjectNull<MasterCenterDropdownDTO>() { ValidationMessage = "Convenient time has required" });
        }

        public async Task Submit()
        {
            if (!Validate())
                return;

            if (!await CheckConnection())
                return;

            try
            {
                CanSubmit = false;
                IsBusy = true;
                if (SelectedActivityStatuse!= null && SelectedActivityStatuse.LeadActivityFollowUpType.Key.Equals(LeadActivityFollowUpType.Disqualify))
                {
                    var lead = await GetLead();
                    var popup = new DisQualifyPopup(lead.FirstName + " " + lead.LastName, lead.PhoneNumber);
                    await DialogService.DisplayPopup(popup);
                    popup.OnConfirm += async (s, e) =>
                    {
                        if (!e)
                            return;

                        await CreateOrUodateActivity();
                        NavigationParameters param = new NavigationParameters();
                        param.Add("RemoveLeadGuid", LeadId);
                        await NavigatedBack(param);
                        
                    };
                }
                else
                {
                    await CreateOrUodateActivity();
                    await NavigatedBack();                    
                }

            }
            catch (ApiException e)
            {
                HandleException(e);
            }
            finally
            {
                CanSubmit = true;
                IsBusy = false;
            }
        }

        public LeadActivityDTO CreateLeadActivityDTO()
        {
            var leadAcitvity = new LeadActivityDTO()
            {
                ActivityType = ActivityType,
                DueDate = DueDate.Value,
                ConvenientTime = ConvertienTimeSelected.Value,
                Description = Remark,
                LeadID = LeadId,
                CreatedBy = CreateBy,
                CreatedDate = CreateDate,
                UpdatedDate = UpdateDate,
                UpdatedBy = UpdateBy,
                ActualDate = ActualDate.Value,
                FollowUpDueDate = FollowUpDaueDate
            };

            if (SelectedActivityStatuse != null)
            {
                if (SelectedActivityStatuse.LeadActivityStatusType.Key.Equals(LeadActivityStatusType.CorrectNumber))
                {
                    leadAcitvity.AppointmentDate = AppointmentDate.Value;
                }

                leadAcitvity.SelectedActivityStatusID = SelectedActivityStatuse.Id;
            }
            return leadAcitvity;
        }

        public async Task DisQualifyLead()
        {
            //Popup
            //SubmitLead
            //NavigateBack
        }

        public async Task<LeadDTO> GetLead()
        {
            return await Run(()=> LeadsApi.GetLead(LeadId));
        }

        public async Task<LeadActivityDTO> CreateOrUodateActivity()
        {
            var leatActivityData = CreateLeadActivityDTO();
            LeadActivityDTO result = null;


            if (FormMode == FormMode.Edit)
                result = await Run(() => LeadsApi.EditLeadActivity(LeadId, ActivityId, CreateLeadActivityDTO()));
            else
                result = await Run(() => LeadsApi.CreateLeadActivity(LeadId, leatActivityData));

            await DisplaySuccessPopup();
            await Task.Delay(550);
            await DialogService.CloseCurrentPopup();
            IsRefreshLeadActivtiy = true;
            return result;
        }
    }
}