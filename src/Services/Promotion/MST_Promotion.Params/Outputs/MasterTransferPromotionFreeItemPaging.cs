using Base.DTOs.PRM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MST_Promotion.Params.Outputs
{
    public class MasterTransferPromotionFreeItemPaging
    {
        public List<MasterTransferPromotionFreeItemDTO> MasterTransferPromotionFreeItemDTOs { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
