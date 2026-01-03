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
    /// รายการโปรโมชั่นที่ไม่ต้องจัดซื้อ
    /// Model = MasterSalePromotionFreeItem
    /// </summary>
    public class MasterSalePromotionFreeItemDTO : BaseDTO
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


        public static MasterSalePromotionFreeItemDTO CreateFromQueryResult(MasterSalePromotionFreeItemQueryResult model)
        {
            if (model != null)
            {
                var result = new MasterSalePromotionFreeItemDTO()
                {
                    Id = model.MasterSalePromotionFreeItem.ID,
                    NameTH = model.MasterSalePromotionFreeItem.NameTH,
                    NameEN = model.MasterSalePromotionFreeItem.NameEN,
                    Quantity = model.MasterSalePromotionFreeItem.Quantity,
                    UnitTH = model.MasterSalePromotionFreeItem.UnitTH,
                    UnitEN = model.MasterSalePromotionFreeItem.UnitEN,
                    ReceiveDays = model.MasterSalePromotionFreeItem.ReceiveDays,
                    WhenPromotionReceive = MST.MasterCenterDropdownDTO.CreateFromModel(model.WhenPromotionReceive),
                    IsShowInContract = model.MasterSalePromotionFreeItem.IsShowInContract,
                    Updated = model.MasterSalePromotionFreeItem.Updated,
                    UpdatedBy = model.MasterSalePromotionFreeItem.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static MasterSalePromotionFreeItemDTO CreateFromModel(MasterSalePromotionFreeItem model)
        {
            if (model != null)
            {
                var result = new MasterSalePromotionFreeItemDTO()
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

        public static void SortBy(MasterSalePromotionFreeItemSortByParam sortByParam, ref IQueryable<MasterSalePromotionFreeItemQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterSalePromotionFreeItemSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionFreeItem.NameTH);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionFreeItem.NameTH);
                        break;
                    case MasterSalePromotionFreeItemSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionFreeItem.NameEN);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionFreeItem.NameEN);
                        break;
                    case MasterSalePromotionFreeItemSortBy.Quantity:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionFreeItem.Quantity);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionFreeItem.Quantity);
                        break;
                    case MasterSalePromotionFreeItemSortBy.UnitTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionFreeItem.UnitTH);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionFreeItem.UnitTH);
                        break;
                    case MasterSalePromotionFreeItemSortBy.UnitEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionFreeItem.UnitEN);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionFreeItem.UnitEN);
                        break;
                    case MasterSalePromotionFreeItemSortBy.ReceiveDays:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionFreeItem.ReceiveDays);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionFreeItem.ReceiveDays);
                        break;
                    case MasterSalePromotionFreeItemSortBy.WhenPromotionReceive:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.WhenPromotionReceive.Name);
                        else query = query.OrderByDescending(o => o.WhenPromotionReceive.Name);
                        break;
                    case MasterSalePromotionFreeItemSortBy.IsShowInContract:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionFreeItem.IsShowInContract);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionFreeItem.IsShowInContract);
                        break;
                    case MasterSalePromotionFreeItemSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionFreeItem.Updated);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionFreeItem.Updated);
                        break;
                    case MasterSalePromotionFreeItemSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.MasterSalePromotionFreeItem.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.MasterSalePromotionFreeItem.NameTH);
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterSalePromotionFreeItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.NameTH.CheckAllLang(true, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0018").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterSalePromotionFreeItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (string.IsNullOrEmpty(this.NameEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterSalePromotionFreeItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.NameEN.CheckLang(false, true, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterSalePromotionFreeItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (string.IsNullOrEmpty(this.UnitTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterSalePromotionFreeItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.UnitTH.CheckAllLang(false, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterSalePromotionFreeItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (string.IsNullOrEmpty(this.UnitEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterSalePromotionFreeItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.UnitEN.CheckLang(false, false, false, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(MasterSalePromotionFreeItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (this.WhenPromotionReceive == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterSalePromotionFreeItemDTO.WhenPromotionReceive)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.ReceiveDays == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterSalePromotionFreeItemDTO.ReceiveDays)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterSalePromotionFreeItem model)
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
    public class MasterSalePromotionFreeItemQueryResult
    {
        public MasterSalePromotionFreeItem MasterSalePromotionFreeItem { get; set; }
        public MasterCenter WhenPromotionReceive { get; set; }
        public User UpdatedBy { get; set; }
    }
}
