using Base.DTOs.CTM;
using Database.Models.CTM;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
    /// ผู้ทำสัญญา
    /// Model: AgreementOwner
    /// </summary>
    public class AgreementOwnerDTO
    {
        /// <summary>
        /// ID ของ Agreement Owner
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ลำดับของผู้ทำสัญญา
        /// </summary>
        public int? Order { get; set; }
        /// <summary>
        /// สัญญา
        /// </summary>
        public AgreementDropdownDTO Agreement { get; set; }
        /// <summary>
        /// มาจาก Contact (กรณีดึงข้อมูลมาจาก Contact)
        /// </summary>
        public Guid? FromContactID { get; set; }
        /// <summary>
        /// ผู้ทำสัญญาหลัก
        /// </summary>
        [Description("ผู้ทำสัญญาหลัก")]
        public bool IsMainOwner { get; set; }
        /// <summary>
        /// รหัสลูกค้า (มาจาก Contact)
        /// </summary>
        [Description("รหัสลูกค้า")]
        public string ContactNo { get; set; }
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
        /// ชื่อเล่น (ภาษาไทย)
        /// </summary>
        [Description("ชื่อเล่น (ภาษาไทย)")]
        public string Nickname { get; set; }
        /// <summary>
        /// คำนำหน้าชื่อ (ภาษาอังกฤษ)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactTitleEN
        /// </summary>
        [Description("คำนำหน้าชื่อ (ภาษาอังกฤษ)")]
        public MST.MasterCenterDropdownDTO ContactTitleEN { get; set; }
        /// <summary>
        /// คำนำหน้าเพิ่มเติม (ภาษาอังกฤษ)
        /// </summary>
        [Description("คำนำหน้าเพิ่มเติม (ภาษาอังกฤษ)")]
        public string TitleExtEN { get; set; }
        /// <summary>
        /// ชื่อจริง (ภาษาอังกฤษ)
        /// </summary>
        [Description("ชื่อจริง (ภาษาอังกฤษ)")]
        public string FirstNameEN { get; set; }
        /// <summary>
        /// ชื่อกลาง (ภาษาอังกฤษ)
        /// </summary>
        [Description("ชื่อกลาง (ภาษาอังกฤษ)")]
        public string MiddleNameEN { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาอังกฤษ)
        /// </summary>
        [Description("นามสกุล (ภาษาอังกฤษ)")]
        public string LastNameEN { get; set; }
        /// <summary>
        /// หมายเลขบัตรประชาชน
        /// </summary>
        [Description("หมายเลขบัตรประชาชน")]
        public string CitizenIdentityNo { get; set; }
        /// <summary>
        /// วันหมดอายุบัตรประชาชน
        /// </summary>
        [Description("วันหมดอายุบัตรประชาชน")]
        public DateTime? CitizenExpireDate { get; set; }
        /// <summary>
        /// สัญชาติ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=National
        /// </summary>
        [Description("สัญชาติ")]
        public MST.MasterCenterDropdownDTO National { get; set; }
        /// <summary>
        /// เพศ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=Gender
        /// </summary>
        [Description("เพศ")]
        public MST.MasterCenterDropdownDTO Gender { get; set; }
        /// <summary>
        /// เลขประจำตัวผู้เสียภาษี
        /// </summary>
        [Description("เลขประจำตัวผู้เสียภาษี")]
        public string TaxID { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์ (นิติบุคคล)
        /// </summary>
        [Description("เบอร์โทรศัพท์ (นิติบุคคล)")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// เบอร์ต่อ (นิติบุคคล)
        /// </summary>
        [Description("เบอร์ต่อ (นิติบุคคล)")]
        public string PhoneNumberExt { get; set; }
        /// <summary>
        /// ชื่อผู้ติดต่อ (นิติบุคคล)
        /// </summary>
        [Description("ชื่อผู้ติดต่อ (นิติบุคคล)")]
        public string ContactFirstName { get; set; }
        /// <summary>
        /// นามสกุลผู้ติดต่อ (นิติบุคคล)
        /// </summary>
        [Description("นามสกุลผู้ติดต่อ (นิติบุคคล)")]
        public string ContactLastname { get; set; }
        /// <summary>
        /// WeChat ID
        /// </summary>
        [Description("WeChat")]
        public string WeChat { get; set; }
        /// <summary>
        /// WhatsApp ID
        /// </summary>
        [Description("WhatsApp")]
        public string WhatsApp { get; set; }
        /// <summary>
        /// Line ID
        /// </summary>
        [Description("Line ID")]
        public string LineID { get; set; }
        /// <summary>
        /// วันเกิด
        /// </summary>
        [Description("วันเกิด")]
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// ลูกค้า VIP
        /// </summary>
        [Description("ลูกค้า VIP")]
        public bool? IsVIP { get; set; }
        /// <summary>
        /// เป็นคนไทยหรือไม่
        /// </summary>
        [Description("เป็นคนไทยหรือไม่")]
        public bool? IsThaiNationality { get; set; }

        [Description("Update ล่าสุด")]
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// เบอร์โทรศัพท์ของผู้ทำสัญญา
        /// </summary>
        [Description("เบอร์โทรศัพท์ของผู้ทำสัญญา")]
        public List<AgreementOwnerPhoneDTO> AgreementOwnerPhones { get; set; }
        /// <summary>
        /// อีเมลของผู้ทำสัญญา
        /// </summary>
        [Description("อีเมลของผู้ทำสัญญา")]
        public List<AgreementOwnerEmailDTO> AgreementOwnerEmails { get; set; }
        /// <summary>
        /// ที่อยู่ของผู้ทำสัญญา (ติดต่อได้/บัตรประชาชน)
        /// </summary>
        [Description("ที่อยู่ของผู้ทำสัญญา")]
        public List<AgreementOwnerAddressDTO> AgreementOwnerAddresses { get; set; }

        /// <summary>
        /// true = ผู้เพิ่มชื่อ,ผู้รับโอนสิทธิ์ / false = ผู้สละชื่อ,ผู้โอนสิทธิ์
        /// </summary>
        [Description("ประเภทผู้ทำสัญญาเข้าหรือออก (ตอนตั้งเรื่องเปลี่ยนแปลงชื่อ)")]
        public bool? ChangeAgreementOwnerInType { get; set; }

        /// <summary>
        /// true = เพิ่มชื่อยังแบบยังไม่ได้บันทึก
        /// </summary>
        [Description("เพิ่มชื่อยังแบบยังไม่ได้บันทึก")]
        public bool? IsNew { get; set; }

        /// <summary>
        /// เลือกคนที่จะโอนสิทธิ์
        /// </summary>
        [Description("เลือกคนที่จะโอนสิทธิ์")]
        public bool? IsSelected { get; set; }

        /// <summary>
        /// คำนำหน้าชื่อ (ภาษาไทย)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactTitleTH
        /// </summary>
        [Description("คำนำหน้าชื่อ (ภาษาไทย)")]
        public MST.MasterCenterDropdownDTO TitleTH { get; set; }

        /// <summary>
        /// คำนำหน้าชื่อ (ภาษาอังกฤษ)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactTitleEN
        /// </summary>
        [Description("คำนำหน้าชื่อ (ภาษาอังกฤษ)")]
        public MST.MasterCenterDropdownDTO TitleEN { get; set; }

        /// <summary>
        /// เบอร์โทรศัพท์ของผู้จอง
        /// </summary>
        [Description("เบอร์โทรศัพท์")]
        public List<AgreementOwnerPhoneDTO> ContactPhones { get; set; }

        /// <summary>
        /// อีเมลของผู้จอง
        /// </summary>
        [Description("อีเมล")]
        public List<AgreementOwnerEmailDTO> ContactEmails { get; set; }

        /// <summary>
        /// สละชื่อ
        /// </summary>
        [Description("สละชื่อ")]
        public bool IsCancelledOwner { get; set; }

        /// <summary>
        /// ชื่อเต็มภาษาไทย
        /// </summary>
        [Description("Full Name Thai")]
        public string FullnameTH { get; set; }

        /// <summary>
        /// ชื่อเต็มภาษาอังกฤษ
        /// </summary>
        [Description("Full Name Eng")]
        public string FullnameEN { get; set; }

        /// <summary>
		/// ลูกค้า IsVVIP
		/// </summary>
		public bool IsVVIP { get; set; }

        /// <summary>
        /// VVIPExcomApprover
        /// </summary>
        public string VVIPExcomApprover { get; set; }


        public async static Task<AgreementOwnerDTO> CreateFromModelAsync(models.SAL.AgreementOwner model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new AgreementOwnerDTO()
                {
                    Id = model.ID,
                    Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),
                    Order = model.Order,
                    FromContactID = model.FromContactID,
                    IsMainOwner = model.IsMainOwner,
                    ContactNo = model.ContactNo,
                    ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
                    ContactTitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleExtTH = model.TitleExtTH,
                    FirstNameTH = model.FirstNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    LastNameTH = model.LastNameTH,
                    Nickname = model.Nickname,
                    ContactTitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
                    TitleExtEN = model.TitleExtEN,
                    FirstNameEN = model.FirstNameEN,
                    MiddleNameEN = model.MiddleNameEN,
                    LastNameEN = model.LastNameEN,
                    CitizenIdentityNo = model.CitizenIdentityNo,
                    CitizenExpireDate = model.CitizenExpireDate,
                    National = MST.MasterCenterDropdownDTO.CreateFromModel(model.National),
                    Gender = MST.MasterCenterDropdownDTO.CreateFromModel(model.Gender),
                    TaxID = model.TaxID,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberExt = model.PhoneNumberExt,
                    ContactFirstName = model.ContactFirstName,
                    ContactLastname = model.ContactLastname,
                    WeChat = model.WeChatID,
                    WhatsApp = model.WhatsAppID,
                    LineID = model.LineID,
                    BirthDate = model.BirthDate,
                    IsVIP = model.IsVIP,
                    IsThaiNationality = model.IsThaiNationality,
                    UpdateDate = model.Updated,
                    TitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
                    TitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    IsCancelledOwner = model.IsCancelledOwner,
                    FullnameEN = model.FullnameEN,
                    FullnameTH = model.FullnameTH,
                    IsNew = model.IsAddNewOwner
                };

                var phones = await DB.AgreementOwnerPhones.Include(o => o.PhoneType).Where(o => o.AgreementOwnerID == model.ID && o.IsDeleted == false).ToListAsync();
                result.AgreementOwnerPhones = phones.Select(o => AgreementOwnerPhoneDTO.CreateFromModel(o)).ToList();
                result.ContactPhones = phones.Select(o => AgreementOwnerPhoneDTO.CreateFromModel(o)).ToList();

                var emails = await DB.AgreementOwnerEmails.Where(o => o.AgreementOwnerID == model.ID && o.IsDeleted == false).ToListAsync();
                result.AgreementOwnerEmails = emails.Select(o => AgreementOwnerEmailDTO.CreateFromModel(o)).ToList();
                result.ContactEmails = emails.Select(o => AgreementOwnerEmailDTO.CreateFromModel(o)).ToList();

                var addresses = await DB.AgreementOwnerAddresses
                    .Include(o => o.ContactAddressType)
                    .Include(o => o.Country)
                    .Include(o => o.Province)
                    .Include(o => o.District)
                    .Include(o => o.SubDistrict)
                    .Where(o => o.AgreementOwnerID == model.ID).ToListAsync();

                #region ตรวจสอบว่ามีข้อมูลที่อยู่ตามบัตรประชาชนหรือไม่

                int countAdrrType1 = 0;

                foreach (var item in addresses)
                {
                    if (item.ContactAddressType.Key == ContactAddressTypeKeys.Citizen) countAdrrType1++;
                }

                if (countAdrrType1 == 0)
                {
                    var addrtemp = new AgreementOwnerAddress
                    {
                        AgreementOwnerID = model.ID,
                        ContactAddressType = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType
                                                            && o.Key == ContactAddressTypeKeys.Citizen).FirstOrDefaultAsync()
                    };

                    addresses.Add(addrtemp);
                }

                #endregion

                result.AgreementOwnerAddresses = addresses.Select(o => AgreementOwnerAddressDTO.CreateFromModel(o, DB)).ToList();

                #region "   ที่อยู่ทีติดต่อได้   "
                if (!addresses.Where(o => o.ContactAddressType.Key == ContactAddressTypeKeys.Contact).Any())
                {
                    var ContactAddressType = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && o.Key == ContactAddressTypeKeys.Contact).FirstOrDefaultAsync();

                    //var contact = await DB.Contacts.Where(o => o.ContactNo == model.ContactNo).FirstOrDefaultAsync();
                    var contactaddress = await DB.ContactAddresses
                                                .Where(o => o.ContactID == model.FromContactID && o.ContactAddressTypeMasterCenterID == ContactAddressType.ID)
                                                .Include(o => o.ContactAddressType)
                                                .Include(o => o.Country)
                                                .Include(o => o.Province)
                                                .Include(o => o.District)
                                                .Include(o => o.SubDistrict)
                                                .ToListAsync();
                    var listContactaddress = contactaddress.Select(o => ContactAddressDTO.CreateFromModelAsync(o, DB)).Select(x => x.Result).ToList();

                    var addressProject = listContactaddress.Where(o => o.Project.Id == model.Agreement.ProjectID).FirstOrDefault();
                    if (addressProject != null)
                    {
                        var contactAddress = contactaddress.Where(o => o.ID == addressProject.Id).FirstOrDefault();

                        if (result.AgreementOwnerAddresses == null)
                        {
                            result.AgreementOwnerAddresses = new List<AgreementOwnerAddressDTO>();
                        }

                        result.AgreementOwnerAddresses.Add(AgreementOwnerAddressDTO.CreateFromContactModel(contactAddress, DB));
                    }
                    else
                    {
                        var contactAddress = contactaddress.OrderByDescending(o => o.Created).FirstOrDefault();

                        //
                        //var newAddPr = new ContactAddressProject();
                        //newAddPr.ContactAddressID = contactAddress.ID;
                        //newAddPr.ProjectID = model.Agreement.ProjectID;
                        //await DB.ContactAddressProjects.AddAsync(newAddPr);
                        //await DB.SaveChangesAsync();

                        if (result.AgreementOwnerAddresses == null)
                        {
                            result.AgreementOwnerAddresses = new List<AgreementOwnerAddressDTO>();
                        }

                        result.AgreementOwnerAddresses.Add(AgreementOwnerAddressDTO.CreateFromContactModel(contactAddress, DB));


                        //var project = await DB.Projects.Where(o => o.ID == model.Agreement.ProjectID).FirstOrDefaultAsync();
                        //var newcontactadd = new AgreementOwnerAddressDTO();
                        //newcontactadd.Project = PRJ.ProjectDropdownDTO.CreateFromModel(project); ;
                        //newcontactadd.ContactAddressType = MST.MasterCenterDropdownDTO.CreateFromModel(ContactAddressType);
                        //var contactAdd = await DB.ContactAddresses
                        //.Include(o => o.ContactAddressType)
                        //.Include(o => o.Country)
                        //.Include(o => o.Province)
                        //.Include(o => o.District)
                        //.Include(o => o.SubDistrict)
                        //.Where(o => o.ContactID == model.FromContactID
                        //        && o.ContactAddressType.Key == ContactAddressTypeKeys.Contact)
                        //.OrderByDescending(o => o.Created).FirstOrDefaultAsync();

                        //if (contactAdd != null)
                        //{
                        //    if (result.AgreementOwnerAddresses == null)
                        //    {
                        //        result.AgreementOwnerAddresses = new List<AgreementOwnerAddressDTO>();
                        //    }

                        //result.AgreementOwnerAddresses.Add(newcontactadd);
                        //}
                    }
                }
                #endregion

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<AgreementOwnerDTO> CreateFromModelDraftAsync(models.SAL.AgreementOwner model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new AgreementOwnerDTO()
                {
                    FromContactID = model.FromContactID,
                    IsMainOwner = model.IsMainOwner,
                    ContactNo = model.ContactNo,
                    ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
                    ContactTitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleExtTH = model.TitleExtTH,
                    FirstNameTH = model.FirstNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    LastNameTH = model.LastNameTH,
                    Nickname = model.Nickname,
                    ContactTitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
                    TitleExtEN = model.TitleExtEN,
                    FirstNameEN = model.FirstNameEN,
                    MiddleNameEN = model.MiddleNameEN,
                    LastNameEN = model.LastNameEN,
                    CitizenIdentityNo = model.CitizenIdentityNo,
                    CitizenExpireDate = model.CitizenExpireDate,
                    National = MST.MasterCenterDropdownDTO.CreateFromModel(model.National),
                    Gender = MST.MasterCenterDropdownDTO.CreateFromModel(model.Gender),
                    TaxID = model.TaxID,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberExt = model.PhoneNumberExt,
                    ContactFirstName = model.ContactFirstName,
                    ContactLastname = model.ContactLastname,
                    WeChat = model.WeChatID,
                    WhatsApp = model.WhatsAppID,
                    LineID = model.LineID,
                    BirthDate = model.BirthDate,
                    IsVIP = model.IsVIP,
                    IsThaiNationality = model.IsThaiNationality,
                    TitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
                    TitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH)
                };

                if (model.ContactTypeMasterCenterID != null)
                    result.ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTypeMasterCenterID).FirstOrDefaultAsync());

                if (model.ContactTitleTHMasterCenterID != null)
                {
                    result.ContactTitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTitleTHMasterCenterID).FirstOrDefaultAsync());
                    result.TitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTitleTHMasterCenterID).FirstOrDefaultAsync());
                }
                if (model.ContactTitleENMasterCenterID != null)
                {
                    result.ContactTitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTitleENMasterCenterID).FirstOrDefaultAsync());
                    result.TitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTitleENMasterCenterID).FirstOrDefaultAsync());
                }
                if (model.NationalMasterCenterID != null)
                    result.National = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.NationalMasterCenterID).FirstOrDefaultAsync());

                if (model.GenderMasterCenterID != null)
                    result.Gender = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.GenderMasterCenterID).FirstOrDefaultAsync());

                var phones = await DB.ContactPhones.Include(o => o.PhoneType).Where(o => o.ContactID == model.FromContactID).ToListAsync();
                result.AgreementOwnerPhones = phones.Select(o => AgreementOwnerPhoneDTO.CreateFromContactModel(o)).ToList();
                result.ContactPhones = phones.Select(o => AgreementOwnerPhoneDTO.CreateFromContactModel(o)).ToList();

                var emails = await DB.ContactEmails.Where(o => o.ContactID == model.FromContactID).ToListAsync();
                result.AgreementOwnerEmails = emails.Select(o => AgreementOwnerEmailDTO.CreateFromContactModel(o)).ToList();
                result.ContactEmails = emails.Select(o => AgreementOwnerEmailDTO.CreateFromContactModel(o)).ToList();

                var addresses = await DB.ContactAddresses
                    .Include(o => o.ContactAddressType)
                    .Include(o => o.Country)
                    .Include(o => o.Province)
                    .Include(o => o.District)
                    .Include(o => o.SubDistrict)
                    .Where(o => o.ContactID == model.FromContactID).ToListAsync();

                if (addresses != null)
                {
                    #region ตรวจสอบว่ามีข้อมูลที่อยู่ตามบัตรประชาชนหรือไม่

                    int countAdrrType1 = 0;

                    foreach (var item in addresses)
                    {
                        if (item.ContactAddressType.Key == ContactAddressTypeKeys.Citizen)
                        {
                            result.AgreementOwnerAddresses = new List<AgreementOwnerAddressDTO>();
                            result.AgreementOwnerAddresses.Add(AgreementOwnerAddressDTO.CreateFromContactModel(item, DB));
                        }
                        countAdrrType1++;
                    }
                    #endregion

                }

                var agreement = await DB.Agreements.Include(o => o.Booking).Where(o => o.ID == model.AgreementID).FirstAsync();
                result.Agreement = AgreementDropdownDTO.CreateFromModel(agreement);

                if (addresses.Where(o => o.ContactAddressType.Key == ContactAddressTypeKeys.Contact).Any())
                {
                    var ContactAddressType = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && o.Key == ContactAddressTypeKeys.Contact).FirstOrDefaultAsync();

                    var contact = await DB.Contacts.Where(o => o.ContactNo == model.ContactNo).FirstOrDefaultAsync();
                    var contactaddress = await DB.ContactAddresses.Where(o => o.ContactID == contact.ID && o.ContactAddressTypeMasterCenterID == ContactAddressType.ID).ToListAsync();

                    var list = contactaddress.Select(o => ContactAddressDTO.CreateFromModelAsync(o, DB)).Select(x => x.Result).Where(o => o.Project != null).ToList();

                    var addressProject = list.Where(o => o.Project.Id == agreement.ProjectID).FirstOrDefault();
                    if (addressProject != null)
                    {
                        var contactAddress = await DB.ContactAddresses
                        .Include(o => o.ContactAddressType)
                        .Include(o => o.Country)
                        .Include(o => o.Province)
                        .Include(o => o.District)
                        .Include(o => o.SubDistrict)
                        .Where(o => o.ID == addressProject.Id).FirstAsync();

                        if (result.AgreementOwnerAddresses == null)
                        {
                            result.AgreementOwnerAddresses = new List<AgreementOwnerAddressDTO>();
                        }

                        result.AgreementOwnerAddresses.Add(AgreementOwnerAddressDTO.CreateFromContactModel(contactAddress, DB));
                    }
                    else
                    {
                        var project = await DB.Projects.Where(o => o.ID == agreement.ProjectID).FirstOrDefaultAsync();
                        var newcontactadd = new AgreementOwnerAddressDTO();
                        newcontactadd.Project = PRJ.ProjectDropdownDTO.CreateFromModel(project); 
                        newcontactadd.ContactAddressType = MST.MasterCenterDropdownDTO.CreateFromModel(ContactAddressType);
                        //var contactAdd = await DB.ContactAddresses
                        //.Include(o => o.ContactAddressType)
                        //.Include(o => o.Country)
                        //.Include(o => o.Province)
                        //.Include(o => o.District)
                        //.Include(o => o.SubDistrict)
                        //.Where(o => o.ContactID == model.FromContactID
                        //        && o.ContactAddressType.Key == ContactAddressTypeKeys.Contact)
                        //.OrderByDescending(o => o.Created).FirstOrDefaultAsync();

                        //if (contactAdd != null)
                        //{
                        //    if (result.AgreementOwnerAddresses == null)
                        //    {
                        //        
                        //    }
                        result.AgreementOwnerAddresses = new List<AgreementOwnerAddressDTO>();
                        result.AgreementOwnerAddresses.Add(newcontactadd);
                        //}
                    }
                }



                //var addressProject = await DB.ContactAddressProjects.Where(o => o.ProjectID == agreement.Booking.ProjectID).FirstOrDefaultAsync();
                //if (addressProject != null)
                //{
                //    var contactAddress = await DB.ContactAddresses
                //    .Include(o => o.ContactAddressType)
                //    .Include(o => o.Country)
                //    .Include(o => o.Province)
                //    .Include(o => o.District)
                //    .Include(o => o.SubDistrict)
                //    .Where(o => o.ID == addressProject.ContactAddressID).FirstAsync();

                //    if (result.AgreementOwnerAddresses == null)
                //    {
                //        result.AgreementOwnerAddresses = new List<AgreementOwnerAddressDTO>();
                //    }

                //    result.AgreementOwnerAddresses.Add(AgreementOwnerAddressDTO.CreateFromContactModel(contactAddress, DB));
                //}

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<AgreementOwnerDTO> CreateFromModelFromBookingAsync(models.SAL.AgreementOwner model, models.DatabaseContext DB, Guid? bookOwnersID)
        {
            if (model != null)
            {
                var result = new AgreementOwnerDTO()
                {
                    FromContactID = model.FromContactID,
                    IsMainOwner = model.IsMainOwner,
                    ContactNo = model.ContactNo,
                    ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
                    ContactTitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleExtTH = model.TitleExtTH,
                    FirstNameTH = model.FirstNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    LastNameTH = model.LastNameTH,
                    Nickname = model.Nickname,
                    ContactTitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
                    TitleExtEN = model.TitleExtEN,
                    FirstNameEN = model.FirstNameEN,
                    MiddleNameEN = model.MiddleNameEN,
                    LastNameEN = model.LastNameEN,
                    CitizenIdentityNo = model.CitizenIdentityNo,
                    CitizenExpireDate = model.CitizenExpireDate,
                    National = MST.MasterCenterDropdownDTO.CreateFromModel(model.National),
                    Gender = MST.MasterCenterDropdownDTO.CreateFromModel(model.Gender),
                    TaxID = model.TaxID,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberExt = model.PhoneNumberExt,
                    ContactFirstName = model.ContactFirstName,
                    ContactLastname = model.ContactLastname,
                    WeChat = model.WeChatID,
                    WhatsApp = model.WhatsAppID,
                    LineID = model.LineID,
                    BirthDate = model.BirthDate,
                    IsVIP = model.IsVIP,
                    IsThaiNationality = model.IsThaiNationality
                };

                if (model.ContactTypeMasterCenterID != null)
                    result.ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTypeMasterCenterID).FirstOrDefaultAsync());

                if (model.ContactTitleTHMasterCenterID != null)
                {
                    result.ContactTitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTitleTHMasterCenterID).FirstOrDefaultAsync());
                    result.TitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTitleTHMasterCenterID).FirstOrDefaultAsync());
                }
                if (model.ContactTitleENMasterCenterID != null)
                {
                    result.ContactTitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTitleENMasterCenterID).FirstOrDefaultAsync());
                    result.TitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTitleENMasterCenterID).FirstOrDefaultAsync());
                }
                if (model.NationalMasterCenterID != null)
                    result.National = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.NationalMasterCenterID).FirstOrDefaultAsync());

                if (model.GenderMasterCenterID != null)
                    result.Gender = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.GenderMasterCenterID).FirstOrDefaultAsync());

                var phones = await DB.ContactPhones.Include(o => o.PhoneType).Where(o => o.ContactID == model.FromContactID).ToListAsync();
                result.AgreementOwnerPhones = phones.Select(o => AgreementOwnerPhoneDTO.CreateFromContactModel(o)).ToList();
                result.ContactPhones = phones.Select(o => AgreementOwnerPhoneDTO.CreateFromContactModel(o)).ToList();

                var emails = await DB.ContactEmails.Where(o => o.ContactID == model.FromContactID).ToListAsync();
                result.AgreementOwnerEmails = emails.Select(o => AgreementOwnerEmailDTO.CreateFromContactModel(o)).ToList();
                result.ContactEmails = emails.Select(o => AgreementOwnerEmailDTO.CreateFromContactModel(o)).ToList();

                // var addresses = await DB.ContactAddresses
                //    .Include(o => o.ContactAddressType)
                //    .Include(o => o.Country)
                //    .Include(o => o.Province)
                //    .Include(o => o.District)
                //    .Include(o => o.SubDistrict)
                //    .Where(o => o.ContactID == model.FromContactID && o.ContactAddressType.Key == "1").FirstOrDefaultAsync();

                //if (addresses != null)
                //{
                //    result.AgreementOwnerAddresses = new List<AgreementOwnerAddressDTO>();
                //    result.AgreementOwnerAddresses.Add(AgreementOwnerAddressDTO.CreateFromContactModel(addresses, DB));
                //}

                var addresses = await DB.BookingOwnerAddresses
                    .Include(o => o.ContactAddressType)
                    .Include(o => o.Country)
                    .Include(o => o.Province)
                    .Include(o => o.District)
                    .Include(o => o.SubDistrict)
                    .Where(o => o.BookingOwnerID == bookOwnersID).ToListAsync();

                if (addresses != null)
                {
                    result.AgreementOwnerAddresses = new List<AgreementOwnerAddressDTO>();
                    var AgreementOwnerAddresses2 = new AgreementOwnerAddressDTO();
                    foreach (var i in addresses)
                    {
                        AgreementOwnerAddresses2 = AgreementOwnerAddressDTO.CreateFromBookingModel(i, DB);
                        result.AgreementOwnerAddresses.Add(AgreementOwnerAddresses2);
                    }

                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<AgreementOwnerDTO> CreateFromModelChangeWorkflowAsync(models.SAL.AgreementOwner model, models.DatabaseContext DB, Guid? changeAgreementOwnerWorkflowID)
        {
            if (model != null)
            {
                var result = new AgreementOwnerDTO()
                {
                    Id = model.ID,
                    Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),
                    Order = model.Order,
                    FromContactID = model.FromContactID,
                    IsMainOwner = model.IsMainOwner,
                    ContactNo = model.ContactNo,
                    ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
                    ContactTitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleExtTH = model.TitleExtTH,
                    FirstNameTH = model.FirstNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    LastNameTH = model.LastNameTH,
                    Nickname = model.Nickname,
                    ContactTitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
                    TitleExtEN = model.TitleExtEN,
                    FirstNameEN = model.FirstNameEN,
                    MiddleNameEN = model.MiddleNameEN,
                    LastNameEN = model.LastNameEN,
                    CitizenIdentityNo = model.CitizenIdentityNo,
                    CitizenExpireDate = model.CitizenExpireDate,
                    National = MST.MasterCenterDropdownDTO.CreateFromModel(model.National),
                    Gender = MST.MasterCenterDropdownDTO.CreateFromModel(model.Gender),
                    TaxID = model.TaxID,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberExt = model.PhoneNumberExt,
                    ContactFirstName = model.ContactFirstName,
                    ContactLastname = model.ContactLastname,
                    WeChat = model.WeChatID,
                    WhatsApp = model.WhatsAppID,
                    LineID = model.LineID,
                    BirthDate = model.BirthDate,
                    IsVIP = model.IsVIP,
                    IsThaiNationality = model.IsThaiNationality,
                    UpdateDate = model.Updated,
                    TitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
                    IsCancelledOwner = model.IsCancelledOwner,
                    IsNew = model.IsAddNewOwner
                };

                var changeType = await DB.ChangeAgreementOwnerWorkflows.Where(o => o.ID == changeAgreementOwnerWorkflowID).Select(o => o.ChangeAgreementOwnerType).FirstOrDefaultAsync();
                if (changeType != null)
                {
                    if (changeType.Key == "2")
                    {
                        if (model.IsCancelledOwner) result.IsSelected = true;
                        else result.IsSelected = false;
                    }
                    else if (changeType.Key == "3")
                    {
                        if (model.IsTransferOwner) result.IsSelected = true;
                        else result.IsSelected = false;
                    }
                }

                var phones = await DB.AgreementOwnerPhones.Include(o => o.PhoneType).Where(o => o.AgreementOwnerID == model.ID).ToListAsync();
                result.AgreementOwnerPhones = phones.Select(o => AgreementOwnerPhoneDTO.CreateFromModel(o)).ToList();
                result.ContactPhones = phones.Select(o => AgreementOwnerPhoneDTO.CreateFromModel(o)).ToList();

                var emails = await DB.AgreementOwnerEmails.Where(o => o.AgreementOwnerID == model.ID).ToListAsync();
                result.AgreementOwnerEmails = emails.Select(o => AgreementOwnerEmailDTO.CreateFromModel(o)).ToList();
                result.ContactEmails = emails.Select(o => AgreementOwnerEmailDTO.CreateFromModel(o)).ToList();

                var addresses = await DB.AgreementOwnerAddresses
                    .Include(o => o.ContactAddressType)
                    .Include(o => o.Country)
                    .Include(o => o.Province)
                    .Include(o => o.District)
                    .Include(o => o.SubDistrict)
                    .Where(o => o.AgreementOwnerID == model.ID).ToListAsync();

                #region ตรวจสอบว่ามีข้อมูลที่อยู่ตามบัตรประชาชนหรือไม่

                int countAdrrType1 = 0;

                foreach (var item in addresses)
                {
                    if (item.ContactAddressType.Key == "1") countAdrrType1++;
                }

                if (countAdrrType1 == 0)
                {
                    var addrtemp = new AgreementOwnerAddress
                    {
                        AgreementOwnerID = model.ID,
                        ContactAddressType = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType
                                    && o.Key == ContactAddressTypeKeys.Citizen).FirstOrDefaultAsync()
                    };

                    addresses.Add(addrtemp);
                }

                #endregion

                result.AgreementOwnerAddresses = addresses.Select(o => AgreementOwnerAddressDTO.CreateFromModel(o, DB)).ToList();
                if (changeAgreementOwnerWorkflowID != null)
                {
                    result.ChangeAgreementOwnerInType = await DB.ChangeAgreementOwnerWorkflowDetails
                                                            .Where(o => o.AgreementOwnerID == model.ID
                                                                     && o.ChangeAgreementOwnerWorkflowID == changeAgreementOwnerWorkflowID)
                                                            .Select(o => o.ChangeAgreementOwnerInType)
                                                            .FirstOrDefaultAsync();
                }
                else
                {
                    result.ChangeAgreementOwnerInType = null;
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<AgreementOwnerDTO> CreateFromModelContactAsync(Guid agreementID, models.CTM.Contact model, models.DatabaseContext DB, bool changeAgreementOwnerInType)
        {
            if (model != null)
            {
                var agreement = await DB.Agreements.Where(o => o.ID == agreementID).FirstOrDefaultAsync();

                var result = new AgreementOwnerDTO()
                {
                    Id = model.ID,
                    Agreement = AgreementDropdownDTO.CreateFromModel(agreement),
                    Order = model.Order,
                    FromContactID = model.ID,
                    IsMainOwner = false,
                    ContactNo = model.ContactNo,
                    ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
                    ContactTitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleExtTH = model.TitleExtTH,
                    FirstNameTH = model.FirstNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    LastNameTH = model.LastNameTH,
                    Nickname = model.Nickname,
                    ContactTitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
                    TitleExtEN = model.TitleExtEN,
                    FirstNameEN = model.FirstNameEN,
                    MiddleNameEN = model.MiddleNameEN,
                    LastNameEN = model.LastNameEN,
                    CitizenIdentityNo = model.CitizenIdentityNo,
                    CitizenExpireDate = model.CitizenExpireDate,
                    National = MST.MasterCenterDropdownDTO.CreateFromModel(model.National),
                    Gender = MST.MasterCenterDropdownDTO.CreateFromModel(model.Gender),
                    TaxID = model.TaxID,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberExt = model.PhoneNumberExt,
                    ContactFirstName = model.ContactFirstName,
                    ContactLastname = model.ContactLastname,
                    WeChat = model.WeChatID,
                    WhatsApp = model.WhatsAppID,
                    LineID = model.LineID,
                    BirthDate = model.BirthDate,
                    IsVIP = model.IsVIP,
                    IsThaiNationality = model.IsThaiNationality,
                    UpdateDate = model.Updated,
                    TitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
                    IsNew = true
                };

                #region Old Code (Backup)

                //var phones = await DB.AgreementOwnerPhones.Include(o => o.PhoneType).Where(o => o.AgreementOwnerID == model.ID).ToListAsync();
                //result.AgreementOwnerPhones = phones.Select(o => AgreementOwnerPhoneDTO.CreateFromModel(o)).ToList();

                //var emails = await DB.AgreementOwnerEmails.Where(o => o.AgreementOwnerID == model.ID).ToListAsync();
                //result.AgreementOwnerEmails = emails.Select(o => AgreementOwnerEmailDTO.CreateFromModel(o)).ToList();


                //var addresses = await DB.AgreementOwnerAddresses
                //    .Include(o => o.ContactAddressType)
                //    .Include(o => o.Country)
                //    .Include(o => o.Province)
                //    .Include(o => o.District)
                //    .Include(o => o.SubDistrict)
                //    .Where(o => o.AgreementOwnerID == model.ID)
                //    .ToListAsync();

                //result.AgreementOwnerAddresses = addresses.Select(o => AgreementOwnerAddressDTO.CreateFromModel(o, DB)).ToList();

                //var ChangeAgreementOwnerWorkflowID = await DB.ChangeAgreementOwnerWorkflowDetails
                //                                        .Where(o => o.AgreementOwnerID == model.ID)
                //                                        .Select(o => o.ID)
                //                                        .FirstOrDefaultAsync();

                #endregion

                var phones = await DB.ContactPhones.Include(o => o.PhoneType).Where(o => o.ContactID == model.ID).ToListAsync();
                result.AgreementOwnerPhones = phones.Select(o => AgreementOwnerPhoneDTO.CreateFromContactModel(o)).ToList();
                result.ContactPhones = result.AgreementOwnerPhones;

                var emails = await DB.ContactEmails.Where(o => o.ContactID == model.ID).ToListAsync();
                result.AgreementOwnerEmails = emails.Select(o => AgreementOwnerEmailDTO.CreateFromContactModel(o)).ToList();
                result.ContactEmails = result.AgreementOwnerEmails;

                var addresses = await DB.ContactAddresses
                    .Include(o => o.ContactAddressType)
                    .Include(o => o.Country)
                    .Include(o => o.Province)
                    .Include(o => o.District)
                    .Include(o => o.SubDistrict)
                    .Where(o => o.ContactID == model.ID).ToListAsync();

                if (addresses != null)
                {
                    result.AgreementOwnerAddresses = addresses.Select(o => AgreementOwnerAddressDTO.CreateFromContactModel(o, DB)).ToList();
                }

                result.ChangeAgreementOwnerInType = changeAgreementOwnerInType;

                return result;
            }
            else
            {
                return null;
            }
        }
        public static AgreementOwnerDTO CreateFromModel(AgreementOwner model)
        {
            if (model != null)
            {
                AgreementOwnerDTO result = new AgreementOwnerDTO
                {
                    FromContactID = model.FromContactID,
                    IsMainOwner = model.IsMainOwner,
                    ContactNo = model.ContactNo,
                    ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
                    ContactTitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleExtTH = model.TitleExtTH,
                    FirstNameTH = model.FirstNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    LastNameTH = model.LastNameTH,
                    Nickname = model.Nickname,
                    ContactTitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
                    TitleExtEN = model.TitleExtEN,
                    FirstNameEN = model.FirstNameEN,
                    MiddleNameEN = model.MiddleNameEN,
                    LastNameEN = model.LastNameEN,
                    CitizenIdentityNo = model.CitizenIdentityNo,
                    CitizenExpireDate = model.CitizenExpireDate,
                    National = MST.MasterCenterDropdownDTO.CreateFromModel(model.National),
                    Gender = MST.MasterCenterDropdownDTO.CreateFromModel(model.Gender),
                    TaxID = model.TaxID,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberExt = model.PhoneNumberExt,
                    ContactFirstName = model.ContactFirstName,
                    ContactLastname = model.ContactLastname,
                    WeChat = model.WeChatID,
                    WhatsApp = model.WhatsAppID,
                    LineID = model.LineID,
                    BirthDate = model.BirthDate,
                    IsVIP = model.IsVIP,
                    IsThaiNationality = model.IsThaiNationality,
                    Agreement = AgreementDropdownDTO.CreateFromModel(model.Agreement),
                    FullnameEN = model.FullnameEN,
                    FullnameTH = model.FullnameTH,
                    IsNew = model.IsAddNewOwner,
                    IsCancelledOwner = model.IsCancelledOwner
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static AgreementOwnerDTO CreateFromBookingModel(BookingOwner model)
        {
            if (model != null)
            {
                AgreementOwnerDTO result = new AgreementOwnerDTO
                {
                    FromContactID = model.FromContactID,
                    IsMainOwner = model.IsMainOwner,
                    ContactNo = model.ContactNo,
                    ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
                    ContactTitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleExtTH = model.TitleExtTH,
                    FirstNameTH = model.FirstNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    LastNameTH = model.LastNameTH,
                    Nickname = model.Nickname,
                    ContactTitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
                    TitleExtEN = model.TitleExtEN,
                    FirstNameEN = model.FirstNameEN,
                    MiddleNameEN = model.MiddleNameEN,
                    LastNameEN = model.LastNameEN,
                    CitizenIdentityNo = model.CitizenIdentityNo,
                    CitizenExpireDate = model.CitizenExpireDate,
                    National = MST.MasterCenterDropdownDTO.CreateFromModel(model.National),
                    Gender = MST.MasterCenterDropdownDTO.CreateFromModel(model.Gender),
                    TaxID = model.TaxID,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberExt = model.PhoneNumberExt,
                    ContactFirstName = model.ContactFirstName,
                    ContactLastname = model.ContactLastname,
                    WeChat = model.WeChatID,
                    WhatsApp = model.WhatsAppID,
                    LineID = model.LineID,
                    BirthDate = model.BirthDate,
                    IsVIP = model.IsVIP,
                    IsThaiNationality = model.IsThaiNationality,
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

        public async Task ValidateAsync(models.DatabaseContext DB)
        {
            ValidateException ex = new ValidateException();

            #region Agreement Owner

            if (this.ContactEmails.Count > 0)
            {
                var isEmailNull = this.ContactEmails.Any(o => string.IsNullOrEmpty(o.Email));
                if (isEmailNull)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = typeof(AgreementOwnerEmailDTO).GetProperty(nameof(AgreementOwnerEmailDTO.Email)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (this.National?.Key == NationalKeys.Thai)
                    { }
                    else
                    {
                        var isEmailInvalid = this.ContactEmails.Any(o => o.Email.IsValidEmail() == false);
                        if (isEmailInvalid)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0015").FirstAsync();
                            var msg = errMsg.Message;
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                }

                List<string> emailList = new List<string>();
                foreach (var email in this.ContactEmails)
                {
                    if (!string.IsNullOrEmpty(email.Email))
                    {
                        if (emailList.Contains(email.Email))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.ContactEmails)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                            break;
                        }
                        else
                        {
                            emailList.Add(email.Email);
                        }
                    }
                }
            }

            if (this.ContactPhones.Count > 0)
            {
                var isPhoneTypeNull = this.ContactPhones.Where(o => o.PhoneType == null).Any();
                if (isPhoneTypeNull)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = typeof(AgreementOwnerPhoneDTO).GetProperty(nameof(AgreementOwnerPhoneDTO.PhoneType)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                var isPhoneNumberNull = this.ContactPhones.Any(o => string.IsNullOrEmpty(o.PhoneNumber));
                if (isPhoneNumberNull)
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = typeof(AgreementOwnerPhoneDTO).GetProperty(nameof(AgreementOwnerPhoneDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    var isNumberNull = this.ContactPhones.Any(o => !o.PhoneNumber.IsOnlyNumber());
                    if (isNumberNull)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                        string desc = typeof(AgreementOwnerPhoneDTO).GetProperty(nameof(AgreementOwnerPhoneDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }

                if (!isPhoneTypeNull)
                {
                    var foreignID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == models.MasterKeys.MasterCenterGroupKeys.PhoneType && o.Key == "3").Select(o => o.ID).FirstAsync();
                    var foreignList = this.ContactPhones.Where(o => o.PhoneType.Id == foreignID).ToList();

                    if (foreignList.Count > 0)
                    {
                        var isCountryCodeNull = foreignList.Any(o => string.IsNullOrEmpty(o.CountryCode));
                        if (isCountryCodeNull)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                            string desc = typeof(AgreementOwnerPhoneDTO).GetProperty(nameof(AgreementOwnerPhoneDTO.CountryCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                        else
                        {
                            var isInvalidCode = foreignList.Any(o => !o.CountryCode.IsOnlyNumberWithSpecialCharacter(false, "+"));
                            if (isInvalidCode)
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
                                string desc = typeof(AgreementOwnerPhoneDTO).GetProperty(nameof(AgreementOwnerPhoneDTO.CountryCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                        }
                    }
                }

                List<AgreementOwnerPhoneList> phoneList = new List<AgreementOwnerPhoneList>();
                foreach (var phone in this.ContactPhones)
                {
                    if (!string.IsNullOrEmpty(phone.PhoneNumber) && (phone.PhoneType != null))
                    {
                        var isPhoneExits = phoneList.Where(o => o.PhoneNumber == phone.PhoneNumber && o.PhoneTypeMastercenterID == phone.PhoneType.Id).Any();

                        if (isPhoneExits)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.ContactPhones)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                            break;
                        }
                        else
                        {
                            phoneList.Add(new AgreementOwnerPhoneList
                            {
                                PhoneNumber = phone.PhoneNumber,
                                PhoneTypeMastercenterID = phone.PhoneType.Id
                            });
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(this.WhatsApp))
            {
                if (!this.WhatsApp.CheckLang(false, true, true, false))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.WhatsApp)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (!string.IsNullOrEmpty(this.WeChat))
            {
                if (!this.WeChat.CheckLang(false, true, true, false))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.WeChat)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (!string.IsNullOrEmpty(this.LineID))
            {
                if (!this.LineID.CheckLang(false, true, true, false))
                {
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.LineID)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (this.ContactType == null)
            {
                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.ContactType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                var legalID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactType" && o.Key == "1").Select(o => o.ID).FirstAsync();
                if (this.ContactType.Id == legalID)
                {
                    if (string.IsNullOrEmpty(this.TaxID))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.TaxID)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    else if (!this.TaxID.IsOnlyNumber())
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.TaxID)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }

                    if (!string.IsNullOrEmpty(this.PhoneNumber))
                    {
                        if (!this.PhoneNumber.IsOnlyNumber())
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                    if (!string.IsNullOrEmpty(this.PhoneNumberExt))
                    {
                        if (!this.PhoneNumberExt.IsOnlyNumber())
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.PhoneNumberExt)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                    if (!string.IsNullOrEmpty(this.ContactFirstName))
                    {
                        if (!this.ContactFirstName.CheckAllLang(false, false, false, null, " "))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.ContactFirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                    if (!string.IsNullOrEmpty(this.ContactLastname))
                    {
                        if (!this.ContactFirstName.CheckAllLang(false, false, false, null, " "))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.ContactFirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (string.IsNullOrEmpty(this.FirstNameTH))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    //else
                    //{
                    //    if (!this.FirstNameTH.CheckLang(true, true, true, false))
                    //    {
                    //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0017").FirstAsync();
                    //        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //        var msg = errMsg.Message.Replace("[field]", desc);
                    //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //    }
                    //}

                    if (string.IsNullOrEmpty(this.FirstNameEN))
                    {
                        //var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        //string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                        //var msg = errMsg.Message.Replace("[field]", desc);
                        //ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    //else
                    //{
                    //    if (!this.FirstNameEN.CheckLang(false, true, true, false))
                    //    {
                    //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                    //        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //        var msg = errMsg.Message.Replace("[field]", desc);
                    //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //    }
                    //}
                }
                else
                {
                    var isCheckNation = false;
                    #region Citizen
                    if (string.IsNullOrEmpty(this.CitizenIdentityNo))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.CitizenIdentityNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    else
                    {
                        if (this.National == null)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.National)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                            isCheckNation = true;
                        }
                        else
                        {
                            var thaiID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == models.MasterKeys.MasterCenterGroupKeys.National && o.Key == models.MasterKeys.NationalKeys.Thai).Select(o => o.ID).FirstAsync();
                            if (this.National.Id == thaiID)
                            {
                                if (!this.CitizenIdentityNo.IsOnlyNumber())
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0014").FirstAsync();
                                    var msg = errMsg.Message;
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                                else if (this.CitizenIdentityNo.Length != 13)
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0014").FirstAsync();
                                    var msg = errMsg.Message;
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                            else
                            {
                                if (!this.CitizenIdentityNo.CheckLang(false, true, false, false))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.CitizenIdentityNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                        }
                    }
                    #endregion

                    #region TH
                    if (this.ContactTitleTH == null)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.ContactTitleTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    else
                    {
                        var externalTitleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH" && o.Key == "-1").Select(o => o.ID).FirstAsync();
                        if (this.ContactTitleTH.Id == externalTitleID && string.IsNullOrEmpty(this.TitleExtTH))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.TitleExtTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (string.IsNullOrEmpty(this.FirstNameTH))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    //else
                    //{
                    //    if (!this.FirstNameTH.CheckAllLang(false, false, false, null, " "))
                    //    {
                    //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                    //        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //        var msg = errMsg.Message.Replace("[field]", desc);
                    //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //    }
                    //}

                    if (string.IsNullOrEmpty(this.LastNameTH))
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.LastNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    //else
                    //{
                    //    if (!this.LastNameTH.CheckAllLang(false, false, false, null, " "))
                    //    {
                    //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                    //        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.LastNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //        var msg = errMsg.Message.Replace("[field]", desc);
                    //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //    }
                    //}

                    //if (!string.IsNullOrEmpty(this.MiddleNameTH))
                    //{
                    //    if (!this.MiddleNameTH.CheckAllLang(false, false, false, null, " "))
                    //    {
                    //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                    //        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.MiddleNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //        var msg = errMsg.Message.Replace("[field]", desc);
                    //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //    }
                    //}

                    //if (!string.IsNullOrEmpty(this.Nickname))
                    //{
                    //    if (!this.Nickname.CheckAllLang(false, false, false, null, " "))
                    //    {
                    //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                    //        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.Nickname)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //        var msg = errMsg.Message.Replace("[field]", desc);
                    //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //    }
                    //}
                    #endregion

                    #region EN
                    if (this.TitleEN != null)
                    {
                        var externalTitleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == models.MasterKeys.MasterCenterGroupKeys.ContactTitleEN && o.Key == "-1").Select(o => o.ID).FirstAsync();
                        if (this.TitleEN.Id == externalTitleID && string.IsNullOrEmpty(this.TitleExtEN))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.TitleExtEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (!string.IsNullOrEmpty(this.FirstNameEN))
                    {
                        if (!this.FirstNameEN.CheckLang(false, true, true, false, null, " "))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (!string.IsNullOrEmpty(this.LastNameEN))
                    {
                        if (!this.LastNameEN.CheckLang(false, true, true, false, null, " "))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.LastNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    //if (!string.IsNullOrEmpty(this.MiddleNameEN))
                    //{
                    //    if (!this.MiddleNameEN.CheckLang(false, false, false, false, null, " "))
                    //    {
                    //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                    //        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.MiddleNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //        var msg = errMsg.Message.Replace("[field]", desc);
                    //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //    }
                    //}
                    #endregion

                    if (!isCheckNation)
                    {
                        if (this.National == null)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.National)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (this.Gender == null)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.Gender)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    if (this.BirthDate == null)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(AgreementOwnerDTO.BirthDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }

            #endregion

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref models.SAL.AgreementOwner model)
        {
            model.FromContactID = this.FromContactID;
            model.IsMainOwner = this.IsMainOwner;
            model.ContactNo = this.ContactNo;
            model.IsThaiNationality = this.IsThaiNationality.Value;
            model.IsVIP = this.IsVIP.Value;
            model.ContactTypeMasterCenterID = this.ContactType?.Id;
            model.ContactTitleTHMasterCenterID = this.TitleTH?.Id;
            model.TitleExtTH = this.TitleExtTH;
            model.FirstNameTH = this.FirstNameTH;
            model.MiddleNameTH = this.MiddleNameTH;
            model.LastNameTH = this.LastNameTH;
            model.Nickname = this.Nickname;
            model.ContactTitleENMasterCenterID = this.TitleEN?.Id;
            model.TitleExtEN = this.TitleExtEN;
            model.FirstNameEN = this.FirstNameEN;
            model.MiddleNameEN = this.MiddleNameEN;
            model.LastNameEN = this.LastNameEN;
            model.CitizenIdentityNo = this.CitizenIdentityNo;
            model.CitizenExpireDate = this.CitizenExpireDate;
            model.NationalMasterCenterID = this.National?.Id;
            model.GenderMasterCenterID = this.Gender?.Id;
            model.TaxID = this.TaxID;
            model.PhoneNumber = this.PhoneNumber;
            model.PhoneNumberExt = this.PhoneNumberExt;
            model.ContactFirstName = this.ContactFirstName;
            model.ContactLastname = this.ContactLastname;
            model.WeChatID = this.WeChat;
            model.WhatsAppID = this.WhatsApp;
            model.LineID = this.LineID;
            model.BirthDate = this.BirthDate;
            model.AgreementID = this.Agreement.Id.Value;
            model.FullnameTH = this.FullnameTH;
            model.FullnameEN = this.FullnameEN;
        }

        public void ToContactModel(ref models.CTM.Contact model)
        {
            model.TitleExtTH = this.TitleExtTH;
            model.FirstNameTH = this.FirstNameTH;
            model.MiddleNameTH = this.MiddleNameTH;
            model.LastNameTH = this.LastNameTH;
            model.Nickname = this.Nickname;
            model.TitleExtEN = this.TitleExtEN;
            model.FirstNameEN = this.FirstNameEN;
            model.MiddleNameEN = this.MiddleNameEN;
            model.LastNameEN = this.LastNameEN;
            model.CitizenIdentityNo = this.CitizenIdentityNo;
            model.BirthDate = this.BirthDate;
            model.TaxID = this.TaxID;
            model.PhoneNumber = this.PhoneNumber;
            model.PhoneNumberExt = this.PhoneNumberExt;
            model.ContactFirstName = this.ContactFirstName;
            model.ContactLastname = this.ContactLastname;
            model.WeChatID = this.WeChat;
            model.WhatsAppID = this.WhatsApp;
            model.LineID = this.LineID;
            model.ContactTypeMasterCenterID = this.ContactType?.Id;
            model.GenderMasterCenterID = this.Gender?.Id;
            model.ContactTitleTHMasterCenterID = this.ContactTitleTH?.Id;
            model.ContactTitleENMasterCenterID = this.ContactTitleEN?.Id;
            model.NationalMasterCenterID = this.National?.Id;
            model.CitizenExpireDate = this.CitizenExpireDate;
        }
    }

    public class AgreementOwnerPhoneList
    {
        public string PhoneNumber { get; set; }
        public Guid? PhoneTypeMastercenterID { get; set; }
    }
}
