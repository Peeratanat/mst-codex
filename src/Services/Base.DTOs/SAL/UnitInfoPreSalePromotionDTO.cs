using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class UnitInfoPreSalePromotionDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโปรโมชั่นก่อนขาย
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// รายการโปรโมชั่นก่อนขาย
        /// </summary>
        public List<UnitInfoPreSalePromotionItemDTO> Items { get; set; }

        public async static Task<UnitInfoPreSalePromotionDTO> CreateFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            //สร้าง PR Job
            var createPRTypeID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.PromotionRequestPRJobType && o.Key == "1").Select(o => o.ID).FirstAsync();
            //completed pr
            var completedPrStatusID = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.SAPPRStatus && o.Key == "1").Select(o => o.ID).FirstAsync();

            //request unit ที่ขอ PR ผ่านทั้งหมดแล้ว
            var requestUnits = await db.PreSalePromotionRequestUnits
                .Include(o => o.PreSalePromotionRequest).ThenInclude(o => o.MasterPreSalePromotion)
                .Where(o => o.UnitID == unitID && o.PromotionRequestPRJobTypeMasterCenterID == createPRTypeID && o.SAPPRStatusMasterCenterID == completedPrStatusID)
                .ToListAsync();

            if (requestUnits.Count > 0)
            {
                // TODO: .Select(o => o.PreSalePromotionRequest?.MasterPreSalePromotion?.PromotionNo) เติมเช็ค Null (toon)
                var result = new UnitInfoPreSalePromotionDTO();
                result.PromotionNo = requestUnits.OrderByDescending(o => o.Created).Select(o => o.PreSalePromotionRequest?.MasterPreSalePromotion?.PromotionNo).FirstOrDefault();

                result.Items = new List<UnitInfoPreSalePromotionItemDTO>();
                foreach (var model in requestUnits)
                {
                    var items = await db.PreSalePromotionRequestItems
                        .Include(o => o.MasterPreSalePromotionItem)
                        .Where(o => o.PreSalePromotionRequestUnitID == model.ID)
                        .Select(o => UnitInfoPreSalePromotionItemDTO.CreateFromMaster(o)).ToListAsync();
                    result.Items.AddRange(items);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<UnitInfoPreSalePromotionDTO> CreateFromModelAsync(PreSalePromotion model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new UnitInfoPreSalePromotionDTO()
                {
                    PromotionNo = model.PreSalePromotionNo,
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                var items = await db.PreSalePromotionItems
                    .Include(o => o.MasterPreSalePromotionItem)
                    .Include(o=>o.PreSalePromotionRequestItem)
                    .Where(o => o.PreSalePromotionID == model.ID).ToListAsync();
                result.Items = new List<UnitInfoPreSalePromotionItemDTO>();
                if (items.Count > 0)
                {
                    result.Items.AddRange(items.Select(o => UnitInfoPreSalePromotionItemDTO.CreateFromModel(o)).ToList());
                }

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
