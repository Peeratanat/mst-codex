using Base.DTOs.SAL;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRM;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// รายการโปรโอนที่ไม่ต้องจัดซื้อ
    /// </summary>
    public class MasterTransferPromotionFreeItemDTO : BaseDTO
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
        /// วันที่ได้รับ
        /// </summary>
        [Description("วันที่ได้รับ")]
        public int? ReceiveDays { get; set; }
        /// <summary>
        /// ลูกค้าได้รับเมื่อ?
        /// Master/api/MasterCenters?masterCenterGroupKey=WhenPromotionReceive
        /// </summary>
        [Description("ลูกค้าได้รับเมื่อ?")]
        public MST.MasterCenterDropdownDTO WhenPromotionReceive { get; set; }
        /// <summary>
        /// แสดงในสัญญา
        /// </summary>
        public bool IsShowInContract { get; set; }

        /// <summary>
        /// ชนิดของรายการโปรโมชั่น
        /// </summary>
        public PromotionItemType Itemtype { get; set; }


        public static MasterTransferPromotionFreeItemDTO CreateFromQueryResult(MasterTransferPromotionFreeItemQueryResult model)
        {
            if (model != null)
            {
                var result = new MasterTransferPromotionFreeItemDTO()
                {
                    Id = model.MasterTransferPromotionFreeItem.ID,
                    NameTH = model.MasterTransferPromotionFreeItem.NameTH,
                    NameEN = model.MasterTransferPromotionFreeItem.NameEN,
                    Quantity = model.MasterTransferPromotionFreeItem.Quantity,
                    UnitTH = model.MasterTransferPromotionFreeItem.UnitTH,
                    UnitEN = model.MasterTransferPromotionFreeItem.UnitEN,
                    Itemtype = PromotionItemType.FreeItem,
                    ReceiveDays = model.MasterTransferPromotionFreeItem.ReceiveDays,
                    WhenPromotionReceive = MST.MasterCenterDropdownDTO.CreateFromModel(model.WhenPromotionReceive),
                    IsShowInContract = model.MasterTransferPromotionFreeItem.IsShowInContract,
                    Updated = model.MasterTransferPromotionFreeItem.Updated,
                    UpdatedBy = model.MasterTransferPromotionFreeItem.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static MasterTransferPromotionFreeItemDTO CreateFromModel(MasterTransferPromotionFreeItem model)
        {
            if (model != null)
            {
                var result = new MasterTransferPromotionFreeItemDTO()
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    Quantity = model.Quantity,
                    UnitTH = model.UnitTH,
                    UnitEN = model.UnitEN,
                    ReceiveDays = model.ReceiveDays,
                    WhenPromotionReceive = MST.MasterCenterDropdownDTO.CreateFromModel(model.WhenPromotionReceive),
                    IsShowInContract = model.IsShowInContract,
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

        public static MasterTransferPromotionItemDTO CreateFromDraftModel(MasterTransferPromotionFreeItem model)
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
                    ReceiveDays = model.ReceiveDays,
                    WhenPromotionReceive = MST.MasterCenterDropdownDTO.CreateFromModel(model.WhenPromotionReceive),
                    IsShowInContract = model.IsShowInContract,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    Itemtype = PromotionItemType.FreeItem,
            };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(MasterTransferPromotionFreeItemSortByParam sortByParam, ref IQueryable<MasterTransferPromotionFreeItemQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterTransferPromotionFreeItemSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionFreeItem.NameTH);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionFreeItem.NameTH);
                        break;
                    case MasterTransferPromotionFreeItemSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionFreeItem.NameEN);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionFreeItem.NameEN);
                        break;
                    case MasterTransferPromotionFreeItemSortBy.Quantity:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionFreeItem.Quantity);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionFreeItem.Quantity);
                        break;
                    case MasterTransferPromotionFreeItemSortBy.UnitTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionFreeItem.UnitTH);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionFreeItem.UnitTH);
                        break;
                    case MasterTransferPromotionFreeItemSortBy.UnitEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionFreeItem.UnitEN);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionFreeItem.UnitEN);
                        break;
                    case MasterTransferPromotionFreeItemSortBy.ReceiveDays:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionFreeItem.ReceiveDays);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionFreeItem.ReceiveDays);
                        break;
                    case MasterTransferPromotionFreeItemSortBy.WhenPromotionReceive:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.WhenPromotionReceive.Name);
                        else query = query.OrderByDescending(o => o.WhenPromotionReceive.Name);
                        break;
                    case MasterTransferPromotionFreeItemSortBy.IsShowInContract:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionFreeItem.IsShowInContract);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionFreeItem.IsShowInContract);
                        break;
                    case MasterTransferPromotionFreeItemSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotionFreeItem.Updated);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotionFreeItem.Updated);
                        break;
                    case MasterTransferPromotionFreeItemSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.MasterTransferPromotionFreeItem.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.MasterTransferPromotionFreeItem.NameTH);
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionFreeItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.NameTH.CheckAllLang(true, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0018").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionFreeItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (string.IsNullOrEmpty(this.NameEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionFreeItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.NameEN.CheckLang(false, true, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionFreeItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (string.IsNullOrEmpty(this.UnitTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionFreeItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.UnitTH.CheckAllLang(false, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionFreeItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (string.IsNullOrEmpty(this.UnitEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionFreeItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.UnitEN.CheckLang(false, false, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionFreeItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (this.WhenPromotionReceive == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionFreeItemDTO.WhenPromotionReceive)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.ReceiveDays == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionFreeItemDTO.ReceiveDays)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterTransferPromotionFreeItem model)
        {
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.Quantity = this.Quantity;
            model.UnitTH = this.UnitTH;
            model.UnitEN = this.UnitEN;
            model.ReceiveDays = this.ReceiveDays;
            model.WhenPromotionReceiveMasterCenterID = this.WhenPromotionReceive?.Id;
            model.IsShowInContract = this.IsShowInContract;
        }
    }
    public class MasterTransferPromotionFreeItemQueryResult
    {
        public MasterTransferPromotionFreeItem MasterTransferPromotionFreeItem { get; set; }
        public MasterCenter WhenPromotionReceive { get; set; }
        public User UpdatedBy { get; set; }
    }
}
