using Base.DTOs.PRM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MST_Promotion.Params.Outputs
{
    public class MasterSalePromotionPaging
    {
        public List<MasterSalePromotionDTO> MasterSalePromotionDTOs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
