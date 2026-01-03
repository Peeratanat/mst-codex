namespace Base.DTOs.FIN
{
    public class PaymentInfoListSortByParam
    {
        public PaymentInfoListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; } = true;
    }

    public enum PaymentInfoListSortBy
    {
        UnitNo,
        HouseNo,
        CustomerName,
        BookingNo,
        AgreementNo,
        TransferNo
    }
}
