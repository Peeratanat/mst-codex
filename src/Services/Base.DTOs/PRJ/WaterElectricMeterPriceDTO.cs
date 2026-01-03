using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class WaterElectricMeterPriceDTO : BaseDTO
    {
        /// <summary>
        /// เวอร์ชั่น
        /// </summary>
        public int? Version { get; set; }
        /// <summary>
        /// ราคามิเตอร์น้ำ
        /// </summary>
        public decimal? WaterMeterPrice { get; set; }
        /// <summary>
        /// ราคามิเตอร์ไฟฟ้า
        /// </summary>
        public decimal? ElectricMeterPrice { get; set; }
        /// <summary>
        /// ขนาดมิเตอร์ไฟฟ้า (แอมป์)
        /// </summary>
        public string ElectricMeterSize { get; set; }
        /// <summary>
        /// ขนาดมิเตอร์น้ำ (นิ้ว)
        /// </summary>
        public string WaterMeterSize { get; set; }
        public static WaterElectricMeterPriceDTO CreateFromModel(WaterElectricMeterPrice model)
        {
            if (model != null)
            {
                var result = new WaterElectricMeterPriceDTO()
                {
                    Id = model.ID,
                    Version = model.Version,
                    WaterMeterPrice = model.WaterMeterPrice,
                    ElectricMeterPrice = model.ElectricMeterPrice,
                    ElectricMeterSize = model.ElectricMeterSize,
                    WaterMeterSize = model.WaterMeterSize,
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

        public static WaterElectricMeterPriceDTO CreateFromQueryResult(WaterElectricMeterPriceQueryResult model)
        {
            if (model != null)
            {
                var result = new WaterElectricMeterPriceDTO()
                {
                    Id = model.WaterElectricMeterPrice.ID,
                    Version = model.WaterElectricMeterPrice.Version,
                    WaterMeterPrice = model.WaterElectricMeterPrice.WaterMeterPrice,
                    ElectricMeterPrice = model.WaterElectricMeterPrice.ElectricMeterPrice,
                    ElectricMeterSize = model.WaterElectricMeterPrice.ElectricMeterSize,
                    WaterMeterSize = model.WaterElectricMeterPrice.WaterMeterSize,
                    Updated = model.WaterElectricMeterPrice.Updated,
                    UpdatedBy = model.WaterElectricMeterPrice.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(SortByParam sortByParam, ref IQueryable<WaterElectricMeterPriceQueryResult> query)
        {
            if (!string.IsNullOrEmpty(sortByParam.SortBy))
            {
                if (sortByParam.SortBy.Equals("Version", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.WaterElectricMeterPrice.Version);
                    else query = query.OrderByDescending(o => o.WaterElectricMeterPrice.Version);
                }
                else if (sortByParam.SortBy.Equals("WaterMeterPrice", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.WaterElectricMeterPrice.WaterMeterPrice);
                    else query = query.OrderByDescending(o => o.WaterElectricMeterPrice.WaterMeterPrice);
                }
                else if (sortByParam.SortBy.Equals("ElectricMeterPrice", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.WaterElectricMeterPrice.ElectricMeterPrice);
                    else query = query.OrderByDescending(o => o.WaterElectricMeterPrice.ElectricMeterPrice);
                }
                else if (sortByParam.SortBy.Equals("ElectricMeterSize", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.WaterElectricMeterPrice.ElectricMeterSize);
                    else query = query.OrderByDescending(o => o.WaterElectricMeterPrice.ElectricMeterSize);
                }
                else if (sortByParam.SortBy.Equals("WaterMeterSize", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.WaterElectricMeterPrice.WaterMeterSize);
                    else query = query.OrderByDescending(o => o.WaterElectricMeterPrice.WaterMeterSize);
                }
                else if (sortByParam.SortBy.Equals("Updated", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.WaterElectricMeterPrice.Updated);
                    else query = query.OrderByDescending(o => o.WaterElectricMeterPrice.Updated);
                }
                else if (sortByParam.SortBy.Equals("UpdatedBy", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy);
                    else query = query.OrderByDescending(o => o.UpdatedBy);
                }
                else
                {
                    query = query.OrderBy(o => o.WaterElectricMeterPrice.Version);
                }
            }
            else
            {
                query = query.OrderBy(o => o.WaterElectricMeterPrice.Version);
            }
        }

        public void ToModel(ref WaterElectricMeterPrice model)
        {
            model.WaterMeterPrice = this.WaterMeterPrice;
            model.ElectricMeterPrice = this.ElectricMeterPrice;
            model.ElectricMeterSize = this.ElectricMeterSize;
            model.WaterMeterSize = this.WaterMeterSize;
        }
    }
    public class WaterElectricMeterPriceQueryResult
    {
        public WaterElectricMeterPrice WaterElectricMeterPrice { get; set; }
        public User UpdatedBy { get; set; }
    }
}
