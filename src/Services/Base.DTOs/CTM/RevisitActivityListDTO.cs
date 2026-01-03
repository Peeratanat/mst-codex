using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class RevisitActivityListDTO
    {
        /// <summary>
        /// ID ของ Activity
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ID ของ Opportunity
        /// </summary>
        public Guid? OpportunityID { get; set; }
        /// <summary>
        /// ประเภทของ Activity
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=RevisitActivityType
        /// </summary>
        public MST.MasterCenterDropdownDTO ActivityType { get; set; }
        /// <summary>
        /// วันที่ Revisit
        /// </summary>
        public DateTime? ActualDate { get; set; }
        /// <summary>
        /// วันที่นัดหมาย
        /// </summary>
        public DateTime? AppointmentDate { get; set; }
        /// <summary>
        /// รายละเอียดเพิ่มเติม
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Activity สำเร็จแล้วหรือไม่
        /// </summary>
        public bool IsCompleted { get; set; }
        /// <summary>
        /// Activity การติดตาม (หมวดหมู่)
        /// </summary>
        public List<RevisitActivityStatusDTO> ActivityStatuses { get; set; }

        public static RevisitActivityListDTO CreateFromModel(models.CTM.RevisitActivity model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                RevisitActivityListDTO result = new RevisitActivityListDTO()
                {
                    Id = model.ID,
                    OpportunityID = model.OpportunityID,
                    ActivityType = MST.MasterCenterDropdownDTO.CreateFromModel(model.RevisitActivityType),
                    ActualDate = model.ActualDate,
                    AppointmentDate = model.AppointmentDate,
                    Description = model.Description,
                    IsCompleted = model.IsCompleted
                };

                var activityResultList = DB.RevisitActivityResults
                    .Include(o => o.RevisitActivityStatus)
                    .Where(o => o.RevisitAcitivityID == model.ID).ToList();

                result.ActivityStatuses = activityResultList.Select(o => RevisitActivityStatusDTO.CreateListFromModel(o)).ToList();

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
