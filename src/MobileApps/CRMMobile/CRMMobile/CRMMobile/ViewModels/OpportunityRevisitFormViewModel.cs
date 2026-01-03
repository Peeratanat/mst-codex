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
    public class OpportunityRevisitFormViewModel : ViewModelBase
    {
        public OpportunityRevisitFormViewModel(INavigationService navigationService) : base(navigationService)
        {
            InitValidation();
            SubmitCommand = new DelegateCommand(Submit);
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
        //public bool IsEditing { get; set; }

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

        private string otherReason;

        public string OtherReason
        {
            get { return otherReason; }
            set { SetProperty(ref otherReason, value); }
        }

        public ObservableCollection<MasterCenterDropdownDTO> ConvertienTime { get; set; }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            FormMode = FormMode.Create;
            if (parameters.TryGetValue("OpportunityId", out Guid? value))
            {
                OpportunityId = value;
            }
            if (parameters.TryGetValue("ActivityId", out Guid? activityIdParam))
            {
                ActivityId = activityIdParam;
                if (ActivityId != null)
                    FormMode = FormMode.Edit;
            }
            if (parameters.TryGetValue("IsCompleted", out bool iscompleted))
            {
                IsCompleted = iscompleted;
                if (IsCompleted)
                    FormMode = FormMode.View;
            }

            await GetRevisit();
            RaisePropertyChanged(nameof(IsEnabled));
        }

        public async Task GetRevisit()
        {
            try
            {
                IsBusy = true;
                RevisitActivityDTO result = null;
                if (FormMode == FormMode.Create)
                    result = await Run(() => OpportunitiesApi.GetRevisitDraft(OpportunityId));
                else
                    result = await Run(() => OpportunitiesApi.GetRevisit(OpportunityId, ActivityId));

                var result3 = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(MasterCenterKey.ConvenientTime, null));
                var activityTypes = await Run(() => MasterCentersApi.GetMasterCenterDropdownList(MasterCenterKey.RevisitActivityType, null));
                foreach (var item in activityTypes)
                {
                    ActivityTypes.Add(item);
                }

                foreach (var item in result3)
                {
                    ConvertienTime.Add(item);
                }

                Setup(result);
                if (FormMode == FormMode.Create)
                {
                    DueDate.Value = DateTime.Now;
                    ActivityType.Value = result.ActivityType;
                }
            }
            catch (ApiException e)
            {
                HandleException(e);
            }
            finally { IsBusy = false; }
        }

        public void Setup(RevisitActivityDTO revisit)
        {
            AppointmentDate.Value = revisit.AppointmentDate;
            ActualDate.Value = revisit.ActualDate;
            ActivityType.Value = revisit.ActivityType;
            ConvertienTimeSelected.Value = revisit.ConvenientTime;
            Remark = revisit.Description;
            CreateBy = revisit.CreatedBy;
            UpdateBy = revisit.UpdatedBy;
            CreateDate = revisit.CreatedDate;
            UpdateDate = revisit.UpdatedDate;
            IsCompleted = revisit.IsCompleted ?? false;

            CheckboxGroup.AddCheckboxValue(revisit.ActivityStatuses, SelectedProperty: "IsSelected");
            var otherItem = revisit.ActivityStatuses.FirstOrDefault(t => t.Code.Equals("-1"));
            if (otherItem != null)
            {
                OtherReason = otherItem.OtherReason;
            }

            if (IsCompleted)
            {
                FormMode = FormMode.View;
            }
            else if (ActivityId.HasValue)
            {
                FormMode = FormMode.Edit;
            }
            else
            {
                FormMode = FormMode.Create;
            }

            RaisePropertyChanged(nameof(IsEnabled));
        }

        public void InitValidation()
        {
            ActivityType = new ValidationObject<MasterCenterDropdownDTO>();
            DueDate = new ValidationObject<DateTime?>();
            ActualDate = new ValidationObject<DateTime?>();
            AppointmentDate = new ValidationObject<DateTime?>();
            ConvertienTimeSelected = new ValidationObject<MasterCenterDropdownDTO>();

            DueDate.Validations.Add(new IsObjectNull<DateTime?>() { ValidationMessage = "กรุณากรอก วันที่ต้องทำ" });
            ActualDate.Validations.Add(new IsObjectNull<DateTime?>() { ValidationMessage = "กรุณาระบุข้อมูล วันที่ Revisit" });
            AppointmentDate.Validations.Add(new IsObjectNull<DateTime?>() { ValidationMessage = "กรุณากรอก วันที่นัดหมาย" });
            ActivityType.Validations.Add(new IsObjectNull<MasterCenterDropdownDTO>() { ValidationMessage = "กรุณาเลือก Activity type" });
        }

        public async void Submit()
        {
            try
            {
                IsBusy = true;

                bool hasRevisit = ActualDate.Validate();
                if (!hasRevisit)
                    return;

                bool activityTypeIsValid = ActivityType.Validate();
                if (!activityTypeIsValid)
                    return;

                var hasActivityStatus = CheckboxGroup.Checkboxs.Any(t => t.IsCheck);

                if (hasActivityStatus)
                {
                    bool isValid = ActualDate.Validate();
                    if (!isValid)
                        return;
                }

                if (FormMode == FormMode.Create)
                {
                    var result = await Run(() => OpportunitiesApi.CreateRevisit(OpportunityId, CreateRequest()));
                    FormMode = FormMode.Edit;
                    ActivityId = result.Id;

                    Setup(result);
                }
                else if (FormMode == FormMode.Edit)
                {
                    var result = await Run(() => OpportunitiesApi.EditOpportunityRevisit(OpportunityId, ActivityId, CreateRequest()));
                    Setup(result);
                }

                await DisplaySuccessPopup();
                await Task.Delay(500);
                await DialogService.CloseCurrentPopup();
                await NavigatedBack();
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

        public RevisitActivityDTO CreateRequest()
        {
            RevisitActivityDTO activityDTO = new RevisitActivityDTO()
            {
                ActivityType = ActivityType.Value,
                AppointmentDate = AppointmentDate.Value,
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

            activityDTO.ActivityStatuses = new System.Collections.Generic.List<RevisitActivityStatusDTO>();
            var activityStatusList = CheckboxGroup.Checkboxs.Where(t => t.IsCheck).ToList();
            foreach (var item in activityStatusList)
            {
                var _item = item.Value as RevisitActivityStatusDTO;
                _item.IsSelected = item.IsCheck;
                if (_item.Code.Equals("-1"))
                {
                    _item.OtherReason = OtherReason;
                }
                activityDTO.ActivityStatuses.Add(_item);
            }

            return activityDTO;
        }
    }
}