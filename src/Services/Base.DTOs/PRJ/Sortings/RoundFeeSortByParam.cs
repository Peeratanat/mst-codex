using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class RoundFeeSortByParam
    {
        public RoundFeeSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum RoundFeeSortBy
    {
        LandOffice,
        OtherFee,
        TransferFeeRoundFormula,
        BusinessTaxRoundFormula,
        LocalTaxRoundFormula,
        IncomeTaxRoundFormula,
        UpdatedBy,
        Updated
    }
}
