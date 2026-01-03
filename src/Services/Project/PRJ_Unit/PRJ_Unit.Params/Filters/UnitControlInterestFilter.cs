using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Unit.Params.Filters
{
    public class UnitControlInterestFilter : BaseFilter
    {
        public Guid? TowerID { get; set; }
        public Guid? FloorID { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public int? InterestCounter { get; set; }
        public string UnitNo { get; set; }
        public string UnitStatusKey { get; set; }
    }
}
