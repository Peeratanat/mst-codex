using Base.DTOs.PRJ;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace COM_File.Params.Outputs
{
    public class BudgetMinPricePaging
    {
        public BudgetMinPriceListDTO BudgetMinPriceListDTO { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
