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
    /// รายการแปลงเบิกโปรก่อนขาย
    /// Model: PreSalePromotionRequestUnit
    /// </summary>
    public class PreSalePromotionRequestUnitListDTO : BaseDTO
    {
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
        /// เลขที่ PR
        /// </summary>
        public List<string> PRNos { get; set; }
        /// <summary>
        /// Error Code จาก Sap
        /// </summary>
        public List<string> ErrorCodes { get; set; }
        /// <summary>
        ///  Error Massage จาก Sap
        /// </summary>
        public List<string> ErrorMessages { get; set; }
        public async static Task<PreSalePromotionRequestUnitListDTO> CreateFromModelAsync(PreSalePromotionRequestUnit model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new PreSalePromotionRequestUnitListDTO
                {
                    Id = model.ID,
                    Unit = await PRJ.UnitDropdownSellPriceDTO.CreateFromModelAsync(model.Unit, DB),
                    SAPPRStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.SAPPRStatus),
                    PRJobType = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionRequestPRJobType),
                    Remark = model.Remark,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                result.PRNos = await DB.PreSalePromotionRequestItems
                                        .Where(o => o.PreSalePromotionRequestUnitID == model.ID && !string.IsNullOrEmpty(o.PRNo))
                                        .Select(o => o.PRNo)
                                        .Distinct()
                                        .ToListAsync();

                if (model.PromotionRequestPRJobType.Key == PromotionRequestPRJobTypeKeys.CreatePR)
                {
                    if (model.SAPPRStatus.Key == SAPPRStatusKeys.Failed)
                    {
                        result.ErrorCodes = new List<string>();
                        result.ErrorMessages = new List<string>();
                        var prRequestItems = await DB.PreSalePromotionRequestItems.Where(o => o.PreSalePromotionRequestUnitID == model.ID).ToListAsync();
                        foreach (var item in prRequestItems)
                        {
                            var prRequestJobItemID = await DB.PRRequestJobItems.Where(o => o.PreSalePromotionRequestItemID == item.ID).FirstOrDefaultAsync();

                            if (prRequestJobItemID != null)
                            {
                                var resultFromSync = await DB.PRRequestJobItemResults.Where(o => o.PRRequestJobItemID == prRequestJobItemID.ID).FirstOrDefaultAsync();

                                if (resultFromSync != null)
                                {
                                    result.ErrorCodes.Add(resultFromSync.ErrorCode);
                                    result.ErrorMessages.Add(resultFromSync.ErrorDescription);
                                }
                            }
                        }
                    }

                }
                if (model.PromotionRequestPRJobType.Key == PromotionRequestPRJobTypeKeys.CancelPR)
                {
                    if (model.SAPPRStatus.Key == SAPPRStatusKeys.Failed)
                    {
                        result.ErrorCodes = new List<string>();
                        result.ErrorMessages = new List<string>();
                        var prRequestItems = await DB.PreSalePromotionRequestItems.Where(o => o.PreSalePromotionRequestUnitID == model.ID).ToListAsync();
                        foreach (var item in prRequestItems)
                        {
                            var prCancelJobItemID = await DB.PRCancelJobItems.Where(o => o.PreSalePromotionRequestItemID == item.ID).FirstOrDefaultAsync();
                            
                            if (prCancelJobItemID != null)
                            {
                                var resultFromSync = await DB.PRCancelJobItemResults.Where(o => o.PRCancelJobItemID == prCancelJobItemID.ID).FirstOrDefaultAsync();

                                if (resultFromSync != null)
                                {
                                    result.ErrorCodes.Add(resultFromSync.ErrorCode);
                                    result.ErrorMessages.Add(resultFromSync.ErrorDescription);
                                }
                            }
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
