using Base.DTOs.PRJ;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MST_Unitmeter.Params.Outputs
{
    public class UnitMeterPaging
    {
        public List<ProjectUnitMeterListDTO> ProjectUnitMeterLists { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
