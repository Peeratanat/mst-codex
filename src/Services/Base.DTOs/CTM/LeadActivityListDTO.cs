using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class LeadActivityListDTO
    {
        /// <summary>
        /// ID ของ Lead Activity
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ID ของ Lead
        /// </summary>
        public Guid? LeadID { get; set; }
        /// <summary>
        /// ประเภทของ Activity เช่น โทรติดตามลูกค้าครั้งที่ 3
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=LeadActivityType
        /// </summary>
        public MST.MasterCenterDropdownDTO ActivityType { get; set; }
        /// <summary>
        /// วันที่ต้องทำ
        /// </summary>
        public DateTime? DueDate { get; set; }
        /// <summary>
        /// วันที่ทำจริง
        /// </summary>
        public DateTime? ActualDate { get; set; }
        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// วันที่แก้ไข
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        /// <summary>
        /// Activity สำเร็จแล้วหรือไม่
        /// </summary>
        public bool IsCompleted { get; set; }

        public static LeadActivityListDTO CreateFromModel(models.CTM.LeadActivity model)
        {
            if (model != null)
            {
                LeadActivityListDTO result = new LeadActivityListDTO()
                {
                    Id = model.ID,
                    LeadID = model.LeadID,
                    ActivityType = MST.MasterCenterDropdownDTO.CreateFromModel(model.LeadActivityType),
                    DueDate = model.DueDate,
                    ActualDate = model.ActualDate,
                    CreatedDate = model.Created,
                    UpdatedDate = model.Updated,
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
