using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MST_General.Params.Filters
{
    public class ProvinceFilter : BaseFilter
    {
        public string NameTH { get; set; }
        public string NameEN { get; set; }
        public bool? IsShow { get; set; }
    }
}
