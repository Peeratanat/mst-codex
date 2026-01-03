using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ACC
{
    [Description("Temp ข้อมูลการ Post GL")]
    [Table("PostGLTemp", Schema = Schema.ACCOUNT)]
    public class PostGLTemp : BaseEntityWithoutMigrate
    {
        public Guid? SessionKey { get; set; }

        [Description("ID ของเอกสาร ใบเสร็จ, นำฝาก")]
        public Guid DocumentKey { get; set; }

        [Description("เลขที่เอกสาร ใบเสร็จ, นำฝาก")]
        [MaxLength(50)]
        public string DocumentText { get; set; }

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
        public decimal TotalFee { get; set; }

        [Description("ID ของรายการที่อ้างอิงการ Post GL")]
        public Guid? ReferentID { get; set; }

        [Description("แหล่งข้อมูลของ ReferentID = DepositHeader(PI),PaymentMethod(RV),UnknowPayment(UN),PostGLHeader(กรณี Type CA),ChangeUnitWorkflow(JV)")]
        [MaxLength(50)]
        public string ReferentType { get; set; }

        [Description("ประเภทรายการ Credit/Debit")]
        [MaxLength(50)]
        public string PostGLType { get; set; }

        [Description("PostingKey 21,31,40,50")]
        [MaxLength(50)]
        public string PostingKey { get; set; }

        [Description("ID GL Account")]
        public Guid? GLAccountID { get; set; }
        [ForeignKey("GLAccountID")]
        public MST.BankAccount GLAccount { get; set; }

        [Description("GLAccountCode")]
        [MaxLength(50)]
        public string GLAccountCode { get; set; }

        [Description("GLAccountName")]
        [MaxLength(50)]
        public string GLAccountName { get; set; }

        [Description("ID Format การ Gen text file ส่งไป SAP")]
        public Guid? FormatTextFileID { get; set; }

        [ForeignKey("FormatTextFileID")]
        public PostGLFormatTextFileHeader FormatTextFile { get; set; }

        [Description("จำนวนเงิน")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        [Description("ID Booking")]
        public Guid? BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("ValueDate")]
        public DateTime? ValueDate { get; set; }

        [Description("AccountCode")]
        [MaxLength(50)]
        public string AccountCode { get; set; }

        [Description("รหัสภาษี OX,VX")]
        [MaxLength(50)]
        public string TaxCode { get; set; }

        [Description("WBSNumber")]
        [MaxLength(50)]
        public string WBSNumber { get; set; }

        [Description("ProfitCenter")]
        [MaxLength(50)]
        public string ProfitCenter { get; set; }

        [Description("CostCenter")]
        [MaxLength(50)]
        public string CostCenter { get; set; }

        [Description("Quantity")]
        [MaxLength(50)]
        public string Quantity { get; set; }

        [Description("Assignment")]
        [MaxLength(50)]
        public string Assignment { get; set; }

        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("ProjectNo")]
        [MaxLength(50)]
        public string ProjectNo { get; set; }

        [Description("แปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }

        [Description("UnitNo")]
        [MaxLength(50)]
        public string UnitNo { get; set; }

        [Description("ObjectNumber")]
        [MaxLength(50)]
        public string ObjectNumber { get; set; }

        [Description("CustomerName")]
        [MaxLength(1000)]
        public string CustomerName { get; set; }

        [Description("Street")]
        [MaxLength(1000)]
        public string Street { get; set; }

        [Description("City")]
        [MaxLength(1000)]
        public string City { get; set; }

        [Description("PostCode")]
        [MaxLength(50)]
        public string PostCode { get; set; }

        [Description("Country")]
        [MaxLength(100)]
        public string Country { get; set; }

        [Description("รายละเอียด")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Description("DocType")]
        [MaxLength(10)]
        public string DocType { get; set; }

        [Description("TaxID")]
        [MaxLength(100)]
        public string TaxID { get; set; }

        [Description("ID Quotation")]
        public Guid? QuotationID { get; set; }

        [ForeignKey("QuotationID")]
        public SAL.Quotation Quotation { get; set; }
    }
}
