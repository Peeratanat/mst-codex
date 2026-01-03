using Base.DTOs.SAL;
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
using static Database.Models.ExtensionAttributes;

namespace Base.DTOs.ADM
{
    /// <summary>
    /// ผู้ทำโอน
    /// Model: TransferOwner
    /// </summary>
    public class AdminTransferOwnerDTO : BaseDTOFromAdmin
    {
        /// <summary>
        /// ID ของ Transfer Owner
        /// </summary>
        [DeviceInformation("ID ของ Transfer Owner", "")]
        public Guid? Id { get; set; }
        /// <summary>
        /// ลำดับของผู้ทำโอน
        /// </summary>
        [DeviceInformation("ลำดับของผู้ทำโอน", "SAL.TransferOwner.Order")]
        public int? Order { get; set; }
        /// <summary>
        /// โอน
        /// </summary>
        [DeviceInformation("โอน", "SAL.TransferOwner.TransferID")]
        public TransferDropdownDTO Transfer { get; set; }
        /// <summary>
        /// มาจาก Contact (กรณีดึงข้อมูลมาจาก Contact)
        /// </summary>
        [DeviceInformation("มาจาก Contact (กรณีดึงข้อมูลมาจาก Contact)", "SAL.TransferOwner.FromContactID")]
        public Guid? FromContactID { get; set; }
        /// <summary>
        /// ผู้ทำโอนหลัก
        /// </summary>
        [DeviceInformation("ผู้ทำโอนหลัก", "SAL.TransferOwner.IsMainOwner")] 
        public bool IsMainOwner { get; set; }
        /// <summary>
        /// รหัสลูกค้า (มาจาก Contact)
        /// </summary>
        [DeviceInformation("รหัสลูกค้า (มาจาก Contact)", "SAL.TransferOwner.ContactNo")]
        public string ContactNo { get; set; }
        /// <summary>
        /// มอบอำนาจหรือไม่
        /// </summary>
        [DeviceInformation("มอบอำนาจหรือไม่", "SAL.TransferOwner.IsAssignAuthority")]
        public bool IsAssignAuthority { get; set; }
        /// <summary>
        /// มอบอำนาจโดยบริษัท
        /// </summary>
        [DeviceInformation("มอบอำนาจโดยบริษัท", "SAL.TransferOwner.IsAssignAuthorityByCompany")]
        public bool IsAssignAuthorityByCompany { get; set; }
        /// <summary>
        /// ชื่อผู้รับมอบอำนาจ
        /// </summary>
        [DeviceInformation("ชื่อผู้รับมอบอำนาจ", "SAL.TransferOwner.AuthorityName")]
        public string AuthorityName { get; set; }
        /// <summary>
        /// ประเภทของลูกค้า (บุคคลทั่วไป/นิติบุคคล)
        /// </summary>
        [DeviceInformation("ประเภทของลูกค้า (บุคคลทั่วไป/นิติบุคคล)", "SAL.TransferOwner.ContactTypeMasterCenterID")]
        public MST.MasterCenterDropdownDTO ContactType { get; set; }
        /// <summary>
        /// คำนำหน้าชื่อ (ภาษาไทย)
        /// </summary>
        [DeviceInformation("คำนำหน้าชื่อ (ภาษาไทย)", "SAL.TransferOwner.ContactTitleTHMasterCenterID")]
        public MST.MasterCenterDropdownDTO ContactTitleTH { get; set; }
        /// <summary>
        /// คำนำหน้าเพิ่มเติม (ภาษาไทย)
        /// </summary>
        [DeviceInformation("คำนำหน้าเพิ่มเติม (ภาษาไทย)", "SAL.TransferOwner.TitleExtTH")]
        public string TitleExtTH { get; set; }
        /// <summary>
        /// ชื่อจริง (ภาษาไทย)
        /// </summary>
        [DeviceInformation("ชื่อจริง (ภาษาไทย)", "SAL.TransferOwner.FirstNameTH")]
        public string FirstNameTH { get; set; }
        /// <summary>
        /// ชื่อกลาง (ภาษาไทย)
        /// </summary>
        [DeviceInformation("ชื่อกลาง (ภาษาไทย)", "SAL.TransferOwner.MiddleNameTH")]
        public string MiddleNameTH { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาไทย)
        /// </summary>
        [DeviceInformation("นามสกุล (ภาษาไทย)", "SAL.TransferOwner.LastNameTH")]
        public string LastNameTH { get; set; }

