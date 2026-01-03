namespace Base.DTOs.FIN
{
    public class DirectCreditDebitExportDetailSortByParam
    {
        public DirectCreditDebitExportDetailSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum DirectCreditDebitExportDetailSortBy
    {
        BatchID,
        Project,
        UnitNo,
        AccountNo,
        PeriodDate,
        DueDate,
        AgreementNo,
        CustomerName,
        ReceiveDate,
        Amount,
        DirectCreditDebitExportStatus,
        ReceiptTempNo,
        OldPeriod,
        Period,
        TranCode,
        TranCodeRemark
    }
}
