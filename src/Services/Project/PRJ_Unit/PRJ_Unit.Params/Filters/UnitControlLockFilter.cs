using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Unit.Params.Filters
{
    public class UnitControlLockFilter : BaseFilter
    {
        public Guid? TowerID { get; set; }
        public Guid? FloorID { get; set; }
        public string FloorNameTH { get; set;}
        public string FloorNameEN { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public string UnitNo { get; set; }
        public string UnitStatusKey { get; set; }
        public int? LockStatus { get; set; }
    }
}
