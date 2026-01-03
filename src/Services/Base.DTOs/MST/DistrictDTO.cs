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
    public class DistrictDTO : BaseDTO
    {
        /// <summary>
        /// จังหวัด
        /// Master/Provinces/DropdownList
        /// </summary>
        [Description("จังหวัด")]
        public ProvinceListDTO Province { get; set; }
        /// <summary>
        /// ชื่อภาษาไทย
        /// </summary>
        [Description("ชื่ออำเภอ ภาษาไทย")]
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อภาษอังกฤษ
        /// </summary>
        [Description("ชื่ออำเภอ ภาษอังกฤษ")]
        public string NameEN { get; set; }
        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        public string PostalCode { get; set; }

        public static DistrictDTO CreateFromModel(District model)
        {
            if (model != null)
            {
                var result = new DistrictDTO()
                {
                    Id = model.ID,
                    NameEN = model.NameEN,
                    NameTH = model.NameTH,
                    Province = ProvinceListDTO.CreateFromModel(model.Province),
                    PostalCode = model.PostalCode,
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

        public static DistrictDTO CreateFromQueryResult(DistrictQueryResult model)
        {
            if (model != null)
            {
                var result = new DistrictDTO()
                {
                    Id = model.District.ID,
                    NameEN = model.District.NameEN,
                    NameTH = model.District.NameTH,
                    Province = ProvinceListDTO.CreateFromModel(model.Province),
                    PostalCode = model.District.PostalCode,
                    Updated = model.District.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(DistrictSortByParam sortByParam, ref IQueryable<DistrictQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case DistrictSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.District.NameEN);
                        else query = query.OrderByDescending(o => o.District.NameEN);
                        break;
                    case DistrictSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.District.NameTH);
                        else query = query.OrderByDescending(o => o.District.NameTH);
                        break;
                    case DistrictSortBy.Province:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Province.NameTH);
                        else query = query.OrderByDescending(o => o.Province.NameTH);
                        break;
                    case DistrictSortBy.PostalCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.District.PostalCode);
                        else query = query.OrderByDescending(o => o.District.PostalCode);
                        break;
                    case DistrictSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.District.Updated);
                        else query = query.OrderByDescending(o => o.District.Updated);
                        break;
                    case DistrictSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.District.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.District.NameTH);
            }
        }

        public async Task ValidateAsync(DatabaseContext db, bool isUpdate)
        {
            ValidateException ex = new ValidateException();
            if (isUpdate)
            {
                var checkUseSubDistricts = await db.SubDistricts.Where(o => o.DistrictID == this.Id).CountAsync() > 0;
                if (checkUseSubDistricts)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0183").FirstAsync();
                    var msg = errMsg.Message;
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DistrictDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameTH.CheckLang(true, false, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0020").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(DistrictDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            var checkUniqueNameTH = this.Id != (Guid?)null ? await db.Districts.Where(o => o.NameTH == this.NameTH && o.ID != this.Id && o.ProvinceID == this.Province.Id).CountAsync() > 0 : await db.Districts.Where(o => o.NameTH == this.NameTH && o.ProvinceID == this.Province.Id).CountAsync() > 0;
            if (checkUniqueNameTH && !string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DistrictDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                msg = msg.Replace("[value]", this.NameTH);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (!string.IsNullOrEmpty(this.NameEN))
            {
                if (!this.NameEN.CheckLang(false, false, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0005").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(DistrictDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (this.Province == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DistrictDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref District model)
        {
            model.ProvinceID = this.Province?.Id ?? Guid.Empty;
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.PostalCode = this.PostalCode;
        }
    }

    public class DistrictQueryResult
    {
        public District District { get; set; }
        public Province Province { get; set; }
        public User UpdatedBy { get; set; }
    }
}
