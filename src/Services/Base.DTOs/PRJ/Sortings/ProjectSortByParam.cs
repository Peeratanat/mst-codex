using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class ProjectSortByParam
    {
        public ProjectSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ProjectSortBy
    {
        ProjectNo,
        ProjectNameTH,
        ProjectNameEN,
        Brand,
        Company,
        ProductType,
        ProjectStatus,
        Updated,
        UpdatedBy,
        IsActive
    }
}
