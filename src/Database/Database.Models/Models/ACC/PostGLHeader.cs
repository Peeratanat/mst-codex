using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ACC
{
    [Description("Header ข้อมูลการ Post GL")]
    [Table("PostGLHeader", Schema = Schema.ACCOUNT)]
    public class PostGLHeader : BaseEntity
    {
        [Description("เลขที่เอกสาร PI,RV,JV,CA")]
        [MaxLength(50)]
        public string DocumentNo { get; set; }

        [Description("ประเภท Doc RV,JV,PI,CA")]
        public Guid? PostGLDocumentTypeMasterCenterID { get; set; }

        [ForeignKey("PostGLDocumentTypeMasterCenterID")]
        public MST.MasterCenter PostGLDocumentType { get; set; }

        [Description("ID Company")]
        public Guid? CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public MST.Company Company { get; set; }

        [Description("วันที่เอกสาร")]
        public DateTime DocumentDate { get; set; }

        [Description("วันที่ Posting date")]
        public DateTime PostingDate { get; set; }

        [Description("สถานะยกเลิก")]
        public bool IsCancel { get; set; }

        [Description("ยอดเงินที่ Post")]
        public decimal TotalAmount { get; set; }

        [Description("มูลค่า ค่าธรรมเนียมที่มีของ Doc นี้")]
        public decimal Fee { get; set; }

        [Description("แหล่งข้อมูลของ ReferentID = DepositHeader(PI),PaymentMethod(RV),UnknowPayment(UN),PostGLHeader(กรณี Type CA),ChangeUnitWorkflow(JV)")]
        [MaxLength(50)]
        public string ReferentType { get; set; }

        [Description("ID ของรายการที่อ้างอิงการ Post GL")]
        public Guid? ReferentID { get; set; }

        [Description("เลขที่เอกสาร ใบเสร็จ, นำฝาก, ยกเลิก ฯ")]
        [MaxLength(50)]
        public string ReferentNo { get; set; }

        [Description("จำนวนครั้งที่ Export text file ส่งไป SAP")]
        public int ExportedTimes { get; set; }

        [Description("วันที่ Export text file ส่งไป SAP ครั้งล่าสุด")]
        public DateTime? LastExportedDate { get; set; }

        [Description("เหตุผลการยกเลิกรายการ")]
        [MaxLength(1000)]
        public string DeleteReason { get; set; }

        [Description("ถ้า Row นี้เป็น Type CA หมายถึง DocumentNo ของรายการที่ยกเลิก / ถ้า Row นี้เป็น Type อื่นๆที่ไม่ใช่ CA หมายถึง รหัส CA ที่มายกเลิกรายการนี้")]
        [MaxLength(100)]
        public string PostGLDocumentNo { get; set; }

        [Description("ถ้า Row นี้เป็น Type CA หมายถึง วันที่ PostingDate ของรายการที่ยกเลิก / ถ้า Row นี้เป็น Type อื่นๆที่ไม่ใช่ CA หมายถึง วันที่ PostingDate ที่มายกเลิกรายการนี้")]
        public DateTime? PostGLDate { get; set; }

        [Description("รายละเอียด")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Description("DocumentType YB-รับเงินจากระบบ CRM,YC	-นำฝากจากระบบ CRM,YD-รายได้จากระบบ CRM,YE-ปรับปรุงจากระบบ CRM")]
        [MaxLength(50)]
        public string DocType { get; set; }
    }
}
