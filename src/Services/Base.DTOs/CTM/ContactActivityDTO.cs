using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Base.DTOs.CTM
{
    public class ContactActivityDTO
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

    }
}
