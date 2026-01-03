using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MST_Finacc.Params.Filters
{
    public class BankFilter : BaseFilter
    {
        public string BankNo { get; set; }
        public string NameTH { get; set; }
        public string NameEN { get; set; }
        public string Alias { get; set; }
        public bool? IsCreditCard { get; set; }
        public bool? IsNonBank { get; set; }
        public bool? IsCoorperative { get; set; }
        public bool? IsFreeMortgage { get; set; }
        public string SwiftCode { get; set; }
    }
}
