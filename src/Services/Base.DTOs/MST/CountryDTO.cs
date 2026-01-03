using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Database.Models;
using Database.Models.MST;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.MST
{
    public class CountryDTO : BaseDTO
    {
        /// <summary>
        /// รหัสประเทศ
        /// </summary>
        [Description("รหัสประเทศ")]
        public string Code { get; set; }
        /// <summary>
        /// ชื่อประเทศ (ภาษาไทย)
        /// </summary>
        [Description("ชื่อประเทศ (ภาษาไทย)")]
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อประเทศ (ภาษาอังกฤษ)
        /// </summary>
        [Description("ชื่อประเทศ (ภาษาอังกฤษ)")]
        public string NameEN { get; set; }

        public static CountryDTO CreateFromModel(Country model)
        {
            if (model != null)
            {
                var result = new CountryDTO()
                {
                    Id = model.ID,
                    NameEN = model.NameEN,
                    NameTH = model.NameTH,
                    Code = model.Code,
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

        public static CountryDTO CreateFromQueryResult(CountryQueryResult model)
        {
            if (model != null)
            {
                var result = new CountryDTO()
                {
                    Id = model.Country.ID,
                    Code = model.Country.Code,
                    NameEN = model.Country.NameEN,
                    NameTH = model.Country.NameTH,
                    Updated = model.Country.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CountrySortByParam sortByParam, ref IQueryable<CountryQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case CountrySortBy.Code:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Country.Code);
                        else query = query.OrderByDescending(o => o.Country.Code);
                        break;
                    case CountrySortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Country.NameTH);
                        else query = query.OrderByDescending(o => o.Country.NameTH);
                        break;
                    case CountrySortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Country.NameEN);
                        else query = query.OrderByDescending(o => o.Country.NameEN);
                        break;
                    case CountrySortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Country.Updated);
                        else query = query.OrderByDescending(o => o.Country.Updated);
                        break;
                    case CountrySortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Country.Code);
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.Code))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(CountryDTO.Code)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.Code.CheckLang(false, false, false, true,2))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0021").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(CountryDTO.Code)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueCode = this.Id != (Guid?)null ? await db.Countries.Where(o => o.Code == this.Code && o.ID != this.Id).CountAsync() > 0 : await db.Countries.Where(o => o.Code == this.Code).CountAsync() > 0;
                if (checkUniqueCode)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(CountryDTO.Code)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.Code);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(CountryDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameTH.CheckLang(true, false, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0020").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(CountryDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.NameEN))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(CountryDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameEN.CheckLang(false, false, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(CountryDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref Country model)
        {
            model.Code = this.Code;
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
        }
    }
    public class CountryQueryResult
    {
        public Country Country { get; set; }
        public User UpdatedBy { get; set; }
    }
}
