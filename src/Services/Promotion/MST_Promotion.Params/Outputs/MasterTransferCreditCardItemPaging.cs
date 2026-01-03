using Base.DTOs.PRM;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MST_Promotion.Params.Outputs
{
    public class MasterTransferCreditCardItemPaging
    {
        public List<MasterTransferCreditCardItemDTO> MasterTransferCreditCardItemDTOs { get; set; }
        public PageOutput PageOutput { get; set; }

    }
}
