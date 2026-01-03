using Database.Models.PRJ;
using System;
using System.Collections.Generic;

namespace Base.DTOs.PRJ
{
    /// <summary>
    /// Model: BudgetMinPrice
    /// </summary>
    public class BudgetMinPriceListDTO
    { 
        public BudgetMinPriceDTO BudgetMinPriceDTO { get; set; }
        public List<BudgetMinPriceUnitDTO> BudgetMinPriceUnitDTO { get; set; }

    }


}
