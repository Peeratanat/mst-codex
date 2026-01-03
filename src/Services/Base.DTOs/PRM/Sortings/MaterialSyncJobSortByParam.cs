using System;
namespace Base.DTOs.PRM
{
    public class MaterialSyncJobSortByParam
    {
        public MaterialSyncJobSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum MaterialSyncJobSortBy
    {
        JobNo,
        Updated,
        Status
    }
}
