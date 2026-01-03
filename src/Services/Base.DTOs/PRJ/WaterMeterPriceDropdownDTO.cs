using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class WaterMeterPriceDropdownDTO : BaseDTO
    {
        /// <summary>
        /// ราคามิเตอร์น้ำ
        /// </summary>
        public decimal? WaterMeterPrice { get; set; }
        public static WaterMeterPriceDropdownDTO CreateFromModel(WaterElectricMeterPrice model)
        {
            if (model != null)
            {
                var result = new WaterMeterPriceDropdownDTO
                {
                    Id = model.ID,
                    WaterMeterPrice = model.WaterMeterPrice,
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
