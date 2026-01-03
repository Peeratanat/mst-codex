using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("ใบเสนอราคา")]
    [Table("Quotation", Schema = Schema.SALE)]
    public class Quotation : BaseEntity
    {
        [Description("เลขที่ใบเสนอราคา")]
        [MaxLength(100)]
        public string QuotationNo { get; set; }
        [Description("วันที่เสนอ")]
        public DateTime? IssueDate { get; set; }

        [Description("สถานะใบเสนอราคา")]
        public Guid? QuotationStatusMasterCenterID { get; set; }
        [ForeignKey("QuotationStatusMasterCenterID")]
        public MST.MasterCenter QuotationStatus { get; set; }

        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("แปลง")]
        public Guid UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }

        [Description("วันที่ทำสัญญา")]
        public DateTime? ContractDate { get; set; }
        [Description("วันที่โอนกรรมสิทธิ์")]
        public DateTime? TransferOwnershipDate { get; set; }

        [Description("ผู้แนะนำ (สำหรับส่วนลด FGF)")]
        public Guid? ReferContactID { get; set; }
        [ForeignKey("ReferContactID")]
        public CTM.Contact ReferContact { get; set; }
        [Description("ชื่อผู้แนะนำ (สำหรับเก็บข้อมูล Free Text)")]
        [MaxLength(1000)]
        public string ReferContactName { get; set; }

        [Description("จากการ Prebook")]
        public bool IsPrebook { get; set; }

        [Description("RefQuotaionIDPrebook")]
        public Guid? RefQuotaionIDPrebook { get; set; }

        [Description("RefOnlinePaymentPrebookID")]
        public Guid? RefOnlinePaymentPrebookID { get; set; }
    }
}
