using System;
using Database.Models.MST;

namespace Base.DTOs.MST
{
    public class MasterPriceItemDTO : BaseDTO
    {
        public MasterCenterDropdownDTO PriceType { get; set; }
        public string Detail { get; set; }
        public string DetailEN { get; set; }
        public string Key { get; set; }

        public MasterCenterDropdownDTO PaymentReceiver { get; set; }

        public static MasterPriceItemDTO CreateFromModel(MasterPriceItem model)
        {
            if (model != null)
            {
                var result = new MasterPriceItemDTO();

                result.Id = model.ID;
                result.PriceType = MasterCenterDropdownDTO.CreateFromModel(model.PriceType);
                result.Detail = model.Detail;
                result.DetailEN = model.DetailEN;
                result.Key = model.Key;
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy?.DisplayName;
                result.PaymentReceiver = MasterCenterDropdownDTO.CreateFromModel(model.PaymentReceiver);

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
