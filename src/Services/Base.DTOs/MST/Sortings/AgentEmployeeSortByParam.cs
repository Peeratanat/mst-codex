using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class AgentEmployeeSortByParam
    {
        public AgentEmployeeSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum AgentEmployeeSortBy
    {
        FirstName,
        LastName,
        TelNo,
        Updated,
        UpdatedBy,
    }
}
