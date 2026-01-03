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
    public class SubDistrictDTO : BaseDTO
    {
        /// <summary>
        /// อำเภอ
        /// masterdata/api/Districts/DropdownList
        /// </summary>
        [Description("อำเภอ")]
        public DistrictListDTO District { get; set; }
        /// <summary>
        /// สำนักงานที่ดิน
        /// </summary>
        public LandOfficeListDTO LandOffice { get; set; }
        /// <summary>
        /// ชื่อตำบล ภาษาไทย
        /// </summary>
        [Description("ชื่อตำบล ภาษาไทย")]
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อตำบล ภาษาอังกฤษ
        /// </summary>
        [Description("ชื่อตำบล ภาษาอังกฤษ")]
        public string NameEN { get; set; }
        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        [Description("รหัสไปรษณีย์")]
        public string PostalCode { get; set; }

        public static SubDistrictDTO CreateFromModel(SubDistrict model)
        {
            if (model != null)
            {
                SubDistrictDTO result = new SubDistrictDTO()
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    LandOffice = LandOfficeListDTO.CreateFromModel(model.LandOffice),
                    District = DistrictListDTO.CreateFromModel(model.District),
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

        public static SubDistrictDTO CreateFromQueryResult(SubDistrictQueryResult model)
        {
            if (model != null)
            {
                var result = new SubDistrictDTO()
                {
                    Id = model.SubDistrict.ID,
                    NameTH = model.SubDistrict.NameTH,
                    NameEN = model.SubDistrict.NameEN,
                    LandOffice = LandOfficeListDTO.CreateFromModel(model.LandOffice),
                    District = DistrictListDTO.CreateFromModel(model.District),
                    PostalCode = model.SubDistrict.PostalCode,
                    Updated = model.SubDistrict.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(SubDistrictSortByParam sortByParam, ref IQueryable<SubDistrictQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case SubDistrictSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SubDistrict.NameTH);
                        else query = query.OrderByDescending(o => o.SubDistrict.NameTH);
                        break;
                    case SubDistrictSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SubDistrict.NameEN);
                        else query = query.OrderByDescending(o => o.SubDistrict.NameEN);
                        break;
                    case SubDistrictSortBy.LandOffice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LandOffice.NameTH);
                        else query = query.OrderByDescending(o => o.LandOffice.NameTH);
                        break;
                    case SubDistrictSortBy.District:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.District.NameTH);
                        else query = query.OrderByDescending(o => o.District.NameTH);
                        break;
                    case SubDistrictSortBy.PostalCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SubDistrict.PostalCode);
                        else query = query.OrderByDescending(o => o.SubDistrict.PostalCode);
                        break;
                    case SubDistrictSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SubDistrict.Updated);
                        else query = query.OrderByDescending(o => o.SubDistrict.Updated);
                        break;
                    case SubDistrictSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.SubDistrict.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.SubDistrict.NameTH);
            }
        }
        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(SubDistrictDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameTH.CheckLang(true, false, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0020").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(SubDistrictDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            var checkUniqueNameTH = this.Id != (Guid?)null ? await db.SubDistricts.Where(o => o.NameTH == this.NameTH && o.ID != this.Id && o.DistrictID == this.District.Id).CountAsync() > 0 : await db.SubDistricts.Where(o => o.NameTH == this.NameTH && o.DistrictID == this.District.Id).CountAsync() > 0;
            if (checkUniqueNameTH && !string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(SubDistrictDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                msg = msg.Replace("[value]", this.NameTH);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (!string.IsNullOrEmpty(this.NameEN))
            {
                if (!this.NameEN.CheckLang(false, false, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0005").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(SubDistrictDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (this.District == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(SubDistrictDTO.District)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (string.IsNullOrEmpty(this.PostalCode))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(SubDistrictDTO.PostalCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.PostalCode.IsOnlyNumber())
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(SubDistrictDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }
        public void ToModel(ref SubDistrict model)
        {
            model.DistrictID = this.District.Id.Value;
            model.LandOfficeID = this.LandOffice?.Id;
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.PostalCode = this.PostalCode;
        }
    }

    public class SubDistrictQueryResult
    {
        public SubDistrict SubDistrict { get; set; }
        public LandOffice LandOffice { get; set; }
        public District District { get; set; }
        public User UpdatedBy { get; set; }
    }
}