using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.ACC
{
    public class TransferSortByParam
    {
        public TransferSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }
    public enum TransferSortBy
    {
        UnitNo,
        CustomerName,
        PettyCashAmount,
        MinistryPCard,
        CompanyIncomeTax,
        BusinessTax,
        CustomerPayFee,
        CompanyPayFee,
        FreeTransferExpenseAmount,//ฟรีค่าใช้จ่าย
        FreedownAmount,//ฟรีดาวน์
        DocumentFeeChargeAmount,//รายได้ค่าดำเนินการเอกสาร
        CommonFeeChargeAmount,//ฟรีค่าส่วนกลาง
        FirstSinkingFundAmount,//ฟรีค่ากองทุน
        WaterMeterAmount,//ฟรีค่ามิเตอร์น้ำ
        ElectricMeterAmount,//ฟรีค่ามิเตอร์ไฟฟ้า
        NonGiveChangeAmount,//ปัดเศษ
        TransferStatus,//สถานะโอนกรรมสิทธิ์
        PostKAStatus,//สถานะโพสค่าใช้จ่าย
        DocNoKA,//เลขที่ Doc (KA)
        PostRRStatus,//สถานะโพส RR
        DocNoRR,//เลขที่ Doc (RR)
        ActualTransferDate,
        HouseNo,
        WelcomeHome,
        DocNoFD,
        ActualDocumentFee,
        PCardFee,
    }
}
