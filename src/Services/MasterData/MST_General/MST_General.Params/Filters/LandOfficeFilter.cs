using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MST_General.Params.Filters
{
    public class LandOfficeFilter : BaseFilter
    {
        public string NameTH { get; set; }
        public string NameEN { get; set; }
        public Guid? SubDistrictID { get; set; }
        public Guid? DistrictID { get; set; }
        public Guid? ProvinceID { get; set; }
    }
}
