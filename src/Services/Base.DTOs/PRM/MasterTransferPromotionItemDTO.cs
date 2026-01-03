using Base.DTOs.SAL;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRM;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary> 
    /// รายการโปรโอน
    /// Model = MasterTransferPromotionItem
    /// </summary>
    public class MasterTransferPromotionItemDTO : BaseDTO
    {
        /// <summary>
        /// ชื่อผลิตภัณฑ์ (TH)
        /// </summary>
        [Description("ชื่อผลิตภัณฑ์ (TH)")]
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อผลิตภัณฑ์ (EN)
        /// </summary>
        [Description("ชื่อผลิตภัณฑ์ (EN)")]
        public string NameEN { get; set; }
        /// <summary>
        /// จำนวน
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// หน่วย (TH)
        /// </summary>
        [Description("หน่วย (TH)")]
        public string UnitTH { get; set; }
        /// <summary>
        /// หน่วย (EN)
        /// </summary>
        [Description("หน่วย (EN)")]
        public string UnitEN { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย
        /// </summary>
        public decimal PricePerUnit { get; set; }
        /// <summary>
        /// ราคารวม
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// วันที่ได้รับ
        /// </summary>
        public int? ReceiveDays { get; set; }
        /// <summary>
        /// ลูกค้าได้รับเมื่อ?
        /// Master/api/MasterCenters?masterCenterGroupKey=WhenPromotionReceive
        /// </summary>
        [Description("ลูกค้าได้รับเมื่อ?")]
        public MST.MasterCenterDropdownDTO WhenPromotionReceive { get; set; }
        /// <summary>
        /// การจัดซื้อ?
        /// </summary>
        public bool IsPurchasing { get; set; }
        /// <summary>
        /// แสดงในสัญญา
        /// </summary>
        public bool IsShowInContract { get; set; }
        /// <summary>
        /// สถานะ
        ///  Master/api/MasterCenters?masterCenterGroupKey=PromotionItemStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO PromotionItemStatus { get; set; }
        /// <summary>
        /// วันที่หมดอายุ
        /// </summary>
        public DateTime? ExpireDate { get; set; }
        /// <summary>
        /// ID ของ Promotion หลัก (กรณี Item นี้เป็น Promotion ย่อย)
        /// </summary>
        public Guid? MainPromotionItemID { get; set; }
        /// <summary>
        /// PromotionMaterial
        /// </summary>
        public PromotionMaterialDTO PromotionMaterial { get; set; }
        /// <summary>
        /// รายการย่อย
        /// </summary>
        public List<MasterTransferPromotionItemDTO> SubItems { get; set; }
        /// <summary>
        /// MasterTotalprice
        /// </summary>
        public decimal MasterTotalprice { get; set; }
        /// <summary>
        /// ชนิดของรายการโปรโมชั่น
        /// </summary>
        public PromotionItemType Itemtype { get; set; }
        /// <summary>
        /// สถานะการนำไปใช้งาน
        /// </summary>
        public bool IsUsed { get; set; }
        /// <summary>
        /// สถานะถูกลบจาก SAP
        /// </summary>
        public bool IsDeletedFromSAP { get; set; }
        /// <summary>
        /// วันที่สร้าง Master
        /// </summary>
        public DateTime? Created { get; set; }

        public static MasterTransferPromotionItemDTO CreateFromModel(MasterTransferPromotionItem model)
        {
            if (model != null)
            {
                var result = new MasterTransferPromotionItemDTO()
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    Quantity = model.Quantity,
                    UnitTH = model.UnitTH,
                    UnitEN = model.UnitEN,
                    PricePerUnit = model.PricePerUnit,
                    TotalPrice = model.TotalPrice,
                    ReceiveDays = model.ReceiveDays,
                    WhenPromotionReceive = MST.MasterCenterDropdownDTO.CreateFromModel(model.WhenPromotionReceive),
                    IsPurchasing = model.IsPurchasing,
                    IsShowInContract = model.IsShowInContract,
                    PromotionItemStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionItemStatus),
                    ExpireDate = model.ExpireDate,
                    MainPromotionItemID = model.MainPromotionItemID,
                    PromotionMaterial = PromotionMaterialDTO.CreateFromModel(model.PromotionMaterialItem),
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
        public static MasterTransferPromotionItemDTO CreateFromQueryResult(MasterTransferPromotionItemQueryResult model, Guid statusDeleteFromSap)
        {
            if (model != null)
            {
                var result = new MasterTransferPromotionItemDTO()
                {
                    Id = model.MasterTransferPromotionItem.ID,
                    NameTH = model.MasterTransferPromotionItem.NameTH,
                    NameEN = model.MasterTransferPromotionItem.NameEN,
                    Quantity = model.MasterTransferPromotionItem.Quantity,
                    UnitTH = model.MasterTransferPromotionItem.UnitTH,
                    UnitEN = model.MasterTransferPromotionItem.UnitEN,
                    PricePerUnit = model.MasterTransferPromotionItem.PricePerUnit,
                    TotalPrice = model.MasterTransferPromotionItem.TotalPrice,
                    ReceiveDays = model.MasterTransferPromotionItem.ReceiveDays,
                    WhenPromotionReceive = MST.MasterCenterDropdownDTO.CreateFromModel(model.WhenPromotionReceive),
                    IsPurchasing = model.MasterTransferPromotionItem.IsPurchasing,
                    IsShowInContract = model.MasterTransferPromotionItem.IsShowInContract,
                    PromotionItemStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionItemStatus),
                    ExpireDate = model.MasterTransferPromotionItem.ExpireDate,
                    MainPromotionItemID = model.MasterTransferPromotionItem.MainPromotionItemID,
                    PromotionMaterial = PromotionMaterialDTO.CreateFromModel(model.PromotionMaterialItem),
                    Updated = model.MasterTransferPromotionItem.Updated,
                    UpdatedBy = model.MasterTransferPromotionItem.UpdatedBy?.DisplayName,
                    IsUsed = model.MasterTransferPromotionItem.IsUsed
                };
                if (model.MasterTransferPromotionItem?.PromotionMaterialItem != null)
                {
                    result.IsDeletedFromSAP = model.MasterTransferPromotionItem?.PromotionMaterialItem?.MaterialItemStatusMasterCenterID != statusDeleteFromSap ? false : true;
                }
                else
                {
                    result.IsDeletedFromSAP = false;
                }
                return result;
            }
            else
            {
                return null;
            }
        }
        public static void SortBy(MasterTransferPromotionItemSortByParam sortByParam, ref IQueryable<MasterTransferPromotionItemQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterTransferPromotionItemSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionItem.NameTH);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionItem.NameTH);
                        break;
                    case MasterTransferPromotionItemSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionItem.NameEN);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionItem.NameEN);
                        break;
                    case MasterTransferPromotionItemSortBy.Quantity:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionItem.Quantity);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionItem.Quantity);
                        break;
                    case MasterTransferPromotionItemSortBy.UnitTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionItem.UnitTH);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionItem.UnitTH);
                        break;
                    case MasterTransferPromotionItemSortBy.UnitEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionItem.UnitEN);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionItem.UnitEN);
                        break;
                    case MasterTransferPromotionItemSortBy.PricePerUnit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionItem.PricePerUnit);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionItem.PricePerUnit);
                        break;
                    case MasterTransferPromotionItemSortBy.TotalPrice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionItem.TotalPrice);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionItem.TotalPrice);
                        break;
                    case MasterTransferPromotionItemSortBy.ReceiveDays:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionItem.ReceiveDays);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionItem.ReceiveDays);
                        break;
                    case MasterTransferPromotionItemSortBy.WhenPromotionReceive:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.WhenPromotionReceive.Name);
                        else query = query.OrderByDescending(o => o.WhenPromotionReceive.Name);
                        break;
                    case MasterTransferPromotionItemSortBy.IsPurchasing:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionItem.IsPurchasing);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionItem.IsPurchasing);
                        break;
                    case MasterTransferPromotionItemSortBy.IsShowInContract:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionItem.IsShowInContract);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionItem.IsShowInContract);
                        break;
                    case MasterTransferPromotionItemSortBy.PromotionItemStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionItemStatus.Name);
                        else query = query.OrderByDescending(o => o.PromotionItemStatus.Name);
                        break;
                    case MasterTransferPromotionItemSortBy.ExpireDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionItem.ExpireDate);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionItem.ExpireDate);
                        break;
                    case MasterTransferPromotionItemSortBy.PromotionMaterial_AgreementNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.AgreementNo);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.AgreementNo);
                        break;
                    case MasterTransferPromotionItemSortBy.PromotionMaterial_ItemNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.ItemNo);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.ItemNo);
                        break;
                    case MasterTransferPromotionItemSortBy.PromotionMaterial_NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.NameTH);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.NameTH);
                        break;
                    case MasterTransferPromotionItemSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionItem.Updated);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionItem.Updated);
                        break;
                    case MasterTransferPromotionItemSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderByDescending(o => o.Created.Value.Date).ThenByDescending(c => c.Created.Value.TimeOfDay);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.Created.Value.Date).ThenByDescending(c => c.Created.Value.TimeOfDay);
            }
        }

        public static void SortByDTO(MasterTransferPromotionItemSortByParam sortByParam, ref List<MasterTransferPromotionItemDTO> listDTOs)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterTransferPromotionItemSortBy.NameTH:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.NameTH).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.NameTH).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.NameEN:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.NameEN).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.NameEN).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.Quantity:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.Quantity).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.Quantity).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.UnitTH:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.UnitTH).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.UnitTH).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.UnitEN:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.UnitEN).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.UnitEN).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.PricePerUnit:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PricePerUnit).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PricePerUnit).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.TotalPrice:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.TotalPrice).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.TotalPrice).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.ReceiveDays:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.ReceiveDays).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.ReceiveDays).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.WhenPromotionReceive:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.WhenPromotionReceive.Name).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.WhenPromotionReceive.Name).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.IsPurchasing:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.IsPurchasing).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.IsPurchasing).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.IsShowInContract:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.IsShowInContract).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.IsShowInContract).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.PromotionItemStatus:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionItemStatus.Name).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionItemStatus.Name).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.ExpireDate:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.ExpireDate).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.ExpireDate).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.PromotionMaterial_AgreementNo:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionMaterial.AgreementNo).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionMaterial.AgreementNo).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.PromotionMaterial_ItemNo:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionMaterial.ItemNo).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionMaterial.ItemNo).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.PromotionMaterial_NameTH:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionMaterial.NameTH).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionMaterial.NameTH).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.Updated:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.Updated).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.Updated).ToList();
                        break;
                    case MasterTransferPromotionItemSortBy.UpdatedBy:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.UpdatedBy).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.UpdatedBy).ToList();
                        break;
                    default:
                        //listDTOs = listDTOs.OrderBy(o => o.PromotionMaterial.AgreementNo).ToList();
                        listDTOs = listDTOs.OrderByDescending(o => o.Created).ToList();
                        break;
                }
            }
            else
            {
                //listDTOs = listDTOs.OrderBy(o => o.PromotionMaterial.AgreementNo).ToList();
                listDTOs = listDTOs.OrderByDescending(o => o.Created).ToList();
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.NameTH.CheckAllLang(true, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0018").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (string.IsNullOrEmpty(this.NameEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.NameEN.CheckAllLang(true, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0018").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (string.IsNullOrEmpty(this.UnitTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.UnitTH.CheckAllLang(false, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (string.IsNullOrEmpty(this.UnitEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.UnitEN.CheckAllLang(false, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (this.WhenPromotionReceive == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionItemDTO.WhenPromotionReceive)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterTransferPromotionItem model)
        {
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.Quantity = this.Quantity;
            model.UnitTH = this.UnitTH;
            model.UnitEN = this.UnitEN;
            model.ReceiveDays = this.ReceiveDays;
            model.WhenPromotionReceiveMasterCenterID = this.WhenPromotionReceive?.Id;

            //IsPurchasing
            if (this.PromotionMaterial.AgreementNo.ToLower().Contains("welcomehome"))
            {
                model.IsPurchasing = false;
            }
            else if (this.NameTH.Trim().Replace(" ", "").ToLower().Contains("welcomehome") 
                    || this.NameTH.Trim().Replace(" ", "").ToLower().Contains("secretdeal")
                    || this.NameTH.Trim().Replace(" ", "").ToLower().Contains("commonfee"))
            {
                model.IsPurchasing = false;
            }
            else
            {
                model.IsPurchasing = this.IsPurchasing;
            }

            model.IsShowInContract = this.IsShowInContract;
            model.PromotionItemStatusMasterCenterID = this.PromotionItemStatus?.Id;
        }
        public static MasterTransferPromotionItemDTO CreateTransferPromotionFromModel(MasterTransferPromotionItem model, DatabaseContext db)
        {
            var result = new MasterTransferPromotionItemDTO();
            if (model != null)
            {
                result.Id = model.ID;
                result.NameTH = model.NameTH;
                result.NameEN = model.NameEN;
                result.Quantity = model.Quantity;
                result.UnitTH = model.UnitTH;
                result.UnitEN = model.UnitEN;
                result.PricePerUnit = model.PricePerUnit;
                result.TotalPrice = model.TotalPrice;
                result.MasterTotalprice= model.TotalPrice;
                result.ReceiveDays = model.ReceiveDays;
                result.Itemtype = PromotionItemType.Item;
                result.WhenPromotionReceive = MST.MasterCenterDropdownDTO.CreateFromModel(model.WhenPromotionReceive);
                result.IsPurchasing = model.IsPurchasing;
                result.IsShowInContract = model.IsShowInContract;
                result.PromotionItemStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionItemStatus);
                result.ExpireDate = model.ExpireDate;
                result.MainPromotionItemID = model.MainPromotionItemID;
                result.PromotionMaterial = PromotionMaterialDTO.CreateFromModel(model.PromotionMaterialItem);
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy?.DisplayName;

                var subItems = db.MasterTransferPromotionItems.Where(o => o.MainPromotionItemID == model.ID && o.ExpireDate >= DateTime.Now).ToList();
                result.SubItems = new List<MasterTransferPromotionItemDTO>();

                if (subItems.Count > 0)
                {
                    foreach (var item in subItems)
                    {
                        var promoItem = new MasterTransferPromotionItemDTO
                        {
                            Id = item.ID,
                            NameTH = item.NameTH,
                            Quantity = item.Quantity,
                            PricePerUnit = item.PricePerUnit,
                            TotalPrice = item.TotalPrice,
                            UnitTH = item.UnitTH,
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
    }
    public class MasterTransferPromotionItemQueryResult
    {
        public MasterTransferPromotionItem MasterTransferPromotionItem { get; set; }
        public PromotionMaterialItem PromotionMaterialItem { get; set; }
        public MasterCenter PromotionItemStatus { get; set; }
        public MasterCenter WhenPromotionReceive { get; set; }
        public User UpdatedBy { get; set; }
        public DateTime? Created { get; set; }
    }

}
