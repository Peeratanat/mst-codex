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
    public class LandOfficeDTO : BaseDTO
    {
        /// <summary>
        /// ชื่อสำนักงานที่ดิน ภาษาไทย
        /// </summary>
        [Description("ชื่อสำนักงานที่ดิน ภาษาไทย")]
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อสำนักงานที่ดิน ภาษาอังกฤษ
        /// </summary>
        [Description("ชื่อสำนักงานที่ดิน ภาษาอังกฤษ")]
        public string NameEN { get; set; }
        /// <summary>
        /// จังหวัด
        /// masterdata/api/Provinces/DropdownList
        /// </summary>
        [Description("จังหวัด")]
        public ProvinceListDTO Province { get; set; }
        /// <summary>
        /// อำเภอ
        /// masterdata/api/Districts/DropdownList
        /// </summary>
        [Description("อำเภอ")]
        public DistrictListDTO District { get; set; }
        /// <summary>
        /// ตำบล
        /// masterdata/api/SubDistricts/DropdownList
        /// </summary>
        [Description("ตำบล")]
        public SubDistrictListDTO SubDistrict { get; set; }
        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        public string PostalCode { get; set; }

        public async static Task<LandOfficeDTO> CreateFromModelAsync(LandOffice model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new LandOfficeDTO()
                {
                    Id = model.ID,
                    NameEN = model.NameEN,
                    NameTH = model.NameTH,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };
                var subDistrict = await db.SubDistricts.Where(o => o.LandOfficeID == model.ID).FirstOrDefaultAsync();
                if (subDistrict != null)
                {
                    var district = await db.Districts.Include(o => o.Province).Where(o => o.ID == subDistrict.DistrictID).FirstOrDefaultAsync();
                    result.SubDistrict = SubDistrictListDTO.CreateFromModel(subDistrict);
                    result.PostalCode = subDistrict.PostalCode;
                    result.District = DistrictListDTO.CreateFromModel(district);
                    result.Province = ProvinceListDTO.CreateFromModel(district.Province);
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<LandOfficeDTO> CreateFromQueryResultAsync(LandOfficeQueryResult model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new LandOfficeDTO()
                {
                    Id = model.LandOffice.ID,
                    NameEN = model.LandOffice.NameEN,
                    NameTH = model.LandOffice.NameTH,
                    Updated = model.LandOffice.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                var subDistrict = await db.SubDistricts.Where(o => o.LandOfficeID == model.LandOffice.ID).FirstOrDefaultAsync();
                if (subDistrict != null)
                {
                    result.PostalCode = subDistrict.PostalCode;
                    var district = await db.Districts.Include(o => o.Province).Where(o => o.ID == subDistrict.DistrictID).FirstOrDefaultAsync();
                    result.SubDistrict = SubDistrictListDTO.CreateFromModel(subDistrict);
                    if (district != null)
                    {
                        result.District = DistrictListDTO.CreateFromModel(district);
                        result.Province = ProvinceListDTO.CreateFromModel(district.Province);
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public static LandOfficeDTO CreateFromModels(LandOffice model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new LandOfficeDTO()
                {
                    Id = model.ID,
                    NameEN = model.NameEN,
                    NameTH = model.NameTH,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };
                var subDistrict = db.SubDistricts.Where(o => o.LandOfficeID == model.ID).FirstOrDefault();
                if (subDistrict != null)
                {
                    var district = db.Districts.Include(o => o.Province).Where(o => o.ID == subDistrict.DistrictID).FirstOrDefault();
                    result.SubDistrict = SubDistrictListDTO.CreateFromModel(subDistrict);
                    result.PostalCode = subDistrict.PostalCode;
                    result.District = DistrictListDTO.CreateFromModel(district);
                    result.Province = ProvinceListDTO.CreateFromModel(district.Province);
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortByList(LandOfficeSortByParam sortByParam, ref List<LandOfficeDTO> results)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case LandOfficeSortBy.NameTH:
                        if (sortByParam.Ascending) results = results.OrderBy(o => o.NameTH).ToList();
                        else results = results.OrderByDescending(o => o.NameTH).ToList();
                        break;
                    case LandOfficeSortBy.NameEN:
                        if (sortByParam.Ascending) results = results.OrderBy(o => o.NameEN).ToList();
                        else results = results.OrderByDescending(o => o.NameEN).ToList();
                        break;
                    case LandOfficeSortBy.Province:
                        if (sortByParam.Ascending) results = results.OrderBy(o => o.Province?.NameTH).ToList();
                        else results = results.OrderByDescending(o => o.Province?.NameEN).ToList();
                        break;
                    case LandOfficeSortBy.District:
                        if (sortByParam.Ascending) results = results.OrderBy(o => o.District?.NameTH).ToList();
                        else results = results.OrderByDescending(o => o.District?.NameEN).ToList();
                        break;
                    case LandOfficeSortBy.SubDistrict:
                        if (sortByParam.Ascending) results = results.OrderBy(o => o.SubDistrict?.NameTH).ToList();
                        else results = results.OrderByDescending(o => o.SubDistrict?.NameEN).ToList();
                        break;
                    case LandOfficeSortBy.Updated:
                        if (sortByParam.Ascending) results = results.OrderBy(o => o.Updated).ToList();
                        else results = results.OrderByDescending(o => o.Updated).ToList();
                        break;
                    case LandOfficeSortBy.UpdatedBy:
                        if (sortByParam.Ascending) results = results.OrderBy(o => o.UpdatedBy).ToList();
                        else results = results.OrderByDescending(o => o.UpdatedBy).ToList();
                        break;
                    default:
                        results = results.OrderBy(o => o.NameTH).ToList();
                        break;
                }
            }
            else
            {
                results = results.OrderBy(o => o.NameTH).ToList();
            }
        }
        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(LandOfficeDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.NameTH.CheckLang(true, false, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0020").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(LandOfficeDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueNameTH = this.Id != (Guid?)null ? db.LandOffices.Any(o => o.NameTH == this.NameTH && o.ID != this.Id) : db.LandOffices.Any(o => o.NameTH == this.NameTH);
                if (checkUniqueNameTH)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(LandOfficeDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                    string desc = this.GetType().GetProperty(nameof(LandOfficeDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (this.SubDistrict == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(LandOfficeDTO.SubDistrict)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                var checkUniqueSubDistrict = this.Id != (Guid?)null ? await db.SubDistricts.Where(o => o.ID == this.SubDistrict.Id && o.LandOfficeID != this.Id && o.LandOfficeID != null).CountAsync() > 0 : await db.SubDistricts.Where(o => o.ID == this.SubDistrict.Id && o.LandOfficeID != null).CountAsync() > 0;
                if (checkUniqueSubDistrict)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(LandOfficeDTO.SubDistrict)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.SubDistrict.NameTH);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (this.District == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(LandOfficeDTO.District)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Province == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(LandOfficeDTO.Province)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref LandOffice model)
        {
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
        }
    }
    public class LandOfficeQueryResult
    {
        public LandOffice LandOffice { get; set; }
        public User UpdatedBy { get; set; }
    }
}
