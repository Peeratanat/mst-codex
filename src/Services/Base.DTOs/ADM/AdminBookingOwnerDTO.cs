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
using static Database.Models.ExtensionAttributes;
using models = Database.Models;

namespace Base.DTOs.ADM
{
    public class AdminBookingOwnerDTO : BaseDTOFromAdmin
    {
        [DeviceInformation("ID ของ Booking Owner", "")]
        public Guid? BookingOwnerId { get; set; }
        /// <summary>
        /// ใบจอง
        /// </summary>
        [DeviceInformation("ใบจอง", "")]
        public BookingDropdownDTO Booking { get; set; }
        [DeviceInformation("มาจาก Contact (กรณีดึงข้อมูลมาจาก Contact)", "SAL.BookingOwner.FromContactID")]
        public Guid? FromContactID { get; set; }
        /// <summary>
        /// ผู้จองหลัก
        /// </summary>
        [DeviceInformation("ผู้จองหลัก", "SAL.BookingOwner.IsMainOwner")]
        public bool IsMainOwner { get; set; }
        /// <summary>
        /// เป็นผู้ทำสัญญาในใบจอง (true = ผู้ทำสัญญา/ false = ผู้จอง)
        /// </summary>
        [DeviceInformation("เป็นผู้จองหรือผู้ทำสัญญา", "SAL.BookingOwner.IsAgreementOwner")]
        public bool IsAgreementOwner { get; set; }
        /// <summary>
        /// รหัสลูกค้า (มาจาก Contact)
        /// </summary>
        [DeviceInformation("รหัสลูกค้า", "SAL.BookingOwner.ContactNo")]
        public string ContactNo { get; set; }
        /// <summary>
        /// ประเภทของลูกค้า (บุคคลทั่วไป/นิติบุคคล)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactType
        /// </summary>
        [DeviceInformation("ประเภทของลูกค้า", "SAL.BookingOwner.ContactTypeMasterCenterID,CTM.Contact.ContactTypeMasterCenterID")]
        public MST.MasterCenterDropdownDTO ContactType { get; set; }
        /// <summary>
        /// คำนำหน้าชื่อ (ภาษาไทย)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactTitleTH
        /// </summary>
        [DeviceInformation("คำนำหน้าชื่อ (ภาษาไทย)", "SAL.BookingOwner.TitleTH,CTM.Contact.TitleTH")]
        public MST.MasterCenterDropdownDTO TitleTH { get; set; }
        /// <summary>
        /// คำนำหน้าเพิ่มเติม (ภาษาไทย)
        /// </summary>
        [DeviceInformation("คำนำหน้าเพิ่มเติม (ภาษาไทย)", "SAL.BookingOwner.TitleExtTH,CTM.Contact.TitleExtTH")]
        public string TitleExtTH { get; set; }
        /// <summary>
        /// ชื่อจริง (ภาษาไทย)
        /// </summary>
        [DeviceInformation("ชื่อจริง (ภาษาไทย)", "SAL.BookingOwner.FirstNameTH,CTM.Contact.FirstNameTH")]
        public string FirstNameTH { get; set; }
        /// <summary>
        /// ชื่อกลาง (ภาษาไทย)
        /// </summary>
        [DeviceInformation("ชื่อกลาง (ภาษาไทย)", "SAL.BookingOwner.MiddleNameTH,CTM.Contact.MiddleNameTH")]
        public string MiddleNameTH { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาไทย)
        /// </summary>
        [DeviceInformation("นามสกุล (ภาษาไทย)", "SAL.BookingOwner.LastNameTH,CTM.Contact.LastNameTH")]
        public string LastNameTH { get; set; }
        /// <summary>
        /// ชื่อเล่น (ภาษาไทย)
        /// </summary>
        [DeviceInformation("ชื่อเล่น (ภาษาไทย)", "SAL.BookingOwner.Nickname,CTM.Contact.Nickname")]
        public string Nickname { get; set; }
        /// <summary>
        /// คำนำหน้าชื่อ (ภาษาอังกฤษ)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactTitleEN
        /// </summary>
        [DeviceInformation("คำนำหน้าชื่อ (ภาษาอังกฤษ)", "SAL.BookingOwner.TitleEN,CTM.Contact.TitleEN")]
        public MST.MasterCenterDropdownDTO TitleEN { get; set; }
        /// <summary>
        /// คำนำหน้าเพิ่มเติม (ภาษาอังกฤษ)
        /// </summary>
        [DeviceInformation("คำนำหน้าเพิ่มเติม (ภาษาอังกฤษ)", "SAL.BookingOwner.TitleExtEN,CTM.Contact.TitleExtEN")]
        public string TitleExtEN { get; set; }
        /// <summary>
        /// ชื่อจริง (ภาษาอังกฤษ)
        /// </summary>
        [DeviceInformation("ชื่อจริง (ภาษาอังกฤษ)", "SAL.BookingOwner.FirstNameEN,CTM.Contact.FirstNameEN")]
        public string FirstNameEN { get; set; }
        /// <summary>
        /// ชื่อกลาง (ภาษาอังกฤษ)
        /// </summary>
        [DeviceInformation("ชื่อกลาง (ภาษาอังกฤษ)", "SAL.BookingOwner.MiddleNameEN,CTM.Contact.MiddleNameEN")]
        public string MiddleNameEN { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาอังกฤษ)
        /// </summary>
        [DeviceInformation("นามสกุล (ภาษาอังกฤษ)", "SAL.BookingOwner.LastNameEN,CTM.Contact.LastNameEN")]
        public string LastNameEN { get; set; }
        /// <summary>
        /// หมายเลขบัตรประชาชน
        /// </summary>
        [DeviceInformation("หมายเลขบัตรประชาชน", "SAL.BookingOwner.CitizenIdentityNo,CTM.Contact.CitizenIdentityNo")]
        public string CitizenIdentityNo { get; set; }
        /// <summary>
        /// วันหมดอายุบัตรประชาชน
        /// </summary>
        [DeviceInformation("วันหมดอายุบัตรประชาชน", "SAL.BookingOwner.CitizenExpireDate,CTM.Contact.CitizenExpireDate")]
        public DateTime? CitizenExpireDate { get; set; }
        /// <summary>
        /// สัญชาติ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=National
        /// </summary>
        [DeviceInformation("สัญชาติ", "SAL.BookingOwner.National,CTM.Contact.National")]
        public MST.MasterCenterDropdownDTO National { get; set; }
        /// <summary>
        /// เพศ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=Gender
        /// </summary>
        [DeviceInformation("เพศ", "SAL.BookingOwner.Gender,CTM.Contact.Gender")]
        public MST.MasterCenterDropdownDTO Gender { get; set; }
        /// <summary>
        /// เลขประจำตัวผู้เสียภาษี
        /// </summary>
        [DeviceInformation("เลขประจำตัวผู้เสียภาษี", "SAL.BookingOwner.TaxID,CTM.Contact.TaxID")]
        public string TaxID { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์ (นิติบุคคล)
        /// </summary>
        [DeviceInformation("เบอร์โทรศัพท์ (นิติบุคคล)", "SAL.BookingOwner.PhoneNumber,CTM.Contact.PhoneNumber")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// เบอร์ต่อ (นิติบุคคล)
        /// </summary>
        [DeviceInformation("เบอร์ต่อ (นิติบุคคล)", "SAL.BookingOwner.PhoneNumberExt,CTM.Contact.PhoneNumberExt")]
        public string PhoneNumberExt { get; set; }
        /// <summary>
        /// ชื่อผู้ติดต่อ (นิติบุคคล)
        /// </summary>
        [DeviceInformation("ชื่อผู้ติดต่อ (นิติบุคคล)", "SAL.BookingOwner.ContactFirstName,CTM.Contact.ContactFirstName")]
        public string ContactFirstName { get; set; }
        /// <summary>
        /// นามสกุลผู้ติดต่อ (นิติบุคคล)
        /// </summary>
        [DeviceInformation("นามสกุลผู้ติดต่อ (นิติบุคคล)", "SAL.BookingOwner.ContactLastname,CTM.Contact.ContactLastname")]
        public string ContactLastname { get; set; }
        /// <summary>
        /// WeChat ID
        /// </summary>
        [DeviceInformation("WeChat ID", "SAL.BookingOwner.WeChat,CTM.Contact.WeChat")]
        public string WeChat { get; set; }
        /// <summary>
        /// WhatsApp ID
        /// </summary>
        [DeviceInformation("WhatsApp ID", "SAL.BookingOwner.WhatsApp,CTM.Contact.WhatsApp")]
        public string WhatsApp { get; set; }
        /// <summary>
        /// Line ID
        /// </summary>
        [DeviceInformation("Line ID", "SAL.BookingOwner.LineID,CTM.Contact.LineID")]
        public string LineID { get; set; }
        /// <summary>
        /// วันเกิด
        /// </summary>
        [DeviceInformation("วันเกิด", "SAL.BookingOwner.BirthDate,CTM.Contact.BirthDate")]
        public DateTime? BirthDate { get; set; }
        /// <summary>
        /// ลูกค้า VIP
        /// </summary>
        [DeviceInformation("ลูกค้า VIP", "SAL.BookingOwner.IsVIP,CTM.Contact.IsVIP")]
        public bool? IsVIP { get; set; }
        /// <summary>
        /// เป็นคนไทยหรือไม่
        /// </summary>
        [DeviceInformation("เป็นคนไทยหรือไม่", "SAL.BookingOwner.IsThaiNationality,CTM.Contact.IsThaiNationality")]
        public bool? IsThaiNationality { get; set; }

        [DeviceInformation("Update ล่าสุด", "")]
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// เบอร์โทรศัพท์ของผู้จอง
        /// </summary>
        [DeviceInformation("เบอร์โทรศัพท์", "SAL.BookingOwnerPhone,CTM.ContactPhone")]
        public List<BookingOwnerPhoneDTO> ContactPhones { get; set; }
        /// <summary>
        /// อีเมลของผู้จอง
        /// </summary>
        [DeviceInformation("อีเมล", "SAL.BookingOwnerEmail,CTM.ContactEmail")]
        public List<BookingOwnerEmailDTO> ContactEmails { get; set; }
        /// <summary>
        /// ที่อยู่ของผู้จอง (ติดต่อได้/บัตรประชาชน)
        /// </summary>
        [DeviceInformation("ที่อยู่", "SAL.BookingOwnerAddress,Fin.ReceiptTempHeader,CTM.ContactAddress")]
        public List<BookingOwnerAddressDTO> BookingOwnerAddresses { get; set; }

        /// <summary>
        /// true = เพิ่มชื่อยังแบบยังไม่ได้บันทึก
        /// </summary>
        [DeviceInformation("เพิ่มชื่อยังแบบยังไม่ได้บันทึก", "")]
        public bool? IsNew { get; set; }

        /// <summary>
        /// เลือกคนที่จะโอนสิทธิ์
        /// </summary>
        [DeviceInformation("เลือกคนที่จะโอนสิทธิ์", "")]
        public bool? isSelected { get; set; }
        /// <summary>
        /// ชื่อเต็มภาษาไทย
        /// </summary>
        [DeviceInformation("ชื่อเต็มภาษาไทย", "SAL.BookingOwner.FullnameTH,Fin.ReceiptTempHeader.ContactName,Fin.Payment.ContactName,SAL.Booking.AllOwnerName,SAL.Booking.AllOwnerName.MainOwnerName")]
        public string FullnameTH { get; set; }
        /// <summary>
        /// ชื่อเต็มภาษาEng
        /// </summary>
        [DeviceInformation("ชื่อเต็มภาษาEng", "SAL.BookingOwner.FullnameEN,Fin.ReceiptTempHeader.ContactName,Fin.Payment.ContactName")]
        public string FullnameEN { get; set; }

        /// <summary>
        /// แก้ไขข้อมูลใบเสร็จ
        /// </summary>
        [DeviceInformation("แก้ไขข้อมูลใบเสร็จ Update Payment", "")]
        public bool? IsEditReceipt { get; set; }

        [DeviceInformation("เลขที่ใบเสร็จที่แก้ไข", "")]
        public List<string> ReceiptTemp { get; set; }

        public async static Task<AdminBookingOwnerDTO> CreateFromModelAsync(models.SAL.BookingOwner model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new AdminBookingOwnerDTO()
                {
                    BookingOwnerId = model.ID,
                    Booking = BookingDropdownDTO.CreateFromModel(model.Booking),
                    FromContactID = model.FromContactID,
                    IsMainOwner = model.IsMainOwner,
                    ContactNo = model.ContactNo,
                    ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
                    TitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleExtTH = model.TitleExtTH,
                    FirstNameTH = model.FirstNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    LastNameTH = model.LastNameTH,
                    Nickname = model.Nickname,
                    TitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
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
                    IsAgreementOwner = model.IsAgreementOwner,
                    FullnameTH = model.FullnameTH,
                    FullnameEN = model.FullnameEN
                };

                var phones = await DB.BookingOwnerPhones.Include(o => o.PhoneType).Where(o => o.BookingOwnerID == model.ID).ToListAsync();
                result.ContactPhones = phones.Select(o => BookingOwnerPhoneDTO.CreateFromModel(o)).ToList();

                var emails = await DB.BookingOwnerEmails.Where(o => o.BookingOwnerID == model.ID).ToListAsync();
                result.ContactEmails = emails.Select(o => BookingOwnerEmailDTO.CreateFromModel(o)).ToList();


                var addresses = await DB.BookingOwnerAddresses
                    .Include(o => o.ContactAddressType)
                    .Include(o => o.Country)
                    .Include(o => o.Province)
                    .Include(o => o.District)
                    .Include(o => o.SubDistrict)
                    .Where(o => o.BookingOwnerID == model.ID)
                    .ToListAsync();

                result.BookingOwnerAddresses = addresses.Select(o => BookingOwnerAddressDTO.CreateFromModel(o, DB)).ToList();

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<AdminBookingOwnerDTO> CreateFromModelListAsync(models.SAL.BookingOwner model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new AdminBookingOwnerDTO()
                {
                    BookingOwnerId = model.ID,
                    Booking = BookingDropdownDTO.CreateFromModel(model.Booking),
                    FromContactID = model.FromContactID,
                    IsMainOwner = model.IsMainOwner,
                    ContactNo = model.ContactNo,
                    ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
                    TitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleExtTH = model.TitleExtTH,
                    FirstNameTH = model.FirstNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    LastNameTH = model.LastNameTH,
                    Nickname = model.Nickname,
                    TitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
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
                    IsAgreementOwner = model.IsAgreementOwner,
                    FullnameTH = model.FullnameTH,
                    FullnameEN = model.FullnameEN
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<AdminBookingOwnerDTO> CreateFromModelDraftAsync(models.SAL.BookingOwner model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new AdminBookingOwnerDTO()
                {
                    FromContactID = model.FromContactID,
                    IsMainOwner = model.IsMainOwner,
                    ContactNo = model.ContactNo,
                    ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
                    TitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
                    TitleExtTH = model.TitleExtTH,
                    FirstNameTH = model.FirstNameTH,
                    MiddleNameTH = model.MiddleNameTH,
                    LastNameTH = model.LastNameTH,
                    Nickname = model.Nickname,
                    TitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
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
                    IsAgreementOwner = false
                };

                if (model.ContactTypeMasterCenterID != null)
                    result.ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTypeMasterCenterID).FirstOrDefaultAsync());

                if (model.ContactTitleTHMasterCenterID != null)
                    result.TitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTitleTHMasterCenterID).FirstOrDefaultAsync());

                if (model.ContactTitleENMasterCenterID != null)
                    result.TitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.ContactTitleENMasterCenterID).FirstOrDefaultAsync());

