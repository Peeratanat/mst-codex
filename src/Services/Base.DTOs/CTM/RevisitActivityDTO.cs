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
    public class RevisitActivityDTO
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
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=RevisitActivityType
        /// </summary>
        [Description("ประเภท Revisit")]
        public MST.MasterCenterDropdownDTO ActivityType { get; set; }
        /// <summary>
        /// วันที่ Revisit
        /// </summary>
        [Description("วันที่ Revisit")]
        public DateTime? ActualDate { get; set; }
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
        /// Activity การติดตาม
        /// </summary>
        public List<RevisitActivityStatusDTO> ActivityStatuses { get; set; }
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

        public async static Task<RevisitActivityDTO> CreateFromModelAsync(models.CTM.RevisitActivity model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                RevisitActivityDTO result = new RevisitActivityDTO()
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
                    ActivityType = MST.MasterCenterDropdownDTO.CreateFromModel(model.RevisitActivityType),
                    ConvenientTime = MST.MasterCenterDropdownDTO.CreateFromModel(model.ConvenientTime),
                    IsCompleted = model.IsCompleted
                };

                var activityStatusList = await DB.RevisitActivityStatuses.OrderBy(o => o.Order).ToListAsync();

                var activityResultList = await DB.RevisitActivityResults.Where(o => o.RevisitAcitivityID == model.ID).ToListAsync();
                result.ActivityStatuses = activityStatusList.Select(o => RevisitActivityStatusDTO.CreateFromModel(o, activityResultList)).ToList();

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<RevisitActivityDTO> CreateDraftFromModelAsync(models.CTM.RevisitActivity latestModel, models.DatabaseContext DB)
        {
            RevisitActivityDTO result = new RevisitActivityDTO();
            if (latestModel != null)
            {

                var lastActivityType = await DB.MasterCenters
                    .Where(o => o.MasterCenterGroupKey == "RevisitActivityType")
                    .OrderByDescending(o => o.Order)
                    .Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o))
                    .FirstOrDefaultAsync();

                var activityTypeOrder = await DB.MasterCenters
                    .Where(x => x.Key == latestModel.RevisitActivityType.Key && x.MasterCenterGroupKey == "RevisitActivityType")
                    .Select(x => x.Order).FirstOrDefaultAsync();

                var activityType = await DB.MasterCenters
                    .Where(x => x.Order == (activityTypeOrder + 1) && x.MasterCenterGroupKey == "RevisitActivityType")
                    .FirstOrDefaultAsync();

                if (activityType != null)
                {
                    result.ActivityType = MST.MasterCenterDropdownDTO.CreateFromModel(activityType);
                }
                else
                {
                    result.ActivityType = lastActivityType;
                }
            }
            else
            {
                var firstActivityType = await DB.MasterCenters
                    .Where(o => o.MasterCenterGroupKey == "RevisitActivityType")
                    .OrderBy(o => o.Order)
                    .Select(o => MST.MasterCenterDropdownDTO.CreateFromModel(o))
                    .FirstOrDefaultAsync();

                result.ActivityType = firstActivityType;
            }

            var activityStatusList = await DB.RevisitActivityStatuses.OrderBy(o => o.Order).ToListAsync();
            result.ActivityStatuses = activityStatusList.Select(o => RevisitActivityStatusDTO.CreateFromModel(o, null)).ToList();

            return result;
        }

        public async Task ValidateAsync(models.DatabaseContext DB)
        {
            ValidateException ex = new ValidateException();
            if (this.ActivityType == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RevisitActivityDTO.ActivityType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (this.ActualDate == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RevisitActivityDTO.ActualDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref models.CTM.RevisitActivity model)
        {
            model.OpportunityID = this.OpportunityID;
            model.AppointmentDate = this.AppointmentDate;
            model.Description = this.Description;
            model.ActualDate = this.ActualDate;
            model.RevisitActivityTypeMasterCenterID = this.ActivityType?.Id;
            model.ConvenientTimeMasterCenterID = this.ConvenientTime?.Id;
        }
    }
}
