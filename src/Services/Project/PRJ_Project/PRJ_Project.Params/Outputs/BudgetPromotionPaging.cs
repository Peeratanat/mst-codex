using Base.DTOs.PRJ;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Project.Params.Outputs
{
    public class BudgetPromotionPaging
    {
        public List<BudgetPromotionDTO> BudgetPromotions { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
