using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Project.Params.Filters
{
    public class FloorsFilter : BaseFilter
    {
        public string NameTH { get; set; }
        public string NameEN { get; set; }
        public string Description { get; set; }
        public DateTime? DueTransferDateFrom { get; set; }
        public DateTime? DueTransferDateTo { get; set; }
    }
}
