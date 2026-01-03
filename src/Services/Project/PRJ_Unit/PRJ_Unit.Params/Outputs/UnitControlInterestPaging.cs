using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Unit.Params.Outputs
{
    public class UnitControlInterestPaging
    {
        public List<UnitInterestDTO> unitInterests { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
