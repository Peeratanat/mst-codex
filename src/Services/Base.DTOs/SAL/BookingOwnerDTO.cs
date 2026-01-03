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
    /// ผู้จอง
    /// Model: BookingOwner
    /// </summary>
    public class BookingOwnerDTO
    {
        /// <summary>
        /// ID ของ Booking Owner
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ลำดับของผู้จอง
        /// </summary>
        public int? Order { get; set; }
        /// <summary>
        /// ใบจอง
        /// </summary>
        public BookingDropdownDTO Booking { get; set; }
        /// <summary>
        /// มาจาก Contact (กรณีดึงข้อมูลมาจาก Contact)
        /// </summary>
        public Guid? FromContactID { get; set; }
        /// <summary>
        /// ผู้จองหลัก
        /// </summary>
        [Description("ผู้จองหลัก")]
        public bool IsMainOwner { get; set; }
        /// <summary>
        /// เป็นผู้ทำสัญญาในใบจอง (true = ผู้ทำสัญญา/ false = ผู้จอง)
        /// </summary>
        [Description("เป็นผู้จองหรือผู้ทำสัญญา")]
        public bool IsAgreementOwner { get; set; }
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
        public MST.MasterCenterDropdownDTO TitleTH { get; set; }
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
        public MST.MasterCenterDropdownDTO TitleEN { get; set; }
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
        [Description("WeChat ID")]
        public string WeChat { get; set; }
        /// <summary>
        /// WhatsApp ID
        /// </summary>
        [Description("WhatsApp ID")]
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
        public bool? IsVIP { get; set; }
        /// <summary>
        /// เป็นคนไทยหรือไม่
        /// </summary>
        public bool? IsThaiNationality { get; set; }

        [Description("Update ล่าสุด")]
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// เบอร์โทรศัพท์ของผู้จอง
        /// </summary>
        [Description("เบอร์โทรศัพท์")]
        public List<BookingOwnerPhoneDTO> ContactPhones { get; set; }
        /// <summary>
        /// อีเมลของผู้จอง
        /// </summary>
        [Description("อีเมล")]
        public List<BookingOwnerEmailDTO> ContactEmails { get; set; }
        /// <summary>
        /// ที่อยู่ของผู้จอง (ติดต่อได้/บัตรประชาชน)
        /// </summary>
        [Description("ที่อยู่")]
        public List<BookingOwnerAddressDTO> BookingOwnerAddresses { get; set; }

        /// <summary>
        /// true = เพิ่มชื่อยังแบบยังไม่ได้บันทึก
        /// </summary>
        [Description("เพิ่มชื่อยังแบบยังไม่ได้บันทึก")]
        public bool? IsNew { get; set; }

        /// <summary>
        /// เลือกคนที่จะโอนสิทธิ์
        /// </summary>
        [Description("เลือกคนที่จะโอนสิทธิ์")]
        public bool? isSelected { get; set; }
        /// <summary>
        /// ชื่อเต็มภาษาไทย
        /// </summary>
        [Description("ชื่อเต็มภาษาไทย")]
        public string FullnameTH { get; set; }
        /// <summary>
        /// ชื่อเต็มภาษาEng
        /// </summary>
        [Description("ชื่อเต็มภาษาEng")]
        public string FullnameEN { get; set; }

        /// <summary>
		/// ลูกค้า IsVVIP
		/// </summary>
		public bool IsVVIP { get; set; }

        /// <summary>
        /// VVIPExcomApprover
        /// </summary>
        public string VVIPExcomApprover { get; set; }

        public async static Task<BookingOwnerDTO> CreateFromModelAsync(models.SAL.BookingOwner model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new BookingOwnerDTO()
                {
                    Id = model.ID,
                    Booking = BookingDropdownDTO.CreateFromModel(model.Booking),
                    Order = model.Order,
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
                    FullnameTH=model.FullnameTH,
                    FullnameEN= model.FullnameEN
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

        public async static Task<BookingOwnerDTO> CreateFromModelDraftAsync(models.SAL.BookingOwner model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new BookingOwnerDTO()
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

                if(addresses != null)
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

                    if(result.BookingOwnerAddresses == null)
                    {
                        result.BookingOwnerAddresses = new List<BookingOwnerAddressDTO>();
                    }

                    result.BookingOwnerAddresses.Add(BookingOwnerAddressDTO.CreateFromContactModel(contactAddress, DB));
                }

                if (result.BookingOwnerAddresses==null)
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

            #region Booking Owner

            var dupContactbooking = await DB.BookingOwners.Where(o => o.BookingID == this.Booking.Id && o.FromContactID == this.FromContactID&&o.IsAgreementOwner==false).FirstOrDefaultAsync();
            var dupContactagreement = await DB.BookingOwners.Where(o => o.BookingID == this.Booking.Id && o.FromContactID == this.FromContactID && o.IsAgreementOwner == true).FirstOrDefaultAsync();

            if (this.IsAgreementOwner)
            {
                if (dupContactagreement != null && !isedit)
                {
                    // Case Duplicate Contact
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0170").FirstAsync();
                    var msg = errMsg.Message;
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (this.ContactEmails.Count > 0)
                    {
                        var isEmailNull = this.ContactEmails.Any(o => string.IsNullOrEmpty(o.Email));
                        if (isEmailNull)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                            string desc = typeof(BookingOwnerEmailDTO).GetProperty(nameof(BookingOwnerEmailDTO.Email)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.ContactEmails)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                            string desc = typeof(BookingOwnerPhoneDTO).GetProperty(nameof(BookingOwnerPhoneDTO.PhoneType)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }

                        var isPhoneNumberNull = this.ContactPhones.Any(o => string.IsNullOrEmpty(o.PhoneNumber));
                        if (isPhoneNumberNull)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                            string desc = typeof(BookingOwnerPhoneDTO).GetProperty(nameof(BookingOwnerPhoneDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                        else
                        {
                            var isNumberNull = this.ContactPhones.Any(o => !o.PhoneNumber.IsOnlyNumber());
                            if (isNumberNull)
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                                string desc = typeof(BookingOwnerPhoneDTO).GetProperty(nameof(BookingOwnerPhoneDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                                    string desc = typeof(BookingOwnerPhoneDTO).GetProperty(nameof(BookingOwnerPhoneDTO.CountryCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                                else
                                {
                                    var isInvalidCode = foreignList.Any(o => !o.CountryCode.IsOnlyNumberWithSpecialCharacter(false, "+"));
                                    if (isInvalidCode)
                                    {
                                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
                                        string desc = typeof(BookingOwnerPhoneDTO).GetProperty(nameof(BookingOwnerPhoneDTO.CountryCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                                        var msg = errMsg.Message.Replace("[field]", desc);
                                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                    }
                                }
                            }
                        }

                        List<BookingOwnerPhoneList> phoneList = new List<BookingOwnerPhoneList>();
                        foreach (var phone in this.ContactPhones)
                        {
                            if (!string.IsNullOrEmpty(phone.PhoneNumber) && (phone.PhoneType != null))
                            {
                                var isPhoneExits = phoneList.Where(o => o.PhoneNumber == phone.PhoneNumber && o.PhoneTypeMastercenterID == phone.PhoneType.Id).Any();

                                if (isPhoneExits)
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.ContactPhones)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                                    break;
                                }
                                else
                                {
                                    phoneList.Add(new BookingOwnerPhoneList
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
                            string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.WhatsApp)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (!string.IsNullOrEmpty(this.WeChat))
                    {
                        if (!this.WeChat.CheckLang(false, true, true, false))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.WeChat)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (!string.IsNullOrEmpty(this.LineID))
                    {
                        if (!this.LineID.CheckLang(false, true, true, false))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.LineID)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (this.ContactType == null)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.ContactType)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.TaxID)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                            else if (!this.TaxID.IsOnlyNumber())
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.TaxID)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }

                            if (!string.IsNullOrEmpty(this.PhoneNumber))
                            {
                                if (!this.PhoneNumber.IsOnlyNumber())
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                            if (!string.IsNullOrEmpty(this.PhoneNumberExt))
                            {
                                if (!this.PhoneNumberExt.IsOnlyNumber())
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.PhoneNumberExt)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                            if (!string.IsNullOrEmpty(this.ContactFirstName))
                            {
                                if (!this.ContactFirstName.CheckAllLang(false, true, false, null, " "))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.ContactFirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                            if (!string.IsNullOrEmpty(this.ContactLastname))
                            {
                                if (!this.ContactFirstName.CheckAllLang(false, true, false, null, " "))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.ContactFirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }

                            if (string.IsNullOrEmpty(this.FirstNameTH))
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                            else
                            {
                                //if (!this.FirstNameTH.CheckLang(true, true, true, false))
                                //{
                                //    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0017").FirstAsync();
                                //    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                //    var msg = errMsg.Message.Replace("[field]", desc);
                                //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                //}
                            }

                            if (string.IsNullOrEmpty(this.FirstNameEN))
                            {
                                //var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                //string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                //var msg = errMsg.Message.Replace("[field]", desc);
                                //ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                            else
                            {
                                if (!this.FirstNameEN.CheckLang(false, true, true, false))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                        }
                        else
                        {
                            var isCheckNation = false;
                            #region Citizen
                            if (this.National?.Key == NationalKeys.Thai)
                            {
                                if (string.IsNullOrEmpty(this.CitizenIdentityNo))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.CitizenIdentityNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                            if (!string.IsNullOrEmpty(this.CitizenIdentityNo))
                            {
                                if (this.National == null)
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.National)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                                            string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.CitizenIdentityNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                                            var msg = errMsg.Message.Replace("[field]", desc);
                                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region TH
                            if (this.TitleTH == null)
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.TitleTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                            else
                            {
                                var externalTitleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH" && o.Key == "-1").Select(o => o.ID).FirstAsync();
                                if (this.TitleTH.Id == externalTitleID && string.IsNullOrEmpty(this.TitleExtTH))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.TitleExtTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }

                            if (string.IsNullOrEmpty(this.FirstNameTH))
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                            //else
                            //{
                            //    if (!this.FirstNameTH.CheckAllLang(false, true, false, null, " "))
                            //    {
                            //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                            //        string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                            //        var msg = errMsg.Message.Replace("[field]", desc);
                            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            //    }
                            //}

                            if (string.IsNullOrEmpty(this.LastNameTH))
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.LastNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                            //else
                            //{
                            //    if (!this.LastNameTH.CheckAllLang(false, true, false, null, " "))
                            //    {
                            //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                            //        string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.LastNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                            //        var msg = errMsg.Message.Replace("[field]", desc);
                            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            //    }
                            //}

                            if (!string.IsNullOrEmpty(this.MiddleNameTH))
                            {
                                if (!this.MiddleNameTH.CheckAllLang(true, true, false, null, " "))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.MiddleNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }

                            if (!string.IsNullOrEmpty(this.Nickname))
                            {
                                if (!this.Nickname.CheckAllLang(false, true, false, null, " "))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.Nickname)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                            #endregion

                            #region EN
                            if (this.TitleEN != null)
                            {
                                var externalTitleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == models.MasterKeys.MasterCenterGroupKeys.ContactTitleEN && o.Key == "-1").Select(o => o.ID).FirstAsync();
                                if (this.TitleEN.Id == externalTitleID && string.IsNullOrEmpty(this.TitleExtEN))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.TitleExtEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }

                            if (!string.IsNullOrEmpty(this.FirstNameEN))
                            {
                                if (!this.FirstNameEN.CheckLang(false, true, true, false, null, " "))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }

                            if (!string.IsNullOrEmpty(this.LastNameEN))
                            {
                                if (!this.LastNameEN.CheckLang(false, true, true, false, null, " "))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.LastNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }

                            if (!string.IsNullOrEmpty(this.MiddleNameEN))
                            {
                                if (!this.MiddleNameEN.CheckLang(false, true, true, false, null, " "))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.MiddleNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                            #endregion

                            if (!isCheckNation)
                            {
                                if (this.National == null)
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.National)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }

                            if (this.Gender == null)
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.Gender)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                        }
                    }
                }
            }
            else
            {
                if (dupContactbooking != null && !isedit)
                {
                    // Case Duplicate Contact
                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0107").FirstAsync();
                    var msg = errMsg.Message;
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                else
                {
                    if (this.ContactEmails.Count > 0)
                    {
                        var isEmailNull = this.ContactEmails.Any(o => string.IsNullOrEmpty(o.Email));
                        if (isEmailNull)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                            string desc = typeof(BookingOwnerEmailDTO).GetProperty(nameof(BookingOwnerEmailDTO.Email)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.ContactEmails)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                            string desc = typeof(BookingOwnerPhoneDTO).GetProperty(nameof(BookingOwnerPhoneDTO.PhoneType)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }

                        var isPhoneNumberNull = this.ContactPhones.Any(o => string.IsNullOrEmpty(o.PhoneNumber));
                        if (isPhoneNumberNull)
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                            string desc = typeof(BookingOwnerPhoneDTO).GetProperty(nameof(BookingOwnerPhoneDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                        else
                        {
                            var isNumberNull = this.ContactPhones.Any(o => !o.PhoneNumber.IsOnlyNumber());
                            if (isNumberNull)
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                                string desc = typeof(BookingOwnerPhoneDTO).GetProperty(nameof(BookingOwnerPhoneDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                                    string desc = typeof(BookingOwnerPhoneDTO).GetProperty(nameof(BookingOwnerPhoneDTO.CountryCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                                else
                                {
                                    var isInvalidCode = foreignList.Any(o => !o.CountryCode.IsOnlyNumberWithSpecialCharacter(false, "+"));
                                    if (isInvalidCode)
                                    {
                                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
                                        string desc = typeof(BookingOwnerPhoneDTO).GetProperty(nameof(BookingOwnerPhoneDTO.CountryCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                                        var msg = errMsg.Message.Replace("[field]", desc);
                                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                    }
                                }
                            }
                        }

                        List<BookingOwnerPhoneList> phoneList = new List<BookingOwnerPhoneList>();
                        foreach (var phone in this.ContactPhones)
                        {
                            if (!string.IsNullOrEmpty(phone.PhoneNumber) && (phone.PhoneType != null))
                            {
                                var isPhoneExits = phoneList.Where(o => o.PhoneNumber == phone.PhoneNumber && o.PhoneTypeMastercenterID == phone.PhoneType.Id).Any();

                                if (isPhoneExits)
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.ContactPhones)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

                                    break;
                                }
                                else
                                {
                                    phoneList.Add(new BookingOwnerPhoneList
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
                            string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.WhatsApp)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (!string.IsNullOrEmpty(this.WeChat))
                    {
                        if (!this.WeChat.CheckLang(false, true, true, false))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.WeChat)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (!string.IsNullOrEmpty(this.LineID))
                    {
                        if (!this.LineID.CheckLang(false, true, true, false))
                        {
                            var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.LineID)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }

                    if (this.ContactType == null)
                    {
                        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.ContactType)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.TaxID)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                            else if (!this.TaxID.IsOnlyNumber())
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.TaxID)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }

                            if (!string.IsNullOrEmpty(this.PhoneNumber))
                            {
                                if (!this.PhoneNumber.IsOnlyNumber())
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                            if (!string.IsNullOrEmpty(this.PhoneNumberExt))
                            {
                                if (!this.PhoneNumberExt.IsOnlyNumber())
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.PhoneNumberExt)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                            if (!string.IsNullOrEmpty(this.ContactFirstName))
                            {
                                if (!this.ContactFirstName.CheckAllLang(false, true, false, null, " "))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.ContactFirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                            if (!string.IsNullOrEmpty(this.ContactLastname))
                            {
                                if (!this.ContactFirstName.CheckAllLang(false, true, false, null, " "))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.ContactFirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }

                            if (string.IsNullOrEmpty(this.FirstNameTH))
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                            else
                            {
                                //if (!this.FirstNameTH.CheckLang(true, true, true, false))
                                //{
                                //    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0017").FirstAsync();
                                //    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                //    var msg = errMsg.Message.Replace("[field]", desc);
                                //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                //}
                            }

                            if (string.IsNullOrEmpty(this.FirstNameEN))
                            {
                                //var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                //string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                                //var msg = errMsg.Message.Replace("[field]", desc);
                                //ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                            else
                            {
                                if (!this.FirstNameEN.CheckLang(false, true, true, false))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                        }
                        else
                        {
                            var isCheckNation = false;
                            #region Citizen
                            
                            if (this.National?.Key == NationalKeys.Thai)
                            {
                                if (string.IsNullOrEmpty(this.CitizenIdentityNo))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.CitizenIdentityNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                           

                            if (!string.IsNullOrEmpty(this.CitizenIdentityNo))
                            {
                                if (this.National == null)
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.National)).GetCustomAttribute<DescriptionAttribute>().Description;
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
                                            string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.CitizenIdentityNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                                            var msg = errMsg.Message.Replace("[field]", desc);
                                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region TH
                            if (this.TitleTH == null)
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.TitleTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                            else
                            {
                                var externalTitleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH" && o.Key == "-1").Select(o => o.ID).FirstAsync();
                                if (this.TitleTH.Id == externalTitleID && string.IsNullOrEmpty(this.TitleExtTH))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.TitleExtTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }

                            if (string.IsNullOrEmpty(this.FirstNameTH))
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                            //else
                            //{
                            //    if (!this.FirstNameTH.CheckAllLang(false, true, false, null, " "))
                            //    {
                            //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                            //        string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                            //        var msg = errMsg.Message.Replace("[field]", desc);
                            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            //    }
                            //}

                            if (string.IsNullOrEmpty(this.LastNameTH))
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.LastNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
                            //else
                            //{
                            //    if (!this.LastNameTH.CheckAllLang(false, true, false, null, " "))
                            //    {
                            //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                            //        string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.LastNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                            //        var msg = errMsg.Message.Replace("[field]", desc);
                            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            //    }
                            //}

                            //if (!string.IsNullOrEmpty(this.MiddleNameTH))
                            //{
                            //    if (!this.MiddleNameTH.CheckAllLang(false, true, false, null, " "))
                            //    {
                            //        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                            //        string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.MiddleNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                            //        var msg = errMsg.Message.Replace("[field]", desc);
                            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            //    }
                            //}

                            if (!string.IsNullOrEmpty(this.Nickname))
                            {
                                if (!this.Nickname.CheckAllLang(false, true, false, null, " "))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.Nickname)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                            #endregion

                            #region EN
                            if (this.TitleEN != null)
                            {
                                var externalTitleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == models.MasterKeys.MasterCenterGroupKeys.ContactTitleEN && o.Key == "-1").Select(o => o.ID).FirstAsync();
                                if (this.TitleEN.Id == externalTitleID && string.IsNullOrEmpty(this.TitleExtEN))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.TitleExtEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }

                            if (!string.IsNullOrEmpty(this.FirstNameEN))
                            {
                                if (!this.FirstNameEN.CheckLang(false, true, true, false, null, " "))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }

                            if (!string.IsNullOrEmpty(this.LastNameEN))
                            {
                                if (!this.LastNameEN.CheckLang(false, true, true, false, null, " "))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.LastNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }

                            if (!string.IsNullOrEmpty(this.MiddleNameEN))
                            {
                                if (!this.MiddleNameEN.CheckLang(false, true, true, false, null, " "))
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.MiddleNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }
                            #endregion

                            if (!isCheckNation)
                            {
                                if (this.National == null)
                                {
                                    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                    string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.National)).GetCustomAttribute<DescriptionAttribute>().Description;
                                    var msg = errMsg.Message.Replace("[field]", desc);
                                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                                }
                            }

                            if (this.Gender == null)
                            {
                                var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                                string desc = this.GetType().GetProperty(nameof(BookingOwnerDTO.Gender)).GetCustomAttribute<DescriptionAttribute>().Description;
                                var msg = errMsg.Message.Replace("[field]", desc);
                                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                            }
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
            model.IsVIP = this.IsVIP !=null && this.IsVIP==true ? true:false;
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
    }

    public class BookingOwnerPhoneList
    {
        public string PhoneNumber { get; set; }
        public Guid? PhoneTypeMastercenterID { get; set; }
    }
}
