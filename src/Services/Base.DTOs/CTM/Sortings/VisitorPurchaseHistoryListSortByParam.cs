using System;
namespace Base.DTOs.CTM
{
    public class VisitorPurchaseHistoryListSortByParam
    {
        public VisitorPurchaseHistoryListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum VisitorPurchaseHistoryListSortBy
    {
        PurchaseDate, Project, Unit, NetSellingPrice, UnitStatus
    }
}
