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
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class AgentDTO : BaseDTO
    {
        /// <summary>
        /// ชื่อ Agent ภาษาไทย (TH)
        /// </summary>
        [Description("ชื่อ Agent ภาษาไทย (TH)")]
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อ Agent อังกฤษ (EN)
        /// </summary>
        [Description("ชื่อ Agent อังกฤษ (EN)")]
        public string NameEN { get; set; }
        /// <summary>
        /// ที่ตั้ง
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// ตึก
        /// </summary>
        public string Building { get; set; }
        /// <summary>
        /// ซอย
        /// </summary>
        public string Soi { get; set; }
        /// <summary>
        /// ถนน
        /// </summary>
        public string Road { get; set; }
        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// จังหวัด
        /// masterdata/api/Provinces/DropdownList
        /// </summary>
        public ProvinceListDTO Province { get; set; }
        /// <summary>
        /// อำเภอ/เขต
        /// masterdata/api/Districts/DropdownList
        /// </summary>
        public DistrictListDTO District { get; set; }
        /// <summary>
        /// ตำบล/แขวง
        /// masterdata/api/SubDistricts/DropdownList
        /// </summary>
        public SubDistrictListDTO SubDistrict { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        [Description("เบอร์โทรศัพท์")]
        public string TelNo { get; set; }
        /// <summary>
        /// เบอร์ Fax
        /// </summary>
        [Description("เบอร์ Fax")]
        public string FaxNo { get; set; }
        /// <summary>
        /// เว็บไซต์
        /// </summary>
        public string Website { get; set; }

        public static AgentDTO CreateFromModel(Agent model)
        {
            if (model != null)
            {
                var result = new AgentDTO()
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    Address = model.Address,
                    Building = model.Building,
                    Soi = model.Soi,
                    Road = model.Road,
                    PostalCode = model.PostalCode,
                    Province = ProvinceListDTO.CreateFromModel(model.Province),
                    District = DistrictListDTO.CreateFromModel(model.District),
                    SubDistrict = SubDistrictListDTO.CreateFromModel(model.SubDistrict),
                    TelNo = model.TelNo,
                    FaxNo = model.FaxNo,
                    Website = model.Website,
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

        public static AgentDTO CreateFromQueryResult(AgentQueryResult model)
        {
            if (model != null)
            {
                var result = new AgentDTO()
                {
                    Id = model.Agent.ID,
                    NameTH = model.Agent.NameTH,
                    NameEN = model.Agent.NameEN,
                    Address = model.Agent.Address,
                    Building = model.Agent.Building,
                    Soi = model.Agent.Soi,
                    Road = model.Agent.Road,
                    PostalCode = model.Agent.PostalCode,
                    Province = ProvinceListDTO.CreateFromModel(model.Province),
                    District = DistrictListDTO.CreateFromModel(model.District),
                    SubDistrict = SubDistrictListDTO.CreateFromModel(model.SubDistrict),
                    TelNo = model.Agent.TelNo,
                    FaxNo = model.Agent.FaxNo,
                    Website = model.Agent.Website,
                    Updated = model.Agent.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(AgentSortByParam sortByParam, ref IQueryable<AgentQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case AgentSortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agent.NameTH);
                        else query = query.OrderByDescending(o => o.Agent.NameTH);
                        break;
                    case AgentSortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agent.NameEN);
                        else query = query.OrderByDescending(o => o.Agent.NameEN);
                        break;
                    case AgentSortBy.Address:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agent.Address);
                        else query = query.OrderByDescending(o => o.Agent.Address);
                        break;
                    case AgentSortBy.Building:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agent.Building);
                        else query = query.OrderByDescending(o => o.Agent.Building);
                        break;
                    case AgentSortBy.Soi:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agent.Soi);
                        else query = query.OrderByDescending(o => o.Agent.Soi);
                        break;
                    case AgentSortBy.Road:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agent.Road);
                        else query = query.OrderByDescending(o => o.Agent.Road);
                        break;
                    case AgentSortBy.PostalCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agent.PostalCode);
                        else query = query.OrderByDescending(o => o.Agent.PostalCode);
                        break;
                    case AgentSortBy.Province:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Province.NameTH);
                        else query = query.OrderByDescending(o => o.Province.NameTH);
                        break;
                    case AgentSortBy.District:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.District.NameTH);
                        else query = query.OrderByDescending(o => o.District.NameTH);
                        break;
                    case AgentSortBy.SubDistrict:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SubDistrict.NameTH);
                        else query = query.OrderByDescending(o => o.SubDistrict.NameTH);
                        break;
                    case AgentSortBy.TelNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agent.TelNo);
                        else query = query.OrderByDescending(o => o.Agent.TelNo);
                        break;
                    case AgentSortBy.FaxNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agent.FaxNo);
                        else query = query.OrderByDescending(o => o.Agent.FaxNo);
                        break;
                    case AgentSortBy.Website:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agent.Website);
                        else query = query.OrderByDescending(o => o.Agent.Website);
                        break;
                    case AgentSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Agent.Updated);
                        else query = query.OrderByDescending(o => o.Agent.Updated);
                        break;
                    case AgentSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.Agent.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Agent.NameTH);
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(AgentDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                //if (!this.NameTH.CheckLang(true, false, true,false))
                //{
                //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0022").FirstAsync();
                //    string desc = this.GetType().GetProperty(nameof(AgentDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                //    var msg = errMsg.Message.Replace("[field]", desc);
                //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                //}
                var checkUniqueNameTH = this.Id != (Guid?)null ? await db.Agents.Where(o => o.NameTH == this.NameTH && o.ID != this.Id).CountAsync() > 0 : await db.Agents.Where(o => o.NameTH == this.NameTH).CountAsync() > 0;
                if (checkUniqueNameTH)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgentDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.NameTH);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            
            //if (!string.IsNullOrEmpty(this.NameEN))
            //{
            //    if (!this.NameEN.CheckLang(false, false, true,false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0005").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(AgentDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (!string.IsNullOrEmpty(this.TelNo))
            {
                if (!this.TelNo.IsOnlyNumber())
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgentDTO.TelNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(this.FaxNo))
            {
                if (!this.FaxNo.IsOnlyNumber())
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgentDTO.FaxNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref Agent model)
        {
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.Address = this.Address;
            model.Building = this.Building;
            model.Soi = this.Soi;
            model.Road = this.Road;
            model.PostalCode = this.PostalCode;
            model.ProvinceID = this.Province?.Id;
            model.DistrictID = this.District?.Id;
            model.SubDistrictID = this.SubDistrict?.Id;
            model.TelNo = this.TelNo;
            model.FaxNo = this.FaxNo;
            model.Website = this.Website;
        }
    }

    public class AgentQueryResult
    {
        public Agent Agent { get; set; }
        public Province Province { get; set; }
        public District District { get; set; }
        public SubDistrict SubDistrict { get; set; }
        public User UpdatedBy { get; set; }
    }
}
