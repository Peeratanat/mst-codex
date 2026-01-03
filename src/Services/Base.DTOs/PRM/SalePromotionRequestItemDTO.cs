using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.DbQueries.PRM;
using Database.Models.MasterKeys;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// รายการสิ่งของเบิกโปรขาย
    /// Model: SalePromotionRequestItem
    /// </summary>
    public class SalePromotionRequestItemDTO : BaseDTO
    {
        /// <summary>
        /// ผูกการเบิกโปรโมชั่นขาย
        /// </summary>
        public Guid? SalePromotionRequestID { get; set; }
        /// <summary>
        /// ผูกสิ่งของโปรโมชั่นขาย
        /// </summary>
        public Guid? SalePromotionItemID { get; set; }
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
        public int SalePromotionQuantity { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย (บาท)
        /// </summary>
        public decimal PricePerUnit { get; set; }
        /// <summary>
        /// ราคารวม (บาท)
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// เบิกแล้ว
        /// </summary>
        public int RequestQuantity { get; set; }
        /// <summary>
        /// คงเหลือเบิก
        /// </summary>
        public int RemainingRequestQuantity { get; set; }
        /// <summary>
        /// จำนวนที่เบิก
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// วันที่คาดว่าจะได้รับ (default = วันที่ปัจจุบัน)
        /// </summary>
        public DateTime? ReceiveDate { get; set; }
        /// <summary>
        /// หมายเหตุไม่เบิก
        /// </summary>
        public string DenyRemark { get; set; }
        /// <summary>
        /// เลขที่ PR
        /// </summary>
        public string PRNo { get; set; }
        /// <summary>
        /// เลขที่ใบเบิก
        /// </summary>
        public string RequestNo { get; set; }
        /// <summary>
        /// หน่วย
        /// </summary>
        public string UnitTH { get; set; }
        /// <summary>
        /// เลขที่ SerialNo
        /// </summary>
        public string SerialNo { get; set; }
        /// <summary>
        /// เลขที่ใบส่งมอบ
        /// </summary>
        public string DeliveryNo { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public string UnitNo { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public string Project { get; set; }
        /// <summary>
        /// material
        /// </summary>
        public string MaterialGroup { get; set; }
        /// <summary>
        /// material
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// material
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// CheckNoRequest
        /// </summary>
        public bool NoRequest { get; set; }
        /// <summary>
        /// รายการที่ถูกเลือก
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// รายการย่อย
        /// </summary>
        public List<SalePromotionRequestItemDTO> SubItems { get; set; }

        /// <summary>
        /// ListTrId
        /// </summary>
        public string ListTrId { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        public async static Task<SalePromotionRequestItemDTO> CreateFromModelAsync(SalePromotionRequestItem model, DatabaseContext DB)
        {
            if (model != null)
            {
                model.SalePromotionItem = model.SalePromotionItem ?? new SalePromotionItem();

                var result = new SalePromotionRequestItemDTO();
                result.Id = model.ID;
                result.SalePromotionRequestID = model.SalePromotionRequest?.ID;
                result.SalePromotionItemID = model.SalePromotionItem.ID;
                result.IsMaster = !model.SalePromotionItem.MainSalePromotionItemID.HasValue;
                result.NameTH = model.SalePromotionItem.MasterPromotionItem?.NameTH;
                result.SalePromotionQuantity = model.SalePromotionItem.Quantity;
                result.PricePerUnit = model.SalePromotionItem.PricePerUnit;
                result.TotalPrice = model.SalePromotionItem.TotalPrice;
                result.MaterialGroup = model?.SalePromotionItem?.MasterPromotionItem?.MaterialGroupKey;
                result.MaterialCode = model?.SalePromotionItem?.MasterPromotionItem?.MaterialCode;
                result.MaterialName = model?.SalePromotionItem?.MasterPromotionItem?.MaterialName;
                result.UnitNo = model?.SalePromotionRequest?.SalePromotion?.Booking?.Unit?.UnitNo;
                result.Project = model?.SalePromotionRequest?.SalePromotion?.Booking?.Project?.ProjectNo;
                result.NoRequest = (model.SalePromotionRequest?.RequestNo ?? "").Contains("RS") ? true : false;

                // เบิกแล้ว
                result.RequestQuantity = model.RequestQuantity;
                // คงเหลือเบิก
                result.RemainingRequestQuantity = model.RemainingRequestQuantity;
                // จำนวนเบิก
                result.Quantity = model.Quantity;

                result.ReceiveDate = model.EstimateRequestDate;
                result.DenyRemark = model.DenyRemark;
                result.PRNo = model.PRNo;

                if (string.IsNullOrEmpty(result.PRNo))
                {
                    var prResult = await DB.PRRequestJobItemResults
                            .Include(o => o.PRRequestJobItem)
                            .Where(o => o.PRRequestJobItem.SalePromotionRequestItemID == model.ID).FirstOrDefaultAsync();

                    if (prResult != null && prResult.IsError)
                    {
                        result.PRNo = prResult.ErrorCode + " : " + prResult.ErrorDescription;
                        var msg = await DB.MasterCenters
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
                    var prResult = await DB.PRCancelJobItemResults
                            .Include(o => o.PRCancelJobItem)
                            .Where(o => o.PRCancelJobItem.SalePromotionRequestItemID == model.ID).FirstOrDefaultAsync();

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

                result.RequestNo = model.SalePromotionRequest?.RequestNo;
                result.UnitTH = model.SalePromotionItem?.MasterPromotionItem?.UnitTH;

                result.Updated = model?.Updated;

                result.UpdatedBy = model?.UpdatedBy == null ? model.CreatedBy?.DisplayName : model.UpdatedBy?.DisplayName;
                
                #region "โปรซัพย่อย"
                var subItems = await DB.SalePromotionRequestItems
                                    .Include(o => o.SalePromotionRequest)
                                    .Include(o => o.SalePromotionItem)
                                    .Include(o => o.SalePromotionItem.MasterPromotionItem)
                                    .Include(o => o.CreatedBy)
                                    .Include(o => o.UpdatedBy)
                                    .Where(o =>
                                        o.SalePromotionRequestID == model.SalePromotionRequestID
                                        && o.SalePromotionItem.MainSalePromotionItemID == model.SalePromotionItemID
                                    )
                                    .ToListAsync();

                result.SubItems = new List<SalePromotionRequestItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        item.SalePromotionItem = item.SalePromotionItem ?? new SalePromotionItem();

                        var promoItem = new SalePromotionRequestItemDTO()
                        {
                            Id = item.ID,
                            SalePromotionRequestID = item.SalePromotionRequest?.ID,
                            SalePromotionItemID = item.SalePromotionItem.ID,
                            IsMaster = !item.SalePromotionItem.MainSalePromotionItemID.HasValue,
                            NameTH = item.SalePromotionItem.MasterPromotionItem?.NameTH,
                            SalePromotionQuantity = model.SalePromotionItem.Quantity,
                            PricePerUnit = item.SalePromotionItem.PricePerUnit,
                            TotalPrice = item.SalePromotionItem.TotalPrice,
                            MaterialGroup = item?.SalePromotionItem?.MasterPromotionItem?.MaterialGroupKey,
                            MaterialCode = item?.SalePromotionItem?.MasterPromotionItem?.MaterialCode,
                            MaterialName = item?.SalePromotionItem?.MasterPromotionItem?.MaterialName,
                            UnitNo = item?.SalePromotionRequest?.SalePromotion?.Booking?.Unit?.UnitNo,
                            Project = item?.SalePromotionRequest?.SalePromotion?.Booking?.Project?.ProjectNo,
                            NoRequest = (item.SalePromotionRequest?.RequestNo ?? "").Contains("RS") ? true : false,
                            RequestQuantity = item.RequestQuantity,
                            RemainingRequestQuantity = item.RemainingRequestQuantity,
                            Quantity = model.Quantity, //item.Quantity,
                            ReceiveDate = item.EstimateRequestDate,
                            DenyRemark = item.DenyRemark,
                            PRNo = item.PRNo,
                            RequestNo = item.SalePromotionRequest?.RequestNo,
                            UnitTH = item.SalePromotionItem?.MasterPromotionItem?.UnitTH,
                            Updated = item?.Updated,
                            UpdatedBy = item?.UpdatedBy == null ? item.CreatedBy?.DisplayName : item.UpdatedBy?.DisplayName
                        };

                        if (string.IsNullOrEmpty(item.PRNo))
                        {
                            var prResult = await DB.PRRequestJobItemResults
                                    .Include(o => o.PRRequestJobItem)
                                    .Where(o => o.PRRequestJobItem.SalePromotionRequestItemID == item.ID).FirstOrDefaultAsync();

                            if (prResult != null && prResult.IsError)
                            {
                                promoItem.PRNo = prResult.ErrorCode + " : " + prResult.ErrorDescription;
                                var msg = await DB.MasterCenters
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
                            var prResult = await DB.PRCancelJobItemResults
                                    .Include(o => o.PRCancelJobItem)
                                    .Where(o => o.PRCancelJobItem.SalePromotionRequestItemID == model.ID).FirstOrDefaultAsync();

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
                }
                #endregion

                return result;
            }
            else
            {
                return null;
            }
        }

        /*
        public async static Task<SalePromotionRequestItemDTO> CreateFromModelDeliveryAsync(SalePromotionRequestItem model, DatabaseContext DB)
        {
            if (model != null)
            {
                model.SalePromotionItem = model.SalePromotionItem ?? new SalePromotionItem();

                var itemResults = await DB.SalePromotionRequestItems
                                    .Include(o => o.SalePromotionRequest)
                                    .Include(o => o.SalePromotionRequest.PromotionRequestPRStatus)
                                    .Include(o => o.SalePromotionRequest.SalePromotion)
                                    .Include(o => o.SalePromotionRequest.SalePromotion.Booking)
                                    .Include(o => o.SalePromotionItem)
                                    .Include(o => o.SalePromotionItem.MasterPromotionItem)
                                    .Where(o => o.SalePromotionItem.ID == model.SalePromotionItemID).ToListAsync();

                var itemDelivery = await DB.SalePromotionDeliveryItems
                                    .Include(o => o.SalePromotionDelivery)
                                    .Include(o => o.SalePromotionRequestItem)
                                    .Include(o => o.SalePromotionRequestItem.SalePromotionItem)
                                    .Include(o => o.SalePromotionRequestItem.SalePromotionItem.SalePromotion)
                                    .Include(o => o.SalePromotionRequestItem.SalePromotionItem.SalePromotion.Booking)
                                    .Where(o => o.SalePromotionRequestItem.SalePromotionItem.ID == model.SalePromotionItemID
                                       ).ToListAsync();

                var result = new SalePromotionRequestItemDTO();
                result.Id = model.ID;
                result.SalePromotionRequestID = model.SalePromotionRequest.ID;
                result.SalePromotionItemID = model.SalePromotionItem.ID;
                result.IsMaster = !model.SalePromotionItem.MainSalePromotionItemID.HasValue;
                result.NameTH = model.SalePromotionItem.MasterPromotionItem?.NameTH;
                result.SalePromotionQuantity = itemResults.Where(o => o.SalePromotionItemID == model.SalePromotionItemID).Sum(o => o.Quantity);
                result.PricePerUnit = model.SalePromotionItem.PricePerUnit;
                result.TotalPrice = model.SalePromotionItem.TotalPrice;
                result.MaterialGroup = model?.SalePromotionItem?.MasterPromotionItem?.MaterialGroupKey;
                result.MaterialCode = model?.SalePromotionItem?.MasterPromotionItem?.MaterialCode;
                result.MaterialName = model?.SalePromotionItem?.MasterPromotionItem?.MaterialName;
                result.UnitNo = model?.SalePromotionRequest?.SalePromotion?.Booking?.Unit?.UnitNo;
                result.Project = model?.SalePromotionRequest?.SalePromotion?.Booking?.Project?.ProjectNo;
                result.NoRequest = (model.SalePromotionRequest.RequestNo ?? "").Contains("RS") ? true : false;
                result.IsSelected = true;

                result.RequestQuantity = itemDelivery.Sum(o => o.Quantity);
                result.RemainingRequestQuantity = result.SalePromotionQuantity - itemDelivery.Sum(o => o.Quantity);

                if (itemDelivery.Where(o => !String.IsNullOrEmpty(o.SalePromotionDelivery?.DeliveryNo)).ToList().Any())
                {
                    result.DeliveryNo = String.Join(",", itemDelivery.Select(o => o.SalePromotionDelivery?.DeliveryNo ?? "").ToList());
                }
                else
                {
                    result.DeliveryNo = "";
                }

                result.Quantity = 0;
                result.ReceiveDate = model.EstimateRequestDate;
                result.DenyRemark = model.DenyRemark;
                result.PRNo = model.PRNo;
                result.RequestNo = model.SalePromotionRequest?.RequestNo;
                result.Updated = model?.Updated;
                result.UpdatedBy = model?.UpdatedBy == null ? model.CreatedBy?.DisplayName : model.UpdatedBy?.DisplayName;

                var subItems = DB.SalePromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainSalePromotionItemID == model.SalePromotionItem.ID).ToList();

                result.SubItems = new List<SalePromotionItemDTO>();

                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new SalePromotionItemDTO()
                        {
                            FromMasterSalePromotionItemID = item.MasterSalePromotionItemID,
                            FromQuotationSalePromotionItemID = model.SalePromotionItem.QuotationSalePromotionItemID,
                            NameTH = item.MasterPromotionItem?.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.MasterPromotionItem?.UnitTH,
                            ItemType = PromotionItemType.Item,
                            Id = model.SalePromotionItem.ID,
                            Updated = model.SalePromotionItem.Updated,
                            UpdatedBy = model.SalePromotionItem.UpdatedBy?.DisplayName,
                            IsSelected = true,
                            SalePromotionID = model.SalePromotionItem.SalePromotionID,

                        };

                        var requestSub = DB.SalePromotionRequestItems
                                .Include(o => o.SalePromotionRequest)
                                .ThenInclude(o => o.PromotionRequestPRStatus)
                                .Where(o => o.SalePromotionItemID == item.ID
                                        && o.SalePromotionRequest.PromotionRequestPRStatus.Key != PromotionRequestPRStatusKeys.Reject).ToList();

                        promoItem.RequestedQuantity = result.RequestQuantity;
                        promoItem.RemainingRequestQuantity = result.RemainingRequestQuantity;
                        promoItem.RequestQuantity = result.Quantity;
                        promoItem.ReceiveDate = result.ReceiveDate;
                        promoItem.DenyRemark = result.DenyRemark;
                        promoItem.RequestNo = result.RequestNo;
                        promoItem.PRNo = result.PRNo;

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

        public async static Task<SalePromotionRequestItemDTO> CreateFromMasterModelAsync(MasterSalePromotionItem model, Guid SalePromotionItemID, DatabaseContext DB)
        {
            if (model != null)
            {
                var itemResults = await DB.SalePromotionRequestItems
                                    .Include(o => o.SalePromotionRequest)
                                        .ThenInclude(o => o.PromotionRequestPRStatus)
                                    .Include(o => o.SalePromotionItem)
                                    .Where(o => o.SalePromotionItem.ID == SalePromotionItemID
                                          ).ToListAsync();

                var result = new SalePromotionRequestItemDTO
                {
                    Id = model.ID,
                    SalePromotionRequestID = null,
                    SalePromotionItemID = null,
                    IsMaster = !model.MainPromotionItemID.HasValue,
                    NameTH = model.NameTH,
                    SalePromotionQuantity = model.Quantity,
                    PricePerUnit = model.PricePerUnit,
                    TotalPrice = model.TotalPrice,
                    RequestQuantity = itemResults.Sum(o => o.Quantity),
                    RemainingRequestQuantity = model.Quantity - itemResults.Sum(o => o.Quantity),
                    Quantity = 0,
                    ReceiveDate = DateTime.Now,
                    DenyRemark = "",
                    PRNo = ""
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        */

        public static SalePromotionRequestItemDTO CreateFromQuery(dbqSalePromotionRequestItemForDelivery model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new SalePromotionRequestItemDTO();
                result.Id = model.ID;
                result.SalePromotionRequestID = model.SalePromotionRequestID;
                result.SalePromotionItemID = model.SalePromotionItemID;
                result.IsMaster = !model.MainPromotionItemID.HasValue;
                result.NameTH = model.NameTH;
                result.SalePromotionQuantity = model.SalePromotionQuantity ?? 0;
                result.PricePerUnit = model.PricePerUnit ?? 0;
                result.TotalPrice = model.TotalPrice ?? 0;
                result.MaterialGroup = model.MaterialGroupKey;
                result.MaterialCode = model.MaterialCode;
                result.MaterialName = model.MaterialName;
                result.UnitNo = model.UnitNo;
                result.Project = model.ProjectNo;
                result.NoRequest = model.NoRequest ?? false;
                result.IsSelected = model.IsSelected ?? true;
                result.RequestQuantity = model.RequestQuantity ?? 0;
                result.RemainingRequestQuantity = model.RemainingRequestQuantity ?? 0;
                result.DeliveryNo = model.DeliveryNo;
                result.Quantity = model.Quantity ?? 0;
                result.ReceiveDate = model.EstimateRequestDate;
                result.DenyRemark = model.DenyRemark;
                result.PRNo = model.PRNo;
                result.RequestNo = model.RequestNo;
                result.Updated = model?.Updated;
                result.UpdatedBy = model.UpdatedByDisplayName;
                result.UnitTH = model.UnitTH;
                result.ListTrId = model.ListTrId;

                if (model.SalePromotionItemID != null)
                {
                    var subItems = DB.SalePromotionRequestItems
                            .Include(o => o.SalePromotionRequest)
                            .Include(o => o.SalePromotionItem)
                            .Include(o => o.SalePromotionItem.MasterPromotionItem)
                            .Include(o => o.CreatedBy)
                            .Include(o => o.UpdatedBy)
                            .Where(o =>
                                o.SalePromotionRequestID == model.SalePromotionRequestID
                                && o.SalePromotionItem.MainSalePromotionItemID == model.SalePromotionItemID
                            )
                            .ToList();

                    result.SubItems = new List<SalePromotionRequestItemDTO>();

                    if (subItems.Count > 0)
                    {
                        foreach (var item in subItems)
                        {
                            var promoItem = new SalePromotionRequestItemDTO()
                            {
                                Id = item.ID,
                                SalePromotionRequestID = item.SalePromotionRequest?.ID,
                                SalePromotionItemID = item.SalePromotionItem.ID,
                                IsMaster = !item.SalePromotionItem.MainSalePromotionItemID.HasValue,
                                NameTH = item.SalePromotionItem.MasterPromotionItem?.NameTH,
                                PricePerUnit = item.SalePromotionItem.PricePerUnit,
                                TotalPrice = item.SalePromotionItem.TotalPrice,
                                MaterialGroup = item?.SalePromotionItem?.MasterPromotionItem?.MaterialGroupKey,
                                MaterialCode = item?.SalePromotionItem?.MasterPromotionItem?.MaterialCode,
                                MaterialName = item?.SalePromotionItem?.MasterPromotionItem?.MaterialName,
                                UnitNo = item?.SalePromotionRequest?.SalePromotion?.Booking?.Unit?.UnitNo,
                                Project = item?.SalePromotionRequest?.SalePromotion?.Booking?.Project?.ProjectNo,
                                NoRequest = (item.SalePromotionRequest?.RequestNo ?? "").Contains("RS") ? true : false,

                                SalePromotionQuantity = result.SalePromotionQuantity, //item.SalePromotionItem.Quantity,
                                RequestQuantity = result.RequestQuantity, //item.RequestQuantity,
                                RemainingRequestQuantity = result.RemainingRequestQuantity, //item.RemainingRequestQuantity,
                                Quantity = result.Quantity, //item.Quantity,

                                ReceiveDate = item.EstimateRequestDate,
                                DenyRemark = item.DenyRemark,
                                PRNo = item.PRNo,
                                RequestNo = item.SalePromotionRequest?.RequestNo,
                                UnitTH = item.SalePromotionItem?.MasterPromotionItem?.UnitTH,
                                Updated = item?.Updated,
                                UpdatedBy = item?.UpdatedBy == null ? item.CreatedBy?.DisplayName : item.UpdatedBy?.DisplayName

                            };

                            if (string.IsNullOrEmpty(item.PRNo))
                            {
                                var prResult = DB.PRRequestJobItemResults
                                        .Include(o => o.PRRequestJobItem)
                                        .Where(o => o.PRRequestJobItem.SalePromotionRequestItemID == item.ID).FirstOrDefault();

                                if (prResult != null && prResult.IsError)
                                {
                                    promoItem.PRNo = prResult.ErrorCode + " : " + prResult.ErrorDescription;
                                    var msg = DB.MasterCenters
                                       .Where(o => o.MasterCenterGroupKey == "SapMessage"
                                               && o.Key == prResult.ErrorCode.Replace("-", ""))
                                       .FirstOrDefault();

                                    if (msg != null)
                                    {
                                        promoItem.PRNo = msg.Name;
                                    }
                                }
                            }

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
