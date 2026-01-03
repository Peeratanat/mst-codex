using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("โอนกรรมสิทธิ์")]
    [Table("Transfer", Schema = Schema.SALE)]
    public class Transfer : BaseEntity
    {
        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("แปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }

        [Description("เลขที่โอนกรรมสิทธิ์")]
        [MaxLength(100)]
        public string TransferNo { get; set; }

        [Description("เลขที่สัญญา")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public Agreement Agreement { get; set; }

        [Description("พื้นที่ (ตร.ว/ตร.ม)")]
        public double? StandardArea { get; set; }
        [Description("พื้นที่ที่ใช้คำนวนราคาประเมิณ")]
        public double? LandArea { get; set; }
        [Description("ราคาประเมิณ")]
        [Column(TypeName = "Money")]
        public decimal? LandEstimatePrice { get; set; }
  
        [Description("LC โอน")]
        public Guid? TransferSaleUserID { get; set; }
        [ForeignKey("TransferSaleUserID")]
        public USR.User TransferSale { get; set; }

        [Description("วันที่นัดโอนกรรมสิทธื์")]
        public DateTime? ScheduleTransferDate { get; set; }
        [Description("วันที่โอนจริง")]
        public DateTime? ActualTransferDate { get; set; }

        //รายละเอียดค่าธรรมเนียม
        [Description("ภาษีเงินได้นิติบุคคล")]
        [Column(TypeName = "Money")]
        public decimal? CompanyIncomeTax { get; set; }
        [Description("ภาษีเงินได้ธุรกิจเฉพาะ")]
        [Column(TypeName = "Money")]
        public decimal? BusinessTax { get; set; }
        [Description("ภาษีท้องถิ่น")]
        [Column(TypeName = "Money")]
        public decimal? LocalTax { get; set; }
        [Description("ผู้ใช้งาน P.Card")]
        public Guid? PCardUserID { get; set; }
        [ForeignKey("PCardUserID")]
        public USR.User PCardUser { get; set; }
        [Description("รูดบัตร P.Card กระทรวงการคลัง")]
        [Column(TypeName = "Money")]
        public decimal? MinistryPCard { get; set; }
        [Description("เงินสดหรือเช็คกระทรวงการคลัง")]
        [Column(TypeName = "Money")]
        public decimal? MinistryCashOrCheque { get; set; }
        [Description("เช็คค่ามิเตอร์")]
        public Guid? MeterChequeMasterCenterID { get; set; }
        [ForeignKey("MeterChequeMasterCenterID")]
        public MST.MasterCenter MeterCheque { get; set; }
        [Description("ลูกค้าจ่ายค่าจดจำนอง")]
        [Column(TypeName = "Money")]
        public decimal? CustomerPayMortgage { get; set; }
        [Description("ลูกค้าจ่ายค่าธรรมเนียม")]
        [Column(TypeName = "Money")]
        public decimal? CustomerPayFee { get; set; }
        [Description("บริษัทจ่ายค่าธรรมเนียม")]
        [Column(TypeName = "Money")]
        public decimal? CompanyPayFee { get; set; }
        [Description("ฟรีค่าธรรมเนียม")]
        [Column(TypeName = "Money")]
        public decimal? FreeFee { get; set; }
        [Description("ค่าดำเนินการเอกสาร (ขาด/เกิน)")]
        [Column(TypeName = "Money")]
        public decimal? DocumentFee { get; set; }

        //ยอดคงเหลือ
        [Description("ยอดคงเหลือ AP")]
        [Column(TypeName = "Money")]
        public decimal? APBalance { get; set; }
        [Description("ยกยอดไปนิติบุคคล")]
        public bool IsAPBalanceTransfer { get; set; }
        [Description("ยอดคงเหลือ AP ยกยอดไปนิติบุคคล")]
        [Column(TypeName = "Money")]
        public decimal? APBalanceTransfer { get; set; }
        [Description("เงินทอนก่อนโอน")]
        [Column(TypeName = "Money")]
        public decimal? APChangeAmountBeforeTransfer { get; set; }
        [Description("รวมเงินทอน AP")]
        [Column(TypeName = "Money")]
        public decimal? APChangeAmount { get; set; }
        [Description("การทอนคืน AP")]
        public bool? IsAPGiveChange { get; set; }

        [Description("เงินทอน AP จ่ายด้วย")]
        public Guid? APPayWithMemoMasterCenterID { get; set; }
        [ForeignKey("APPayWithMemoMasterCenterID")]
        public MST.MasterCenter APPayWithMemo { get; set; }

        [Description("เงินรอรับชำระจากการตั้งพัก")]
        [Column(TypeName = "Money")]
        public decimal? SuspenseAmount { get; set; }
        [Description("ยกยอดไปเป็นเงินทอน (เงินตั้งพัก)")]
        public bool? IsSuspenseChange { get; set; }

        [Description("ยอดคงเหลือนิติบุคคล")]
        [Column(TypeName = "Money")]
        public decimal? LegalEntityBalance { get; set; }
        [Description("ยกยอดไป AP")]
        public bool IsLegalEntityBalanceTransfer { get; set; }
        [Description("ยอดคงเหลือนิติบุคคล ยกยอดไป AP")]
        [Column(TypeName = "Money")]
        public decimal? LegalEntityBalanceTransfer { get; set; }
        [Description("รวมเงินทอน นิติบุคคล")]
        [Column(TypeName = "Money")]
        public decimal? LegalEntityChangeAmount { get; set; }
        [Description("การทอนคืนนิติบุคคล")]
        public bool? IsLegalEntityGiveChange { get; set; }

        [Description("เงินทอนนิติบุคคล จ่ายด้วย")]
        public Guid? LegalEntityPayWithMemoMasterCenterID { get; set; }
        [ForeignKey("LegalEntityPayWithMemoMasterCenterID")]
        public MST.MasterCenter LegalEntityPayWithMemo { get; set; }

        //เงินสดย่อย
        [Description("รวมรับเงินสดย่อย")]
        [Column(TypeName = "Money")]
        public decimal? PettyCashAmount { get; set; }
        [Description("ค่าเดินทางไป")]
        [Column(TypeName = "Money")]
        public decimal? GoTransportAmount { get; set; }
        [Description("ค่าเดินทางกลับ")]
        [Column(TypeName = "Money")]
        public decimal? ReturnTransportAmount { get; set; }
        [Description("ค่าเดินทางระหว่าง สนง. ที่ดิน")]
        [Column(TypeName = "Money")]
        public decimal? LandOfficeTransportAmount { get; set; }
        [Description("ค่าทางด่วนไป")]
        [Column(TypeName = "Money")]
        public decimal? GoTollWayAmount { get; set; }
        [Description("ค่าทางด่วนกลับ")]
        [Column(TypeName = "Money")]
        public decimal? ReturnTollWayAmount { get; set; }
        [Description("ค่าทางด่วนระหว่าง สนง. ที่ดิน")]
        [Column(TypeName = "Money")]
        public decimal? LandOfficeTollWayAmount { get; set; }
        [Description("รับรองเจ้าหน้าที่")]
        [Column(TypeName = "Money")]
        public decimal? SupportOfficerAmount { get; set; }
        [Description("ค่าถ่ายเอกสาร")]
        [Column(TypeName = "Money")]
        public decimal? CopyDocumentAmount { get; set; }

        //สถานะ
        [Description("พร้อมโอน")]
        public bool IsReadyToTransfer { get; set; }
        [Description("ผู้กดพร้อมโอน")]
        public Guid? ReadyToTransferUserID { get; set; }
        [ForeignKey("ReadyToTransferUserID")]
        public USR.User ReadyToTransferUser { get; set; }
        [Description("วันที่พร้อมโอน")]
        public DateTime? ReadyToTransferDate { get; set; }

        [Description("ยืนยันโอนจริง")]
        public bool IsTransferConfirmed { get; set; }
        [Description("ผู้กดยืนยันโอนจริง")]
        public Guid? TransferConfirmedUserID { get; set; }
        [ForeignKey("TransferConfirmedUserID")]
        public USR.User TransferConfirmedUser { get; set; }
        [Description("วันที่ยืนยันโอนจริง")]
        public DateTime? TransferConfirmedDate { get; set; }

        //[Description("นำส่งการเงิน")]
        //public bool IsSentToFinance { get; set; }
        //[Description("ผู้กดนำส่งการเงิน")]
        //public Guid? SentToFinanceUserID { get; set; }
        //[ForeignKey("SentToFinanceUserID")]
        //public USR.User SentToFinanceUser { get; set; }
        //[Description("วันที่นำส่งการเงิน")]
        //public DateTime? SentToFinanceDate { get; set; }

        [Description("ยืนยันชำระเงิน")]
        public bool IsPaymentConfirmed { get; set; }
        [Description("ผู้กดยืนยันชำระเงิน")]
        public Guid? PaymentConfirmedUserID { get; set; }
        [ForeignKey("PaymentConfirmedUserID")]
        public USR.User PaymentConfirmedUser { get; set; }
        [Description("วันที่ยืนยันชำระเงิน")]
        public DateTime? PaymentConfirmedDate { get; set; }

        [Description("บัญชีอนุมัติ")]
        public bool IsAccountApproved { get; set; }
        [Description("ผู้กดบัญชีอนุมัติ")]
        public Guid? AccountApprovedUserID { get; set; }
        [ForeignKey("AccountApprovedUserID")]
        public USR.User AccountApprovedUser { get; set; }
        [Description("วันที่บัญชีอนุมัติ")]
        public DateTime? AccountApprovedDate { get; set; }

        [Description("สถานะโอนกรรมสิทธิ์")]
        public Guid? TransferStatusMasterCenterID { get; set; }
        [ForeignKey("TransferStatusMasterCenterID")]
        public MST.MasterCenter TransferStatus { get; set; }

        //สรุปรับเงิน
        [Description("ค่าบ้าน (เงินที่เรียกเก็บ)")]
        [Column(TypeName = "Money")]
        public decimal? APAmount { get; set; }
        [Description("ค่ามิเตอร์ (เงินที่เรียกเก็บ)")]
        [Column(TypeName = "Money")]
        public decimal? MeterAmount { get; set; }
        [Description("ค่าเงินจ่ายที่ดิน (เงินที่เรียกเก็บ)")]
        [Column(TypeName = "Money")]
        public decimal? LandAmount { get; set; }
        [Description("ค่าสาธารณูปโภค (เงินที่เรียกเก็บ)")]
        [Column(TypeName = "Money")]
        public decimal? LegalEntityAmount { get; set; }
        [Description("รวมเงินที่ชำระมาแล้ว")]
        [Column(TypeName = "Money")]
        public decimal TotalPaidAmount { get; set; }
        [Description("ค่าส่วนกลาง")]
        [Column(TypeName = "Money")]
        public decimal CommonFeeCharge { get; set; }
        [Description("ค่ามิเตอร์")]
        [Column(TypeName = "Money")]
        public decimal MeterAmountCharge { get; set; }
        [Description("ค่่าจ่ายทีดิน")]
        [Column(TypeName = "Money")]
        public decimal LandAmountCharge { get; set; }
        [Description("รวมเงินที่เก็บจากลูกค้า")]
        [Column(TypeName = "Money")]
        public decimal? TotalCustomerPayAmount { get; set; }       
        [Description("ค่าใช้จ่ายที่เก็บจากลูกค้า")]
        [Column(TypeName = "Money")]
        public decimal? CustomerPayAmount { get; set; }
        [Description("ค่าใช้จ่ายที่ไม่เก็บจากลูกค้า")]
        [Column(TypeName = "Money")]
        public decimal? CustomerNoPayAmount { get; set; }

        //รวมเงินที่จ่ายมา
        [Description("เงินบริษัทที่จ่ายมา")]
        [Column(TypeName = "Money")]
        public decimal? SumAPPaymented { get; set; }
        [Description("เงินนิติบุคคลที่จ่ายมา")]
        [Column(TypeName = "Money")]
        public decimal? SumLegalEntityPaymented { get; set; }

        //owner
        [Description("ชื่อเต็ม ผู้โอนหลัก")]
        [MaxLength(500)]
        public string MainOwnerName { get; set; }

        [Description("ชื่อเต็ม ผู้โอนทั้งหมด")]
        [MaxLength(1500)]
        public string AllOwnerName { get; set; }

        //document control
        [Description("มอบอำนาจหรือไม่?")]
        public bool? IsAssignAuthority { get; set; }
        [Description("ผู้แก้ไขมอบอำนาจ")]
        public Guid? AssignAuthorityUserID { get; set; }
        [ForeignKey("AssignAuthorityUserID")]
        public USR.User AssignAuthorityUser { get; set; }
        [Description("วันที่แก้ไขมอบอำนาจ")]
        public DateTime? AssignAuthorityDate { get; set; }
        [Description("ลูกค้าได้รับเอกสารหลังโอนกรรมสิทธิ์แล้วหรือไม่?")]
        public bool? IsReceiveDocument { get; set; }
        [Description("ผู้แก้ไขลูกค้าได้รับเอกสารหลังโอนกรรมสิทธิ์แล้ว")]
        public Guid? ReceiveDocumentUserID { get; set; }
        [ForeignKey("ReceiveDocumentUserID")]
        public USR.User ReceiveDocumentUser { get; set; }
        [Description("วันที่แก้ไขลูกค้าได้รับเอกสารหลังโอนกรรมสิทธิ์แล้ว")]
        public DateTime? ReceiveDocumentDate { get; set; }
        [Description("เหตุผล")]
        [MaxLength(5000)]
        public string TransferDocumentRemark { get; set; }

        //freedown
        [Description("จำนวนครั้งที่พิมพ์เอกสารโปรโมชั่นฟรีดาวน์")]
        public int? FreeDownPrintCount { get; set; }
   
        [Description("ค่าดำเนินการเอกสาร")]
        [Column(TypeName = "Money")]
        public decimal? DocumentFeeCharge { get; set; }

        [Description("รับเงินก่อนโอน")]
        [Column(TypeName = "Money")]
        public decimal? BeforeTransferAmount { get; set; }

        [Description("User ทำ การทอนคืน (AP)")]
        public bool? FlagUserAPGiveChange { get; set; }

        [Description("User ทำ จ่ายด้วย (AP)")]
        public bool? FlagUserAPPayWithMemo { get; set; }

        [Description("User ทำ การทอนคืน (Legal)")]
        public bool? FlagUserLegalEntityGiveChange { get; set; }

        [Description(" User ทำ จ่ายด้วย (Legal)")]
        public bool? FlagUserLegalEntityPayWithMemo { get; set; }

        [Description(" โอนกรรมสิทธิ์โดย Agent")]
        public bool? IsTransferByAgent { get; set; }
        [Description("ค่าธรรมเนียมรูด P Card")]
        public decimal? PCardFee { get; set; }
        [Description("P Card ผิดธนาคาร")]
        public bool? IsPCardBankWrong { get; set; }
    }
}
