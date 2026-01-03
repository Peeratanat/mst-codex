using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbqReceiptHeaderSP : BaseDbQueries
    {
        public Guid? ID { get; set; }
        /// <summary>
        /// Default สถานะ ส่ง Email
        /// </summary>
        public bool? SendEmail { get; set; }

        public bool? IsLockEmail { get; set; }
        

        /// <summary>
        /// Default สถานะ ส่ง โรงพิมพ์
        /// </summary>
        public bool? SendPrinting { get; set; }
        public bool? IsLockPrinting { get; set; }

        /// <summary>
        /// ประเภทลูกค้า 0=ไทย,1=ตปท
        /// </summary>
        public bool IsForeigner { get; set; }

        /// <summary>
        /// ประเภทลูกค้า  ไทย,ต่่างประเภท
        /// </summary>
        public string CustomerType { get; set; }

        /// <summary>
        /// เลขทีใบเสร็จ
        /// </summary>
        public string ReceiptNo { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// วันที่นำส่งโรงพิมพ์
        /// </summary>
        public DateTime? SendPrintingDate { get; set; }

        /// <summary>
        /// วันที่ส่งอีเมลล?
        /// </summary>
        public DateTime? SendEmailDate { get; set; }

        

        /// <summary>
        /// วันที่ทำรายการ
        /// </summary>
        public DateTime? ActiveDate { get; set; }

        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        public Guid? ProjectID { get; set; }
        public string ProjectNo { get; set; }

        /// <summary>
        /// ชื่อโครงการ
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Unit
        /// </summary>
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }

        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        public Guid? CustomerID { get; set; }
        public string CustomerName { get; set; }

        // <summary>
        // จำนวนเงินรวมของใบเสร็จ
        // </summary>
        public decimal? TotalAmount { get; set; }

        /// <summary>
        /// สถานะส่ง Email
        /// </summary>
        public Guid? ReceiptSendEmailStatusID { get; set; }
        public string ReceiptSendEmailStatusKey { get; set; }
        public string ReceiptSendEmailStatusName { get; set; }
        

        /// <summary>
        /// สถานะส่ง โรงพิมพ์
        /// </summary>
        public Guid? ReceiptSendPrintingStatusID { get; set; }
        public string ReceiptSendPrintingStatusKey { get; set; }
        public string ReceiptSendPrintingStatusName { get; set; }

        /// <summary>
        /// สถานะใบเสร็จ
        /// </summary> 
        public string ReceiptStatusName { get; set; }

        // <summary>
        // เลขที่ Lot
        // </summary>
        public string LotNo { get; set; }

        /// <summary>
        /// LockEmailRemarkCancel
        /// </summary>
        public string LockEmailRemarkCancel { get; set; }

        /// <summary>
        /// LockPrintingRemarkCancel
        /// </summary>
        public string LockPrintingRemarkCancel { get; set; }

        /// <summary>
        /// รวมจำนวนที่ส่งโรงพิมพ์
        /// </summary>
        public int TotalSendPrinting { get; set; }
        /// <summary>
        /// รวมจำนวนที่ส่งอีเมลล์
        /// </summary>
        public int TotalSendEmail { get; set; }


        /// <summary>
        /// วันที่ 
        /// </summary>
        public DateTime? ExportDate { get; set; }
        

        ////////////// Detail /////////////////////////////


        public Guid? PaymentID { get; set; }
        public Guid? BookingID { get; set; }

        // <summary>
        // จำนวนเงิน
        // </summary>
        public decimal Amount { get; set; }


        /// <summary>
        /// ธนาคาร
        /// </summary>
        public Guid? BankID { get; set; }
        public string BankName { get; set; }

        /// <summary>
        /// ชำระโดย
        /// </summary>
        public Guid? MethodID { get; set; }
        public string MethodName { get; set; }

        /// <summary>
        /// งวดชำระ
        /// </summary>
        public Guid? MasterPriceItemID { get; set; }
        public string MasterPriceItemName { get; set; }

        /// <summary>
        /// เลขที่ใบนำฝาก
        /// </summary>
        public Guid? DepositID { get; set; }
        public string DepositNo { get; set; }

    }
}
