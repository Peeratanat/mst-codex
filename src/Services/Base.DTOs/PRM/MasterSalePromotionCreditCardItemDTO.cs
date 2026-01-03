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
    /// ค่าธรรมเนียมรูดบัตร
    /// Model = MasterSalePromotionCreditCardItem
    /// </summary>
    public class MasterSalePromotionCreditCardItemDTO : BaseDTO
    {
        /// <summary>
        /// ธนาคาร
        /// Master/api/Banks/DropdownList
        /// </summary>
        [Description("ธนาคาร")]
        public MST.BankDropdownDTO Bank { get; set; }
        /// <summary>
        /// ชื่อโปรโมชั่น (TH)
        /// </summary>
        [Description("ชื่อโปรโมชั่น (TH)")]
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อโปรโมชั่น (EN)
        /// </summary>
        [Description("ชื่อโปรโมชั่น (EN)")]
        public string NameEN { get; set; }
        /// <summary>
        /// ค่าธรรมเนียม (%)
        /// </summary>
        public double Fee { get; set; }
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
        /// สถานะ
        ///  Master/api/MasterCenters?masterCenterGroupKey=PromotionItemStatus
        /// </summary>
        [Description("สถานะ")]
        public MST.MasterCenterDropdownDTO PromotionItemStatus { get; set; }
        /// <summary>
        /// จำนวน
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// มาจาก EDC ไหน
        /// </summary>
        public MST.EDCFeeDTO EDCFee { get; set; }
        /// <summary>
        /// ชื่อธนาคาร
        /// </summary>
        [Description("ชื่อธนาคาร")]
        public string BankName { get; set; }


        public static MasterSalePromotionCreditCardItemDTO CreateFromQueryResult(MasterSalePromotionCreditCardItemQueryResult model)
        {
            if (model != null)
            {
                var result = new MasterSalePromotionCreditCardItemDTO()
                {
                    Id = model.MasterSalePromotionCreditCardItem.ID,
                    Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank),
                    NameTH = model.MasterSalePromotionCreditCardItem.NameTH,
                    NameEN = model.MasterSalePromotionCreditCardItem.NameEN,
                    Fee = model.MasterSalePromotionCreditCardItem.Fee,
                    UnitTH = model.MasterSalePromotionCreditCardItem.UnitTH,
                    UnitEN = model.MasterSalePromotionCreditCardItem.UnitEN,
                    PromotionItemStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionItemStatus),
                    EDCFee = MST.EDCFeeDTO.CreateFromModel(model.EDCFee),
                    Quantity = model.MasterSalePromotionCreditCardItem.Quantity,
                    Updated = model.MasterSalePromotionCreditCardItem.Updated,
                    UpdatedBy = model.MasterSalePromotionCreditCardItem.UpdatedBy?.DisplayName,
                    BankName = model.MasterSalePromotionCreditCardItem.BankName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static MasterSalePromotionCreditCardItemDTO CreateFromModel(MasterSalePromotionCreditCardItem model)
        {
            if (model != null)
            {
                var result = new MasterSalePromotionCreditCardItemDTO()
                {
                    Id = model.ID,
                    Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank),
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    Fee = model.Fee,
                    UnitTH = model.UnitTH,
                    UnitEN = model.UnitEN,
                    PromotionItemStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionItemStatus),
                    Quantity = model.Quantity,
                    EDCFee = MST.EDCFeeDTO.CreateFromModel(model.EDCFee),
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    BankName = model.BankName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(MasterSalePromotionCreditCardItemSortByParam sortByParam, ref IQueryable<MasterSalePromotionCreditCardItemQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterSalePromotionCreditCardItemSortBy.Bank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.NameTH);
                        else query = query.OrderByDescending(o => o.Bank.NameTH);
                        break;
                    case MasterSalePromotionCreditCardItemSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionCreditCardItem.NameTH);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionCreditCardItem.NameTH);
                        break;
                    case MasterSalePromotionCreditCardItemSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionCreditCardItem.NameEN);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionCreditCardItem.NameEN);
                        break;
                    case MasterSalePromotionCreditCardItemSortBy.Fee:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionCreditCardItem.Fee);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionCreditCardItem.Fee);
                        break;
                    case MasterSalePromotionCreditCardItemSortBy.UnitTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionCreditCardItem.UnitTH);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionCreditCardItem.UnitTH);
                        break;
                    case MasterSalePromotionCreditCardItemSortBy.UnitEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionCreditCardItem.UnitEN);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionCreditCardItem.UnitEN);
                        break;
                    case MasterSalePromotionCreditCardItemSortBy.PromotionItemStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionItemStatus.Name);
                        else query = query.OrderByDescending(o => o.PromotionItemStatus.Name);
                        break;
                    case MasterSalePromotionCreditCardItemSortBy.Quantity:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionCreditCardItem.Quantity);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionCreditCardItem.Quantity);
                        break;
                    case MasterSalePromotionCreditCardItemSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotionCreditCardItem.Updated);
                        else query = query.OrderByDescending(o => o.MasterSalePromotionCreditCardItem.Updated);
                        break;
                    case MasterSalePromotionCreditCardItemSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.Bank.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Bank.NameTH);
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
            else
            {
                if (!this.NameTH.CheckAllLang(true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterSalePromotionFreeItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.NameEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterSalePromotionCreditCardItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameEN.CheckAllLang(true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterSalePromotionCreditCardItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            //if (string.IsNullOrEmpty(this.UnitTH))
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(MasterSalePromotionCreditCardItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            //if (string.IsNullOrEmpty(this.UnitEN))
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(MasterSalePromotionCreditCardItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            if (this.PromotionItemStatus == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterSalePromotionCreditCardItemDTO.PromotionItemStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterSalePromotionCreditCardItem model)
        {
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.PromotionItemStatusMasterCenterID = this.PromotionItemStatus?.Id;
        }
    }
    public class MasterSalePromotionCreditCardItemQueryResult
    {
        public MasterSalePromotionCreditCardItem MasterSalePromotionCreditCardItem { get; set; }
        public Bank Bank { get; set; }
        public EDCFee EDCFee { get; set; }
        public MasterCenter PromotionItemStatus { get; set; }
        public User UpdatedBy { get; set; }
    }
}
