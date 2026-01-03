using Database.Models.PRM;
using System;
using System.ComponentModel;

namespace Base.DTOs.PRM
{
    public class MasterPreSalePromotionDropdownDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// ชื่อโปรโมชั่น
        /// </summary>
        [Description("ชื่อโปรโมชั่น")]
        public string Name { get; set; }
        public static MasterPreSalePromotionDropdownDTO CreateFromModel(MasterPreSalePromotion model)
        {
            if (model != null)
            {
                MasterPreSalePromotionDropdownDTO result = new MasterPreSalePromotionDropdownDTO()
                {
                    Id = model.ID,
                    PromotionNo = model.PromotionNo,
                    Name = model.Name,
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
