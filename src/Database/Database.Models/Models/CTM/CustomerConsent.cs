using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.CTM
{
    [Description("ประวัติการยินยอม")]
    [Table("CustomerConsentHistory", Schema = Schema.CUSTOMER)]
    public class CustomerConsentHistory
    {
        public CustomerConsentHistory()
        {
            if (ID == Guid.Empty)
                ID = Guid.NewGuid();

            if (Created == null)
                Created = DateTime.Now;
        }

        [Key]
        [Column(Order = 1)]
        public Guid ID { get; set; }

        [Column(Order = 100)]
        [Description("วันที่สร้างข้อมูล")]
        public DateTime? Created { get; set; }

        [Description("ประเภทข้อมูล Lead/Contact")]
        public Guid? ReferentTypeID { get; set; }
        [ForeignKey("ConsentReferentTypeMasterCenterID")]
        public MST.MasterCenter ConsentReferentType { get; set; }

        [Description("ID Lead/Contact")]
        public Guid? ReferentID { get; set; }

        [Description("ประเภทการยินยอม (ทุกโครงการ = AllProject, เฉพาะโครงการ = OnlyProject, ไม่ยินยอม = NotConsent)")]
        public Guid? OldConsentTypeMasterCenterID { get; set; }
        [ForeignKey("OldConsentTypeMasterCenterID")]
        public MST.MasterCenter OldConsentType { get; set; }

        [Description("ประเภทการยินยอม (ทุกโครงการ = AllProject, เฉพาะโครงการ = OnlyProject, ไม่ยินยอม = NotConsent)")]
        public Guid? NewConsentTypeMasterCenterID { get; set; }
        [ForeignKey("NewConsentTypeMasterCenterID")]
        public MST.MasterCenter NewConsentType { get; set; }

        [Description("รหัสพนักงงานที่แก้ไข")]
        [MaxLength(100)]
        public string EmployeeNo { get; set; }

        [Description("ชื่อ-สุกล พนักงานที่แก้ไข")]
        [MaxLength(100)]
        public string EmployeeName { get; set; }
    }
}
