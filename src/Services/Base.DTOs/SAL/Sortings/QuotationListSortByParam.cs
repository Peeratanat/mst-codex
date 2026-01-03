using System;
namespace Base.DTOs.SAL.Sortings
{
    public class QuotationListSortByParam
    {
        public QuotationListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum QuotationListSortBy
    {
        QuotationNo,
        Project,
        UnitNo,
        HouseNo,
        IssueDate,
        QuotationStatus,
        CreatedBy
    }
}
