using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_CombineUnit.Params.Filters
{
    public class CombineUnitFilter : BaseFilter
    {
        public Guid? ProjectID { get; set; }
        public string UnitNo { get; set; }
        public string UnitNoCombine { get; set; }
        public Guid? CombineStatusMasterCenterID { get; set; }
        public Guid? CombineDocTypeMasterCenterID { get; set; }
        public Guid? UserID { get; set; }
    }
}
