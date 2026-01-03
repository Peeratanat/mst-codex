using Base.DTOs.PRM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MST_Promotion.Params.Outputs
{
    public class MasterPreSalePromotionItemPaging
    {
        public List<MasterPreSalePromotionItemDTO> MasterPreSalePromotionItemDTOs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
