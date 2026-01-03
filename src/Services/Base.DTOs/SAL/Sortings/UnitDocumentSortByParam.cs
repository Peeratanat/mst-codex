namespace Base.DTOs.SAL
{
    public class UnitDocumentSortByParam
    {
        public UnitDocumentSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum UnitDocumentSortBy
    {
        DocumentNo,
        Project,
        Unit,
        CustomerName
    }
}
