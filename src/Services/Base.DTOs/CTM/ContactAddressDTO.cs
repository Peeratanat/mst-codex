using Database.Models;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class ContactAddressDTO
    {
        /// <summary>
        /// ID ของที่อยู่
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ID ของ Contact
        /// </summary>
        public Guid? contactID { get; set; }
        /// <summary>
        /// ประเภทของที่อยู่ (ตามที่อยู่บัตรประชาชน/ติดต่อได้/ทะเบียนบ้าน/ที่ทำงาน)
        /// master/api/MasterCenters?masterCenterGroupKey=ContactAddressType
        /// </summary>
        [Description("ประเภทของที่อยู่")]
        public MST.MasterCenterDropdownDTO ContactAddressType { get; set; }
        /// <summary>
        /// โครงการ
        /// project/api/Projects/DropdownList
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDTO Project { get; set; }
        /// <summary>
        /// บ้านเลขที่ (ภาษาไทย)
        /// </summary>
        [Description("บ้านเลขที่ (ภาษาไทย)")]
        public string HouseNoTH { get; set; }
        /// <summary>
        /// หมู่ที่ (ภาษาไทย)
        /// </summary>
        [Description("หมู่ที่ (ภาษาไทย)")]
        public string MooTH { get; set; }
        /// <summary>
        /// ชื่อหมู่บ้าน (ภาษาไทย)
        /// </summary>
        [Description("ชื่อหมู่บ้าน (ภาษาไทย)")]
        public string VillageTH { get; set; }
        /// <summary>
        /// ซอย (ภาษาไทย)
        /// </summary>
        [Description("ซอย (ภาษาไทย)")]
        public string SoiTH { get; set; }
        /// <summary>
        /// ถนน (ภาษาไทย)
        /// </summary>
        [Description("ถนน (ภาษาไทย)")]
        public string RoadTH { get; set; }
        /// <summary>
        /// บ้านเลขที่ (ภาษาอังกฤษ)
        /// </summary>
        [Description("บ้านเลขที่ (ภาษาอังกฤษ)")]
        public string HouseNoEN { get; set; }
        /// <summary>
        /// หมู่ที่ (ภาษาอังกฤษ)
        /// </summary>
        [Description("หมู่ที่ (ภาษาอังกฤษ)")]
        public string MooEN { get; set; }
        /// <summary>
        /// ชื่อหมู่บ้าน (ภาษาอังกฤษ)
        /// </summary>
        [Description("ชื่อหมู่บ้าน (ภาษาอังกฤษ)")]
        public string VillageEN { get; set; }
        /// <summary>
        /// ซอย (ภาษาอังกฤษ)
        /// </summary>
        [Description("ซอย (ภาษาอังกฤษ)")]
        public string SoiEN { get; set; }
        /// <summary>
        /// ถนน (ภาษาอังกฤษ)
        /// </summary>
        [Description("ถนน (ภาษาอังกฤษ)")]
        public string RoadEN { get; set; }
        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        [Description("รหัสไปรษณีย์")]
        public string PostalCode { get; set; }
        /// <summary>
        /// ประเทศ
        /// Master/api/Countries
        /// </summary>
        [Description("ประเทศ")]
        public MST.CountryDTO Country { get; set; }
        /// <summary>
        /// จังหวัด
        /// Master/api/Provinces/DropdownList
        /// </summary>
        [Description("จังหวัด")]
        public MST.ProvinceListDTO Province { get; set; }
        /// <summary>
        /// อำเภอ
        /// Master/api/Districts/DropdownList
        /// </summary>
        [Description("อำเภอ")]
        public MST.DistrictListDTO District { get; set; }
        /// <summary>
        /// ตำบล
        /// Master/api/SubDistricts/DropdownList
        /// </summary>
        [Description("ตำบล")]
        public MST.SubDistrictListDTO SubDistrict { get; set; }
        /// <summary>
        /// จังหวัด (ต่างประเทศ)
        /// </summary>
        [Description("จังหวัด (ต่างประเทศ)")]
        public string ForeignProvince { get; set; }
        /// <summary>
        /// อำเภอ (ต่างประเทศ)
        /// </summary>
        [Description("อำเภอ (ต่างประเทศ)")]
        public string ForeignDistrict { get; set; }
        /// <summary>
        /// ตำบล (ต่างประเทศ)
        /// </summary>
        [Description("ตำบล (ต่างประเทศ)")]
        public string ForeignSubDistrict { get; set; }
        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// สร้างโดย
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// วันที่แก้ไข
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        /// <summary>
        /// แก้ไขโดย
        /// </summary>
        public string UpdatedBy { get; set; }

        public async static Task<ContactAddressDTO> CreateFromModelAsync(models.CTM.ContactAddress model, DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new ContactAddressDTO()
                {
                    Id = model.ID,
                    contactID = model.ContactID,
                    HouseNoTH = model.HouseNoTH,
                    MooTH = model.MooTH,
                    VillageTH = model.VillageTH,
                    SoiTH = model.SoiTH,
                    RoadTH = model.RoadTH,
                    Country = MST.CountryDTO.CreateFromModel(model.Country),
                    Province = MST.ProvinceListDTO.CreateFromModel(model.Province),
                    District = MST.DistrictListDTO.CreateFromModel(model.District),
                    SubDistrict = MST.SubDistrictListDTO.CreateFromModel(model.SubDistrict),
                    HouseNoEN = model.HouseNoEN,
                    MooEN = model.MooEN,
                    VillageEN = model.VillageEN,
                    SoiEN = model.SoiEN,
                    RoadEN = model.RoadEN,
                    PostalCode = model.PostalCode,
                    ContactAddressType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactAddressType),
                    ForeignSubDistrict = model.ForeignSubDistrict,
                    ForeignDistrict = model.ForeignDistrict,
                    ForeignProvince = model.ForeignProvince,
                    CreatedDate = model.Created,
                    CreatedBy = model.CreatedBy?.DisplayName,
                    UpdatedDate = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                var addressProject = await DB.ContactAddressProjects.Include(o => o.Project).Where(a => a.ContactAddressID == model.ID).FirstOrDefaultAsync();
                if (addressProject != null)
                {
                    if (addressProject.Project.ID != null)
                    {
                        result.Project = PRJ.ProjectDTO.CreateFromModel(addressProject.Project);
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(DatabaseContext DB)
        {
            ValidateException ex = new ValidateException();
            var thaiID = await DB.Countries.Where(o => o.NameEN == "Thailand" || o.NameTH == "Thailand").Select(o => o.ID).FirstOrDefaultAsync();
            if (!string.IsNullOrEmpty(this.HouseNoTH))
            {
                if (!this.HouseNoTH.CheckThaiWithEngLang(true, true, false))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.HouseNoTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(this.MooTH))
            {
                if (!this.MooTH.CheckAllLang(true, true, false, null, " "))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.MooTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(this.VillageTH))
            {
                if (!this.VillageTH.CheckAllLang(true, true, false, null, " "))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.VillageTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                if (string.IsNullOrEmpty(this.VillageEN))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.VillageEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (!this.VillageEN.CheckLang(false, true, true, false, null, " "))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.VillageEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }
            if (!string.IsNullOrEmpty(this.SoiTH))
            {
                if (!this.SoiTH.CheckAllLang(true, true, false, null, " "))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.SoiTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                if (string.IsNullOrEmpty(this.SoiEN))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.SoiEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (!this.SoiEN.CheckLang(false, true, true, false, null, " "))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.SoiEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }
            if (!string.IsNullOrEmpty(this.RoadTH))
            {
                if (!this.RoadTH.CheckAllLang(true, true, false, null, " "))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.RoadTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                if (string.IsNullOrEmpty(this.RoadEN))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.RoadEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (!this.RoadEN.CheckLang(false, true, true, false, null, " "))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.RoadEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }
            if (this.Country != null)
            {
                if (this.Country.Id == thaiID)
                {
                    this.ForeignProvince = null;
                    this.ForeignDistrict = null;
                    this.ForeignSubDistrict = null;
                    if (!string.IsNullOrEmpty(PostalCode))
                    {
                        if (!this.PostalCode.IsOnlyNumber())
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.PostalCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                }
                else
                {
                    this.Province = null;
                    this.District = null;
                    this.SubDistrict = null; 
                    if (!string.IsNullOrEmpty(PostalCode))
                    {
                        if (!this.PostalCode.CheckLang(false, true, true, false, null, " "))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.PostalCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                }
            }

            #region Foreign
            if (this.Country != null)
            {
                if (this.Country.Id != thaiID)
                {
                    if (!string.IsNullOrEmpty(this.ForeignProvince))
                    {
                        if (!this.ForeignProvince.CheckAllLang(false, true, true, null, " "))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.ForeignProvince)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (!string.IsNullOrEmpty(this.ForeignDistrict))
                    {
                        if (!this.ForeignDistrict.CheckAllLang(false, true, true, null, " "))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.ForeignDistrict)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (!string.IsNullOrEmpty(this.ForeignSubDistrict))
                    {
                        if (!this.ForeignSubDistrict.CheckAllLang(false, true, true, null, " "))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(ContactAddressDTO.ForeignSubDistrict)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                }
            }
            #endregion 

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref models.CTM.ContactAddress model)
        {
            model.HouseNoTH = this.HouseNoTH;
            model.MooTH = this.MooTH;
            model.VillageTH = this.VillageTH;
            model.SoiTH = this.SoiTH;
            model.RoadTH = this.RoadTH;
            model.CountryID = this.Country?.Id;
            model.ProvinceID = this.Province?.Id;
            model.DistrictID = this.District?.Id;
            model.SubDistrictID = this.SubDistrict?.Id;
            model.HouseNoEN = this.HouseNoEN;
            model.MooEN = this.MooEN;
            model.VillageEN = this.VillageEN;
            model.SoiEN = this.SoiEN;
            model.RoadEN = this.RoadEN;
            model.PostalCode = this.PostalCode;
            model.ForeignSubDistrict = this.ForeignSubDistrict;
            model.ForeignDistrict = this.ForeignDistrict;
            model.ForeignProvince = this.ForeignProvince;
        }

        public void ToBookingOwnerModel(ref models.SAL.BookingOwnerAddress model)
        {
            model.HouseNoTH = this.HouseNoTH;
            model.MooTH = this.MooTH;
            model.VillageTH = this.VillageTH;
            model.SoiTH = this.SoiTH;
            model.RoadTH = this.RoadTH;
            model.CountryID = this.Country?.Id;
            model.ProvinceID = this.Province?.Id;
            model.DistrictID = this.District?.Id;
            model.SubDistrictID = this.SubDistrict?.Id;
            model.HouseNoEN = this.HouseNoEN;
            model.MooEN = this.MooEN;
            model.VillageEN = this.VillageEN;
            model.SoiEN = this.SoiEN;
            model.RoadEN = this.RoadEN;
            model.PostalCode = this.PostalCode;
            model.ForeignSubDistrict = this.ForeignSubDistrict;
            model.ForeignDistrict = this.ForeignDistrict;
            model.ForeignProvince = this.ForeignProvince;
        }
    }
}
