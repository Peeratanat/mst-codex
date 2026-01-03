using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Project.Params.Filters
{
    public class RoundFeeFilter : BaseFilter
    {
        public Guid? LandOfficeID { get; set; }
        public decimal? OtherFee { get; set; }
        public string TransferFeeRoundFormulaKey { get; set; }
        public string BusinessTaxRoundFormulaKey { get; set; }
        public string LocalTaxRoundFormulaKey { get; set; }
        public string IncomeTaxRoundFormulaKey { get; set; }
    }
}