        /// <summary>
        /// หมายเลขบัตรประชาชน
        /// </summary>
        [DeviceInformation("หมายเลขบัตรประชาชน", "SAL.TransferOwner.CitizenIdentityNo")]
        public string CitizenIdentityNo { get; set; }
        /// <summary>
        /// วันเกิด
        /// </summary>
        [DeviceInformation("วันเกิด", "SAL.TransferOwner.BirthDate")]
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        [DeviceInformation("เบอร์โทรศัพท์", "SAL.TransferOwner.PhoneNumber")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// เบอร์มือถือ
        /// </summary>
        [DeviceInformation("เบอร์มือถือ", "SAL.TransferOwner.MobileNumber")]
        public string MobileNumber { get; set; }
        /// <summary>
        /// อีเมลล์
        /// </summary>
        [DeviceInformation("อีเมลล์", "SAL.TransferOwner.Email")]
        public string Email { get; set; }
        /// <summary>
        /// ชื่อผู้ติดต่อ
        /// </summary>
        [DeviceInformation("ชื่อผู้ติดต่อ", "SAL.TransferOwner.ContactFirstName")]
        public string ContactFirstName { get; set; }
        /// <summary>
        /// นามสกุลผู้ติดต่อ
        /// </summary>
        [DeviceInformation("นามสกุลผู้ติดต่อ", "SAL.TransferOwner.ContactLastname")]
        public string ContactLastname { get; set; }
        /// <summary>
        /// สัญชาติ
        /// </summary>
        [DeviceInformation("สัญชาติ", "SAL.TransferOwner.NationalMasterCenterID")]
        public MST.MasterCenterDropdownDTO National { get; set; }

        /// <summary>
        /// สถานะสมรส
        /// </summary>
        [DeviceInformation("สถานะสมรส", "SAL.TransferOwner.TransferMarriageStatusMasterCenterID")]
        public MST.MasterCenterDropdownDTO TransferMarriageStatus { get; set; }

        /// <summary>
        /// คำนำหน้าชื่อคู่สมรส
        /// </summary>
        [DeviceInformation("คำนำหน้าชื่อคู่สมรส", "SAL.TransferOwner.MarriageTitleTHMasterCenterID")]
        public MST.MasterCenterDropdownDTO MarriageTitleTH { get; set; }

        /// <summary>
        /// ชื่อคู่สมรส
        /// </summary>
        [DeviceInformation("ชื่อคู่สมรส", "SAL.TransferOwner.MarriageName")]
        public string MarriageName { get; set; }
        /// <summary>
        /// สัญชาติของคู่สมรส 
        /// </summary>
        [DeviceInformation("สัญชาติของคู่สมรส", "SAL.TransferOwner.MarriageNationalMasterCenterID")]
        public MST.MasterCenterDropdownDTO MarriageNational { get; set; }
        /// <summary>
        /// สัญชาติอื่นๆ ของคู่สมรส
        /// </summary>
        [DeviceInformation("สัญชาติอื่นๆ ของคู่สมรส", "SAL.TransferOwner.MarriageOtherNational")]
        public string MarriageOtherNational { get; set; }
        /// <summary>
        /// ชื่อบิดา-มารดา
        /// </summary>
        [DeviceInformation("ชื่อบิดา-มารดา", "SAL.TransferOwner.ParentName")]
        public string ParentName { get; set; }

        //ที่อยู่ตามทะเบียนบ้าน
        /// <summary>
        /// บ้านเลขที่ (ภาษาไทย)
        /// </summary>
        [DeviceInformation("บ้านเลขที่ (ภาษาไทย)", "SAL.TransferOwner.HouseNoTH")]
        public string HouseNoTH { get; set; }
        /// <summary>
        /// นามสกุลผู้ติดต่อ
        /// </summary>
        [DeviceInformation("นามสกุลผู้ติดต่อ", "SAL.TransferOwner.MooTH")]
        public string MooTH { get; set; }
        /// <summary>
        /// หมู่บ้าน/อาคาร (ภาษาไทย)
        /// </summary>
        [DeviceInformation("หมู่บ้าน/อาคาร (ภาษาไทย)", "SAL.TransferOwner.VillageTH")]
        public string VillageTH { get; set; }
        /// <summary>
        /// ซอย (ภาษาไทย)
        /// </summary>
        [DeviceInformation("ซอย (ภาษาไทย)", "SAL.TransferOwner.SoiTH")]
        public string SoiTH { get; set; }
        /// <summary>
        /// ถนน (ภาษาไทย)
        /// </summary>
        [DeviceInformation("ถนน (ภาษาไทย)", "SAL.TransferOwner.RoadTH")]
        public string RoadTH { get; set; }
        /// <summary>
        /// รหัสไปรษณีย์
        /// </summary>
        [DeviceInformation("รหัสไปรษณีย์", "SAL.TransferOwner.PostalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// ประเทศ
        /// Master/api/Countries
        /// </summary>
        [DeviceInformation("ประเทศ", "SAL.TransferOwner.CountryID")]
        public MST.CountryDTO Country { get; set; }
        /// <summary>
        /// จังหวัด
        /// Master/api/Provinces/DropdownList
        /// </summary>
        [DeviceInformation("จังหวัด", "SAL.TransferOwner.ProvinceID")]
        public MST.ProvinceListDTO Province { get; set; }
        /// <summary>
        /// เขต/อำเภอ
        /// Master/api/Districts/DropdownList
        /// </summary>
        [DeviceInformation("เขต/อำเภอ", "SAL.TransferOwner.DistrictID")]
        public MST.DistrictListDTO District { get; set; }
        /// <summary>
        /// แขวง/ตำบล
        /// Master/api/SubDistricts/DropdownList
        /// </summary>
        [DeviceInformation("แขวง/ตำบล", "SAL.TransferOwner.SubDistrictID")]
        public MST.SubDistrictListDTO SubDistrict { get; set; }

        /// <summary>
        /// จังหวัด (ต่างประเทศ)
        /// </summary>
        [DeviceInformation("จังหวัด (ต่างประเทศ)", "SAL.TransferOwner.ForeignProvince")]
        public string ForeignProvince { get; set; }
        /// <summary>
        /// อำเภอ (ต่างประเทศ)
        /// </summary>
        [DeviceInformation("อำเภอ (ต่างประเทศ)", "SAL.TransferOwner.ForeignDistrict")]
        public string ForeignDistrict { get; set; }
        /// <summary>
        /// ตำบล (ต่างประเทศ)
        /// </summary>
        [DeviceInformation("ตำบล (ต่างประเทศ)", "SAL.TransferOwner.ForeignSubDistrict")]
        public string ForeignSubDistrict { get; set; }

        /// <summary>
        /// มาจาก Project (กรณีดึงข้อมูลมาจาก Project)
        /// </summary>
        [DeviceInformation("มาจาก Project (กรณีดึงข้อมูลมาจาก Project)", "SAL.TransferOwner.FromProjectID")]
        public Guid? FromProjectID { get; set; }

        /// <summary>
        /// ลูกค้า VIP
        /// </summary>
        [DeviceInformation("ลูกค้า VIP", "SAL.TransferOwner.IsVIP")]
        public bool? IsVIP { get; set; }

        /// <summary>
        /// เบอร์โทรศัพท์ (ต่อ)
        /// </summary>
        [DeviceInformation("เบอร์โทรศัพท์ (ต่อ)", "SAL.TransferOwner.PhoneNumberExt")]
        public string PhoneNumberExt { get; set; }

        /// <summary>
        /// ชื่อเต็ม (ภาษาไทย)
        /// </summary>
        [DeviceInformation("ชื่อเต็ม (ภาษาไทย)", "SAL.TransferOwner.FullnameTH")]
        public string FullnameTH { get; set; }

        /// <summary>
        /// ชื่อเต็ม (ภาษาอังกฤษ)
        /// </summary>
        [DeviceInformation("ชื่อเต็ม (ภาษาอังกฤษ)", "SAL.TransferOwner.FullnameEN")]
        public string FullnameEN { get; set; }

        /// <summary>
        /// เลขประจำตัวผู้เสียภาษี
        /// </summary>
        [DeviceInformation("เลขประจำตัวผู้เสียภาษี", "SAL.TransferOwner.TaxID")]
        public string TaxID { get; set; }

        /// <summary>
        /// สัญชาติ ภาษาไทย
        /// </summary>
        [DeviceInformation("สัญชาติ ภาษาไทย", "SAL.TransferOwner.NationalTH")]
        public string NationalTH { get; set; }

        /// <summary>
        /// ประเทศ ภาษาไทย
        /// </summary>
        [DeviceInformation("ประเทศ ภาษาไทย", "SAL.TransferOwner.CountryTH")]
        public string CountryTH { get; set; }
        [DeviceInformation("Updated", "")]
        public DateTime? Updated { get; set; }
        [DeviceInformation("UpdatedNo", "")]
        public string UpdatedNo { get; set; }
        [DeviceInformation("UpdatedBy", "")]
        public string UpdatedBy { get; set; }
        [DeviceInformation("Created", "")]
        public DateTime? Created { get; set; }
        [DeviceInformation("CreatedNo", "")]
        public string CreatedNo { get; set; }
        [DeviceInformation("CreatedBy", "")]
        public string CreatedBy { get; set; }

        public async static Task<AdminTransferOwnerDTO> CreateFromModelAsync(
                models.SAL.TransferOwner model
                , models.SAL.AgreementOwner agreementOwner
            )
        {
            if (model != null)
            {

                var IsVIP = agreementOwner.IsVIP;

                var result = new AdminTransferOwnerDTO()
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

        public static AdminTransferOwnerDTO CreateFromModelList(models.SAL.TransferOwner model)
        {
            if (model != null)
            {
                var result = new AdminTransferOwnerDTO()
                {
                    Id = model.ID,
                    Order = model.Order,
                    ContactNo = model.FromContact?.ContactNo,
                    FullnameEN = model.FullnameEN,
                    FullnameTH = model.FullnameTH,
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public TransferOwnerDTO ToTransferOwnerDTO()
        {
            var result = new TransferOwnerDTO
            {
                Id = this.Id,
                Order = this.Order,
                Transfer = this.Transfer,
                FromContactID = this.FromContactID,
                IsMainOwner = this.IsMainOwner,
                ContactNo = this.ContactNo,
                IsAssignAuthority = this.IsAssignAuthority,
                IsAssignAuthorityByCompany = this.IsAssignAuthorityByCompany,
                AuthorityName = this.AuthorityName,
                ContactType = this.ContactType,
                ContactTitleTH = this.ContactTitleTH,
                TitleExtTH = this.TitleExtTH,
                FirstNameTH = this.FirstNameTH,
                MiddleNameTH = this.MiddleNameTH,
                LastNameTH = this.LastNameTH,
                CitizenIdentityNo = this.CitizenIdentityNo,
                BirthDate = this.BirthDate,
                PhoneNumber = this.PhoneNumber,
                MobileNumber = this.MobileNumber,
                Email = this.Email,
                ContactFirstName = this.ContactFirstName,
                ContactLastname = this.ContactLastname,
                National = this.National,
                TransferMarriageStatus = this.TransferMarriageStatus,
                MarriageTitleTH = this.MarriageTitleTH,
                MarriageName = this.MarriageName,
                MarriageNational = this.MarriageNational,
                MarriageOtherNational = this.MarriageOtherNational,
                ParentName = this.ParentName,
                HouseNoTH = this.HouseNoTH,
                MooTH = this.MooTH,
                VillageTH = this.VillageTH,
                SoiTH = this.SoiTH,
                RoadTH = this.RoadTH,
                PostalCode = this.PostalCode,
                Country = this.Country,
                Province = this.Province,
                District = this.District,
                SubDistrict = this.SubDistrict,
                ForeignProvince = this.ForeignProvince,
                ForeignDistrict = this.ForeignDistrict,
                ForeignSubDistrict = this.ForeignSubDistrict,
                FromProjectID = this.FromProjectID,
                IsVIP = this.IsVIP,
                PhoneNumberExt = this.PhoneNumberExt,
                FullnameTH = this.FullnameTH,
                FullnameEN = this.FullnameEN,
                TaxID = this.TaxID,
                NationalTH = this.NationalTH,
                CountryTH = this.CountryTH
            };
            return result;
        }
    }
}
