using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MST_Titledeeds.Params.Filters
{
    public class BaseFilter
    {
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
    }
}
