using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.ACC
{
    [Description("Detail ข้อมูลการ Post GL")]
    [Table("PostGLDetail", Schema = Schema.ACCOUNT)]
    public class PostGLDetail : BaseEntity
    {
        [Description("ID Header Post GL")]
        public Guid? PostGLHeaderID { get; set; }

        [ForeignKey("PostGLHeaderID")]
        public PostGLHeader PostGLHeader { get; set; }

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

        [Description("ชื่อ GL Account")]
        [MaxLength(1000)]
        public string AccountName { get; set; }

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

        [Description("Unit")]
        [MaxLength(50)]
        public string Unit { get; set; }

        [Description("Assignment")]
        [MaxLength(50)]
        public string Assignment { get; set; }

        [Description("ProjectNo")]
        [MaxLength(50)]
        public string ProjectNo { get; set; }

        [Description("UnitNo")]
        [MaxLength(50)]
        public string UnitNo { get; set; }

        [Description("ObjectNumber")]
        [MaxLength(50)]
        public string ObjectNumber { get; set; }

        [Description("CustomerName")]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [Description("Street")]
        [MaxLength(100)]
        public string Street { get; set; }

        [Description("City")]
        [MaxLength(100)]
        public string City { get; set; }

        [Description("PostCode")]
        [MaxLength(50)]
        public string PostCode { get; set; }

        [Description("Country")]
        [MaxLength(100)]
        public string Country { get; set; }

        [Description("เก็บค่าอื่นๆ")]
        [MaxLength(100)]
        public string Text1 { get; set; }

        [Description("เก็บค่าอื่นๆ")]
        [MaxLength(100)]
        public string Text2 { get; set; }

        [Description("เก็บค่าอื่นๆ")]
        [MaxLength(100)]
        public string Text3 { get; set; }

        [Description("TaxID เลขประจำตัวผู้เสียภาษีของบริษัท")]
        [MaxLength(50)]
        public string TaxID { get; set; }

        [Description("BankBranch สาขาธนาคาร")]
        [MaxLength(50)]
        public string BankBranch { get; set; }

        [Description("BankAccount เลขที่บัญชี")]
        [MaxLength(50)]
        public string BankAccount { get; set; }

        [Description("ID Quotation")]
        public Guid? QuotationID { get; set; }

        [ForeignKey("QuotationID")]
        public SAL.Quotation Quotation { get; set; }
    }
}
