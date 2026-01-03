using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// โปรก่อนขาย
    /// </summary>
    public class PreSalePromotionDTO
    {
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// รายการโปรโมชั่น
        /// </summary>
        public List<PreSalePromotinItemDTO> Items { get; set; }

        public async static Task<PreSalePromotionDTO> CreateFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            //สร้าง PR Job
            var createPRTypeID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionRequestPRJobType" && o.Key == "1").Select(o => o.ID).FirstAsync();
            //completed pr
            var completedPrStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "SAPPRStatus" && o.Key == "1").Select(o => o.ID).FirstAsync();

            //request unit ที่ขอ PR ผ่านทั้งหมดแล้ว
            var requestUnits = await db.PreSalePromotionRequestUnits
                .Include(o => o.PreSalePromotionRequest).ThenInclude(o => o.MasterPreSalePromotion)
                .Where(o => o.UnitID == unitID &&
            o.PromotionRequestPRJobTypeMasterCenterID == createPRTypeID &&
            o.SAPPRStatusMasterCenterID == completedPrStatusID)
                .ToListAsync();


            if (requestUnits.Count > 0)
            {
                PreSalePromotionDTO result = new PreSalePromotionDTO();
                result.Items = new List<PreSalePromotinItemDTO>();
                foreach (var model in requestUnits)
                {
                    var items = await db.PreSalePromotionRequestItems
                        .Where(o => o.PreSalePromotionRequestUnitID == model.ID)
                        .Select(o => PreSalePromotinItemDTO.CreateFromRequestItemAsync(o)).ToListAsync();
                    result.Items.AddRange(items);
                }
                //เลขโปรล่าสุด
                result.PromotionNo = requestUnits.OrderByDescending(o => o.Created)
                    .Select(o => o.PreSalePromotionRequest.MasterPreSalePromotion.PromotionNo).FirstOrDefault();

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
