using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class OpportunityActivityListDTO
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
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=OpportunityActivityType
        /// </summary>
        public MST.MasterCenterDropdownDTO OpportunityActivityType { get; set; }
        /// <summary>
        /// วันที่ทำจริง
        /// </summary>
        public DateTime? ActualDate { get; set; }
        /// <summary>
        /// วันที่ต้องทำ
        /// </summary>
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// วันที่นัดหมาย
        /// </summary>
        public DateTime? AppointmentDate { get; set; }
        /// <summary>
        /// Activity สำเร็จแล้วหรือไม่
        /// </summary>
        public bool IsCompleted { get; set; }

        public static OpportunityActivityListDTO CreateFromModel(models.CTM.OpportunityActivity model)
        {
            if (model != null)
            {
                OpportunityActivityListDTO result = new OpportunityActivityListDTO()
                {
                    Id = model.ID,
                    OpportunityID = model.OpportunityID,
                    OpportunityActivityType = MST.MasterCenterDropdownDTO.CreateFromModel(model.OpportunityActivityType),
                    ActualDate = model.ActualDate,
                    DueDate = model.DueDate,
                    AppointmentDate = model.AppointmentDate,
                    IsCompleted = model.IsCompleted
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
