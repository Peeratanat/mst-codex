using Database.Models;
using Database.Models.MST;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class BankDTO : BaseDTO
    {
        /// <summary>
        /// รหัสธนาคาร
        /// </summary>
        [Description("รหัสธนาคาร")]
        public string BankNo { get; set; }
        /// <summary>
        /// ชื่อธนาคารภาษาไทย
        /// </summary>
        [Description("ชื่อธนาคารภาษาไทย")]
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อธนาคารภาษาอังกฤษ
        /// </summary>
        [Description("ชื่อธนาคารภาษาอังกฤษ")]
        public string NameEN { get; set; }
        /// <summary>
        /// ชื่อย่อ
        /// </summary>
        [Description("ชื่อย่อ")]
        public string Alias { get; set; }
        /// <summary>
        /// มีบริการบัตร Credit ไหม
        /// </summary>
        public bool IsCreditCard { get; set; }
        /// <summary>
        /// เป็น Bank หรือ NonBank
        /// </summary>
        public bool IsNonBank { get; set; }
        /// <summary>
        /// เป็น Coorperative Bank หรือเปล่า
        /// </summary>
        public bool IsCoorperative { get; set; }
        /// <summary>
        /// ขอสินเชื่อฟรีไหม
        /// </summary>
        public bool IsFreeMortgage { get; set; }
        /// <summary>
        /// SWIFT Code
        /// </summary>
        public string SwiftCode { get; set; }

        public static BankDTO CreateFromModel(Bank model)
        {
            if (model != null)
            {
                var result = new BankDTO()
                {
                    Id = model.ID,
                    BankNo = model.BankNo,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    Alias = model.Alias,
                    IsCreditCard = model.IsCreditCard,
                    IsNonBank = model.IsNonBank,
                    IsCoorperative = model.IsCoorperative,
                    IsFreeMortgage = model.IsFreeMortgage,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    SwiftCode = model.SwiftCode
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static BankDTO CreateFromQueryResult(BankQueryResult model)
        {
            if (model != null)
            {
                var result = new BankDTO()
                {
                    Id = model.Bank.ID,
                    BankNo = model.Bank.BankNo,
                    NameTH = model.Bank.NameTH,
                    NameEN = model.Bank.NameEN,
                    Alias = model.Bank.Alias,
                    IsCreditCard = model.Bank.IsCreditCard,
                    IsNonBank = model.Bank.IsNonBank,
                    IsCoorperative = model.Bank.IsCoorperative,
                    IsFreeMortgage = model.Bank.IsFreeMortgage,
                    Updated = model.Bank.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    SwiftCode = model.Bank.SwiftCode
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(BankSortByParam sortByParam, ref IQueryable<BankQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case BankSortBy.BankNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.BankNo);
                        else query = query.OrderByDescending(o => o.Bank.BankNo);
                        break;
                    case BankSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.NameTH);
                        else query = query.OrderByDescending(o => o.Bank.NameTH);
                        break;
                    case BankSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.NameEN);
                        else query = query.OrderByDescending(o => o.Bank.NameEN);
                        break;
                    case BankSortBy.Alias:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.Alias);
                        else query = query.OrderByDescending(o => o.Bank.Alias);
                        break;
                    case BankSortBy.IsCreditCard:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.IsCreditCard);
                        else query = query.OrderByDescending(o => o.Bank.IsCreditCard);
                        break;
                    case BankSortBy.IsNonBank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.IsNonBank);
                        else query = query.OrderByDescending(o => o.Bank.IsNonBank);
                        break;
                    case BankSortBy.IsCoorperative:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.IsCoorperative);
                        else query = query.OrderByDescending(o => o.Bank.IsCoorperative);
                        break;
                    case BankSortBy.IsFreeMortgage:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.IsFreeMortgage);
                        else query = query.OrderByDescending(o => o.Bank.IsFreeMortgage);
                        break;
                    case BankSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.Updated);
                        else query = query.OrderByDescending(o => o.Bank.Updated);
                        break;
                    case BankSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    case BankSortBy.SwiftCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.SwiftCode);
                        else query = query.OrderByDescending(o => o.Bank.SwiftCode);
                        break;
                    default:
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
            if (string.IsNullOrEmpty(this.BankNo))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankDTO.BankNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.BankNo.IsOnlyNumberWithMaxLength(3))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankDTO.BankNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueBankNo = this.Id != (Guid?)null ? await db.Banks.Where(o => o.BankNo == this.BankNo && o.ID != this.Id).CountAsync() > 0 : await db.Banks.Where(o => o.BankNo == this.BankNo).CountAsync() > 0;
                if (checkUniqueBankNo)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankDTO.BankNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.BankNo);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameTH.CheckLang(true,true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueNameTH = this.Id != (Guid?)null ? await db.Banks.Where(o => o.NameTH == this.NameTH && o.ID != this.Id).CountAsync() > 0 : await db.Banks.Where(o => o.NameTH == this.NameTH).CountAsync() > 0;
                if (checkUniqueNameTH)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.NameTH);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(this.Alias))
            {
                //if (!this.Alias.CheckLang(false, false, false, false))
                //{
                //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                //    string desc = this.GetType().GetProperty(nameof(BankDTO.Alias)).GetCustomAttribute<DescriptionAttribute>().Description;
                //    var msg = errMsg.Message.Replace("[field]", desc);
                //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                //}
                var checkUniqueAlias = this.Id != (Guid?)null ? await db.Banks.Where(o => o.Alias == this.Alias && o.ID != this.Id).CountAsync() > 0 : await db.Banks.Where(o => o.Alias == this.Alias).CountAsync() > 0;
                if (checkUniqueAlias)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankDTO.Alias)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.Alias);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(this.NameEN))
            {
                if (!this.NameEN.CheckLang(false, true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }
        public void ToModel(ref Bank model)
        {
            model.BankNo = this.BankNo;
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.Alias = this.Alias;
            model.IsCreditCard = this.IsCreditCard;
            model.IsNonBank = this.IsNonBank;
            model.IsCoorperative = this.IsNonBank ?  true : false;
            model.IsFreeMortgage = this.IsFreeMortgage;
            model.SwiftCode = this.SwiftCode;
        }
    }
    public class BankQueryResult
    {
        public Bank Bank { get; set; }
        public User UpdatedBy { get; set; }
    }
}
