using Base.DTOs.PRJ;
using Database.Models.MasterKeys;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class OpportunityActivityDTO
    {
        /// <summary>
        /// ID ของ Activity
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ID ของ Opportunity
        /// </summary>
        public Guid OpportunityID { get; set; }
        /// <summary>
        /// ประเภท Activity
        /// </summary>
        [Description("ประเภท Activity Opportunity")]
        public MST.MasterCenterDropdownDTO ActivityType { get; set; }
        /// <summary>
        /// Dropdown List ของ Activity type (Walk) ทั้งหมด
        /// </summary>
        [Description("Dropdown List ของ Activity type (Walk) ")]
        public List<MST.MasterCenterDropdownDTO> ActivityTypeDropdownItems { get; set; }
        /// <summary>
        /// วันที่ทำจริง
        /// </summary>
        [Description("วันที่ทำจริง")]
        public DateTime? ActualDate { get; set; }
        /// <summary>
        /// วันที่ต้องทำ
        /// </summary>
        [Description("วันที่ต้องทำ")]
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// เวลาที่สะดวกติดต่อกลับ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ConvenientTime
        /// </summary>
        public MST.MasterCenterDropdownDTO ConvenientTime { get; set; }
        /// <summary>
        /// วันที่นัดหมาย
        /// </summary>
        public DateTime? AppointmentDate { get; set; }
        /// <summary>
        /// รายละเอียด
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// ผลการติดตาม
        /// </summary>
        [Description("ผลการติดตาม")]
        public List<OpportunityActivityStatusDTO> ActivityStatuses { get; set; }
        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// สร้างโดย
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// วันที่แก้ไข
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        /// <summary>
        /// แก้ไขโดย
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Activity สำเร็จแล้วหรือไม่
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Project
        /// </summary>
        public ProjectDropdownDTO Project { get; set; }


        public async static Task<OpportunityActivityDTO> CreateFromModelAsync(models.CTM.OpportunityActivity model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var OppActMaster = await DB.MasterCenters.Where(o => o.Key == "8" && o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType).Select(o => o.ID).FirstOrDefaultAsync();
                var OppAct = await DB.OpportunityActivities.Where(o => o.ID == model.ID && o.OpportunityActivityTypeMasterCenterID == OppActMaster).OrderByDescending(o => o.Created).Select(o => o.ID).FirstOrDefaultAsync();

                var lead = await DB.Leads.Where(o => o.RefID == OppAct.ToString()).FirstOrDefaultAsync();
                var Opportunity = await DB.Opportunities.Where(o => o.ID == model.OpportunityID).FirstOrDefaultAsync();
                //if (lead == null)
                //{
                //    var ProjectID = Opportunity.ProjectID;
                //} else
                //{
                //    var ProjectID = lead.ProjectID;
                //}
                var ProjectID = lead == null ? Opportunity.ProjectID : lead.ProjectID;
                var Project = await DB.Projects.Where(o => o.ID == ProjectID).FirstOrDefaultAsync();

                OpportunityActivityDTO result = new OpportunityActivityDTO()
                {
                    Id = model.ID,
                    OpportunityID = model.OpportunityID,
                    AppointmentDate = model.AppointmentDate,
                    Description = model.Description,
                    CreatedDate = model.Created,
                    CreatedBy = model.CreatedBy?.DisplayName,
                    UpdatedDate = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    ActualDate = model.ActualDate,
                    DueDate = model.DueDate,
                    ActivityType = MST.MasterCenterDropdownDTO.CreateFromModel(model.OpportunityActivityType),
                    ConvenientTime = MST.MasterCenterDropdownDTO.CreateFromModel(model.ConvenientTime),
                    IsCompleted = model.IsCompleted,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(Project)
                };

                var activityStatusList = await DB.OpportunityActivityStatuses
                    .Include(o => o.WalkActivityStatusType)
                    .OrderBy(o => o.Order)
                    .ToListAsync();

                var activityResultList = await DB.OpportunityActivityResults.Where(o => o.OpportunityAcitivityID == model.ID).ToListAsync();
                result.ActivityStatuses = activityStatusList.Select(o => OpportunityActivityStatusDTO.CreateFromModel(o, activityResultList)).ToList();

                List<int> selectedActivityOrder = new List<int>() { 2, 7 };
                List<int> callActivityOrder = new List<int>() { 3, 4, 5, 6 };

                var isPlanActivity = await DB.OpportunityActivities
                   .Include(o => o.OpportunityActivityType)
                   .Where(o => o.OpportunityID == model.OpportunityID && o.OpportunityActivityType.Key == "1")
                   .AnyAsync();

                if (!isPlanActivity)
                {
                    selectedActivityOrder.Add(1);
                }

                selectedActivityOrder.Add(model.OpportunityActivityType.Order);

                result.ActivityTypeDropdownItems = await DB.MasterCenters
                        .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && selectedActivityOrder.Contains(o.Order))
                        .OrderBy(o => o.Order)
                        .Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o))
                        .ToListAsync();

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<OpportunityActivityDTO> CreateDraftFromModelAsync(models.CTM.OpportunityActivity latestModel, models.DatabaseContext DB)
        {
            OpportunityActivityDTO result = new OpportunityActivityDTO();

            if (latestModel != null)
            {
                var activityTypeOrder = await DB.MasterCenters
                    .Where(x => x.Key == latestModel.OpportunityActivityType.Key && x.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType)
                    .Select(x => x.Order).FirstOrDefaultAsync();

                var nextOrder = (activityTypeOrder == 6) ? activityTypeOrder : activityTypeOrder + 1;
                var activityType = await DB.MasterCenters
                    .Where(o => (o.Order == nextOrder) && o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType)
                    .FirstOrDefaultAsync();

                List<int> selectedActivityOrder = new List<int>() { 2, 7, 8 }; ////
                List<int> callActivityOrder = new List<int>() { 3, 4, 5, 6 };

                var isPlanActivity = await DB.OpportunityActivities
                   .Include(o => o.OpportunityActivityType)
                   .Where(o => o.OpportunityID == latestModel.OpportunityID && o.OpportunityActivityType.Key == "1")
                   .AnyAsync();

                if (!isPlanActivity)
                {
                    selectedActivityOrder.Add(1);
                }

                if (activityType != null)
                {
                    result.ActivityType = MST.MasterCenterDropdownDTO.CreateFromModel(activityType);
                    if (callActivityOrder.Contains(activityType.Order))
                    {
                        selectedActivityOrder.Add(activityType.Order);
                    }
                    else
                    {
                        selectedActivityOrder.Add(3);
                        
                    }

                    var callActivities = await DB.OpportunityActivities
                        .Include(o => o.OpportunityActivityType)
                        .Where(o => o.OpportunityID == latestModel.OpportunityID && o.OpportunityActivityType.Order < activityType.Order)
                        .Select(o => o.OpportunityActivityType.Order)
                        .ToListAsync();

                    List<int> callOrder = new List<int>() { 3, 4, 5 };
                    foreach (var item in callOrder)
                    {
                        if(item < activityType.Order)
                        {
                            var order = await DB.OpportunityActivities
                            .Include(o => o.OpportunityActivityType)
                            .Where(o => o.OpportunityID == latestModel.OpportunityID && o.OpportunityActivityType.Order == item)
                            .AnyAsync();

                            if (!order)
                            {
                                selectedActivityOrder.Add(item);
                            }
                        }
                    }
                }
                else
                {
                    var activityType2 = await DB.MasterCenters
                    .Where(o => (o.Order == 3) && o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType)
                    .FirstOrDefaultAsync();
                    result.ActivityType = MST.MasterCenterDropdownDTO.CreateFromModel(activityType2);

                    if (callActivityOrder.Contains(activityType2.Order))
                    {
                        selectedActivityOrder.Add(activityType2.Order);
                    }
                    else
                    {
                        selectedActivityOrder.Add(3);

                    }

                    var callActivities = await DB.OpportunityActivities
                        .Include(o => o.OpportunityActivityType)
                        .Where(o => o.OpportunityID == latestModel.OpportunityID && o.OpportunityActivityType.Order < activityType2.Order)
                        .Select(o => o.OpportunityActivityType.Order)
                        .ToListAsync();

                    List<int> callOrder = new List<int>() { 3, 4, 5 };
                    foreach (var item in callOrder)
                    {
                        if (item < activityType2.Order)
                        {
                            var order = await DB.OpportunityActivities
                            .Include(o => o.OpportunityActivityType)
                            .Where(o => o.OpportunityID == latestModel.OpportunityID && o.OpportunityActivityType.Order == item)
                            .AnyAsync();

                            if (!order)
                            {
                                selectedActivityOrder.Add(item);
                            }
                        }
                    }
                }

                result.ActivityTypeDropdownItems = await DB.MasterCenters
                        .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && selectedActivityOrder.Contains(o.Order))
                        .OrderBy(o => o.Order)
                        .Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o))
                        .ToListAsync();
            }
            else
            {
                var firstActivityType = await DB.MasterCenters
                    .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType)
                    .OrderBy(o => o.Order)
                    .Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o))
                    .FirstOrDefaultAsync();

                result.ActivityType = firstActivityType;

                List<int> selectedActivityOrder = new List<int>() { 1, 2, 3, 7, 8 };
                result.ActivityTypeDropdownItems = await DB.MasterCenters
                    .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && selectedActivityOrder.Contains(o.Order))
                    .OrderBy(o => o.Order)
                    .Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o))
                    .ToListAsync();
            }

            var activityStatusQuery = await DB.OpportunityActivityStatuses
                    .Include(o => o.WalkActivityStatusType)
                    .OrderBy(o => o.Order)
                    .ToListAsync();

            result.ActivityStatuses = activityStatusQuery.Select(o => OpportunityActivityStatusDTO.CreateFromModel(o, null)).ToList();

            return result;
        }

        public async Task ValidateAsync(models.DatabaseContext DB)
        {
            ValidateException ex = new ValidateException();
            if (this.ActivityType == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(OpportunityActivityDTO.ActivityType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.DueDate == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(OpportunityActivityDTO.DueDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.ActivityStatuses != null)
            {
                var isSelected = this.ActivityStatuses.Where(o => o.IsSelected == true).Any();

                if (isSelected)
                {
                    if (this.ActualDate == null)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(OpportunityActivityDTO.ActualDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                else
                {
                    var planActivityId = await DB.MasterCenters
                        .Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.OpportunityActivityType && o.Key == "1")
                        .Select(o => o.ID).FirstAsync();

                    if(this.ActualDate != null && this.ActivityType.Id != planActivityId)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(OpportunityActivityDTO.ActivityStatuses)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref models.CTM.OpportunityActivity model)
        {
            model.OpportunityID = this.OpportunityID;
            model.AppointmentDate = this.AppointmentDate;
            model.Description = this.Description;
            model.ActualDate = this.ActualDate;
            model.OpportunityActivityTypeMasterCenterID = this.ActivityType?.Id;
            model.ConvenientTimeMasterCenterID = this.ConvenientTime?.Id;
            model.DueDate = this.DueDate;
            if (this.ActualDate != null)
            {
                model.IsCompleted = true;
            }
            else
            {
                model.IsCompleted = false;
            }
        }
    }
}