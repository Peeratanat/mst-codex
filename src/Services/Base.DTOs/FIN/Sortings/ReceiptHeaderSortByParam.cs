namespace Base.DTOs.FIN
{
    public class ReceiptHeaderSortByParam
    {
        public ReceiptHeaderSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ReceiptHeaderSortBy
    {
        IsForeigner,
        ReceiptNo,
        ReceiveDate,
        SendPrintingDate,
        ActiveDate,//วันที่ทำรายการ
        ProjectNo,
        UnitNo,
        ContactName,
        DepositNo,
        TotalAmount,
        SendEmailStatus,
        SendPrintingStatus,
        LotNo,
        SendEmailDate,
        LockEmailRemarkCancel,
        LockPrintingRemarkCancel,
        TotalSendPrinting,
        TotalSendEmail,
        ReceiptSendEmailStatusName,
        ReceiptSendPrintingStatusName,
        ExportDate

    }
}
