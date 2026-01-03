namespace Base.DTOs.FIN
{
    public class UnitDocumentFINSortByParam
    {
        public UnitDocumentFinSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum UnitDocumentFinSortBy
    {
        DocumentNo,
        Project,
        Unit,
        CustomerName,
        CustomerNo
    }
}
