using Base.DTOs.PRM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MST_Promotion.Params.Outputs
{
    public class MasterSalePromotionItemPaging
    {
        public List<MasterSalePromotionItemDTO> MasterSalePromotionItemDTOs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
