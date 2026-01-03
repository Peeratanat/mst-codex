using CRMMobile.Helper;
using CRMMobile.Models;
using CRMMobile.Validations;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CRMMobile.ViewModels
{
    public class OpportunityActivityFormViewModel : ViewModelBase, IFormLifeCycle
    {
        public OpportunityActivityFormViewModel(INavigationService navigationService) : base(navigationService)
        {
            ActivityType = new ValidationObject<MasterCenterDropdownDTO>();
            DueDate = new ValidationObject<DateTime?>();
            ActualDate = new ValidationObject<DateTime?>();
            AppointmentDate = new ValidationObject<DateTime?>();
            ConvertienTimeSelected = new ValidationObject<MasterCenterDropdownDTO>();

            DueDate.Validations.Add(new IsObjectNull<DateTime?>() { ValidationMessage = "กรุณากรอก วันที่ต้องทำ" });
            ActualDate.Validations.Add(new IsObjectNull<DateTime?>() { ValidationMessage = "กรุณากรอก วันที่ทำจริง" });
            AppointmentDate.Validations.Add(new IsObjectNull<DateTime?>() { ValidationMessage = "กรุณากรอก วันที่นัดหมาย" });
            ActivityType.Validations.Add(new IsObjectNull<MasterCenterDropdownDTO>() { ValidationMessage = "กรุณาเลือก Activity type" });

            SaveCommad = new DelegateCommand(Save);
            ActivityTypes = new ObservableCollection<MasterCenterDropdownDTO>();
            ConvertienTime = new ObservableCollection<MasterCenterDropdownDTO>();
            CheckboxGroup = new CheckBoxGroup() { IsMultiple = true };
        }

        [Unity.Dependency]
        public IOpportunitiesApi OpportunitiesApi { get; set; }

        [Unity.Dependency]
        public IMasterCentersApi MasterCentersApi { get; set; }

        public Guid? OpportunityId { get; set; }
        public Guid? ActivityId { get; set; }
        public FormMode FormMode { get; set; }
        public DelegateCommand SubmitCommand { get; set; }
        public bool IsEnabled => FormMode != FormMode.View;
        public bool IsCompleted { get; set; }

        private CheckBoxGroup checkboxGroup;

        public CheckBoxGroup CheckboxGroup
        {
            get => checkboxGroup;
            set { SetProperty(ref checkboxGroup, value); }
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

        private ValidationObject<MasterCenterDropdownDTO> convertienTimeSelected;

        public ValidationObject<MasterCenterDropdownDTO> ConvertienTimeSelected
        {
            get => convertienTimeSelected;
            set { SetProperty(ref convertienTimeSelected, value); }
        }

        private ValidationObject<DateTime?> appointmentDate;

        public ValidationObject<DateTime?> AppointmentDate
        {
            get { return appointmentDate; }
            set { SetProperty(ref appointmentDate, value); }
        }

        private ValidationObject<MasterCenterDropdownDTO> activityType;

        public ValidationObject<MasterCenterDropdownDTO> ActivityType
        {
            get { return activityType; }
            set { SetProperty(ref activityType, value); }
        }

        private ValidationObject<OpportunityActivityStatusDTO> activityStatus;

        public ValidationObject<OpportunityActivityStatusDTO> ActivityStatus
        {
            get { return activityStatus; }
            set { SetProperty(ref activityStatus, value); }
        }

        private string remark;

        public string Remark
        {
            get { return remark; }
            set { SetProperty(ref remark, value); }
        }

        private string otherReason;

        public string OtherReason
        {
            get { return otherReason; }
            set { SetProperty(ref otherReason, value); }
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

        private ObservableCollection<MasterCenterDropdownDTO> activityTypes;

        public ObservableCollection<MasterCenterDropdownDTO> ActivityTypes
        {
            get { return activityTypes; }
            set { SetProperty(ref activityTypes, value); }
        }

        private OpportunityActivityStatusDTO activityEnd;

        public OpportunityActivityStatusDTO ActivityEnd
        {
            get { return activityEnd; }
            set { SetProperty(ref activityEnd, value); }
        }

        public ObservableCollection<MasterCenterDropdownDTO> ConvertienTime { get; set; }
        public DelegateCommand SaveCommad { get; set; }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue("OpportunityId", out Guid? value))
            {
                OpportunityId = value;
            }
            if (parameters.TryGetValue("ActivityId", out Guid? activityIdParam))
            {
                ActivityId = activityIdParam;
            }
            if (parameters.TryGetValue("IsCompleted", out bool iscompleted))
            {
                IsCompleted = iscompleted;
            }

            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                InitializeForm();
            }
        }

        public void UpdateFormMode()
        {
            if (IsCompleted)
                FormMode = FormMode.View;
            else if (ActivityId.HasValue)
                FormMode = FormMode.Edit;
            else
                FormMode = FormMode.Create;

            RaisePropertyChanged(nameof(IsEnabled));
        }

        public OpportunityActivityDTO CreateRequest()
        {
            OpportunityActivityDTO activityDTO = new OpportunityActivityDTO()
            {
                ActivityType = ActivityType.Value,
                AppointmentDate = AppointmentDate.Value,
                DueDate = DueDate.Value,
                ActualDate = ActualDate.Value,
                ConvenientTime = ConvertienTimeSelected.Value,
                Description = Remark,
                CreatedBy = CreateBy,
                UpdatedBy = UpdateBy,
                CreatedDate = CreateDate,
                UpdatedDate = UpdateDate,
                IsCompleted = IsCompleted,
                OpportunityID = OpportunityId
            };

            activityDTO.ActivityStatuses = new System.Collections.Generic.List<OpportunityActivityStatusDTO>();
            var activityStatusList = CheckboxGroup.Checkboxs.Where(t => t.IsCheck).ToList();
            foreach (var item in activityStatusList)
            {
                var _item = item.Value as OpportunityActivityStatusDTO;
                _item.IsSelected = item.IsCheck;
                if (_item.Code.Equals("-1"))
                {
                    _item.OtherReason = OtherReason;
                }
                activityDTO.ActivityStatuses.Add(_item);
            }

            if (ActivityEnd.IsSelected == true)
                activityDTO.ActivityStatuses.Add(ActivityEnd);

            return activityDTO;
        }

        public async void InitializeForm()
        {
            try
            {
                IsBusy = true;
                UpdateFormMode();
                OpportunityActivityDTO result = null;

                if (FormMode == FormMode.Create)
                    result = await Run(() => OpportunitiesApi.GetOpportunityActivityDraft(OpportunityId));
                else
                    result = await Run(() => OpportunitiesApi.GetOpportunityActivity(OpportunityId, ActivityId));

                SetupForm(result);
                RaisePropertyChanged(nameof(IsEnabled));

                var _conventionTime = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(MasterCenterKey.ConvenientTime, null));
                foreach (var item in _conventionTime) { ConvertienTime.Add(item); }
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public void SetupForm(params object[] arr)
        {
            OpportunityActivityDTO opportunity = arr[0] as OpportunityActivityDTO;

            if (opportunity == null) return;

            foreach (var activityType in opportunity.ActivityTypeDropdownItems)
            {
                ActivityTypes.Add(activityType);
            }

            OpportunityId = opportunity.OpportunityID;
            ActivityId = opportunity.Id;
            DueDate.Value = FormMode == FormMode.Create ? DateTime.Now : opportunity.DueDate;
            AppointmentDate.Value = opportunity.AppointmentDate;
            ActualDate.Value = opportunity.ActualDate;
            ActivityType.Value = opportunity.ActivityType;
            ConvertienTimeSelected.Value = opportunity.ConvenientTime;
            Remark = opportunity.Description;
            CreateBy = opportunity.CreatedBy;
            UpdateBy = opportunity.UpdatedBy;
            CreateDate = opportunity.CreatedDate;
            UpdateDate = opportunity.UpdatedDate;
            IsCompleted = opportunity.IsCompleted ?? false;

            var _activityEnd = opportunity.ActivityStatuses.FirstOrDefault(t => t.WalkActivityStatusType.Key.Equals(WalkActivityStatusType.ActivityEnd));
            ActivityEnd = _activityEnd;
            opportunity.ActivityStatuses.Remove(_activityEnd);
            CheckboxGroup.AddCheckboxValue(opportunity.ActivityStatuses, SelectedProperty: "IsSelected");
            var otherItem = opportunity.ActivityStatuses.FirstOrDefault(t => t.Code.Equals("-1"));
            if (otherItem != null)
                OtherReason = otherItem.OtherReason;
            UpdateFormMode();
        }

        public async void Save()
        {
            try
            {
                if (FormMode == FormMode.View)
                    return;

                IsBusy = true;

                bool activityTypeIsValid = ActivityType.Validate();
                if (!activityTypeIsValid)
                    return;

                bool dueDateIsValid = DueDate.Validate();
                if (!dueDateIsValid)
                    return;

                var hasActivityStatus = CheckboxGroup.Checkboxs.Any(t => t.IsCheck);
                var hasActivityStatusEnd = ActivityEnd.IsSelected != null ? ActivityEnd.IsSelected.Value == true ? true : false : false;

                if (hasActivityStatus || hasActivityStatusEnd)
                {
                    bool isValid = ActualDate.Validate();
                    if (!isValid)
                        return;
                }

                if (actualDate.Value != null)
                {
                    if (!hasActivityStatusEnd && !hasActivityStatus)
                    {
                        await DisplayAlert("แจ้งเตือน", "กรุณาเลือกผลการติดตาม", "ปิด");
                        return;
                    }
                }

                var request = CreateRequest();
                OpportunityActivityDTO opportunity = null;
                if (FormMode == FormMode.Create)
                {
                    opportunity = await Run(() => OpportunitiesApi.CreateOpportunityActivity(OpportunityId, request));
                    SetupForm(opportunity);
                }
                else if (FormMode == FormMode.Edit)
                {
                    opportunity = await Run(() => OpportunitiesApi.EditOpportunityActivity(OpportunityId, ActivityId, request));
                    SetupForm(opportunity);
                }

                await DisplaySuccessPopup(async () => await NavigatedBack());
                await Task.Delay(1000);
                await DialogService.CloseCurrentPopup();
                await NavigatedBack();
                //await DisplaySuccessPopup();
            }
            catch (ApiException e)
            {
                HandleException(e);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}