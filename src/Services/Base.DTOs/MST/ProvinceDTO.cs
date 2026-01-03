using System;
using System.Collections.Generic;
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
    public class ProvinceDTO : BaseDTO
    {
        /// <summary>
        /// จังหวัด ภาษาไทย
        /// </summary>
        [Description("จังหวัด ภาษาไทย")]
        public string NameTH { get; set; }
        /// <summary>
        /// จังหวัดภาษา อังกฤษ
        /// </summary>
        [Description("จังหวัดภาษา อังกฤษ")]
        public string NameEN { get; set; }
        /// <summary>
        /// แสดงอยู่หรือไม่
        /// </summary>
        public bool IsShow { get; set; }

        public static ProvinceDTO CreateFromModel(Province model)
        {
            if (model != null)
            {
                ProvinceDTO result = new ProvinceDTO()
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    IsShow = model.IsShow,
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

        public static ProvinceDTO CreateFromQueryResult(ProvinceQueryResult model)
        {
            if (model != null)
            {
                var result = new ProvinceDTO()
                {
                    Id = model.Province.ID,
                    NameTH = model.Province.NameTH,
                    NameEN = model.Province.NameEN,
                    IsShow = model.Province.IsShow,
                    Updated = model.Province.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(ProvinceSortByParam sortByParam, ref IQueryable<ProvinceQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case ProvinceSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Province.NameTH);
                        else query = query.OrderByDescending(o => o.Province.NameTH);
                        break;
                    case ProvinceSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Province.NameEN);
                        else query = query.OrderByDescending(o => o.Province.NameEN);
                        break;
                    case ProvinceSortBy.IsShow:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Province.IsShow);
                        else query = query.OrderByDescending(o => o.Province.IsShow);
                        break;
                    case ProvinceSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Province.Updated);
                        else query = query.OrderByDescending(o => o.Province.Updated);
                        break;
                    case ProvinceSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.Province.NameTH);
                        break;
                    
                }
            }
            else
            {
                query = query.OrderBy(o => o.Province.NameTH);
            }
        }
        public async Task ValidateAsync(DatabaseContext db, bool isUpdate)
        {
            ValidateException ex = new ValidateException();

            if (isUpdate)
            {
                var checkUseProvince = await db.Districts.Where(o => o.ProvinceID == this.Id).CountAsync() > 0;
                if (checkUseProvince)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0182").FirstAsync();
                    var msg = errMsg.Message;
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ProvinceDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameTH.CheckLang(true, false, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0020").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ProvinceDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueNameTH = this.Id != (Guid?)null ? await db.Provinces.Where(o => o.NameTH == this.NameTH && o.ID != this.Id).CountAsync() > 0 : await db.Provinces.Where(o => o.NameTH == this.NameTH).CountAsync() > 0;
                if (checkUniqueNameTH)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ProvinceDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.NameTH);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(this.NameEN))
            {
                if (!this.NameEN.CheckLang(false, false, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0005").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ProvinceDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueNameEN = this.Id != (Guid?)null ? await db.Provinces.Where(o => o.NameEN == this.NameEN && o.ID != this.Id).CountAsync() > 0 : await db.Provinces.Where(o => o.NameEN == this.NameEN).CountAsync() > 0;
                if (checkUniqueNameEN)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ProvinceDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.NameEN);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }
        public void ToModel(ref Province model)
        {
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.IsShow = this.IsShow;
        }
    }

    public class ProvinceQueryResult
    {
        public Province Province { get; set; }
        public User UpdatedBy { get; set; }
    }
}
