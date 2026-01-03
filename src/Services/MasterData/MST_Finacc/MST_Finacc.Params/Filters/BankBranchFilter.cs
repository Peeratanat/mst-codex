using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MST_Finacc.Params.Filters
{
    public class BankBranchFilter : BaseFilter
    {
        public Guid? BankID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Building { get; set; }
        public string Soi { get; set; }
        public string Road { get; set; }
        public Guid? SubDistrictID { get; set; }
        public Guid? DistrictID { get; set; }
        public Guid? ProvinceID { get; set; }
        public string PostalCode { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public bool? IsCreditBank { get; set; }
        public bool? IsDirectDebit { get; set; }
        public bool? IsDirectCredit { get; set; }
        public string AreaCode { get; set; }
        public string OldBankID { get; set; }
        public string OldBranchID { get; set; }
        public bool? IsActive { get; set; }
    }
}
