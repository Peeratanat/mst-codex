using Database.Models;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    public class BookingOwnerAddressDTO
    {
        /// <summary>
        /// ID ของที่อยู่ผู้จอง
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ID contact
        /// </summary>
        public Guid? ContactID { get; set; }
        /// <summary>
        /// มาจากที่อยู่ Contact
        /// </summary>
        public Guid? FromContactAddressID { get; set; }
        /// <summary>
        /// ประเภทของที่อยู่
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactAddressType
        /// </summary>
        [Description("ประเภทของที่อยู่")]
        public MST.MasterCenterDropdownDTO ContactAddressType { get; set; }
        /// <summary>
        /// โครงการ
        /// project/api/Projects/DropdownList
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDropdownDTO Project { get; set; }
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
        /// หมู่บ้าน/อาคาร (ภาษาไทย)
        /// </summary>
        [Description("หมู่บ้าน/อาคาร (ภาษาไทย)")]
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
        /// หมู่บ้าน/อาคาร (ภาษาอังกฤษ)
        /// </summary>
        [Description("หมู่บ้าน/อาคาร (ภาษาอังกฤษ)")]
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

        public static BookingOwnerAddressDTO CreateFromModel(models.SAL.BookingOwnerAddress model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new BookingOwnerAddressDTO()
                {
                    Id = model.ID,
                    FromContactAddressID = model.FromContactAddressID,
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
                };

                if(model.ContactAddressType.Key == "0")
                {
                    var bookingID = DB.BookingOwners.Where(o => o.ID == model.BookingOwnerID).Select(o => o.BookingID).First();
                    var bookingProject = DB.Bookings.Include(o => o.Project).Where(o => o.ID == bookingID).First();
                    result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(bookingProject.Project);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public static BookingOwnerAddressDTO CreateFromContactModel(models.CTM.ContactAddress model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new BookingOwnerAddressDTO()
                {
                    FromContactAddressID = model.ID,
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
                    ForeignProvince = model.ForeignProvince
                };

                if(model.ContactAddressType.Key == "0")
                {
                    var addressProject = DB.ContactAddressProjects.Include(o => o.Project).Where(o => o.ContactAddressID == model.ID).FirstOrDefault();
                    result.Project = PRJ.ProjectDropdownDTO.CreateFromModel(addressProject.Project);
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
            var thaiID = await DB.Countries.Where(o => o.NameEN == "Thailand" || o.NameTH == "Thailand").Select(o => o.ID).FirstAsync();
            if (!string.IsNullOrEmpty(this.HouseNoTH))
            {
                //if (!this.HouseNoTH.CheckLang(false, true, true, false))
                //{
                //    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
                //    string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.HouseNoTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                //    var msg = errMsg.Message.Replace("[field]", desc);
                //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                //}
            }
            if (!string.IsNullOrEmpty(this.MooTH))
            {
                if (!this.MooTH.CheckAllLang(true, true, false, null, " "))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.MooTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(this.VillageTH))
            {
                if (!this.VillageTH.CheckAllLang(true, true, false, null, " "))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.VillageTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                if (string.IsNullOrEmpty(this.VillageEN))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.VillageEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (!this.VillageEN.CheckLang(false, true, true, false, null, " "))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.VillageEN)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                    string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.SoiTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                if (string.IsNullOrEmpty(this.SoiEN))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.SoiEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (!this.SoiEN.CheckLang(false, true, true, false, null, " "))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.SoiEN)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                    string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.RoadTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                if (string.IsNullOrEmpty(this.RoadEN))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.RoadEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (!this.RoadEN.CheckLang(false, true, true, false, null, " "))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.RoadEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }
            if (this.Country != null)
            {
                if (this.Country.Id == thaiID)
                {
                    if (!this.PostalCode.IsOnlyNumber())
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.PostalCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
                else
                {
                    if (!this.PostalCode.CheckLang(false, true, true, false, null, " "))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.PostalCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
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
                            string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.ForeignProvince)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (!string.IsNullOrEmpty(this.ForeignDistrict))
                    {
                        if (!this.ForeignDistrict.CheckAllLang(false, true, true, null, " "))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.ForeignDistrict)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (!string.IsNullOrEmpty(this.ForeignSubDistrict))
                    {
                        if (!this.ForeignSubDistrict.CheckAllLang(false, true, true, null, " "))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(BookingOwnerAddressDTO.ForeignSubDistrict)).GetCustomAttribute<DescriptionAttribute>().Description;
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

        public void ToModel(ref models.SAL.BookingOwnerAddress model)
        {
            model.FromContactAddressID = this.FromContactAddressID;
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
            model.ContactAddressTypeMasterCenterID = this.ContactAddressType?.Id;
            model.ForeignSubDistrict = this.ForeignSubDistrict;
            model.ForeignDistrict = this.ForeignDistrict;
            model.ForeignProvince = this.ForeignProvince;
        }

        public void ToContactModel(ref models.CTM.ContactAddress model)
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
            model.ContactAddressTypeMasterCenterID = this.ContactAddressType?.Id;
            model.ForeignSubDistrict = this.ForeignSubDistrict;
            model.ForeignDistrict = this.ForeignDistrict;
            model.ForeignProvince = this.ForeignProvince;
        }
    }
}
