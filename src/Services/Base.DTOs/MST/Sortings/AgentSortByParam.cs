using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class AgentSortByParam
    { 
        public AgentSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum AgentSortBy
    {
        NameTH,
        NameEN,
        Address,
        Building,
        Soi,
        Road,
        PostalCode,
        Province,
        District,
        SubDistrict,
        TelNo,
        FaxNo,
        Website,
        Updated,
        UpdatedBy
    }
}
