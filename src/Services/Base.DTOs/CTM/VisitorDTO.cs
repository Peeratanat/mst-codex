using Base.DTOs.USR;
using Database.Models.MasterKeys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class VisitorDTO
    {
        /// <summary>
        /// ID ของ Visitor
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ข้อมูลของ Contact
        /// </summary>
        public ContactListDTO Contact { get; set; }
        /// <summary>
        /// สถานะลูกค้า
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO ContactStatus { get; set; }
        /// <summary>
        /// เลขที่รับ
        /// </summary>
        public string ReceiveNumber { get; set; }
        /// <summary>
        /// ข้อมูลโครงการ
        /// project/api/Projects/DropdownList
        /// </summary>
        public PRJ.ProjectDTO Project { get; set; }
        /// <summary>
        /// วันที่เข้าโครงการ
        /// </summary>
        public DateTime? VisitDateIn { get; set; }
        /// <summary>
        /// วันที่ออกโครงการ
        /// </summary>
        public DateTime? VisitDateOut { get; set; }
        /// <summary>
        /// เดินทางโดย
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=VisitBy
        /// </summary>
        public MST.MasterCenterDropdownDTO VisitBy { get; set; }
        /// <summary>
        /// ประเภทรถยนต์
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=Vehicle
        /// </summary>
        public MST.MasterCenterDropdownDTO Vehicle { get; set; }
        /// <summary>
        /// รายละเอียดการเดินทางเพิ่มเติม
        /// </summary>
        public string VehicleDescription { get; set; }
        /// <summary>
        /// สถานะ Walk
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=WalkStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO WalkStatus { get; set; }
        /// <summary>
        /// ผู้ดูแล
        /// </summary>
        public USR.UserListDTO Owner { get; set; }
        /// <summary>
        /// สถานะต้อนรับลูกค้า
        /// </summary>
        public bool? IsWelcome { get; set; }
        /// <summary>
        /// คำนำหน้า (ภาษาไทย)
        /// </summary>
        public string TitleTH { get; set; }
        /// <summary>
        /// ชื่อจริง (ภาษาไทย)
        /// </summary>
        public string FirstNameTH { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาไทย)
        /// </summary>
        public string LastNameTH { get; set; }
        /// <summary>
        /// คำนำหน้า (ภาษาอังกฤษ)
        /// </summary>
        public string TitleEN { get; set; }
        /// <summary>
        /// ชื่อจริง (ภาษาอังกฤษ)
        /// </summary>
        public string FirstNameEN { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาอังกฤษ)
        /// </summary>
        public string LastNameEN { get; set; }
        /// <summary>
        /// เพศ
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// วันเกิด
        /// </summary>
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// กรุ๊ปเลือด
        /// </summary>
        public string BloodType { get; set; }
        /// <summary>
        /// กรุ๊ปเลือด
        /// </summary>
        public string HouseNo { get; set; }
        /// <summary>
        /// หมู่ที่
        /// </summary>
        public string Moo { get; set; }
        /// <summary>
        /// หมู่บ้าน/อาคาร
        /// </summary>
        public string Village { get; set; }
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
        /// ประเทศ
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// จังหวัด
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// เขต/อำเภอ
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// แขวง/ตำบล
        /// </summary>
        public string SubDistrict { get; set; }
        /// <summary>
        /// สัญชาติ
        /// </summary>
        public string National { get; set; }
        /// <summary>
        /// Issue
        /// </summary>
        public string Issue { get; set; }
        /// <summary>
        /// วัน Issue
        /// </summary>
        public DateTime? IssueDate { get; set; }
        /// <summary>
        /// วันหมดอายุ Issue
        /// </summary>
        public DateTime? IssueExpireDate { get; set; }
        /// <summary>
        /// เบอร์โทร
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// เป็น contact หรือไม่
        /// </summary>
        public bool? IsContact { get; set; }
        /// <summary>
        /// สถานะบันทึก Opportunity (มี/ไม่มี)
        /// </summary>
        public bool? IsSavedOpportunity { get; set; }
        /// <summary>
        /// ที่อยู่ที่ติดต่อได้ของ Contact
        /// </summary>
        public List<ContactAddressDTO> ContactAddresses { get; set; }
        /// <summary>
        /// ที่อยู่บัตรประชาชนของ Contact
        /// </summary>
        public ContactAddressDTO CitizenAddress { get; set; }
        /// <summary>
        /// ที่อยู่ทะเบียนบ้านของ Contact
        /// </summary>
        public ContactAddressDTO HomeAddress { get; set; }
        /// <summary>
        /// ที่อยู่ที่ทำงานของ Contact
        /// </summary>
        public ContactAddressDTO WorkAddress { get; set; }
        /// <summary>
        /// อีเมลล์ของ Contact
        /// </summary>
        public ContactEmailDTO ContactEmail { get; set; }
        /// <summary>
        /// ข้อมูลไฟล์แนบ
        /// </summary>
        public FileDTO IDCardImage { get; set; }
        /// <summary>
        /// เลขทะเบียนรถ
        /// </summary>
        public string VehicleRegistrationNo { get; set; }
        /// <summary>
        /// สีรถ
        /// </summary>
        public string VehicleColor { get; set; }
        /// <summary>
        /// ยี่ห้อรถ
        /// </summary>
        public string VehicleBrand { get; set; }
        /// <summary>
        /// เลขที่บัตรประชาชน
        /// </summary>
        public string CitizenIdentityNo { get; set; }

        public async static Task<VisitorDTO> CreateFromModelAsync(models.CTM.Visitor model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                VisitorDTO result = new VisitorDTO()
                {
                    Id = model.ID,
                    ReceiveNumber = model.VisitorRunning,
                    VisitBy = MST.MasterCenterDropdownDTO.CreateFromModel(model.VisitBy),
                    Vehicle = MST.MasterCenterDropdownDTO.CreateFromModel(model.Vehicle),
                    WalkStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.WalkStatus),
                    VehicleDescription = model.VehicleDescription,
                    VisitDateIn = model.VisitDateIn,
                    VisitDateOut = model.VisitDateOut,
                    IsWelcome = model.IsWelcome,
                    ContactStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactStatus),
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    TitleTH = model.TitleTH,
                    FirstNameTH = model.FirstNameTH,
                    LastNameTH = model.LastNameTH,
                    TitleEN = model.TitleEN,
                    FirstNameEN = model.FirstNameEN,
                    LastNameEN = model.LastNameEN,
                    Gender = model.Gender,
                    BirthDate = model.BirthDate,
                    BloodType = model.BloodType,
                    HouseNo = model.HouseNo,
                    Moo = model.Moo,
                    Village = model.Village,
                    Soi = model.Soi,
                    Road = model.Road,
                    PostalCode = model.PostalCode,
                    Country = model.Country,
                    Province = model.Province,
                    District = model.District,
                    SubDistrict = model.SubDistrict,
                    National = model.National,
                    Issue = model.Issue,
                    IssueDate = model.IssueDate,
                    IssueExpireDate = model.IssueExpireDate,
                    PhoneNumber = model.PhoneNumber,
                    VehicleRegistrationNo = model.VehicleRegistrationNo,
                    VehicleColor = model.VehicleColor,
                    VehicleBrand = model.VehicleBrand,
                    Owner = UserListDTO.CreateFromModel(model.Owner),
                    CitizenIdentityNo = model.CitizenIdentityNo
                };

                if (model.ContactID != null)
                {
                    result.IsContact = true;

                    var contactModel = await DB.Contacts.Where(o => o.ID == model.ContactID).FirstOrDefaultAsync();
                    result.Contact = ContactListDTO.CreateFromModel(contactModel, DB);

                    if (result.Contact != null && result.PhoneNumber == null)
                    {
                        result.PhoneNumber = result.Contact.PhoneNumber;
                    }

                    var isOpportunity = await DB.Opportunities.Where(o => o.ContactID == model.ContactID && o.ProjectID == model.ProjectID).AnyAsync();
                    if (isOpportunity)
                    {
                        result.IsSavedOpportunity = true;
                    }
                    else
                    {
                        result.IsSavedOpportunity = false;
                    }

                    var addressMaster = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType).ToListAsync();
                    var contactMasterID = addressMaster.Find(o => o.Key == ContactAddressTypeKeys.Contact)?.ID;
                    var citizenMasterID = addressMaster.Find(o => o.Key == ContactAddressTypeKeys.Citizen)?.ID;
                    var workMasterID = addressMaster.Find(o => o.Key == ContactAddressTypeKeys.Home)?.ID;
                    var homeMasterID = addressMaster.Find(o => o.Key == ContactAddressTypeKeys.Work)?.ID;

                    var contactAddressModel = await DB.ContactAddresses
                        .Include(o => o.Country)
                        .Include(o => o.Province)
                        .Include(o => o.District)
                        .Include(o => o.SubDistrict)
                        .Include(o => o.ContactAddressType)
                        .Where(c => c.ContactID == model.ContactID && c.ContactAddressTypeMasterCenterID == contactMasterID)
                        .ToListAsync();
                    result.ContactAddresses = contactAddressModel.Select(o => ContactAddressDTO.CreateFromModelAsync(o, DB)).Select(x => x.Result).ToList();

                    var citizenModel = await DB.ContactAddresses
                        .Include(o => o.Country)
                        .Include(o => o.Province)
                        .Include(o => o.District)
                        .Include(o => o.SubDistrict)
                        .Include(o => o.ContactAddressType)
                        .Where(c => c.ContactID == model.ContactID && c.ContactAddressTypeMasterCenterID == citizenMasterID)
                        .FirstOrDefaultAsync();
                    result.CitizenAddress = await ContactAddressDTO.CreateFromModelAsync(citizenModel, DB);

                    var workModel = await DB.ContactAddresses
                        .Include(o => o.Country)
                        .Include(o => o.Province)
                        .Include(o => o.District)
                        .Include(o => o.SubDistrict)
                        .Include(o => o.ContactAddressType)
                    .Where(c => c.ContactID == model.ContactID && c.ContactAddressTypeMasterCenterID == workMasterID)
                    .FirstOrDefaultAsync();
                    result.WorkAddress = await ContactAddressDTO.CreateFromModelAsync(citizenModel, DB);

                    var homeModel = await DB.ContactAddresses
                        .Include(o => o.Country)
                        .Include(o => o.Province)
                        .Include(o => o.District)
                        .Include(o => o.SubDistrict)
                        .Include(o => o.ContactAddressType)
                    .Where(c => c.ContactID == model.ContactID && c.ContactAddressTypeMasterCenterID == homeMasterID)
                    .FirstOrDefaultAsync();
                    result.HomeAddress = await ContactAddressDTO.CreateFromModelAsync(citizenModel, DB);

                    result.ContactEmail = await DB.ContactEmails
                    .Where(e => e.ContactID == model.ContactID && e.IsMain == true)
                    .Select(x => new ContactEmailDTO
                    {
                        Id = x.ID,
                        Email = x.Email,
                        IsMain = x.IsMain
                    }).FirstOrDefaultAsync();
                }
                else
                {
                    result.IsContact = false;
                }
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
