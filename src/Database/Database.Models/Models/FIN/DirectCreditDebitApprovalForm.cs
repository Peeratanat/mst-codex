using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("แบบฟอร์มอนุมัติ Direct Credit/Debit")]
    [Table("DirectCreditDebitApprovalForm", Schema = Schema.FINANCE)]
    public class DirectCreditDebitApprovalForm : BaseEntity
    {
        [Description("ผูกกับใบจอง ที่ทำสัญญาแล้ว")]
        public Guid BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("ชนิดของแบบฟอร์ม Credit,Debit")]
        public Guid? DirectApprovalFormTypeMasterCenterID { get; set; }
        [ForeignKey("DirectApprovalFormTypeMasterCenterID")]
        public MST.MasterCenter DirectApprovalFormType { get; set; }

        [Description("สถานะขออนุมัติ Direct Credit/Debit")]
        public Guid? DirectApprovalFormStatusMasterCenterID { get; set; }
        [ForeignKey("DirectApprovalFormStatusMasterCenterID")]
        public MST.MasterCenter DirectApprovalFormStatus { get; set; }

        [Description("เลขที่บัญชีบริษัท ที่จะตัดเงินเข้า")]
        public Guid? BankAccountID { get; set; }
        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }

        [Description("รอบการตัดเงิน วันที่ 1 หรือ 15")]
        public int? DirectPeriod { get; set; }

        [Description("ธนาคารลูกค้า")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public MST.Bank Bank { get; set; }

        [Description("สาขาธนาคาร")]
        public Guid? BankBranchID { get; set; }
        [ForeignKey("BankBranchID")]
        public MST.BankBranch BankBranch { get; set; }

        [Description("ชื่อสาขาธนาคาร")]
        public string BankBranchName { get; set; }

        [Description("จังหวัด")]
        public Guid? ProvinceID { get; set; }
        [ForeignKey("ProvinceID")]
        public MST.Province Province { get; set; }        

        [Description("เลขที่บัญชี/เลขที่บัตรเครดิต")]
        [MaxLength(50)]
        public string AccountNO { get; set; }

        [Description("ปีที่หมดอายุบัตรเครดิต")]
        public int? CreditCardExpireYear { get; set; }

        [Description("เดือนที่หมดอายุบัตรเครดิต")]
        public int? CreditCardExpireMonth { get; set; }

        [Description("ชื่อลูกค้าเจ้าของ บัญชี/บัตรเครดิต")]
        public string OwnerName { get; set; }

        [Description("เลขที่บัตรประชาชนลูกค้าเจ้าของ บัญชี/บัตรเครดิต")]
        [MaxLength(50)]
        public string CitizenIdentityNo { get; set; }

        [Description("วันที่เริ่มตัดเงิน")]
        public DateTime? StartDate { get; set; }

        [Description("วันที่ธนาคารอนุมัติ")]
        public DateTime? ApproveDate { get; set; }

        [Description("วันที่ ธนาคาร Reject Form")]
        public DateTime? RejectDate { get; set; }

        [Description("วันที่ยกเลิก")]
        public DateTime? CancelDate { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Description("ประเภทลูกค้า 0=ในประเทศ  1=ต่างประเทศ")]
        public bool IsForeigner { get; set; }
    }
}
