using Base.DTOs.PRJ;
using Database.Models.PRJ;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Unit.Params.Outputs
{
    public class UnitControlLockPaging
    {
        public List<UnitControlLockDTO> unitControlLocks { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
