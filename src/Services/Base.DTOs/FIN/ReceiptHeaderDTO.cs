using Base.DTOs.PRJ;
using Base.DTOs.MST;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using Database.Models.USR;
namespace Base.DTOs.FIN
{
    public class ReceiptHeaderDTO : BaseDTO
    {
        /// <summary>
        /// ประเภทลูกค้า ไทย,ตปท
        /// </summary>
        public bool IsForeigner { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
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
        /// วันที่นำส่งโรงพิมพ์
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


        /// <summary>
        /// เลขที่ใบนำฝาก
        /// </summary>
        public Guid? DepositID { get; set; }
        public string DepositNo { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal Amount { get; set; }

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
        /// เลขที่ Lot
        /// </summary>
        public string LotNo { get; set; }

        /// <summary>
        /// Default สถานะ ส่ง Email
        /// </summary>
        public bool SendEmail { get; set; }

        /// <summary>
        /// Default สถานะ ส่ง โรงพิมพ์
        /// </summary>
        public bool SendPrinting { get; set; }


        /// <summary>
        /// Default สถานะ ส่ง โรงพิมพ์
        /// </summary>
        public bool IsLockPrinting { get; set; }

        /// <summary>
        /// Default สถานะ ส่ง Email
        /// </summary>
        public bool IsLockEmail { get; set; }

        /// <summary>
        /// ประเภทลูกค้า  ไทย,ต่่างประเภท
        /// </summary>
        public string CustomerType { get; set; }

        /// <summary>
        /// วันที่ Export 
        /// </summary>
        public DateTime? ExportDate { get; set; }

        /// <summary>
        /// สถานะใบเสร็จ
        /// </summary> 
        public string ReceiptStatusName { get; set; }

        public List<ReceiptDetailDTO> ReceiptDetail { get; set; }
        
        public class ReceiptHeaderResult
        {
            public Guid ReceiptHeaderID { get; set; }
            public string BookingEmail { get; set; }
            public string AgreementEmail { get; set; }
            public string CompanyName { get; set; }
            public string ProjectName { get; set; }
            public ReceiptHeader ReceiptHeaders { get; set; }

        }
    }
}
