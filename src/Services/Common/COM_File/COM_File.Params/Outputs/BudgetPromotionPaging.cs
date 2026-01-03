using Base.DTOs.PRJ;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace COM_File.Params.Outputs
{
    public class BudgetPromotionPaging
    {
        public List<BudgetPromotionDTO> BudgetPromotions { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
