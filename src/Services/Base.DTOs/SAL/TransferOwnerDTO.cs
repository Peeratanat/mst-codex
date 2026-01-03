using Database.Models.MasterKeys;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// ผู้ทำโอน
    /// Model: TransferOwner
    /// </summary>
    public class TransferOwnerDTO
    {
        /// <summary>
        /// ID ของ Transfer Owner
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ลำดับของผู้ทำโอน
        /// </summary>
        public int? Order { get; set; }
        /// <summary>
        /// โอน
        /// </summary>
        public TransferDropdownDTO Transfer { get; set; }
        /// <summary>
        /// มาจาก Contact (กรณีดึงข้อมูลมาจาก Contact)
        /// </summary>
        public Guid? FromContactID { get; set; }
        /// <summary>
        /// ผู้ทำโอนหลัก
        /// </summary>
        [Description("ผู้ทำโอนหลัก")]
        public bool IsMainOwner { get; set; }
        /// <summary>
        /// รหัสลูกค้า (มาจาก Contact)
        /// </summary>
        [Description("รหัสลูกค้า")]
        public string ContactNo { get; set; }
        /// <summary>
        /// มอบอำนาจหรือไม่
        /// </summary>
        [Description("มอบอำนาจหรือไม่?")]
        public bool IsAssignAuthority { get; set; }
        /// <summary>
        /// มอบอำนาจโดยบริษัท
        /// </summary>
        [Description("มอบอำนาจโดยบริษัท")]
        public bool IsAssignAuthorityByCompany { get; set; }
        /// <summary>
        /// ชื่อผู้รับมอบอำนาจ
        /// </summary>
        [Description("ชื่อผู้รับมอบอำนาจ")]
        public string AuthorityName { get; set; }
        /// <summary>
        /// ประเภทของลูกค้า (บุคคลทั่วไป/นิติบุคคล)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactType
        /// </summary>
        [Description("ประเภทของลูกค้า")]
        public MST.MasterCenterDropdownDTO ContactType { get; set; }
        /// <summary>
        /// คำนำหน้าชื่อ (ภาษาไทย)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactTitleTH
        /// </summary>
        [Description("คำนำหน้าชื่อ (ภาษาไทย)")]
        public MST.MasterCenterDropdownDTO ContactTitleTH { get; set; }
        /// <summary>
        /// คำนำหน้าเพิ่มเติม (ภาษาไทย)
        /// </summary>
        [Description("คำนำหน้าเพิ่มเติม (ภาษาไทย)")]
        public string TitleExtTH { get; set; }
        /// <summary>
        /// ชื่อจริง (ภาษาไทย)
        /// </summary>
        [Description("ชื่อจริง (ภาษาไทย)")]
        public string FirstNameTH { get; set; }
        /// <summary>
        /// ชื่อกลาง (ภาษาไทย)
        /// </summary>
        [Description("ชื่อกลาง (ภาษาไทย)")]
        public string MiddleNameTH { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาไทย)
        /// </summary>
        [Description("นามสกุล (ภาษาไทย)")]
        public string LastNameTH { get; set; }

        /// <summary>
        /// หมายเลขบัตรประชาชน
        /// </summary>
        [Description("หมายเลขบัตรประชาชน")]
        public string CitizenIdentityNo { get; set; }
        /// <summary>
        /// วันเกิด
        /// </summary>
        [Description("วันเกิด")]
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        [Description("เบอร์โทรศัพท์")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// เบอร์มือถือ
        /// </summary>
        [Description("เบอร์มือถือ")]
        public string MobileNumber { get; set; }
        /// <summary>
        /// อีเมลล์
        /// </summary>
        [Description("อีเมลล์")]
        public string Email { get; set; }
        /// <summary>
        /// ชื่อผู้ติดต่อ
        /// </summary>
        [Description("ชื่อผู้ติดต่อ")]
        public string ContactFirstName { get; set; }
        /// <summary>
        /// นามสกุลผู้ติดต่อ
        /// </summary>
        [Description("นามสกุลผู้ติดต่อ")]
        public string ContactLastname { get; set; }
        /// <summary>
        /// สัญชาติ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=National
        /// </summary>
        [Description("สัญชาติ")]
        public MST.MasterCenterDropdownDTO National { get; set; }

        /// <summary>
        /// สถานะสมรส
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=TransferMarriageStatus
        /// </summary>
        [Description("สถานะสมรส")]
        public MST.MasterCenterDropdownDTO TransferMarriageStatus { get; set; }

        /// <summary>
        /// คำนำหน้าชื่อคู่สมรส
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=MarriageTitleTH
        /// </summary>
        [Description("คำนำหน้าชื่อคู่สมรส")]
        public MST.MasterCenterDropdownDTO MarriageTitleTH { get; set; }

        /// <summary>
        /// ชื่อคู่สมรส
        /// </summary>
        [Description("ชื่อคู่สมรส")]
        public string MarriageName { get; set; }
        /// <summary>
        /// สัญชาติของคู่สมรส
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=National
        /// </summary>
        [Description("สัญชาติของคู่สมรส")]
        public MST.MasterCenterDropdownDTO MarriageNational { get; set; }
        /// <summary>
        /// สัญชาติอื่นๆ ของคู่สมรส
        /// </summary>
        [Description("สัญชาติอื่นๆ ของคู่สมรส")]
        public string MarriageOtherNational { get; set; }
        /// <summary>
        /// ชื่อบิดา-มารดา
        /// </summary>
        [Description("ชื่อบิดา-มารดา")]
        public string ParentName { get; set; }

        //ที่อยู่ตามทะเบียนบ้าน
        /// <summary>
        /// บ้านเลขที่ (ภาษาไทย)
        /// </summary>
        [Description("บ้านเลขที่ (ภาษาไทย)")]
        public string HouseNoTH { get; set; }
        /// <summary>
        /// นามสกุลผู้ติดต่อ
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
        /// เขต/อำเภอ
        /// Master/api/Districts/DropdownList
        /// </summary>
        [Description("เขต/อำเภอ")]
        public MST.DistrictListDTO District { get; set; }
        /// <summary>
        /// แขวง/ตำบล
        /// Master/api/SubDistricts/DropdownList
        /// </summary>
        [Description("แขวง/ตำบล")]
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
        /// มาจาก Project (กรณีดึงข้อมูลมาจาก Project)
        /// </summary>
        public Guid? FromProjectID { get; set; }

        /// <summary>
        /// ลูกค้า VIP
        /// </summary>
        [Description("ลูกค้า VIP")]
        public bool? IsVIP { get; set; }

        /// <summary>
        /// เบอร์โทรศัพท์ (ต่อ)
        /// </summary>
        [Description("เบอร์โทรศัพท์ (ต่อ)")]
        public string PhoneNumberExt { get; set; }

        /// <summary>
        /// ชื่อเต็ม (ภาษาไทย)
        /// </summary>
        [Description("ชื่อเต็ม (ภาษาไทย)")]
        public string FullnameTH { get; set; }

        /// <summary>
        /// ชื่อเต็ม (ภาษาอังกฤษ)
        /// </summary>
        [Description("ชื่อเต็ม (ภาษาอังกฤษ)")]
        public string FullnameEN { get; set; }

        /// <summary>
        /// เลขประจำตัวผู้เสียภาษี
        /// </summary>
        [Description("เลขประจำตัวผู้เสียภาษี")]
        public string TaxID { get; set; }

        /// <summary>
        /// สัญชาติ ภาษาไทย
        /// </summary>
        [Description("สัญชาติ ภาษาไทย")]
        public string NationalTH { get; set; }

        /// <summary>
        /// ประเทศ ภาษาไทย
        /// </summary>
        [Description("ประเทศ ภาษาไทย")]
        public string CountryTH { get; set; }

        public DateTime? Updated { get; set; }
        public string UpdatedNo { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedNo { get; set; }
        public string CreatedBy { get; set; }

        public async static Task<TransferOwnerDTO> CreateFromModelAsync(
                models.SAL.TransferOwner model
                , models.SAL.AgreementOwner agreementOwner
            )
        {
            if (model != null)
            {

                var IsVIP = agreementOwner.IsVIP;

                var result = new TransferOwnerDTO()
                {
                    Id = model.ID,
                    Order = model.Order,
                    Transfer = TransferDropdownDTO.CreateFromModel(model.Transfer),
                    FromContactID = model.FromContactID,
                    IsMainOwner = model.Order == 1,
                    //ContactNo = model.ContactNo,
                    IsAssignAuthority = model.IsAssignAuthority,
                    IsAssignAuthorityByCompany = model.IsAssignAuthorityByCompany,
                    AuthorityName = model.AuthorityName,
                    ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
                    ContactTitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleExtTH = model.TitleExtTH,
                    FirstNameTH = model.FirstNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    LastNameTH = model.LastNameTH,
                    CitizenIdentityNo = model.CitizenIdentityNo,
                    BirthDate = model.BirthDate,
                    PhoneNumber = model.PhoneNumber,
                    MobileNumber = model.MobileNumber,
                    Email = model.Email,
                    ContactFirstName = model.ContactFirstName,
                    ContactLastname = model.ContactLastname,
                    National = MST.MasterCenterDropdownDTO.CreateFromModel(model.National),
                    MarriageName = model.MarriageName,
                    MarriageNational = MST.MasterCenterDropdownDTO.CreateFromModel(model.MarriageNational),
                    MarriageOtherNational = model.MarriageOtherNational,
                    ParentName = model.ParentName,
                    HouseNoTH = model.HouseNoTH,
                    MooTH = model.MooTH,
                    VillageTH = model.VillageTH,
                    SoiTH = model.SoiTH,
                    RoadTH = model.RoadTH,
                    PostalCode = model.PostalCode,
                    Country = MST.CountryDTO.CreateFromModel(model.Country),
                    Province = MST.ProvinceListDTO.CreateFromModel(model.Province),
                    District = MST.DistrictListDTO.CreateFromModel(model.District),
                    SubDistrict = MST.SubDistrictListDTO.CreateFromModel(model.SubDistrict),
                    ForeignProvince = model.ForeignProvince,
                    ForeignDistrict = model.ForeignDistrict,
                    ForeignSubDistrict = model.ForeignSubDistrict,
                    TransferMarriageStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.TransferMarriageStatus),
                    MarriageTitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.MarriageTitleTH),
                    IsVIP = IsVIP,
                    PhoneNumberExt = model.PhoneNumberExt,

                    TaxID = model.TaxID,

                    NationalTH = model.NationalTH,
                    CountryTH = model.CountryTH,

                    FullnameTH = model.FullnameTH,
                    FullnameEN = model.FullnameEN,

                    ContactNo = model.FromContact?.ContactNo,
                    FromProjectID = model.Transfer?.ProjectID,

                    Created = model.Created,
                    CreatedBy = model.CreatedBy?.DisplayName,
                    CreatedNo = model.CreatedBy?.EmployeeNo,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    UpdatedNo = model.UpdatedBy?.EmployeeNo

                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<TransferOwnerDTO> CreateFromAgreementOwnerModelAsync(models.SAL.AgreementOwner model, models.DatabaseContext DB)
        {
            if (model != null)
            {

                var agPhoneList = await DB.AgreementOwnerPhones
                        .Include(o => o.PhoneType)
                        .Where(o => o.AgreementOwnerID == model.ID).ToListAsync() ?? new();

                var PhoneNumber = string.Empty;
                var MobileNumber = string.Empty;
                var PhoneNumberExt = string.Empty;

                if (agPhoneList.Any())
                {
                    var modelPhone = agPhoneList.Find(o => o.IsMain == true) ?? new();
                    var modelMobile = agPhoneList.Find(o => o.PhoneType.Key == PhoneTypeKeys.Mobile) ?? new();
                    PhoneNumber = modelPhone.PhoneNumber;
                    MobileNumber = modelMobile.PhoneNumber;
                    PhoneNumberExt = modelPhone.PhoneNumberExt;
                }

                var result = new TransferOwnerDTO()
                {
                    Id = model.ID,
                    Order = model.Order,
                    //Transfer = TransferDTO.CreateFromModel(model.Transfer),
                    FromContactID = model.FromContactID,
                    IsMainOwner = model.IsMainOwner,
                    ContactNo = model.ContactNo,
                    IsAssignAuthority = false,
                    IsAssignAuthorityByCompany = true,
                    // IsAssignAuthorityByCompany = false,
                    AuthorityName = "",
                    ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
                    ContactTitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleExtTH = model.TitleExtTH,
                    FirstNameTH = model.FirstNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    LastNameTH = model.LastNameTH,
                    CitizenIdentityNo = model.CitizenIdentityNo,
                    BirthDate = model.BirthDate,
                    PhoneNumber = PhoneNumber,
                    MobileNumber = MobileNumber, //model.MobileNumber,
                    //Email = model.Email,
                    ContactFirstName = model.ContactFirstName,
                    ContactLastname = model.ContactLastname,
                    National = MST.MasterCenterDropdownDTO.CreateFromModel(model.National),
                    //MarriageName = model.MarriageName,
                    //MarriageNational = MST.MasterCenterDropdownDTO.CreateFromModel(model.MarriageNational),
                    //MarriageOtherNational = model.MarriageOtherNational,
                    ParentName = "",
                    //HouseNoTH = model.HouseNoTH,
                    //MooTH = model.MooTH,
                    //VillageTH = model.VillageTH,
                    //SoiTH = model.SoiTH,
                    //RoadTH = model.RoadTH,
                    //PostalCode = model.PostalCode,
                    //Country = MST.CountryDTO.CreateFromModel(model.Country),
                    //Province = MST.ProvinceDTO.CreateFromModel(model.Province),
                    //District = MST.DistrictDTO.CreateFromModel(model.District),
                    //SubDistrict = MST.SubDistrictDTO.CreateFromModel(model.SubDistrict),
                    //ForeignProvince = model.ForeignProvince,
                    //ForeignDistrict = model.ForeignDistrict,
                    //ForeignSubDistrict = model.ForeignSubDistrict,
                    IsVIP = model.IsVIP,
                    PhoneNumberExt = PhoneNumberExt,

                    TaxID = model.TaxID,

                    NationalTH = "",
                    CountryTH = "",

                    FullnameTH = model.FullnameTH,
                    FullnameEN = model.FullnameEN,

                    Created = null,
                    CreatedBy = "",
                    CreatedNo = "",
                    Updated = null,
                    UpdatedBy = "",
                    UpdatedNo = ""

                };

                if (model.ContactTypeMasterCenterID != null)
                    result.ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTypeMasterCenterID).FirstOrDefaultAsync());

                if (model.ContactTitleTHMasterCenterID != null)
                    result.ContactTitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTitleTHMasterCenterID).FirstOrDefaultAsync());

                var phones = await DB.ContactPhones.Include(o => o.PhoneType).Where(o => o.ContactID == model.FromContactID && o.PhoneType.Key == "0").FirstOrDefaultAsync();
                if (phones != null)
                    result.MobileNumber = phones.PhoneNumber;

                var emails = await DB.AgreementOwnerEmails
                        .Include(o => o.AgreementOwner)
                        .Where(o =>
                            o.AgreementOwner.ID == model.ID
                            && o.IsMain == true
                        ).FirstOrDefaultAsync();
                if (emails != null)
                    result.Email = emails.Email;

                //var cont = await DB.Contacts.Where(o => o.ID == model.FromContactID).FirstOrDefaultAsync();
                //if (cont != null)
                //    result.National = MST.MasterCenterDropdownDTO.CreateFromModel(cont.National);

                var addresses = await DB.ContactAddresses
                    .Include(o => o.ContactAddressType)
                    .Include(o => o.Country)
                    .Include(o => o.Province)
                    .Include(o => o.District)
                    .Include(o => o.SubDistrict)
                    .Where(o => o.ContactID == model.FromContactID && o.ContactAddressType.Key == "2").FirstOrDefaultAsync();

                if (addresses != null)
                {
                    result.HouseNoTH = addresses.HouseNoTH;
                    result.MooTH = addresses.MooTH;
                    result.VillageTH = addresses.VillageTH;
                    result.SoiTH = addresses.SoiTH;
                    result.RoadTH = addresses.RoadTH;
                    result.PostalCode = addresses.PostalCode;
                    result.Country = MST.CountryDTO.CreateFromModel(addresses.Country);
                    result.Province = MST.ProvinceListDTO.CreateFromModel(addresses.Province);
                    result.District = MST.DistrictListDTO.CreateFromModel(addresses.District);
                    result.SubDistrict = MST.SubDistrictListDTO.CreateFromModel(addresses.SubDistrict);
                    result.ForeignProvince = addresses.ForeignProvince;
                    result.ForeignDistrict = addresses.ForeignDistrict;
                    result.ForeignSubDistrict = addresses.ForeignSubDistrict;
                }

                var Agreement = await DB.Agreements
                    .Where(o => o.ID == model.AgreementID).FirstOrDefaultAsync();

                if (Agreement != null)
                {
                    #region Project
                    result.FromProjectID = Agreement.ProjectID;
                    #endregion

                    var config = await DB.AgreementConfigs
                        .Include(o => o.AttorneyNameTransfer)
                        .Where(o => o.ProjectID == Agreement.ProjectID).FirstOrDefaultAsync();

                    if (config != null)
                    {
                        result.AuthorityName = config.AttorneyNameTransfer?.Atty_FullName;
                    }

                }

                //var Transfer = await DB.Transfers.Include(o => o.Agreement).Where(o => o.ID == model.TransferID).FirstAsync();
                result.Transfer = new TransferDropdownDTO();

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(models.DatabaseContext DB)
        {
            ValidateException ex = new ValidateException();

            #region Transfer Owner

            if (string.IsNullOrEmpty(this.Email))
            {

                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.Email)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }



            //if (this.ContactType == null)
            //{
            //    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.ContactType)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            //else
            //{
            //    var legalID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactType" && o.Key == "1").Select(o => o.ID).FirstAsync();
            //    if (this.ContactType.Id == legalID)
            //    {
            //        if (string.IsNullOrEmpty(this.TaxID))
            //        {
            //            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //            string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.TaxID)).GetCustomAttribute<DescriptionAttribute>().Description;
            //            var msg = errMsg.Message.Replace("[field]", desc);
            //            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //        }
            //        else if (!this.TaxID.IsOnlyNumber())
            //        {
            //            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
            //            string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.TaxID)).GetCustomAttribute<DescriptionAttribute>().Description;
            //            var msg = errMsg.Message.Replace("[field]", desc);
            //            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //        }

            //        if (!string.IsNullOrEmpty(this.PhoneNumber))
            //        {
            //            if (!this.PhoneNumber.IsOnlyNumber())
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }
            //        if (!string.IsNullOrEmpty(this.PhoneNumberExt))
            //        {
            //            if (!this.PhoneNumberExt.IsOnlyNumber())
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.PhoneNumberExt)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }
            //        if (!string.IsNullOrEmpty(this.ContactFirstName))
            //        {
            //            if (!this.ContactFirstName.CheckAllLang(false, false, false, null, " "))
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.ContactFirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }
            //        if (!string.IsNullOrEmpty(this.ContactLastname))
            //        {
            //            if (!this.ContactFirstName.CheckAllLang(false, false, false, null, " "))
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.ContactFirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }

            //        if (string.IsNullOrEmpty(this.FirstNameTH))
            //        {
            //            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //            string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //            var msg = errMsg.Message.Replace("[field]", desc);
            //            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //        }
            //        else
            //        {
            //            if (!this.FirstNameTH.CheckLang(true, true, true, false))
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0017").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }

            //        if (string.IsNullOrEmpty(this.FirstNameEN))
            //        {
            //            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //            string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //            var msg = errMsg.Message.Replace("[field]", desc);
            //            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //        }
            //        else
            //        {
            //            if (!this.FirstNameEN.CheckLang(false, true, true, false))
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        var isCheckNation = false;
            //        #region Citizen
            //        if (string.IsNullOrEmpty(this.CitizenIdentityNo))
            //        {
            //            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //            string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.CitizenIdentityNo)).GetCustomAttribute<DescriptionAttribute>().Description;
            //            var msg = errMsg.Message.Replace("[field]", desc);
            //            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //        }
            //        else
            //        {
            //            if (this.National == null)
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.National)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

            //                isCheckNation = true;
            //            }
            //            else
            //            {
            //                var thaiID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == models.MasterKeys.MasterCenterGroupKeys.National && o.Key == models.MasterKeys.NationalKeys.Thai).Select(o => o.ID).FirstAsync();
            //                if (this.National.Id == thaiID)
            //                {
            //                    if (!this.CitizenIdentityNo.IsOnlyNumber())
            //                    {
            //                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0014").FirstAsync();
            //                        var msg = errMsg.Message;
            //                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //                    }
            //                    else if (this.CitizenIdentityNo.Length != 13)
            //                    {
            //                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0014").FirstAsync();
            //                        var msg = errMsg.Message;
            //                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //                    }
            //                }
            //                else
            //                {
            //                    if (!this.CitizenIdentityNo.CheckLang(false, true, false, false))
            //                    {
            //                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
            //                        string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.CitizenIdentityNo)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                        var msg = errMsg.Message.Replace("[field]", desc);
            //                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //                    }
            //                }
            //            }
            //        }
            //        #endregion

            //        #region TH
            //        if (this.ContactTitleTH == null)
            //        {
            //            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //            string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.ContactTitleTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //            var msg = errMsg.Message.Replace("[field]", desc);
            //            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //        }
            //        else
            //        {
            //            var externalTitleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH" && o.Key == "-1").Select(o => o.ID).FirstAsync();
            //            if (this.ContactTitleTH.Id == externalTitleID && string.IsNullOrEmpty(this.TitleExtTH))
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.TitleExtTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }

            //        if (string.IsNullOrEmpty(this.FirstNameTH))
            //        {
            //            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //            string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //            var msg = errMsg.Message.Replace("[field]", desc);
            //            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //        }
            //        else
            //        {
            //            if (!this.FirstNameTH.CheckAllLang(false, false, false, null, " "))
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }

            //        if (string.IsNullOrEmpty(this.LastNameTH))
            //        {
            //            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //            string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.LastNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //            var msg = errMsg.Message.Replace("[field]", desc);
            //            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //        }
            //        else
            //        {
            //            if (!this.LastNameTH.CheckAllLang(false, false, false, null, " "))
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.LastNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }

            //        if (!string.IsNullOrEmpty(this.MiddleNameTH))
            //        {
            //            if (!this.MiddleNameTH.CheckAllLang(false, false, false, null, " "))
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.MiddleNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }

            //        if (!string.IsNullOrEmpty(this.Nickname))
            //        {
            //            if (!this.Nickname.CheckAllLang(false, false, false, null, " "))
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.Nickname)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }
            //        #endregion

            //        #region EN
            //        if (this.ContactTitleEN != null)
            //        {
            //            var externalTitleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == models.MasterKeys.MasterCenterGroupKeys.ContactTitleEN && o.Key == "-1").Select(o => o.ID).FirstAsync();
            //            if (this.ContactTitleEN.Id == externalTitleID && string.IsNullOrEmpty(this.TitleExtEN))
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.TitleExtEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }

            //        if (!string.IsNullOrEmpty(this.FirstNameEN))
            //        {
            //            if (!this.FirstNameEN.CheckLang(false, false, false, false, null, " "))
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }

            //        if (!string.IsNullOrEmpty(this.LastNameEN))
            //        {
            //            if (!this.LastNameEN.CheckLang(false, false, false, false, null, " "))
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.LastNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }

            //        if (!string.IsNullOrEmpty(this.MiddleNameEN))
            //        {
            //            if (!this.MiddleNameEN.CheckLang(false, false, false, false, null, " "))
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.MiddleNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }
            //        #endregion

            //        if (!isCheckNation)
            //        {
            //            if (this.National == null)
            //            {
            //                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //                string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.National)).GetCustomAttribute<DescriptionAttribute>().Description;
            //                var msg = errMsg.Message.Replace("[field]", desc);
            //                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //            }
            //        }

            //        if (this.Gender == null)
            //        {
            //            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //            string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.Gender)).GetCustomAttribute<DescriptionAttribute>().Description;
            //            var msg = errMsg.Message.Replace("[field]", desc);
            //            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //        }
            //        if (this.BirthDate == null)
            //        {
            //            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //            string desc = this.GetType().GetProperty(nameof(TransferOwnerDTO.BirthDate)).GetCustomAttribute<DescriptionAttribute>().Description;
            //            var msg = errMsg.Message.Replace("[field]", desc);
            //            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //        }
            //    }
            //}
            #endregion

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref models.SAL.TransferOwner model)
        {
            //model.Id = this.Id;
            model.Order = this.Order ?? 0;

            //model.Transfer = this.Transfer;
            if (this.Transfer != null)
            {
                model.TransferID = this.Transfer?.Id ?? new Guid();
            }

            model.FromContactID = this.FromContactID;
            //model.IsMainOwner = this.IsMainOwner;
            //model.ContactNo = this.ContactNo;
            model.IsAssignAuthority = this.IsAssignAuthority;
            model.IsAssignAuthorityByCompany = this.IsAssignAuthorityByCompany;
            model.AuthorityName = this.AuthorityName;

            //model.ContactType = this.ContactType;
            model.ContactTypeMasterCenterID = this.ContactType?.Id;

            //model.ContactTitleTH = this.ContactTitleTH;
            model.ContactTitleTHMasterCenterID = this.ContactTitleTH?.Id;

            model.TitleExtTH = this.TitleExtTH;
            model.FirstNameTH = this.FirstNameTH;
            model.MiddleNameTH = this.MiddleNameTH;
            model.LastNameTH = this.LastNameTH;
            model.CitizenIdentityNo = this.CitizenIdentityNo;
            model.BirthDate = this.BirthDate;
            model.PhoneNumber = this.PhoneNumber;
            model.MobileNumber = this.MobileNumber;
            model.Email = this.Email;
            model.ContactFirstName = this.ContactFirstName;
            model.ContactLastname = this.ContactLastname;

            //model.National = this.National;
            model.NationalMasterCenterID = this.National?.Id;

            model.MarriageName = this.MarriageName;

            //model.MarriageNational = this.MarriageNational;
            model.MarriageNationalMasterCenterID = this.MarriageNational?.Id;

            model.MarriageOtherNational = this.MarriageOtherNational;
            model.ParentName = this.ParentName;
            model.HouseNoTH = this.HouseNoTH;
            model.MooTH = this.MooTH;
            model.VillageTH = this.VillageTH;
            model.SoiTH = this.SoiTH;
            model.RoadTH = this.RoadTH;
            model.PostalCode = this.PostalCode;

            //model.Country = this.Country;
            model.CountryID = this.Country?.Id;

            //model.Province = this.Province;
            model.ProvinceID = this.Province?.Id;

            //model.District = this.District;
            model.DistrictID = this.District?.Id;

            //model.SubDistrict = this.SubDistrict;
            model.SubDistrictID = this.SubDistrict?.Id;

            model.ForeignProvince = this.ForeignProvince;
            model.ForeignDistrict = this.ForeignDistrict;
            model.ForeignSubDistrict = this.ForeignSubDistrict;

            //model.MarriageStatus = this.MarriageStatus;
            model.TransferMarriageStatusMasterCenterID = this.TransferMarriageStatus?.Id;
            //model.MarriageTitleTH = this.MarriageTitleTH;
            model.MarriageTitleTHMasterCenterID = this.MarriageTitleTH?.Id;

            //model.IsVIP = this.IsVIP;
            model.PhoneNumberExt = this.PhoneNumberExt;

            model.TaxID = this.TaxID;

            #region Clear Data

            if (this.ContactType?.Key == ContactTypeKeys.Legal)
            {
                model.ContactTitleTHMasterCenterID = null;
                model.MiddleNameTH = null;
                model.LastNameTH = null;
                model.CitizenIdentityNo = null;
                model.TitleExtTH = null;
            }

            if (this.ContactType?.Key == ContactTypeKeys.Individual)
            {
                model.TaxID = null;
            }

            if (this.ContactTitleTH?.Key != ContactTitleTHKeys.Other)
            {
                model.TitleExtTH = null;
            }

            if (
                    this.TransferMarriageStatus?.Key == TransferMarriageStatusKeys.Single
                    || this.TransferMarriageStatus?.Key == TransferMarriageStatusKeys.Divorce
                )
            {
                model.MarriageTitleTHMasterCenterID = null;
                model.MarriageName = null;
                model.MarriageNationalMasterCenterID = null;
            }


            if (this.Country?.Code == "TH")
            {
                model.CountryTH = null;

                model.ForeignProvince = null;
                model.ForeignDistrict = null;
                model.ForeignSubDistrict = null;
            }
            else
            {
                model.ProvinceID = null;
                model.DistrictID = null;
                model.SubDistrictID = null;
            }

            if (this.National?.Key == "T03")
            {
                model.NationalTH = null;
            }

            if (this.MarriageNational?.Key == "T03")
            {
                model.MarriageOtherNational = null;
            }

            model.NationalTH = this.NationalTH;
            model.CountryTH = this.CountryTH;

            model.FullnameTH = this.FullnameTH;
            model.FullnameEN = this.FullnameEN;

            #endregion

        }

        public void ToContactModel(ref models.CTM.Contact model)
        {
            //model.Id = this.Id;
            //model.Order = this.Order;
            //model.Transfer = this.Transfer;
            //model.FromContactID = this.FromContactID;
            //model.IsMainOwner = this.IsMainOwner;
            model.ContactNo = this.ContactNo;
            //model.IsAssignAuthority = this.IsAssignAuthority;
            //model.IsAssignAuthorityByCompany = this.IsAssignAuthorityByCompany;
            //model.AuthorityName = this.AuthorityName;
            //model.ContactType = this.ContactType;
            //model.ContactTitleTH = this.ContactTitleTH;
            model.TitleExtTH = this.TitleExtTH;
            model.FirstNameTH = this.FirstNameTH;
            model.MiddleNameTH = this.MiddleNameTH;
            model.LastNameTH = this.LastNameTH;
            model.CitizenIdentityNo = this.CitizenIdentityNo;
            model.BirthDate = this.BirthDate;
            model.PhoneNumber = this.PhoneNumber;
            //model.MobileNumber = this.MobileNumber;
            //model.Email = this.Email;
            model.ContactFirstName = this.ContactFirstName;
            model.ContactLastname = this.ContactLastname;
            //model.National = this.National;
            model.MarriageName = this.MarriageName;
            //model.MarriageNational = this.MarriageNational;
            model.MarriageOtherNational = this.MarriageOtherNational;
            // model.ParentName = this.ParentName;
            //model.HouseNoTH = this.HouseNoTH;
            //model.MooTH = this.MooTH;
            //model.VillageTH = this.VillageTH;
            //model.SoiTH = this.SoiTH;
            //model.RoadTH = this.RoadTH;
            //model.PostalCode = this.PostalCode;
            //model.Country = this.Country;
            //model.Province = this.Province;
            //model.District = this.District;
            //model.SubDistrict = this.SubDistrict;
            //model.ForeignProvince = this.ForeignProvince;
            //model.ForeignDistrict = this.ForeignDistrict;
            //model.ForeignSubDistrict = this.ForeignSubDistrict;
        }
    }
}
