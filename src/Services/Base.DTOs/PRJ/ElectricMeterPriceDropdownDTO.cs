using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class ElectricMeterPriceDropdownDTO : BaseDTO
    {
        /// <summary>
        /// ราคามิเตอร์ไฟฟ้า
        /// </summary>
        public decimal? ElectricMeterPrice { get; set; }
        public static ElectricMeterPriceDropdownDTO CreateFromModel(WaterElectricMeterPrice model)
        {
            if (model != null)
            {
                var result = new ElectricMeterPriceDropdownDTO
                {
                    Id = model.ID,
                    ElectricMeterPrice = model.ElectricMeterPrice,
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
