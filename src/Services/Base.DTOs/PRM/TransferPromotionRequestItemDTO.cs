using Base.DTOs.PRJ;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.DbQueries.PRM;
using Database.Models.MasterKeys;
using Database.Models.Migrations;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// รายการสิ่งของเบิกโปรโอน
    /// Model: TransferPromotionRequestItem
    /// </summary>
    public class TransferPromotionRequestItemDTO : BaseDTO
    {
        /// <summary>
        /// ผูกการเบิกโปรโมชั่นขาย
        /// </summary>
        public Guid? TransferPromotionRequestID { get; set; }

        /// <summary>
        /// ผูกสิ่งของโปรโมชั่นขาย
        /// </summary>
        public Guid? TransferPromotionItemID { get; set; }

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
        public int TransferPromotionQuantity { get; set; }
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
        /// เลขที่ใบส่งมอบ
        /// </summary>
        public string DeliveryNo { get; set; }

        /// <summary>
        /// รายการย่อย
        /// </summary>
        public List<TransferPromotionRequestItemDTO> SubItems { get; set; }

        /// <summary>
        /// ListTrId
        /// </summary>
        public string ListTrId { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }

        public async static Task<TransferPromotionRequestItemDTO> CreateFromModelAsync(TransferPromotionRequestItem model, DatabaseContext DB)
        {
            if (model != null)
            {
                model.TransferPromotionItem = model.TransferPromotionItem ?? new TransferPromotionItem();

                var result = new TransferPromotionRequestItemDTO();
                result.Id = model.ID;
                result.TransferPromotionRequestID = model.TransferPromotionRequest?.ID;
                result.TransferPromotionItemID = model.TransferPromotionItem.ID;
                result.IsMaster = !model.TransferPromotionItem.MainTransferPromotionItemID.HasValue;
                result.NameTH = model?.TransferPromotionItem?.MasterPromotionItem?.NameTH;
                result.TransferPromotionQuantity = model.TransferPromotionItem.Quantity;
                result.PricePerUnit = model.TransferPromotionItem.PricePerUnit;
                result.TotalPrice = model.TransferPromotionItem.TotalPrice;
                result.MaterialGroup = model?.TransferPromotionItem?.MasterPromotionItem?.MaterialGroupKey;
                result.MaterialCode = model?.TransferPromotionItem?.MasterPromotionItem?.MaterialCode;
                result.MaterialName = model?.TransferPromotionItem?.MasterPromotionItem?.MaterialName;
                result.UnitNo = model?.TransferPromotionRequest?.TransferPromotion?.Booking?.Unit?.UnitNo;
                result.Project = model?.TransferPromotionRequest?.TransferPromotion?.Booking?.Project?.ProjectNo;
                result.NoRequest = (model.TransferPromotionRequest?.RequestNo ?? "").Contains("RT") ? true : false;

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
                            .Where(o => o.PRRequestJobItem.TransferPromotionRequestItemID == model.ID).FirstOrDefaultAsync();

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
                            .Where(o => o.PRCancelJobItem.TransferPromotionRequestItemID == model.ID).FirstOrDefaultAsync();

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

                result.UnitTH = model?.TransferPromotionItem?.MasterPromotionItem?.UnitTH;

                result.Updated = model?.Updated;
               
                result.UpdatedBy = model?.UpdatedBy == null ? model.CreatedBy?.DisplayName : model.UpdatedBy?.DisplayName;
                

                #region "โปรซัพย่อย"
                var subItems = await DB.TransferPromotionRequestItems
                            .Include(o => o.TransferPromotionRequest)
                            .Include(o => o.TransferPromotionItem)
                            .Include(o => o.TransferPromotionItem.MasterPromotionItem)
                            .Include(o => o.CreatedBy)
                            .Include(o => o.UpdatedBy)
                            .Where(o =>
                                o.TransferPromotionRequestID == model.TransferPromotionRequestID
                                && o.TransferPromotionItem.MainTransferPromotionItemID == model.TransferPromotionItemID
                            )
                            .ToListAsync();

                result.SubItems = new List<TransferPromotionRequestItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        item.TransferPromotionItem = item.TransferPromotionItem ?? new TransferPromotionItem();

                        var promoItem = new TransferPromotionRequestItemDTO()
                        {
                            Id = item.ID,
                            TransferPromotionRequestID = item.TransferPromotionRequest?.ID,
                            TransferPromotionItemID = item.TransferPromotionItem.ID,
                            IsMaster = !item.TransferPromotionItem.MainTransferPromotionItemID.HasValue,
                            NameTH = item.TransferPromotionItem.MasterPromotionItem?.NameTH,
                            TransferPromotionQuantity = model.TransferPromotionItem.Quantity,
                            PricePerUnit = item.TransferPromotionItem.PricePerUnit,
                            TotalPrice = item.TransferPromotionItem.TotalPrice,
                            MaterialGroup = item?.TransferPromotionItem?.MasterPromotionItem?.MaterialGroupKey,
                            MaterialCode = item?.TransferPromotionItem?.MasterPromotionItem?.MaterialCode,
                            MaterialName = item?.TransferPromotionItem?.MasterPromotionItem?.MaterialName,
                            UnitNo = item?.TransferPromotionRequest?.TransferPromotion?.Booking?.Unit?.UnitNo,
                            Project = item?.TransferPromotionRequest?.TransferPromotion?.Booking?.Project?.ProjectNo,
                            NoRequest = (item.TransferPromotionRequest?.RequestNo ?? "").Contains("RT") ? true : false,
                            RequestQuantity = item.RequestQuantity,
                            RemainingRequestQuantity = item.RemainingRequestQuantity,
                            Quantity = model.Quantity,
                            ReceiveDate = item.EstimateRequestDate,
                            DenyRemark = item.DenyRemark,
                            PRNo = item.PRNo,
                            RequestNo = item.TransferPromotionRequest?.RequestNo,
                            UnitTH = item.TransferPromotionItem?.MasterPromotionItem?.UnitTH,
                            Updated = item?.Updated,
                            UpdatedBy = item?.UpdatedBy == null ? item.CreatedBy?.DisplayName : item.UpdatedBy?.DisplayName
                        };

                        if (string.IsNullOrEmpty(item.PRNo))
                        {
                            var prResult = await DB.PRRequestJobItemResults
                                    .Include(o => o.PRRequestJobItem)
                                    .Where(o => o.PRRequestJobItem.TransferPromotionRequestItemID == item.ID).FirstOrDefaultAsync();

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
                                    .Where(o => o.PRCancelJobItem.TransferPromotionRequestItemID == model.ID).FirstOrDefaultAsync();

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
        public async static Task<TransferPromotionRequestItemDTO> CreateFromModelDeliveryAsync(TransferPromotionRequestItem model, DatabaseContext DB)
        {
            if (model != null)
            {
                model.TransferPromotionItem = model.TransferPromotionItem ?? new TransferPromotionItem();

                var itemResults = await DB.TransferPromotionRequestItems
                                 .Include(o => o.TransferPromotionRequest)
                                 .Include(o => o.TransferPromotionRequest.PromotionRequestPRStatus)
                                 .Include(o => o.TransferPromotionRequest.TransferPromotion)
                                 .Include(o => o.TransferPromotionRequest.TransferPromotion.Booking)
                                 .Include(o => o.TransferPromotionItem)
                                 .Include(o => o.TransferPromotionItem.MasterPromotionItem)
                                 .Where(o => o.TransferPromotionItem.ID == model.TransferPromotionItemID
                                       //&& o.TransferPromotionRequestID == model.TransferPromotionRequestID
                                       //&& o.TransferPromotionRequest.PromotionRequestPRStatus.Key == "1"
                                       ).ToListAsync();

                var itemDelivery = await DB.TransferPromotionDeliveryItems
                                 .Include(o => o.TransferPromotionDelivery)
                                 .Include(o => o.TransferPromotionRequestItem)
                                   .ThenInclude(o => o.TransferPromotionItem)
                                 .Where(o => o.TransferPromotionRequestItem.TransferPromotionItem.ID == model.TransferPromotionItemID
                                       ).ToListAsync();



                var result = new TransferPromotionRequestItemDTO();
                result.Id = model.ID;
                result.TransferPromotionRequestID = model.TransferPromotionRequest.ID;
                result.TransferPromotionItemID = model.TransferPromotionItem.ID;
                result.IsMaster = !model.TransferPromotionItem.MainTransferPromotionItemID.HasValue;
                result.NameTH = model?.TransferPromotionItem?.MasterPromotionItem?.NameTH;
                result.TransferPromotionQuantity = itemResults.Where(o => o.TransferPromotionItemID == model.TransferPromotionItemID).Sum(o => o.Quantity);
                result.PricePerUnit = model.TransferPromotionItem.PricePerUnit;
                result.TotalPrice = model.TransferPromotionItem.TotalPrice;
                result.MaterialGroup = model?.TransferPromotionItem?.MasterPromotionItem?.MaterialGroupKey;
                result.MaterialCode = model?.TransferPromotionItem?.MasterPromotionItem?.MaterialCode;
                result.MaterialName = model?.TransferPromotionItem?.MasterPromotionItem?.MaterialName;
                result.UnitNo = model?.TransferPromotionRequest?.TransferPromotion?.Booking?.Unit?.UnitNo;
                result.Project = model?.TransferPromotionRequest?.TransferPromotion?.Booking?.Project?.ProjectNo;
                result.NoRequest = (model.TransferPromotionRequest.RequestNo ?? "").Contains("RT") ? true : false;
                result.IsSelected = true;

                //result.RequestedQuantity = request.Sum(o => o.Quantity);
                //result.RemainingRequestQuantity = itemModel.Quantity - request.Sum(o => o.Quantity);
                //result.RequestQuantity = 0;

                //if (itemResults.Any())
                //{
                var booking = DB.Bookings
                    .Include(o => o.Project)
                    .Include(o => o.Unit)
                    .Where(o => itemDelivery.Select(i => i.TransferPromotionDelivery.TransferPromotionRequest.TransferPromotion.Booking.ID == o.ID).FirstOrDefault());

                result.RequestQuantity = itemDelivery.Sum(o => o.Quantity); //model.RequestQuantity;
                result.RemainingRequestQuantity = result.TransferPromotionQuantity - itemDelivery.Sum(o => o.Quantity); //model.RemainingRequestQuantity;
                                                                                                                        //}
                if (itemDelivery.Where(o => !String.IsNullOrEmpty(o.TransferPromotionDelivery?.DeliveryNo)).ToList().Any())
                {
                    result.DeliveryNo = String.Join(",", itemDelivery.Select(o => o.TransferPromotionDelivery?.DeliveryNo ?? "").ToList());
                }
                else
                {
                    result.DeliveryNo = "";
                }
                result.Quantity = 0;
                result.ReceiveDate = model.EstimateRequestDate;
                result.DenyRemark = model.DenyRemark;
                result.PRNo = model.PRNo;
                result.UnitTH = model?.TransferPromotionItem?.MasterPromotionItem?.UnitTH;

                result.Updated = model?.Updated;
                result.UpdatedBy = model?.UpdatedBy == null ? model.CreatedBy?.DisplayName : model.UpdatedBy?.DisplayName;

                var subItems = DB.TransferPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.MainTransferPromotionItemID == model.TransferPromotionItem.ID).ToList();
                result.SubItems = new List<TransferPromotionItemDTO>();
                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new TransferPromotionItemDTO()
                        {
                            FromMasterTansferPromotionItemID = item.MasterTransferPromotionItemID,
                            FromQuotationTansferPromotionItemID = model.TransferPromotionItem.QuotationTransferPromotionItemID,
                            NameTH = item.MasterPromotionItem.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.MasterPromotionItem.UnitTH,
                            ItemType = PromotionItemType.Item,
                            Id = model.TransferPromotionItem.ID,
                            Updated = model.TransferPromotionItem.Updated,
                            UpdatedBy = model.TransferPromotionItem.UpdatedBy?.DisplayName,
                            IsSelected = true,
                            TransferPromotionID = model.TransferPromotionItem.TransferPromotionID
                        };


                        var requestSub = DB.TransferPromotionRequestItems
                                .Include(o => o.TransferPromotionRequest)
                                .ThenInclude(o => o.PromotionRequestPRStatus)
                                .Where(o => o.TransferPromotionItemID == item.ID
                                        && o.TransferPromotionRequest.PromotionRequestPRStatus.Key != "4").ToList();

                        //promoItem.RequestedQuantity = requestSub.Sum(o => o.Quantity);
                        //promoItem.RemainingRequestQuantity = model.SalePromotionItem.Quantity - requestSub.Sum(o => o.Quantity);
                        //promoItem.RequestQuantity = 0;
                        //promoItem.ReceiveDate = DateTime.Now.Date;
                        //promoItem.DenyRemark = String.Join(",", requestSub.Select(o => o.DenyRemark ?? "").ToList());
                        //promoItem.RequestNo = String.Join(",", requestSub.Select(o => o.SalePromotionRequest.RequestNo ?? "").ToList());
                        //promoItem.PRNo = String.Join(",", requestSub.Select(o => o.PRNo ?? "").ToList());

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

        public async static Task<TransferPromotionRequestItemDTO> CreateFromMasterModelAsync(MasterTransferPromotionItem model, Guid transferPromotionItemID, DatabaseContext DB)
        {
            if (model != null)
            {
                var itemResults = await DB.TransferPromotionRequestItems
                                  .Include(o => o.TransferPromotionRequest)
                                      .ThenInclude(o => o.PromotionRequestPRStatus)
                                  .Include(o => o.TransferPromotionItem)
                                  .Where(o => o.TransferPromotionItem.ID == transferPromotionItemID
                                        //&& o.TransferPromotionRequest.PromotionRequestPRStatus.Key == "1"
                                        ).ToListAsync();

                var result = new TransferPromotionRequestItemDTO
                {
                    Id = model.ID,
                    TransferPromotionRequestID = null,
                    TransferPromotionItemID = null,
                    IsMaster = !model.MainPromotionItemID.HasValue,
                    NameTH = model.NameTH,
                    TransferPromotionQuantity = model.Quantity,
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

        public static TransferPromotionRequestItemDTO CreateFromQuery(dbqTransferPromotionRequestItemForDelivery model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new TransferPromotionRequestItemDTO();
                result.Id = model.ID;
                result.TransferPromotionRequestID = model.TransferPromotionRequestID;
                result.TransferPromotionItemID = model.TransferPromotionItemID;
                result.IsMaster = !model.MainPromotionItemID.HasValue;
                result.NameTH = model.NameTH;
                result.TransferPromotionQuantity = model.TransferPromotionQuantity ?? 0;
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
                result.Updated = model?.Updated;
                result.UpdatedBy = model.UpdatedByDisplayName;
                result.UnitTH = model.UnitTH;
                result.ListTrId = model.ListTrId;
                result.RequestNo = model.RequestNo;

                if (model.TransferPromotionItemID != null)
                {
                    var subItems = DB.TransferPromotionRequestItems
                            .Include(o => o.TransferPromotionRequest)
                            .Include(o => o.TransferPromotionItem)
                            .Include(o => o.TransferPromotionItem.MasterPromotionItem)
                            .Include(o => o.CreatedBy)
                            .Include(o => o.UpdatedBy)
                            .Where(o =>
                                o.TransferPromotionRequestID == model.TransferPromotionRequestID
                                && o.TransferPromotionItem.MainTransferPromotionItemID == model.TransferPromotionItemID
                            )
                            .ToList();

                    result.SubItems = new List<TransferPromotionRequestItemDTO>();
                    if (subItems.Count > 0)
                    {
                        foreach (var item in subItems)
                        {
                            var promoItem = new TransferPromotionRequestItemDTO()
                            {
                                Id = item.ID,
                                TransferPromotionRequestID = item.TransferPromotionRequest?.ID,
                                TransferPromotionItemID = item.TransferPromotionItem.ID,
                                IsMaster = !item.TransferPromotionItem.MainTransferPromotionItemID.HasValue,
                                NameTH = item.TransferPromotionItem.MasterPromotionItem?.NameTH,
                                PricePerUnit = item.TransferPromotionItem.PricePerUnit,
                                TotalPrice = item.TransferPromotionItem.TotalPrice,
                                MaterialGroup = item?.TransferPromotionItem?.MasterPromotionItem?.MaterialGroupKey,
                                MaterialCode = item?.TransferPromotionItem?.MasterPromotionItem?.MaterialCode,
                                MaterialName = item?.TransferPromotionItem?.MasterPromotionItem?.MaterialName,
                                UnitNo = item?.TransferPromotionRequest?.TransferPromotion?.Booking?.Unit?.UnitNo,
                                Project = item?.TransferPromotionRequest?.TransferPromotion?.Booking?.Project?.ProjectNo,
                                NoRequest = (item.TransferPromotionRequest?.RequestNo ?? "").Contains("RT") ? true : false,

                                TransferPromotionQuantity = result.TransferPromotionQuantity, //item.TransferPromotionItem.Quantity,
                                RequestQuantity = result.RequestQuantity, //item.RequestQuantity,
                                RemainingRequestQuantity = result.RemainingRequestQuantity, //item.RemainingRequestQuantity,
                                Quantity = result.Quantity, //item.Quantity,

                                ReceiveDate = item.EstimateRequestDate,
                                DenyRemark = item.DenyRemark,
                                PRNo = item.PRNo,
                                RequestNo = item.TransferPromotionRequest?.RequestNo,
                                UnitTH = item.TransferPromotionItem?.MasterPromotionItem?.UnitTH,
                                Updated = item?.Updated,
                                UpdatedBy = item?.UpdatedBy == null ? item.CreatedBy?.DisplayName : item.UpdatedBy?.DisplayName
                            };

                            if (string.IsNullOrEmpty(item.PRNo))
                            {
                                var prResult = DB.PRRequestJobItemResults
                                        .Include(o => o.PRRequestJobItem)
                                        .Where(o => o.PRRequestJobItem.TransferPromotionRequestItemID == item.ID).FirstOrDefault();

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
