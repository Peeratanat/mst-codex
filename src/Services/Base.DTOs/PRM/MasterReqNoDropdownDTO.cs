using Database.Models.PRM;
using System;
using System.ComponentModel;

namespace Base.DTOs.PRM
{
    public class MasterReqNoDropdownDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string Reqno { get; set; }

        public static MasterReqNoDropdownDTO CreateFromSaleModel(SalePromotionRequest model)
        {
            if (model != null)
            {
                MasterReqNoDropdownDTO result = new MasterReqNoDropdownDTO()
                {
                    Id = model.ID,
                    Reqno = model.RequestNo,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static MasterReqNoDropdownDTO CreateFromTranferModel(TransferPromotionRequest model)
        {
            if (model != null)
            {
                MasterReqNoDropdownDTO result = new MasterReqNoDropdownDTO()
                {
                    Id = model.ID,
                    Reqno = model.RequestNo,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
