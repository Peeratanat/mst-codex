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
    /// รายการค่าธรรมเนียมบัตรเครดิตโปรโอน
    /// </summary>
    public class MasterTransferCreditCardItemDTO : BaseDTO
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
        [Description("ค่าธรรมเนียม (%)")]
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
        /// ชนิดของรายการโปรโมชั่น
        /// </summary>
        public PromotionItemType Itemtype { get; set; }
        /// <summary>
        /// ชื่อธนาคาร
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// ค่าธรรมเนียม
        /// </summary>
        public decimal TotalPrice { get; set; }

        public static MasterTransferCreditCardItemDTO CreateFromQueryResult(MasterTransferCreditCardItemQueryResult model)
        {
            if (model != null)
            {
                var result = new MasterTransferCreditCardItemDTO()
                {
                    Id = model.MasterTransferCreditCardItem.ID,
                    Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank),
                    NameTH = model.MasterTransferCreditCardItem.NameTH,
                    NameEN = model.MasterTransferCreditCardItem.NameEN,
                    Fee = model.MasterTransferCreditCardItem.Fee,
                    UnitTH = model.MasterTransferCreditCardItem.UnitTH,
                    UnitEN = model.MasterTransferCreditCardItem.UnitEN,
                    PromotionItemStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionItemStatus),
                    Quantity = model.MasterTransferCreditCardItem.Quantity,
                    EDCFee = MST.EDCFeeDTO.CreateFromModel(model.EDCFee),
                    Updated = model.MasterTransferCreditCardItem.Updated,
                    UpdatedBy = model.MasterTransferCreditCardItem.UpdatedBy?.DisplayName,
                    BankName = model.BankName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static MasterTransferCreditCardItemDTO CreateFromModel(MasterTransferPromotionCreditCardItem model)
        {
            if (model != null)
            {
                var result = new MasterTransferCreditCardItemDTO()
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
        
        public static MasterTransferPromotionItemDTO CreateFromDraftModel(MasterTransferPromotionCreditCardItem model)
        {
            if (model != null)
            {
                var result = new MasterTransferPromotionItemDTO()
                {
                    Id = model.ID,
                  //  Bank = MST.BankDropdownDTO.CreateFromModel(model.Bank),
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    //Fee = model.Fee,
                    UnitTH = model.UnitTH,
                    UnitEN = model.UnitEN,
                    Itemtype= PromotionItemType.CreditCard,
                    PromotionItemStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionItemStatus),
                    Quantity = model.Quantity,
                    //EDCFee = MST.EDCFeeDTO.CreateFromModel(model.EDCFee),
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

        public static void SortBy(MasterTransferCreditCardItemSortByParam sortByParam, ref IQueryable<MasterTransferCreditCardItemQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterTransferCreditCardItemSortBy.Bank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.NameTH);
                        else query = query.OrderByDescending(o => o.Bank.NameTH);
                        break;
                    case MasterTransferCreditCardItemSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferCreditCardItem.NameTH);
                        else query = query.OrderByDescending(o => o.MasterTransferCreditCardItem.NameTH);
                        break;
                    case MasterTransferCreditCardItemSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferCreditCardItem.NameEN);
                        else query = query.OrderByDescending(o => o.MasterTransferCreditCardItem.NameEN);
                        break;
                    case MasterTransferCreditCardItemSortBy.Fee:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferCreditCardItem.Fee);
                        else query = query.OrderByDescending(o => o.MasterTransferCreditCardItem.Fee);
                        break;
                    case MasterTransferCreditCardItemSortBy.UnitTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferCreditCardItem.UnitTH);
                        else query = query.OrderByDescending(o => o.MasterTransferCreditCardItem.UnitTH);
                        break;
                    case MasterTransferCreditCardItemSortBy.UnitEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferCreditCardItem.UnitEN);
                        else query = query.OrderByDescending(o => o.MasterTransferCreditCardItem.UnitEN);
                        break;
                    case MasterTransferCreditCardItemSortBy.PromotionItemStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionItemStatus.Name);
                        else query = query.OrderByDescending(o => o.PromotionItemStatus.Name);
                        break;
                    case MasterTransferCreditCardItemSortBy.Quantity:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferCreditCardItem.Quantity);
                        else query = query.OrderByDescending(o => o.MasterTransferCreditCardItem.Quantity);
                        break;
                    case MasterTransferCreditCardItemSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferCreditCardItem.Updated);
                        else query = query.OrderByDescending(o => o.MasterTransferCreditCardItem.Updated);
                        break;
                    case MasterTransferCreditCardItemSortBy.UpdatedBy:
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
                string desc = this.GetType().GetProperty(nameof(MasterTransferCreditCardItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameTH.CheckAllLang(true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterTransferCreditCardItemDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.NameEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferCreditCardItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameEN.CheckAllLang(true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterTransferCreditCardItemDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.UnitTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferCreditCardItemDTO.UnitTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (string.IsNullOrEmpty(this.UnitEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferCreditCardItemDTO.UnitEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.PromotionItemStatus == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(MasterTransferCreditCardItemDTO.PromotionItemStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterTransferPromotionCreditCardItem model)
        {
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.PromotionItemStatusMasterCenterID = this.PromotionItemStatus?.Id;
        }
    }
    public class MasterTransferCreditCardItemQueryResult
    {
        public MasterTransferPromotionCreditCardItem MasterTransferCreditCardItem { get; set; }
        public Bank Bank { get; set; }
        public EDCFee EDCFee { get; set; }
        public MasterCenter PromotionItemStatus { get; set; }
        public User UpdatedBy { get; set; }
        public string BankName { get; set; }
    }
}
