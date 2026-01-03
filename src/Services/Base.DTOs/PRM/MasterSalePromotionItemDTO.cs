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
    /// รายการโปรโมชั่นขาย
    /// Model = MasterSalePromotionItem
    /// </summary>
    public class MasterSalePromotionItemDTO : BaseDTO
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

        public static MasterSalePromotionItemDTO CreateFromModel(MasterSalePromotionItem model)
        {
            if (model != null)
            {
                var result = new MasterSalePromotionItemDTO()
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
        public static MasterSalePromotionItemDTO CreateFromQueryResult(MasterSalePromotionItemQueryResult model, Guid statusDeleteFromSap)
        {
            if (model != null)
            {
                var result = new MasterSalePromotionItemDTO()
                {
                    Id = model.MasterSalePromotionItem.ID,
                    NameTH = model.MasterSalePromotionItem.NameTH,
                    NameEN = model.MasterSalePromotionItem.NameEN,
                    Quantity = model.MasterSalePromotionItem.Quantity,
                    UnitTH = model.MasterSalePromotionItem.UnitTH,
                    UnitEN = model.MasterSalePromotionItem.UnitEN,
                    PricePerUnit = model.MasterSalePromotionItem.PricePerUnit,
                    TotalPrice = model.MasterSalePromotionItem.TotalPrice,
                    ReceiveDays = model.MasterSalePromotionItem.ReceiveDays,
                    WhenPromotionReceive = MST.MasterCenterDropdownDTO.CreateFromModel(model.WhenPromotionReceive),
                    IsPurchasing = model.MasterSalePromotionItem.IsPurchasing,
                    IsShowInContract = model.MasterSalePromotionItem.IsShowInContract,
                    PromotionItemStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionItemStatus),
                    ExpireDate = model.MasterSalePromotionItem.ExpireDate,
                    MainPromotionItemID = model.MasterSalePromotionItem.MainPromotionItemID,
                    PromotionMaterial = PromotionMaterialDTO.CreateFromModel(model.PromotionMaterialItem),
                    Updated = model.MasterSalePromotionItem.Updated,
                    UpdatedBy = model.MasterSalePromotionItem.UpdatedBy?.DisplayName,
                    IsUsed = model.MasterSalePromotionItem.IsUsed
                };

                if (model.MasterSalePromotionItem?.PromotionMaterialItem != null)
                {
                    result.IsDeletedFromSAP = model.MasterSalePromotionItem?.PromotionMaterialItem?.MaterialItemStatusMasterCenterID != statusDeleteFromSap ? false : true;
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
        public static void SortBy(MasterSalePromotionItemSortByParam sortByParam, ref IQueryable<MasterSalePromotionItemQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterSalePromotionItemSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionItem.NameTH);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionItem.NameTH);
                        break;
                    case MasterSalePromotionItemSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionItem.NameEN);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionItem.NameEN);
                        break;
                    case MasterSalePromotionItemSortBy.Quantity:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionItem.Quantity);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionItem.Quantity);
                        break;
                    case MasterSalePromotionItemSortBy.UnitTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionItem.UnitTH);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionItem.UnitTH);
                        break;
                    case MasterSalePromotionItemSortBy.UnitEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionItem.UnitEN);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionItem.UnitEN);
                        break;
                    case MasterSalePromotionItemSortBy.PricePerUnit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionItem.PricePerUnit);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionItem.PricePerUnit);
                        break;
                    case MasterSalePromotionItemSortBy.TotalPrice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionItem.TotalPrice);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionItem.TotalPrice);
                        break;
                    case MasterSalePromotionItemSortBy.ReceiveDays:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionItem.ReceiveDays);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionItem.ReceiveDays);
                        break;
                    case MasterSalePromotionItemSortBy.WhenPromotionReceive:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.WhenPromotionReceive.Name);
                        else query = query.OrderByDescending(o => o.WhenPromotionReceive.Name);
                        break;
                    case MasterSalePromotionItemSortBy.IsPurchasing:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionItem.IsPurchasing);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionItem.IsPurchasing);
                        break;
                    case MasterSalePromotionItemSortBy.IsShowInContract:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionItem.IsShowInContract);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionItem.IsShowInContract);
                        break;
                    case MasterSalePromotionItemSortBy.PromotionItemStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionItemStatus.Name);
                        else query = query.OrderByDescending(o => o.PromotionItemStatus.Name);
                        break;
                    case MasterSalePromotionItemSortBy.ExpireDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionItem.ExpireDate);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionItem.ExpireDate);
                        break;
                    case MasterSalePromotionItemSortBy.PromotionMaterial_AgreementNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.AgreementNo);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.AgreementNo);
                        break;
                    case MasterSalePromotionItemSortBy.PromotionMaterial_ItemNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.ItemNo);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.ItemNo);
                        break;
                    case MasterSalePromotionItemSortBy.PromotionMaterial_NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.NameTH);
                        else query = query.OrderByDescending(o => o.PromotionMaterialItem.NameTH);
                        break;
                    case MasterSalePromotionItemSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionItem.Updated);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionItem.Updated);
                        break;
                    case MasterSalePromotionItemSortBy.UpdatedBy:
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
        public static void SortByDTO(MasterSalePromotionItemSortByParam sortByParam, ref List<MasterSalePromotionItemDTO> listDTOs)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterSalePromotionItemSortBy.NameTH:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.NameTH).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.NameTH).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.NameEN:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.NameEN).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.NameEN).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.Quantity:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.Quantity).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.Quantity).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.UnitTH:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.UnitTH).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.UnitTH).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.UnitEN:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.UnitEN).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.UnitEN).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.PricePerUnit:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PricePerUnit).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PricePerUnit).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.TotalPrice:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.TotalPrice).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.TotalPrice).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.ReceiveDays:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.ReceiveDays).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.ReceiveDays).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.WhenPromotionReceive:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.WhenPromotionReceive.Name).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.WhenPromotionReceive.Name).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.IsPurchasing:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.IsPurchasing).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.IsPurchasing).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.IsShowInContract:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.IsShowInContract).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.IsShowInContract).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.PromotionItemStatus:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionItemStatus.Name).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionItemStatus.Name).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.ExpireDate:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.ExpireDate).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.ExpireDate).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.PromotionMaterial_AgreementNo:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionMaterial.AgreementNo).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionMaterial.AgreementNo).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.PromotionMaterial_ItemNo:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionMaterial.ItemNo).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionMaterial.ItemNo).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.PromotionMaterial_NameTH:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.PromotionMaterial.NameTH).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.PromotionMaterial.NameTH).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.Updated:
                        if (sortByParam.Ascending) listDTOs = listDTOs.OrderBy(o => o.Updated).ToList();
                        else listDTOs = listDTOs.OrderByDescending(o => o.Updated).ToList();
                        break;
                    case MasterSalePromotionItemSortBy.UpdatedBy:
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
                string desc = this.GetType().GetProperty(nameof(MasterSalePromotionItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.NameTH.CheckAllLang(true, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0018").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterSalePromotionItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (string.IsNullOrEmpty(this.NameEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterSalePromotionItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.NameEN.CheckAllLang(true, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0018").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterSalePromotionItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (string.IsNullOrEmpty(this.UnitTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterSalePromotionItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.UnitTH.CheckAllLang(false, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterSalePromotionItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (string.IsNullOrEmpty(this.UnitEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterSalePromotionItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.UnitEN.CheckAllLang(false, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterSalePromotionItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (this.WhenPromotionReceive == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterSalePromotionItemDTO.WhenPromotionReceive)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterSalePromotionItem model)
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
    }
    public class MasterSalePromotionItemQueryResult
    {
        public MasterSalePromotionItem MasterSalePromotionItem { get; set; }
        public PromotionMaterialItem PromotionMaterialItem { get; set; }
        public MasterCenter PromotionItemStatus { get; set; }
        public MasterCenter WhenPromotionReceive { get; set; }
        public User UpdatedBy { get; set; }
        public DateTime? Created { get; set; }

    }
}
