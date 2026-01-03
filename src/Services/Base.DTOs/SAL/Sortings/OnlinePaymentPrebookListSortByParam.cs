using System;
namespace Base.DTOs.SAL.Sortings
{
    public class OnlinePaymentPrebookListSortByParam
    {
        public OnlinePaymentPerbookListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum OnlinePaymentPerbookListSortBy
    {
        PaymentDate,
        PaymentChannel,
        TotalAmount,
        ProjectName,
        UnitNo,
        ContractNo,
        CustomerName,
        PhoneNumber,
        UnitStatus,
        Email,
        QuotationNo,
        PaymentStatus,
        BookingNo,
        SourceCardMasking,
        PaymentType,
        SourceBrand,
        SourceIssuerBank
    }
}
