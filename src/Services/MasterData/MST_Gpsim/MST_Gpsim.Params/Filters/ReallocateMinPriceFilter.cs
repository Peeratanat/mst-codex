using System;
using System.Collections.Generic;
using System.Text;

namespace MST_Gpsim.Params.Filters
{
    public class ReallocateMinPriceFilter : BaseFilter
    {
        public string Version { get; set; }
        public string Remark { get; set; }
        public Guid? ProjectID { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }
}
