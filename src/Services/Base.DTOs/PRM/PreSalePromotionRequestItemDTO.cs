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
    /// รายการสิ่งของเบิกโปรก่อนขาย
    /// Model: PreSalePromotionRequestItem
    /// </summary>
    public class PreSalePromotionRequestItemDTO : BaseDTO
    {
        /// <summary>
        /// ถูกเลือก
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// ข้อมูลมาจาก Master
        /// </summary>
        public bool IsMaster { get; set; }
        /// <summary>
        /// ชื่อผลิตภัณฑ์ (TH)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// จำนวน
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย (บาท)
        /// </summary>
        public decimal PricePerUnit { get; set; }
        /// <summary>
        /// ราคารวม (บาท)
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// วันที่คาดว่าจะได้รับ (default = วันที่ปัจจุบัน)
        /// </summary>
        public DateTime? ReceiveDate { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// เลขที่ PR
        /// </summary>
        public string PRNo { get; set; }
        /// <summary>
        /// MasterPreSalePromotionItemID
        /// </summary>
        public Guid? MasterPreSalePromotionItemID { get; set; }
        /// <summary>
        /// เบิกแล้ว
        /// </summary>
        public int RequestQuantity { get; set; }
        /// <summary>
        /// คงเหลือเบิก
        /// </summary>
        public int RemainingRequestQuantity { get; set; }
        /// <summary>
        /// หน่วย
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// รายการซัพย่อย
        /// </summary>
        public List<PreSalePromotionRequestItemDTO> SubItems { get; set; }

        /// <summary>
        /// แก้ไข จำนวน
        /// </summary>
        public bool IsEditQuantity{ get; set; }

        public static async Task<PreSalePromotionRequestItemDTO> CreateFromModelAsync(PreSalePromotionRequestItem model, DatabaseContext db)
        {
            if (model != null)
            {
                var masterItem = await db.MasterPreSalePromotionItems
                                        .Where(o => o.ID == model.MasterPreSalePromotionItemID
                                                 && o.MainPromotionItemID == null)
                                        .FirstOrDefaultAsync();

                var result = new PreSalePromotionRequestItemDTO
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    Quantity = model.Quantity,
                    PricePerUnit = masterItem.TotalPrice,
                    TotalPrice = model.Quantity * masterItem.TotalPrice,
                    ReceiveDate = model.ReceiveDate,
                    Remark = model.Remark,
                    PRNo = model.PRNo,
                    MasterPreSalePromotionItemID = model.MasterPreSalePromotionItemID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    RequestQuantity = 0,
                    RemainingRequestQuantity = 0,
                    UnitName = model.UnitTH,
                    IsMaster = false,
                    IsSelected = true,
                    IsEditQuantity = false
                };

                if (string.IsNullOrEmpty(result.PRNo))
                {
                    var prResult = await db.PRRequestJobItemResults
                            .Include(o => o.PRRequestJobItem)
                            .Where(o => o.PRRequestJobItem.PreSalePromotionRequestItemID == model.ID).FirstOrDefaultAsync();

                    if (prResult != null && prResult.IsError)
                    {
                        result.PRNo = prResult.ErrorCode + " : " + prResult.ErrorDescription;
                        var msg = await db.MasterCenters
                                   .Where(o => o.MasterCenterGroupKey == "SapMessage"
                                           && o.Key == prResult.ErrorCode.Replace("-", ""))
                                   .FirstOrDefaultAsync();

                        if (msg != null)
                        {
                            result.PRNo = msg.Name;
                        }
                    }
                }
                else
                {
                    var prResult = await db.PRCancelJobItemResults
                            .Include(o => o.PRCancelJobItem)
                            .Where(o => o.PRCancelJobItem.PreSalePromotionRequestItemID == model.ID).FirstOrDefaultAsync();

                    if (prResult != null && !prResult.SAPDeleteFlag)
                    {
                        if (!string.IsNullOrEmpty(prResult.ErrorCode))
                        {
                            result.Remark = prResult.ErrorCode + " : " + prResult.ErrorDescription;
                        }
                        else
                        {
                            result.Remark = "ของได้ถูกสั่งซื้อแล้ว";
                        }
                    }
                    else
                    {
                        result.Remark = "";
                    }
                }

                var subItems = await db.MasterPreSalePromotionItems
                                        .Include(o => o.UpdatedBy)
                                        .Where(o => o.MasterPreSalePromotionID == masterItem.MasterPreSalePromotionID
                                                      && o.MainPromotionItemID == masterItem.ID)
                                        .ToListAsync();

                result.SubItems = new List<PreSalePromotionRequestItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var requestItems = await db.PreSalePromotionRequestItems
                                            .Include(o => o.MasterPreSalePromotionItem)
                                            .Include(o => o.UpdatedBy)
                                            .Where(o => o.PreSalePromotionRequestUnitID == model.PreSalePromotionRequestUnitID
                                                        && o.MasterPreSalePromotionItemID == item.ID)
                                            .FirstOrDefaultAsync();

                        if (requestItems != null)
                        {
                            var promoItem = new PreSalePromotionRequestItemDTO()
                            {
                                Id = requestItems?.ID,
                                NameTH = item.NameTH,
                                Quantity = requestItems.Quantity,
                                PricePerUnit = item.PricePerUnit,
                                TotalPrice = requestItems.Quantity * item.PricePerUnit,
                                ReceiveDate = requestItems?.ReceiveDate,
                                Remark = requestItems?.Remark,
                                PRNo = requestItems?.PRNo,
                                MasterPreSalePromotionItemID = requestItems?.MasterPreSalePromotionItemID,
                                Updated = requestItems?.Updated,
                                UpdatedBy = requestItems?.UpdatedBy?.DisplayName,
                                RequestQuantity = 0,
                                RemainingRequestQuantity = 0,
                                UnitName = item.UnitTH,
                                IsMaster = false,
                                IsSelected = true,
                                IsEditQuantity = false
                            };

                            if (string.IsNullOrEmpty(requestItems?.PRNo))
                            {
                                var prResult = await db.PRRequestJobItemResults
                                        .Include(o => o.PRRequestJobItem)
                                        .Where(o => o.PRRequestJobItem.PreSalePromotionRequestItemID == requestItems.ID).FirstOrDefaultAsync();

                                if (prResult != null && prResult.IsError)
                                {
                                    promoItem.PRNo = prResult.ErrorCode + " : " + prResult.ErrorDescription;
                                    var msg = await db.MasterCenters
                                       .Where(o => o.MasterCenterGroupKey == "SapMessage"
                                               && o.Key == prResult.ErrorCode.Replace("-", ""))
                                       .FirstOrDefaultAsync();

                                    if (msg != null)
                                    {
                                        promoItem.PRNo = msg.Name;
                                    }
                                }
                            }
                            else
                            {
                                var prResult = await db.PRCancelJobItemResults
                                        .Include(o => o.PRCancelJobItem)
                                        .Where(o => o.PRCancelJobItem.PreSalePromotionRequestItemID == requestItems.ID).FirstOrDefaultAsync();

                                if (prResult != null && !prResult.SAPDeleteFlag)
                                {
                                    if (!string.IsNullOrEmpty(prResult.ErrorCode))
                                    {
                                        promoItem.Remark = prResult.ErrorCode + " : " + prResult.ErrorDescription;
                                    }
                                    else
                                    {
                                        promoItem.Remark = "ของได้ถูกสั่งซื้อแล้ว";
                                    }
                                }
                                else
                                {
                                    promoItem.Remark = "";
                                }
                            }

                            result.SubItems.Add(promoItem);
                        }
                        else
                        {
                            var promoItem = new PreSalePromotionRequestItemDTO()
                            {
                                Id = item?.ID,
                                NameTH = item.NameTH,
                                Quantity = model.Quantity,
                                PricePerUnit = item.PricePerUnit,
                                TotalPrice = model.Quantity * item.PricePerUnit,
                                ReceiveDate = model?.ReceiveDate,
                                Remark = model?.Remark,
                                PRNo = "",
                                MasterPreSalePromotionItemID = item?.ID,
                                Updated = model?.Updated,
                                UpdatedBy = model?.UpdatedBy?.DisplayName,
                                RequestQuantity = 0,
                                RemainingRequestQuantity = 0,
                                UnitName = item.UnitTH,
                                IsMaster = false,
                                IsSelected = true,
                                IsEditQuantity = false
                            };
                            result.SubItems.Add(promoItem);
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

        public static async Task<PreSalePromotionRequestItemDTO> CreateFromMasterModelAsync(MasterPreSalePromotionItem model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new PreSalePromotionRequestItemDTO
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    Quantity = model.Quantity,
                    PricePerUnit = model.TotalPrice,
                    TotalPrice = model.Quantity * model.TotalPrice,
                    ReceiveDate = null,
                    Remark = model.Remark,
                    MasterPreSalePromotionItemID = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    RequestQuantity = 0,
                    RemainingRequestQuantity = 0,
                    UnitName = model.UnitTH,
                    IsMaster = true,
                    IsSelected = false,
                    IsEditQuantity = false
                };

                var subItems = await db.MasterPreSalePromotionItems
                                        .Include(o => o.PromotionItemStatus)
                                        .Include(o => o.UpdatedBy)
                                        .Where(o => o.MasterPreSalePromotionID == model.MasterPreSalePromotionID
                                                      && o.MainPromotionItemID == model.ID
                                                      && o.ExpireDate > DateTime.Now
                                                      && o.PromotionItemStatus.Key == "1")
                                        .ToListAsync(); ;

                result.SubItems = new List<PreSalePromotionRequestItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new PreSalePromotionRequestItemDTO()
                        {
                            Id = item.ID,
                            NameTH = item.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.Quantity * item.PricePerUnit,
                            ReceiveDate = null,
                            Remark = item.Remark,
                            MasterPreSalePromotionItemID = item.ID,
                            Updated = item.Updated,
                            UpdatedBy = item.UpdatedBy?.DisplayName,
                            RequestQuantity = 0,
                            RemainingRequestQuantity = 0,
                            UnitName = item.UnitTH,
                            IsMaster = true,
                            IsSelected = false,
                            IsEditQuantity = false
                        };

                        result.SubItems.Add(promoItem);
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public static async Task<PreSalePromotionRequestItemDTO> CreateFromMasterEditQuantityModelAsync(MasterPreSalePromotionItem model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new PreSalePromotionRequestItemDTO
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    Quantity = model.Quantity,
                    PricePerUnit = model.TotalPrice,
                    TotalPrice = model.Quantity * model.TotalPrice,
                    ReceiveDate = null,
                    Remark = model.Remark,
                    MasterPreSalePromotionItemID = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    RequestQuantity = 0,
                    RemainingRequestQuantity = 0,
                    UnitName = model.UnitTH,
                    IsMaster = true,
                    IsSelected = false,
                    IsEditQuantity = true
                };

                //var subItems = await db.MasterPreSalePromotionItems
                //                        .Include(o => o.PromotionItemStatus)
                //                        .Include(o => o.UpdatedBy)
                //                        .Where(o => o.MasterPreSalePromotionID == model.MasterPreSalePromotionID
                //                                      && o.MainPromotionItemID == model.ID
                //                                      && o.ExpireDate > DateTime.Now
                //                                      && o.PromotionItemStatus.Key == "1")
                //                        .ToListAsync();


                //result.SubItems = new List<PreSalePromotionRequestItemDTO>();
                //if (subItems.Count > 0)
                //{
                //    foreach (var item in subItems)
                //    {
                //        var promoItem = new PreSalePromotionRequestItemDTO()
                //        {
                //            Id = item.ID,
                //            NameTH = item.NameTH,
                //            Quantity = item.Quantity,
                //            PricePerUnit = item.PricePerUnit,
                //            TotalPrice = item.Quantity * item.PricePerUnit,
                //            ReceiveDate = null,
                //            Remark = item.Remark,
                //            MasterPreSalePromotionItemID = item.ID,
                //            Updated = item.Updated,
                //            UpdatedBy = item.UpdatedBy?.DisplayName,
                //            RequestQuantity = 0,
                //            RemainingRequestQuantity = 0,
                //            UnitName = item.UnitTH,
                //            IsMaster = true,
                //            IsSelected = false,
                //            IsEditQuantity = true
                //        };

                //        result.SubItems.Add(promoItem);
                //    }
                //}

                var masterItems = await db.MasterPreSalePromotionItems
                        .Include(o => o.PromotionItemStatus)
                        .Include(o => o.UpdatedBy)
                        .Where(o => o.MasterPreSalePromotionID == model.MasterPreSalePromotionID
                                      && o.MainPromotionItemID == model.ID
                                      && o.ExpireDate > DateTime.Now
                                      && o.PromotionItemStatus.Key == "1")
                        .ToListAsync();

                var presaleItems = await db.PreSalePromotionRequestItems
                                  .Include(o => o.MasterPreSalePromotionItem)
                                  .Where(o => o.PreSalePromotionRequestUnitID == model.ID
                                              && o.MasterPreSalePromotionItem.MainPromotionItemID == null)
                                  .ToListAsync();

                result.SubItems = new List<PreSalePromotionRequestItemDTO>();
                if (masterItems.Count > 0)
                {
                    foreach (var item in masterItems)
                    {
                        var requestItem = presaleItems.Where(o => o.MasterPreSalePromotionItemID == item.ID).FirstOrDefault();
                        if (requestItem != null)
                        {
                            var promoItem = new PreSalePromotionRequestItemDTO()
                            {
                                Id = requestItem.ID,
                                NameTH = requestItem.NameTH,
                                Quantity = requestItem.Quantity,
                                PricePerUnit = requestItem.PricePerUnit,
                                TotalPrice = requestItem.TotalPrice,
                                ReceiveDate = null,
                                Remark = requestItem.Remark,
                                MasterPreSalePromotionItemID = requestItem.ID,
                                Updated = requestItem.Updated,
                                UpdatedBy = requestItem.UpdatedBy?.DisplayName,
                                RequestQuantity = 0,
                                RemainingRequestQuantity = 0,
                                UnitName = requestItem.UnitTH,
                                IsMaster = true,
                                IsSelected = false,
                                IsEditQuantity = true
                            };

                            result.SubItems.Add(promoItem);
                        }
                        else
                        {
                            var promoItem = new PreSalePromotionRequestItemDTO()
                            {
                                Id = item.ID,
                                NameTH = item.NameTH,
                                Quantity = item.Quantity,
                                PricePerUnit = item.PricePerUnit,
                                TotalPrice = item.Quantity * item.PricePerUnit,
                                ReceiveDate = null,
                                Remark = item.Remark,
                                MasterPreSalePromotionItemID = item.ID,
                                Updated = item.Updated,
                                UpdatedBy = item.UpdatedBy?.DisplayName,
                                RequestQuantity = 0,
                                RemainingRequestQuantity = 0,
                                UnitName = item.UnitTH,
                                IsMaster = true,
                                IsSelected = false,
                                IsEditQuantity = true
                            };

                            result.SubItems.Add(promoItem);
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
