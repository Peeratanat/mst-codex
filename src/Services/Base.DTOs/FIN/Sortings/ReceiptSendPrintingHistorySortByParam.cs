
namespace Base.DTOs.FIN
{
    public class ReceiptSendPrintingHistorySortByParam
    {
        public ReceiptSendPrintingHistorySortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ReceiptSendPrintingHistorySortBy
    {
        LotNo //เลขที่ Lot
        , TotalReceipts //จำนวนใบเสร็จ
        , ExportDate //วันที่ export
        , ExportBy //export โดย
        , SendDate //วันที่ส่งโรงพิมพ์
    }
}
