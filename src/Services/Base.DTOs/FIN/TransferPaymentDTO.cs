using System;
using System.ComponentModel;
using Base.DTOs.MST;

namespace Base.DTOs.FIN
{
    public class TransferPaymentDTO : BaseDTO
    {
        /// <summary>
        /// ใบจอง
        /// </summary>
        [Description("ใบจอง")]
        public Guid BookingID { get; set; }

        /// <summary>
        /// เลขที่ใบรับ
        /// </summary>
        [Description("เลขที่ใบรับ")]
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        [Description("เลขที่ใบเสร็จ")]
        public string ReceiptNo { get; set; }

        /// <summary>
        /// ชำระโดย
        /// </summary>
        [Description("ชำระโดย")]
        public MasterCenterDropdownDTO PaymentMethodType { get; set; }
        
        /// <summary>
        /// วันที่ชำระ
        /// </summary>
        [Description("วันที่ชำระ")]
        public DateTime ReceiveDate { get; set; }


        /// <summary>
        /// เลขที่เช็ค
        /// </summary>
        [Description("เลขที่เช็ค")]
        public string ChequeNo { get; set; }
        
        /// <summary>
        /// วันที่เช็ค
        /// </summary>
        [Description("วันที่เช็ค")]
        public DateTime? ChequeDate { get; set; }

        /// <summary>
        /// ธนาคาร
        /// </summary>
        [Description("ธนาคาร")]
        public BankDTO Bank { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        [Description("จำนวนเงิน")]
        public decimal Amount { get; set; }

        /// <summary>
        /// สถานะนำฝาก
        /// </summary>
        [Description("สถานะนำฝาก")]
        public string DepositStatus { get; set; }

        /// <summary>
        /// เลขที่นำฝาก
        /// </summary>
        [Description("เลขที่นำฝาก")]
        public string DepositNo { get; set; }

        /// <summary>
        /// ชำระรายละเอียด
        /// </summary>
        [Description("ชำระรายละเอียด")]
        public string PaymentBy { get; set; }

        /// <summary>
        /// จำนวนเงินที่ต้องจ่าย
        /// </summary>
        [Description("จำนวนเงินที่ต้องจ่าย")]
        public decimal PayAmount { get; set; }

        /// <summary>
        /// ไอดีใบเสร็จ
        /// </summary>
        [Description("ไอดีใบเสร็จ")]
        public Guid? ReceiptTempHeaderID { get; set; }

        [Description("มีไฟล์แนบ")]
        public bool? HasFETAttachFile { get; set; }

        [Description("PaymentID")]
        public Guid PaymentID { get; set; }

    }
}
