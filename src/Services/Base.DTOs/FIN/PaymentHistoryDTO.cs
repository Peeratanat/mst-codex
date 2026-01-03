using Base.DTOs.MST;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.DbQueries.Finance;
using System;
using System.ComponentModel;

namespace Base.DTOs.FIN
{
    public class PaymentHistoryDTO : BaseDTO
    {
        public bool showPrint { get; set; } = true;
        public bool showDelete { get; set; } = true;
        public bool showFET { get; set; } = true;

        public bool showFile { get; set; }

        public Guid? PaymentID { get; set; }

        public Guid? PaymentMethodID { get; set; }

        /// <summary>
        /// ขอ Fet
        /// </summary>
        public bool? IsFET { get; set; }

        /// <summary>
        /// มีไฟล์แนบ FET ?
        /// </summary>
        public bool? hasFETAttachFile { get; set; }

        /// <summary>
        /// จำนวนเงินรวมตามใบเสร็จ
        /// </summary>
        public decimal? ReceiveAmount { get; set; }

        /// <summary>
        /// วันที่ชำระ
        /// </summary>
        [Description("วันที่ชำระ")]
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// รายการ
        /// </summary>
        public string Name { get; set; }

        public string MasterPriceItemKey { get; set; }

        /// <summary>
        /// จำนวนเงินตามประเภทการชำระ
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// ชนิดของช่องทางชำระ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PaymentMethod
        /// </summary>
        [Description("ชนิดของช่องทางชำระ")]
        public MasterCenterDropdownDTO PaymentMethodType { get; set; }

        /// <summary>
        /// วันที่ตั้งพัก
        /// </summary>
        public DateTime? UnknownPaymentDate { get; set; }

        /// <summary>
        /// สถานะบันทึกการนำฝาก
        /// </summary>
        public DepositHeaderDTO DepositHeader { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public ReceiptTempListDTO Receipt { get; set; }


        public UserDTO PaymentCreated { get; set; }

        /// <summary>
        /// ข้อมูล Post GL
        /// สถานะ "Post GL แล้ว" = Object นี้มีค่า
        /// สถานะ "ยังไม่ Post GL" = Object เป็นค่า NULL
        /// </summary>
        [Description("ข้อมูล Post GL")]
        public string PostGLDocumentNo { get; set; }

        [Description("วันที่ Post GL")]
        public DateTime? PostGLDate { get; set; }

        public bool IsFromLC { get; set; }
        public bool IsLockCalendar { get; set; }
        public string PDFReceiptFile { get; set; }
        public bool? IsOldFET { get; set; }

        public static PaymentHistoryDTO CreateFromSQLQueryResult(sqlPaymentHistory.QueryResult model)
        {
            if (model != null)
            {
                var result = new PaymentHistoryDTO();

                result.Id = Guid.NewGuid();
                result.PaymentID = model.PaymentID;
                result.PaymentMethodID = model.PaymentMethodID;

                result.IsFET = model.IsFET;
                result.hasFETAttachFile = model.hasFETAttachFile;
                result.showFile = model.hasAttachFile ?? false;

                result.ReceiveAmount = model.ReceiveAmount;

                result.ReceiveDate = model.ReceiveDate;
                result.Name = model.MasterPriceItemName;
                result.MasterPriceItemKey = model.MasterPriceItemKey;
                result.PDFReceiptFile = model.PDFReceiptFile;

                result.Amount = model.Amount;
                result.PaymentMethodType = new MasterCenterDropdownDTO
                {
                    Id = model.PaymentMethodTypeID ?? Guid.Empty,
                    Name = model.PaymentMethodTypeName,
                    Key = model.PaymentMethodTypeKey,
                    Order = model.PaymentMethodTypeOrder ?? 0
                };

                result.UnknownPaymentDate = model.UnknownPaymentDate;

                result.DepositHeader = PaymentMethodKeys.IsDepositMethodType.Contains(model.PaymentMethodTypeKey) ? new DepositHeaderDTO { DepositNo = model.DepositNo, DepositDate = model.DepositDate } : new DepositHeaderDTO { DepositNo = "นำฝากแล้ว" };

                if (model.QuotationID.HasValue)
                    result.DepositHeader = new DepositHeaderDTO { DepositNo = model.DepositNo, DepositDate = model.DepositDate };

                result.Receipt = new ReceiptTempListDTO
                {
                    Id = model.ReceiptTempHeaderID,
                    ReceiptTempNo = model.ReceiptTempNo,
                    ReceiptNo = model.ReceiptNo,
                    PrintingDate = model.PrintingDate,
                    SendMailDate = model.SendMailDate
                };

                result.PostGLDocumentNo = model.PostGLDocumentNo;
                result.PostGLDate = model.PostGLDate;

                result.IsFromLC = model.IsFromLC ?? false;
                result.IsLockCalendar = model.IsLockCalendar ?? false;
                result.Updated = model.PaymentCreateDate;
                result.IsOldFET = model.IsOldFET;
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
