using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.ACC
{
    public class dbqTransfer : BaseDbQueries
    {
        public Guid? ID { get; set; }// sal.TransferID
        public Guid? CompanyID { get; set; }// บริษัท 
        public string CompanyName { get; set; }// โครงการ 
        public Guid? ProjectID { get; set; }// โครงการ 
        public string ProjectName { get; set; }// โครงการ 
        public Guid? UnitID { get; set; }// UnitNo 
        public string UnitNo { get; set; }// UnitNo 
        public string HouseNo { get; set; }// UnitNo 
        public string CustomerName { get; set; }// ชื่อ - สกุล 
        public decimal PettyCashAmount { get; set; }// จำนวนเงินทำคืนฝ่ายโอน
        public decimal MinistryPCard { get; set; }// PCard กระทรวงการคลัง
        public decimal CompanyIncomeTax { get; set; }// ภาษีเงินได้
        public decimal BusinessTax { get; set; }// ภาษีธุรกิจเฉพาะ
        public decimal CompanyPayFee { get; set; }// ค่าธรรมเนียมการโอนบริษัทจ่าย
        public decimal CustomerPayFee { get; set; }// ค่าธรรมเนียมการโอนลูกค้าจ่าย
        public decimal FreeTransferExpenseAmount { get; set; }// ฟรีค่าใช้จ่าย
        public decimal FreedownAmount { get; set; }// ฟรีดาวน์
        public decimal DocumentFeeChargeAmount { get; set; }// รายได้ค่าดำเนินการเอกสาร
        public decimal CommonFeeChargeAmount { get; set; }// ฟรีค่าส่วนกลาง
        public decimal FirstSinkingFundAmount { get; set; }// ฟรีค่ากองทุน
        public decimal WaterMeterAmount { get; set; }// ฟรีค่ามิเตอร์น้ำ
        public decimal ElectricMeterAmount { get; set; }// ฟรีค่ามิเตอร์ไฟฟ้า
        public decimal NonGiveChangeAmount { get; set; }// ปัดเศษ
        public decimal NetPrice { get; set; }// ปัดเศษ
        public decimal WelcomeHome { get; set; }// ปัดเศษ
        public decimal ActualDocumentFee { get; set; }// ค่าดำเนินการเอกสารจริง 
        public string TransferStatus { get; set; }// สถานะโอนกรรมสิทธิ์ =>AccountApproved=บัญชีอนุมัติ,PaymentConfirmed=ยืนยันชำระเงิน,TransferConfirmed=ยืนยันโอนจริง,ReadyToTransfer=พร้อมโอน,Start=ตั้งเรื่องโอน
        public string TransferStatusName { get; set; }// สถานะโอนกรรมสิทธิ์ =>AccountApproved=บัญชีอนุมัติ,PaymentConfirmed=ยืนยันชำระเงิน,TransferConfirmed=ยืนยันโอนจริง,ReadyToTransfer=พร้อมโอน,Start=ตั้งเรื่องโอน
        public bool IsAccountApproved { get; set; }// บัญชีอนุมัติ 1=อนุมัติ  0=ยังไม่อนุมัติ
        public Guid? AgreementID { get; set; }
        public bool PostKAStatus { get; set; }// สถานะโพสค่าใช้จ่าย 1=Post แล้ว  0=ยังไม่ Post
        public bool PostRRStatus { get; set; }// สถานะโพส RR 1=Post แล้ว  0=ยังไม่ Post
        public string DocNoRR { get; set; }// เลขที่ Doc (RR) 
        public string DocNoKA { get; set; }// เลขที่ Doc (KA)  
        public string DocNoFD { get; set; }// เลขที่ Doc (KA)  
        public DateTime? ActualTransferDate { get; set; }// วันที่โอน
        public DateTime? DocKADate { get; set; }// วันที่Doc (KA)  
        public DateTime? DocRRDate { get; set; }// วันที่Doc (RR) 
        public decimal? PCardFee { get; set; }// ค่าดำเนินการเอกสารจริง
        public bool? IsPCardBankWrong { get; set; }// ค่าดำเนินการเอกสารจริง

        public string CreatedByKA { get; set; }// เลขที่ Doc (RR) 
        public string CreatedByRR { get; set; }// เลขที่ Doc (RR) 
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public decimal SumMinistryPCard { get; set; }
        public decimal SumCompanyIncomeTax { get; set; }
        public decimal SumBusinessTax { get; set; }
        public decimal SumCompanyPayFee { get; set; }
        public decimal SumCustomerPayFee { get; set; }
        public decimal SumPettyCashAmount { get; set; }
        public decimal SumFreeTransferExpenseAmount { get; set; }
        public decimal SumFreedownAmount { get; set; }
        public decimal SumDocumentFeeChargeAmount { get; set; }
        public decimal SumCommonFeeChargeAmount { get; set; }
        public decimal SumFirstSinkingFundAmount { get; set; }
        public decimal SumWaterMeterAmount { get; set; }
        public decimal SumElectricMeterAmount { get; set; }
        public decimal SumNonGiveChangeAmount { get; set; }
        public decimal SumNetPrice { get; set; }
        public decimal SumWelcomeHome { get; set; }
        public decimal SumActualDocumentFee { get; set; }
        public decimal SumPCardFee { get; set; }
    }
}
