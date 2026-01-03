using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MST_Finacc.Params.Filters
{
    public class BankBranchBOTFilter : BaseFilter
    {
        public string BankBranchCode { get; set; }
        public string BankBranchName { get; set; }
        public string BankCode { get; set; }
        public Guid BankId { get; set; }

    }
}
