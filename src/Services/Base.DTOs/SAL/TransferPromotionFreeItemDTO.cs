using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class TransferPromotionFreeItemDTO : BaseDTO
    {
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// รายการโปรโมชั่น
        /// </summary>
        public List<TransferPromotionFreeItemDTO> Items { get; set; }

        public async static Task<TransferPromotionFreeItemDTO> CreateFromModelAsync(TransferPromotionFreeItem model, DatabaseContext db)
        {
            //if (model != null)
            //{
            //    var result = new TransferPromotionFreeItemDTO()
            //    {
            //        PromotionNo = model.MasterPromotion?.PromotionNo,
            //        Remark = model.Remark,
            //        Id = model.ID,
            //        Updated = model.Updated,
            //        UpdatedBy = model.UpdatedBy?.DisplayName
            //    };

            //    result.Items = new List<TransferPromotionFreeItemDTO>();
            //    var itemModels = await db.TransferPromotionItems
            //        .Include(o => o.MasterPromotionItem)
            //        .Include(o => o.QuotationTransferPromotionItem)
            //        .Include(o => o.UpdatedBy)
            //        .Where(o => o.TransferPromotionID == model.ID && o.MainTransferPromotionItemID == null).ToListAsync();
            //    if (itemModels.Count > 0)
            //    {
            //        var items = itemModels.Select(o => TransferPromotionFreeItemDTO.CreateFromModel(o, null, null, db)).ToList();
            //        result.Items.AddRange(items);
            //    }

            //    var freeModels = await db.TransferPromotionFreeItems
            //        .Include(o => o.MasterTransferPromotionFreeItem)
            //        .Include(o => o.QuotationTransferPromotionFreeItem)
            //        .Include(o => o.UpdatedBy)
            //        .Where(o => o.TransferPromotionID == model.ID).ToListAsync();
            //    if (freeModels.Count > 0)
            //    {
            //        var items = freeModels.Select(o => TransferPromotionFreeItemDTO.CreateFromModel(null, o, null, db)).ToList();
            //        result.Items.AddRange(items);
            //    }

            //    var creditModels = await db.TransferCreditCardItems
            //        .Include(o => o.MasterTransferCreditCardItem)
            //        .Include(o => o.QuotationTransferCreditCardItem)
            //        .Include(o => o.UpdatedBy)
            //        .Where(o => o.TransferPromotionID == model.ID).ToListAsync();
            //    if (creditModels.Count > 0)
            //    {
            //        var items = creditModels.Select(o => TransferPromotionFreeItemDTO.CreateFromModel(null, null, o, db)).ToList();
            //        result.Items.AddRange(items);
            //    }

            //    return result;
            //}
            //else
            //{
            //    return null;
            //}
            return null;
        }
        public void ToModel(ref TransferPromotionFreeItem model)
        {
            model = model ?? new TransferPromotionFreeItem();

            //model.ScheduleTransferDate = this.ScheduleTransferDate;
            //model.TransferSaleUserID = this.TransferSale?.Id;

            //model.ProjectID = this.Project?.Id;
            //model.UnitID = this.Unit?.Id;
            //model.AgreementID = this.Agreement?.Id;
            //model.MeterChequeMasterCenterID = this.MeterCheque?.Id;

        }
    }
}
