using Database.Models;
using Database.Models.DbQueries.PRJ;
using Database.Models.PRJ;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class UnitDropdownSellPriceDTO
    {
        /// <summary>
        /// Identity UnitID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// สถานะแปลง
        /// Master/api/MasterCenters?masterCenterGroupKey=UnitStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO UnitStatus { get; set; }
        /// <summary>
        /// ราคาขาย
        /// </summary>
        public decimal? SellPrice { get; set; }


        public async static Task<UnitDropdownSellPriceDTO> CreateFromModelAsync(Unit model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new UnitDropdownSellPriceDTO
                {
                    Id = model.ID,
                    UnitNo = model.UnitNo
                };
                result.UnitStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitStatus);
                var activePriceList = await db.GetActivePriceListAsync(model.ID);
                result.SellPrice = activePriceList?.PriceListItems
                    .Where(o => o.MasterPriceItemID == MasterPriceItemIDs.SellPrice).Select(o => o.Amount)
                    .FirstOrDefault();

                return result;
            }
            else
            {
                return null;
            }
        }

        public static UnitDropdownSellPriceDTO CreateFromSQLQueryResult(sqlUnitDropdownSellPriceList.QueryResult model, DatabaseContext db)
        {
            if (model != null)
            {
                var modelUnitStatus = db.MasterCenters.Where(o => o.ID == model.UnitStatusMasterCenterID).FirstOrDefault();

                var result = new UnitDropdownSellPriceDTO
                {
                    Id = model.ID.Value,
                    UnitNo = model.UnitNo
                };
                result.UnitStatus = MST.MasterCenterDropdownDTO.CreateFromModel(modelUnitStatus);
                result.SellPrice = model.SellPrice;

                //var activePriceList = db.PriceLists
                //                    .Include(o => o.PriceListItems)
                //                    .Where(p => p.ActiveDate <= DateTime.Now
                //                            && p.UnitID == model.ID)
                //                    .OrderByDescending(o => o.ActiveDate)
                //                    .FirstOrDefault();

                //if (activePriceList != null)
                //{
                //    result.SellPrice = activePriceList?.PriceListItems
                //        .Where(o => o.MasterPriceItemID == MasterPriceItemIDs.SellPrice)
                //        .Select(o => o.Amount)
                //        .FirstOrDefault();
                //}

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