                if (model.NationalMasterCenterID != null)
                    result.National = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.NationalMasterCenterID).FirstOrDefaultAsync());

                if (model.GenderMasterCenterID != null)
                    result.Gender = MST.MasterCenterDropdownDTO.CreateFromModel(await DB.MasterCenters.Where(o => o.ID == model.GenderMasterCenterID).FirstOrDefaultAsync());

                var phones = await DB.ContactPhones.Include(o => o.PhoneType).Where(o => o.ContactID == model.FromContactID).ToListAsync();
                result.ContactPhones = phones.Select(o => BookingOwnerPhoneDTO.CreateFromContactModel(o)).ToList();

                var emails = await DB.ContactEmails.Where(o => o.ContactID == model.FromContactID).ToListAsync();
                result.ContactEmails = emails.Select(o => BookingOwnerEmailDTO.CreateFromContactModel(o)).ToList();

                var addresses = await DB.ContactAddresses
                    .Include(o => o.ContactAddressType)
                    .Include(o => o.Country)
                    .Include(o => o.Province)
                    .Include(o => o.District)
                    .Include(o => o.SubDistrict)
                    .Where(o => o.ContactID == model.FromContactID && o.ContactAddressType.Key == ContactAddressTypeKeys.Citizen).FirstOrDefaultAsync();

                if (addresses != null)
                {
                    result.BookingOwnerAddresses = new List<BookingOwnerAddressDTO>();
                    result.BookingOwnerAddresses.Add(BookingOwnerAddressDTO.CreateFromContactModel(addresses, DB));
                }

                var bookingProject = await DB.Bookings.Where(o => o.ID == model.BookingID).FirstAsync();
                result.Booking = BookingDropdownDTO.CreateFromModel(bookingProject);
                var addressProject = await DB.ContactAddressProjects
                    .Include(o => o.ContactAddress)
                    .Where(o => o.ProjectID == bookingProject.ProjectID && o.ContactAddress.ContactID == model.FromContactID).FirstOrDefaultAsync();
                if (addressProject != null)
                {
                    var contactAddress = await DB.ContactAddresses
                    .Include(o => o.ContactAddressType)
                    .Include(o => o.Country)
                    .Include(o => o.Province)
                    .Include(o => o.District)
                    .Include(o => o.SubDistrict)
                    .Where(o => o.ID == addressProject.ContactAddressID).FirstAsync();

                    if (result.BookingOwnerAddresses == null)
                    {
                        result.BookingOwnerAddresses = new List<BookingOwnerAddressDTO>();
                    }

                    result.BookingOwnerAddresses.Add(BookingOwnerAddressDTO.CreateFromContactModel(contactAddress, DB));
                }

                if (result.BookingOwnerAddresses == null)
                {
                    result.BookingOwnerAddresses = new List<BookingOwnerAddressDTO>();
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(models.DatabaseContext DB, bool isedit)
        {
            ValidateException ex = new ValidateException();

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref models.SAL.BookingOwner model)
        {
            model.FromContactID = this.FromContactID;
            model.IsMainOwner = this.IsMainOwner;
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
            model.IsVIP = this.IsVIP != null && this.IsVIP == true ? true : false;
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
            model.ContactTitleTHMasterCenterID = this.TitleTH?.Id;
            model.ContactTitleENMasterCenterID = this.TitleEN?.Id;
            model.NationalMasterCenterID = this.National?.Id;
            model.CitizenExpireDate = this.CitizenExpireDate;
        }

        public BookingOwnerDTO ToBookingOwnerDTO()
        {
            var result = new BookingOwnerDTO
            {
                Id = this.BookingOwnerId,
                Booking = this.Booking,
                FromContactID = this.FromContactID,
                IsMainOwner = this.IsMainOwner,
                IsAgreementOwner = this.IsAgreementOwner,
                ContactNo = this.ContactNo,
                ContactType = this.ContactType,
                TitleTH = this.TitleTH,
                TitleExtTH = this.TitleExtTH,
                FirstNameTH = this.FirstNameTH,
                MiddleNameTH = this.MiddleNameTH,
                LastNameTH = this.LastNameTH,
                Nickname = this.Nickname,
                TitleEN = this.TitleEN,
                TitleExtEN = this.TitleExtEN,
                FirstNameEN = this.FirstNameEN,
                MiddleNameEN = this.MiddleNameEN,
                LastNameEN = this.LastNameEN,
                CitizenIdentityNo = this.CitizenIdentityNo,
                CitizenExpireDate = this.CitizenExpireDate,
                National = this.National,
                Gender = this.Gender,
                TaxID = this.TaxID,
                PhoneNumber = this.PhoneNumber,
                PhoneNumberExt = this.PhoneNumberExt,
                ContactFirstName = this.ContactFirstName,
                ContactLastname = this.ContactLastname,
                WeChat = this.WeChat,
                WhatsApp = this.WhatsApp,
                LineID = this.LineID,
                BirthDate = this.BirthDate,
                IsVIP = this.IsVIP,
                IsThaiNationality = this.IsThaiNationality,
                UpdateDate = this.UpdateDate,
                ContactPhones = this.ContactPhones,
                ContactEmails = this.ContactEmails,
                BookingOwnerAddresses = this.BookingOwnerAddresses,
                IsNew = this.IsNew,
                isSelected = this.isSelected,
                FullnameTH = this.FullnameTH,
                FullnameEN = this.FullnameEN
            };
            return result;
        }
    }

}
