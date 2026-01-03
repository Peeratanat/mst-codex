using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// รายละเอียดแปลงเบิกโปรก่อนขาย
    /// Model: PreSalePromotionRequestUnit
    /// </summary>
    public class PreSalePromotionRequestUnitDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ใบเบิก
        /// </summary>
        public string RequestNo { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// แปลง
        /// GET masterdata/Projects/{projectID}/Units/DropdownListSellPrice
        /// </summary>
        public PRJ.UnitDropdownSellPriceDTO Unit { get; set; }
        /// <summary>
        /// สถานะการสร้าง/ยกเลิก PR
        /// </summary>
        public MST.MasterCenterDropdownDTO SAPPRStatus { get; set; }
        /// <summary>
        /// ชนิดของการสร้าง/ยกเลิก PR
        /// </summary>
        public MST.MasterCenterDropdownDTO PRJobType { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// รายการสิ่งของ
        /// </summary>
        public List<PreSalePromotionRequestItemDTO> RequestItems { get; set; }
        /// <summary>
        /// จำนวนครั้งที่พิมพ์
        /// </summary>
        public int? PrintCount { get; set; }
        /// <summary>
        /// วันที่พิมพ์
        /// </summary>
        public DateTime? PrintDate { get; set; }
        /// <summary>
        /// ขายแล้วหรือยัง?
        /// </summary>
        public bool IsSaled { get; set; }

        public async static Task<PreSalePromotionRequestUnitDTO> CreateFromModelAsync(PreSalePromotionRequestUnit model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new PreSalePromotionRequestUnitDTO
                {
                    Id = model.ID,
                    RequestNo = model.PreSalePromotionRequest?.RequestNo,
                    Project = ProjectDropdownDTO.CreateFromModel(model.Unit?.Project),
                    Unit = await UnitDropdownSellPriceDTO.CreateFromModelAsync(model.Unit, DB),
                    SAPPRStatus = MasterCenterDropdownDTO.CreateFromModel(model.SAPPRStatus),
                    PRJobType = MasterCenterDropdownDTO.CreateFromModel(model.PromotionRequestPRJobType),
                    Remark = model.Remark,
                    PromotionNo = model.PreSalePromotionRequest?.MasterPreSalePromotion?.PromotionNo
                };
                result.Updated = model?.Updated;
                result.UpdatedBy = result?.UpdatedBy == null ? model.CreatedBy?.EmployeeNo + " - " + model.CreatedBy?.DisplayName : model.UpdatedBy?.EmployeeNo + " - " + model.UpdatedBy?.DisplayName;

                var book = await DB.Bookings
                                    .Where(o => o.UnitID == model.UnitID && o.IsCancelled == false)
                                    .FirstOrDefaultAsync();

                if (book != null)
                {
                    result.IsSaled = true;
                    result.Remark = "ห้องนี้ทำการขายไปแล้ว ไม่สามารถทำการถอย PR ได้!!!";
                }
                else
                {
                    result.IsSaled = false;
                }

                var masterItems = await DB.MasterPreSalePromotionItems
                                            .Include(o => o.PromotionItemStatus)
                                            .Where(o => o.MasterPreSalePromotionID == model.PreSalePromotionRequest.MasterPreSalePromotion.ID
                                                  && o.MainPromotionItemID == null)
                                            //&& o.ExpireDate > DateTime.Now
                                            //&& o.PromotionItemStatus.Key == "1")
                                            .OrderBy(o => o.Created)
                                            .ToListAsync();

                var presaleItems = await DB.PreSalePromotionRequestItems
                                            .Include(o => o.MasterPreSalePromotionItem)
                                            .Where(o => o.PreSalePromotionRequestUnitID == model.ID
                                                        && o.MasterPreSalePromotionItem.MainPromotionItemID == null)
                                            .ToListAsync();

                result.RequestItems = new List<PreSalePromotionRequestItemDTO>();

                foreach (var item in masterItems)
                {
                    var requestItem = presaleItems.Where(o => o.MasterPreSalePromotionItemID == item.ID).FirstOrDefault();
                    if (requestItem != null)
                    {
                        var modelItem = await PreSalePromotionRequestItemDTO.CreateFromModelAsync(requestItem, DB);
                        result.RequestItems.Add(modelItem);
                    }
                    else
                    {
                        if (item.ExpireDate > DateTime.Now && item.PromotionItemStatus.Key == "1")
                        {
                            var modelItem = await PreSalePromotionRequestItemDTO.CreateFromMasterEditQuantityModelAsync(item, DB);
                            result.RequestItems.Add(modelItem);
                        }
                    }
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
