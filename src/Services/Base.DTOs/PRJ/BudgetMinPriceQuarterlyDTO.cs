using System;
using System.Collections.Generic;

namespace Base.DTOs.PRJ
{
    public class BudgetMinPriceQuarterlyDTO
    {
        /// <summary>
        /// Total Budget
        /// </summary>
        public BudgetMinPriceDTO BudgetMinPrice { get; set; }
        /// <summary>
        /// รายการ Unit
        /// </summary>
        public List<BudgetMinPriceUnitDTO> Units { get; set; }
        /// <summary>
        /// Msg Error
        /// </summary>
        public ChkErrorDTO ChkError { get; set; }
        
    }
}
