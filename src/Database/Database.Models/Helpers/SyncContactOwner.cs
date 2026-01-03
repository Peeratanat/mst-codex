using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models.CTM;
using Database.Models.MasterKeys;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;

namespace Database.Models.Helpers
{
    public class SyncContactOwner
    {
        private readonly DatabaseContext DB;

        public SyncContactOwner(DatabaseContext db)
        {
            this.DB = db;
        }

        public async Task SyncOwnerAsync(Contact contact, BaseEntity owner, Type main)
        {

            if (main == typeof(Contact))
            {
                var type = owner.GetType();
                switch (type)
                {
                    case Type bookingType when bookingType == typeof(BookingOwner):
                        {
                            var bookingOwner = (BookingOwner)owner;
                            var bookingModel = await DB.Bookings
                                .Include(o => o.Unit)
                                .ThenInclude(o => o.UnitStatus)
                                .Where(o => o.ID == bookingOwner.BookingID).FirstOrDefaultAsync();



                            var BookingPayAmount = await DB.PaymentItems
                          .Include(o => o.MasterPriceItem)
                          .Include(o => o.Payment)
                          .Where(o => o.MasterPriceItem.Key == MasterPriceItemKeys.BookingAmount && o.Payment.BookingID == bookingOwner.BookingID && o.Payment.IsCancel == false).SumAsync(o => o.PayAmount);

                            var BookingAmount = await DB.UnitPrices
                                  .Include(o => o.UnitPriceStage)
                                  .Where(o => o.UnitPriceStage.Key == UnitPriceStageKeys.Booking && o.BookingID == bookingOwner.BookingID)
                                  .Select(o => o.BookingAmount).FirstOrDefaultAsync();

                            bool? isPaid = false;
                            if (BookingPayAmount == 0)
                            {
                                isPaid = null;
                            }
                            else if (BookingPayAmount >= BookingAmount)
                            {
                                isPaid = true;
                            }
                            else
                            {
                                isPaid = false;
                            }

                            if (bookingModel != null)
                            {
                                if ((bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForConfirmBooking || bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForAgreement)
                                    && isPaid == null
                                    && bookingOwner.IsAgreementOwner == false)
                                {
                                    ToBookingOwnerModel(ref bookingOwner, contact);
                                    DB.Entry(bookingOwner).State = EntityState.Modified;
                                    await DB.SaveChangesAsync();

                                    await UpdateBookingOwnerInfoAsync(bookingOwner, bookingModel.ProjectID.Value, contact);
                                }

                                if (bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForAgreement
                                    && bookingModel.IsPaid == false
                                    && bookingOwner.IsAgreementOwner == true)
                                {
                                    ToBookingOwnerModel(ref bookingOwner, contact);
                                    DB.Entry(bookingOwner).State = EntityState.Modified;
                                    await DB.SaveChangesAsync();

                                    await UpdateBookingOwnerInfoAsync(bookingOwner, bookingModel.ProjectID.Value, contact);
                                }
                            }
                            break;
                        }

                    case Type agreementType when agreementType == typeof(AgreementOwner):
                        {
                            var agreementOwner = (AgreementOwner)owner;
                            var agreementModel = await DB.Agreements.Where(o => o.ID == agreementOwner.AgreementID).FirstOrDefaultAsync();

                            if (agreementModel != null)
                            {
                                var bookingModel = await DB.Bookings
                                .Include(o => o.Unit)
                                .ThenInclude(o => o.UnitStatus)
                                .Where(o => o.ID == agreementModel.BookingID).FirstOrDefaultAsync();

                                if (bookingModel != null)
                                {
                                    if (bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForAgreement
                                    && (bookingModel.IsPaid == true || bookingModel.IsPaid == false))
                                    {
                                        ToAgreementOwnerModel(ref agreementOwner, contact);
                                        DB.Entry(agreementOwner).State = EntityState.Modified;
                                        await DB.SaveChangesAsync();

                                        await UpdateAgreementOwnerInfoAsync(agreementOwner, bookingModel.ProjectID.Value, contact);
                                    }
                                }
                            }
                            break;
                        }
                }
            }
            else if (main == typeof(BookingOwner))
            {
                var bookingOwner = (BookingOwner)owner;
                Guid? test = bookingOwner.BookingID;
                var bookingModel = await DB.Bookings
                            .Include(o => o.Unit)
                            .ThenInclude(o => o.UnitStatus)
                            .Where(o => o.ID == test).FirstOrDefaultAsync();
                if (bookingModel != null)
                {


                    var BookingPayAmount = await DB.PaymentItems
                  .Include(o => o.MasterPriceItem)
                  .Include(o => o.Payment)
                  .Where(o => o.MasterPriceItem.Key == MasterPriceItemKeys.BookingAmount && o.Payment.BookingID == bookingOwner.BookingID && o.Payment.IsCancel == false).SumAsync(o => o.PayAmount);

                    var BookingAmount = await DB.UnitPrices
                          .Include(o => o.UnitPriceStage)
                          .Where(o => o.UnitPriceStage.Key == UnitPriceStageKeys.Booking && o.BookingID == bookingOwner.BookingID)
                          .Select(o => o.BookingAmount).FirstOrDefaultAsync();

                    bool? isPaid = false;
                    if (BookingPayAmount == 0)
                    {
                        isPaid = null;
                    }
                    else if (BookingPayAmount >= BookingAmount)
                    {
                        isPaid = true;
                    }
                    else
                    {
                        isPaid = false;
                    }


                    if ((bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForConfirmBooking || bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForAgreement)
                    && isPaid == null
                    && bookingOwner.IsAgreementOwner == false)
                    {
                        BookingToContactModel(ref contact, bookingOwner);
                        DB.Entry(contact).State = EntityState.Modified;
                        await DB.SaveChangesAsync();

                        await UpdateContactInfoAsync(bookingOwner.ID, bookingModel.ProjectID.Value, contact, true);
                    }

                    if ((bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForAgreement)
                        && bookingOwner.IsAgreementOwner == true)
                    {
                        BookingToContactModel(ref contact, bookingOwner);
                        DB.Entry(contact).State = EntityState.Modified;
                        await DB.SaveChangesAsync();

                        await UpdateContactInfoAsync(bookingOwner.ID, bookingModel.ProjectID.Value, contact, true);
                    }
                }
            }
            else if (main == typeof(AgreementOwner))
            {
                var agreementOwner = (AgreementOwner)owner;
                var agreementModel = await DB.Agreements.Include(o => o.AgreementStatus).Where(o => o.ID == agreementOwner.AgreementID).FirstOrDefaultAsync();
                if (agreementModel != null)
                {
                    var bookingModel = await DB.Bookings
                                .Include(o => o.Unit)
                                .ThenInclude(o => o.UnitStatus)
                                .Where(o => o.ID == agreementModel.BookingID).FirstOrDefaultAsync();
                    if (bookingModel != null)
                    {
                        if (bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForAgreement
                        && (bookingModel.IsPaid ?? false) == false)
                        {
                            AgreementToContactModel(ref contact, agreementOwner);
                            DB.Entry(contact).State = EntityState.Modified;
                            await DB.SaveChangesAsync();

                            await UpdateContactInfoAsync(agreementOwner.ID, bookingModel.ProjectID.Value, contact, false);
                        }

                        if (bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForAgreement
                                && bookingModel.IsPaid == true
                                && agreementModel.AgreementStatus.Key == AgreementStatusKeys.WaitingForContract
                                && agreementModel.AgreementNo == null)
                        {
                            AgreementToContactModel(ref contact, agreementOwner);
                            DB.Entry(contact).State = EntityState.Modified;
                            await DB.SaveChangesAsync();

                            await UpdateContactInfoAsync(agreementOwner.ID, bookingModel.ProjectID.Value, contact, false);
                        }

                        if (bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForTransfer
                                && agreementModel.AgreementStatus.Key == AgreementStatusKeys.WaitingForSignContract)
                        {
                            AgreementToContactModel(ref contact, agreementOwner);
                            DB.Entry(contact).State = EntityState.Modified;
                            await DB.SaveChangesAsync();

                            await UpdateContactInfoAsync(agreementOwner.ID, bookingModel.ProjectID.Value, contact, false);
                        }

                        if (bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForTransfer
                                && agreementModel.AgreementStatus.Key == AgreementStatusKeys.WaitingForTransfer
                                && agreementModel.AgreementNo != null)
                        {
                            AgreementToContactModel(ref contact, agreementOwner);
                            DB.Entry(contact).State = EntityState.Modified;
                            await DB.SaveChangesAsync();

                            await UpdateContactInfoAsync(agreementOwner.ID, bookingModel.ProjectID.Value, contact, false);
                        }
                    }
                }
            }
        }

        public async Task SyncOwnerFromAdminAsync(Contact contact, BaseEntity owner, Type main)
        {

            if (main == typeof(Contact))
            {
                var type = owner.GetType();
                switch (type)
                {
                    case Type bookingType when bookingType == typeof(BookingOwner):
                        {
                            var bookingOwner = (BookingOwner)owner;
                            var bookingModel = await DB.Bookings
                                .Include(o => o.Unit)
                                .ThenInclude(o => o.UnitStatus)
                                .Where(o => o.ID == bookingOwner.BookingID).FirstOrDefaultAsync();



                            var BookingPayAmount = await DB.PaymentItems
                          .Include(o => o.MasterPriceItem)
                          .Include(o => o.Payment)
                          .Where(o => o.MasterPriceItem.Key == MasterPriceItemKeys.BookingAmount && o.Payment.BookingID == bookingOwner.BookingID && o.Payment.IsCancel == false).SumAsync(o => o.PayAmount);

                            var BookingAmount = await DB.UnitPrices
                                  .Include(o => o.UnitPriceStage)
                                  .Where(o => o.UnitPriceStage.Key == UnitPriceStageKeys.Booking && o.BookingID == bookingOwner.BookingID)
                                  .Select(o => o.BookingAmount).FirstOrDefaultAsync();

                            bool? isPaid = false;
                            if (BookingPayAmount == 0)
                            {
                                isPaid = null;
                            }
                            else if (BookingPayAmount >= BookingAmount)
                            {
                                isPaid = true;
                            }
                            else
                            {
                                isPaid = false;
                            }

                            if (bookingModel != null)
                            {
                                if ((bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForConfirmBooking || bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForAgreement)
                                    && isPaid == null
                                    && bookingOwner.IsAgreementOwner == false)
                                {
                                    ToBookingOwnerModel(ref bookingOwner, contact);
                                    DB.Entry(bookingOwner).State = EntityState.Modified;
                                    await DB.SaveChangesAsync();

                                    await UpdateBookingOwnerInfoAsync(bookingOwner, bookingModel.ProjectID.Value, contact);
                                }

                                if (bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForAgreement
                                    && bookingModel.IsPaid == false
                                    && bookingOwner.IsAgreementOwner == true)
                                {
                                    ToBookingOwnerModel(ref bookingOwner, contact);
                                    DB.Entry(bookingOwner).State = EntityState.Modified;
                                    await DB.SaveChangesAsync();

                                    await UpdateBookingOwnerInfoAsync(bookingOwner, bookingModel.ProjectID.Value, contact);
                                }
                            }
                            break;
                        }

                    case Type agreementType when agreementType == typeof(AgreementOwner):
                        {
                            var agreementOwner = (AgreementOwner)owner;
                            var agreementModel = await DB.Agreements.Where(o => o.ID == agreementOwner.AgreementID).FirstOrDefaultAsync();

                            if (agreementModel != null)
                            {
                                var bookingModel = await DB.Bookings
                                .Include(o => o.Unit)
                                .ThenInclude(o => o.UnitStatus)
                                .Where(o => o.ID == agreementModel.BookingID).FirstOrDefaultAsync();

                                if (bookingModel != null)
                                {
                                    if (bookingModel.Unit.UnitStatus.Key == UnitStatusKeys.WaitingForAgreement
                                    && (bookingModel.IsPaid == true || bookingModel.IsPaid == false))
                                    {
                                        ToAgreementOwnerModel(ref agreementOwner, contact);
                                        DB.Entry(agreementOwner).State = EntityState.Modified;
                                        await DB.SaveChangesAsync();

                                        await UpdateAgreementOwnerInfoAsync(agreementOwner, bookingModel.ProjectID.Value, contact);
                                    }
                                }
                            }
                            break;
                        }
                }
            }
            else if (main == typeof(BookingOwner))
            {
                var bookingOwner = (BookingOwner)owner;
                Guid? test = bookingOwner.BookingID;
                var bookingModel = await DB.Bookings
                            .Include(o => o.Unit)
                            .ThenInclude(o => o.UnitStatus)
                            .Where(o => o.ID == test).FirstOrDefaultAsync();
                if (bookingModel != null)
                { 
                    BookingToContactModel(ref contact, bookingOwner);
                    DB.Entry(contact).State = EntityState.Modified;
                    await DB.SaveChangesAsync();

                    await UpdateContactInfoAsync(bookingOwner.ID, bookingModel.ProjectID.Value, contact, true);
                }
            }
            else if (main == typeof(AgreementOwner))
            {
                var agreementOwner = (AgreementOwner)owner;
                var agreementModel = await DB.Agreements.Include(o => o.AgreementStatus).Where(o => o.ID == agreementOwner.AgreementID).FirstOrDefaultAsync();
                if (agreementModel != null)
                { 
                    AgreementToContactModel(ref contact, agreementOwner);
                    DB.Entry(contact).State = EntityState.Modified;
                    await DB.SaveChangesAsync();

                    await UpdateContactInfoAsync(agreementOwner.ID, agreementModel.ProjectID.Value, contact, false);
                         
                }
            }
        }

        private void ToBookingOwnerModel(ref BookingOwner model, Contact contact)
        {
            var contactProps = contact.GetType().GetProperties();
            var props = model.GetType().GetProperties();
            foreach (var contactProp in contactProps)
            {
                foreach (var prop in props)
                {
                    if (contactProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(model, contactProp.GetValue(contact));
                    }
                }
            }
        }

        private void BookingToContactModel(ref Contact model, BookingOwner owner)
        {
            var ownerProps = owner.GetType().GetProperties();
            var props = model.GetType().GetProperties();
            foreach (var ownerProp in ownerProps)
            {
                foreach (var prop in props)
                {
                    if (ownerProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order" && prop.Name!= "Created" && prop.Name != "CreatedByUserID"))
                    {
                        prop.SetValue(model, ownerProp.GetValue(owner));
                    }
                }
            }
        }

        private void BookingToContactAdreessModel(ref ContactAddress model, BookingOwnerAddress address)
        {
            var ownerProps = address.GetType().GetProperties();
            var props = model.GetType().GetProperties();
            foreach (var ownerProp in ownerProps)
            {
                foreach (var prop in props)
                {
                    if (ownerProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(model, ownerProp.GetValue(address));
                    }
                }
            }
        }

        private void ToAgreementOwnerModel(ref AgreementOwner model, Contact contact)
        {
            var contactProps = contact.GetType().GetProperties();
            var props = model.GetType().GetProperties();
            foreach (var contactProp in contactProps)
            {
                foreach (var prop in props)
                {
                    if (contactProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(model, contactProp.GetValue(contact));
                    }
                }
            }
        }

        private void AgreementToContactModel(ref Contact model, AgreementOwner owner)
        {
            var ownerProps = owner.GetType().GetProperties();
            var props = model.GetType().GetProperties();
            foreach (var ownerProp in ownerProps)
            {
                foreach (var prop in props)
                {
                    if (ownerProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order" && prop.Name != "Created" && prop.Name != "CreatedByUserID"))
                    {
                        prop.SetValue(model, ownerProp.GetValue(owner));
                    }
                }
            }
        }

        private void AgreementToContactAdreessModel(ref ContactAddress model, AgreementOwnerAddress address)
        {
            var ownerProps = address.GetType().GetProperties();
            var props = model.GetType().GetProperties();
            foreach (var ownerProp in ownerProps)
            {
                foreach (var prop in props)
                {
                    if (ownerProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(model, ownerProp.GetValue(address));
                    }
                }
            }
        }

        private void ContactToBookingOwnerAdreessModel(ref BookingOwnerAddress model, ContactAddress address)
        {
            var ownerProps = address.GetType().GetProperties();
            var props = model.GetType().GetProperties();
            foreach (var ownerProp in ownerProps)
            {
                foreach (var prop in props)
                {
                    if (ownerProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(model, ownerProp.GetValue(address));
                    }
                }
            }
        }

        private void ContactToAgreementOwnerAdreessModel(ref AgreementOwnerAddress model, ContactAddress address)
        {
            var ownerProps = address.GetType().GetProperties();
            var props = model.GetType().GetProperties();
            foreach (var ownerProp in ownerProps)
            {
                foreach (var prop in props)
                {
                    if (ownerProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(model, ownerProp.GetValue(address));
                    }
                }
            }
        }

        private async Task UpdateContactInfoAsync(Guid ownerId, Guid projectId, Contact contact, bool isBookingOwner)
        {
            if (isBookingOwner)
            {
                #region Email
                var bookingOwnerEmails = await DB.BookingOwnerEmails.Where(o => o.BookingOwnerID == ownerId).ToListAsync();
                var contactEmails = await DB.ContactEmails.Where(e => e.ContactID == contact.ID).ToListAsync();
                List<ContactEmail> contactEmailAdd = new List<ContactEmail>();
                List<ContactEmail> contactEmailUpdate = new List<ContactEmail>();
                List<ContactEmail> contactEmailDelete = new List<ContactEmail>();

                foreach (var item in bookingOwnerEmails)
                {
                    if (contactEmails.Any(e => e.ID == item.FromContactEmailID))
                    {
                        var contactEmailItem = await DB.ContactEmails.Where(e => e.ContactID == contact.ID && e.ID == item.FromContactEmailID).FirstAsync();
                        contactEmailItem.Email = item.Email;
                        contactEmailItem.IsMain = item.IsMain;
                        contactEmailUpdate.Add(contactEmailItem);
                    }
                    else
                    {
                        var contactEmail = new ContactEmail
                        {
                            ContactID = contact.ID,
                            Email = item.Email,
                            IsMain = item.IsMain,
                        };
                        contactEmailAdd.Add(contactEmail);

                        var bookingOwnerEmailModel = await DB.BookingOwnerEmails.Where(o => o.ID == item.ID).FirstAsync();
                        bookingOwnerEmailModel.FromContactEmailID = contactEmail.ID;
                        DB.Entry(bookingOwnerEmailModel).State = EntityState.Modified;
                    }
                }

                foreach (var item in contactEmails)
                {
                    var existed = bookingOwnerEmails.Where(e => e.FromContactEmailID == item.ID).FirstOrDefault();
                    if (existed == null)
                        contactEmailDelete.Add(item);
                }

                if (contactEmailAdd.Any())
                    await DB.ContactEmails.AddRangeAsync(contactEmailAdd);

                if (contactEmailUpdate.Any())
                {
                    foreach (var emailItem in contactEmailUpdate)
                        DB.Entry(emailItem).State = EntityState.Modified;
                }

                if (contactEmailDelete.Any())
                {
                    foreach (var emailItem in contactEmailDelete)
                    {
                        emailItem.IsDeleted = true;
                        DB.Entry(emailItem).State = EntityState.Modified;
                    }
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Phone
                var bookingOwnerPhones = await DB.BookingOwnerPhones.Where(o => o.BookingOwnerID == ownerId).ToListAsync();
                var contactPhones = await DB.ContactPhones.Where(p => p.ContactID == contact.ID).ToListAsync();
                List<ContactPhone> contactPhoneAdd = new List<ContactPhone>();
                List<ContactPhone> contactPhoneUpdate = new List<ContactPhone>();
                List<ContactPhone> contactPhoneDelete = new List<ContactPhone>();

                foreach (var item in bookingOwnerPhones)
                {
                    if (contactPhones.Any(e => e.ID == item.FromContactPhoneID))
                    {
                        var phoneItem = await DB.ContactPhones.Where(o => o.ContactID == contact.ID && o.ID == item.FromContactPhoneID).FirstAsync();
                        phoneItem.PhoneTypeMasterCenterID = item.PhoneTypeMasterCenterID;
                        phoneItem.PhoneNumber = item.PhoneNumber;
                        phoneItem.PhoneNumberExt = item.PhoneNumberExt;
                        phoneItem.IsMain = item.IsMain;
                        phoneItem.CountryCode = item.CountryCode;
                        contactPhoneUpdate.Add(phoneItem);
                    }
                    else
                    {
                        var contactPhone = new ContactPhone
                        {
                            ContactID = contact.ID,
                            IsMain = item.IsMain,
                            PhoneNumber = item.PhoneNumber,
                            PhoneNumberExt = item.PhoneNumberExt,
                            PhoneTypeMasterCenterID = item.PhoneTypeMasterCenterID,
                            CountryCode = item.CountryCode
                        };

                        contactPhoneAdd.Add(contactPhone);

                        var bookingOwnerPhoneModel = await DB.BookingOwnerPhones.Where(o => o.ID == item.ID).FirstAsync();
                        bookingOwnerPhoneModel.FromContactPhoneID = contactPhone.ID;
                        DB.Entry(bookingOwnerPhoneModel).State = EntityState.Modified;
                    }
                }
                foreach (var item in contactPhones)
                {
                    var existed = bookingOwnerPhones.Where(p => p.FromContactPhoneID == item.ID).FirstOrDefault();
                    if (existed == null)
                        contactPhoneDelete.Add(item);
                }

                if (contactPhoneAdd.Any())
                    await DB.ContactPhones.AddRangeAsync(contactPhoneAdd);

                if (contactPhoneUpdate.Any())
                {
                    foreach (var phoneItem in contactPhoneUpdate)
                        DB.Entry(phoneItem).State = EntityState.Modified;
                }

                if (contactPhoneDelete.Any())
                {
                    foreach (var phoneItem in contactPhoneDelete)
                    {
                        phoneItem.IsDeleted = true;
                        DB.Entry(phoneItem).State = EntityState.Modified;
                    }
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Address
                var bookingOwnerAdresses = await DB.BookingOwnerAddresses.Where(o => o.BookingOwnerID == ownerId).ToListAsync();
                foreach (var address in bookingOwnerAdresses)
                {
                    var contactAddress = await DB.ContactAddresses.Where(o => o.ID == address.FromContactAddressID).FirstOrDefaultAsync();
                    if (contactAddress != null)
                    {
                        BookingToContactAdreessModel(ref contactAddress, address);
                        DB.Entry(contactAddress).State = EntityState.Modified;
                    }
                    else
                    {
                        var addressMasterCenterId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && o.Key == "0").Select(o => o.ID).FirstAsync();
                        var contactAddressModel = new ContactAddress()
                        {
                            ContactID = contact.ID
                        };
                        BookingToContactAdreessModel(ref contactAddressModel, address);
                        await DB.ContactAddresses.AddAsync(contactAddressModel);

                        if (address.ContactAddressTypeMasterCenterID == addressMasterCenterId)
                        {
                            var addressProject = new ContactAddressProject()
                            {
                                ContactAddressID = contactAddressModel.ID,
                                ProjectID = projectId
                            };
                            await DB.ContactAddressProjects.AddAsync(addressProject);
                        }

                        var bookingOwnerAdressModel = await DB.BookingOwnerAddresses.Where(o => o.ID == address.ID).FirstAsync();
                        bookingOwnerAdressModel.FromContactAddressID = contactAddressModel.ID;
                        DB.Entry(bookingOwnerAdressModel).State = EntityState.Modified;
                    }
                }

                await DB.SaveChangesAsync();
                #endregion
            }
            else
            {
                #region Email
                var agreementOwnerEmails = await DB.AgreementOwnerEmails.Where(o => o.AgreementOwnerID == ownerId).ToListAsync();
                var contactEmails = await DB.ContactEmails.Where(e => e.ContactID == contact.ID).ToListAsync();
                List<ContactEmail> contactEmailAdd = new List<ContactEmail>();
                List<ContactEmail> contactEmailUpdate = new List<ContactEmail>();
                List<ContactEmail> contactEmailDelete = new List<ContactEmail>();

                foreach (var item in agreementOwnerEmails)
                {
                    if (contactEmails.Any(e => e.ID == item.FromContactEmailID))
                    {
                        var contactEmailItem = await DB.ContactEmails.Where(e => e.ContactID == contact.ID && e.ID == item.FromContactEmailID).FirstAsync();
                        contactEmailItem.Email = item.Email;
                        contactEmailItem.IsMain = item.IsMain;
                        contactEmailUpdate.Add(contactEmailItem);
                    }
                    else
                    {
                        var contactEmail = new ContactEmail
                        {
                            ContactID = contact.ID,
                            Email = item.Email,
                            IsMain = item.IsMain,
                        };
                        contactEmailAdd.Add(contactEmail);

                        var agreementOwnerEmailModel = await DB.AgreementOwnerEmails.Where(o => o.ID == item.ID).FirstAsync();
                        agreementOwnerEmailModel.FromContactEmailID = contactEmail.ID;
                        DB.Entry(agreementOwnerEmailModel).State = EntityState.Modified;
                    }
                }

                foreach (var item in contactEmails)
                {
                    var existed = agreementOwnerEmails.Where(e => e.FromContactEmailID == item.ID).FirstOrDefault();
                    if (existed == null)
                        contactEmailDelete.Add(item);
                }

                if (contactEmailAdd.Any())
                    await DB.ContactEmails.AddRangeAsync(contactEmailAdd);

                if (contactEmailUpdate.Any())
                {
                    foreach (var emailItem in contactEmailUpdate)
                        DB.Entry(emailItem).State = EntityState.Modified;
                }

                if (contactEmailDelete.Any())
                {
                    foreach (var emailItem in contactEmailDelete)
                    {
                        emailItem.IsDeleted = true;
                        DB.Entry(emailItem).State = EntityState.Modified;
                    }
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Phone
                var agreementOwnerPhones = await DB.AgreementOwnerPhones.Where(o => o.AgreementOwnerID == ownerId).ToListAsync();
                var contactPhones = await DB.ContactPhones.Where(p => p.ContactID == contact.ID).ToListAsync();
                List<ContactPhone> contactPhoneAdd = new List<ContactPhone>();
                List<ContactPhone> contactPhoneUpdate = new List<ContactPhone>();
                List<ContactPhone> contactPhoneDelete = new List<ContactPhone>();

                foreach (var item in agreementOwnerPhones)
                {
                    if (contactPhones.Any(e => e.ID == item.FromContactPhoneID))
                    {
                        var phoneItem = await DB.ContactPhones.Where(o => o.ContactID == contact.ID && o.ID == item.FromContactPhoneID).FirstAsync();
                        phoneItem.PhoneTypeMasterCenterID = item.PhoneTypeMasterCenterID;
                        phoneItem.PhoneNumber = item.PhoneNumber;
                        phoneItem.PhoneNumberExt = item.PhoneNumberExt;
                        phoneItem.IsMain = item.IsMain;
                        phoneItem.CountryCode = item.CountryCode;
                        contactPhoneUpdate.Add(phoneItem);
                    }
                    else
                    {
                        var contactPhone = new ContactPhone
                        {
                            ContactID = contact.ID,
                            IsMain = item.IsMain,
                            PhoneNumber = item.PhoneNumber,
                            PhoneNumberExt = item.PhoneNumberExt,
                            PhoneTypeMasterCenterID = item.PhoneTypeMasterCenterID,
                            CountryCode = item.CountryCode
                        };

                        contactPhoneAdd.Add(contactPhone);

                        var agreementOwnerPhoneModel = await DB.AgreementOwnerPhones.Where(o => o.ID == item.ID).FirstAsync();
                        agreementOwnerPhoneModel.FromContactPhoneID = contactPhone.ID;
                        DB.Entry(agreementOwnerPhoneModel).State = EntityState.Modified;
                    }
                }
                foreach (var item in contactPhones)
                {
                    var existed = agreementOwnerPhones.Where(p => p.FromContactPhoneID == item.ID).FirstOrDefault();
                    if (existed == null)
                        contactPhoneDelete.Add(item);
                }

                if (contactPhoneAdd.Any())
                    await DB.ContactPhones.AddRangeAsync(contactPhoneAdd);

                if (contactPhoneUpdate.Any())
                {
                    foreach (var phoneItem in contactPhoneUpdate)
                        DB.Entry(phoneItem).State = EntityState.Modified;
                }

                if (contactPhoneDelete.Any())
                {
                    foreach (var phoneItem in contactPhoneDelete)
                    {
                        phoneItem.IsDeleted = true;
                        DB.Entry(phoneItem).State = EntityState.Modified;
                    }
                }

                await DB.SaveChangesAsync();
                #endregion

                #region Address
                var agreementOwnerAdresses = await DB.AgreementOwnerAddresses.Where(o => o.AgreementOwnerID == ownerId).ToListAsync();
                foreach (var address in agreementOwnerAdresses)
                {
                    var contactAddress = await DB.ContactAddresses.Where(o => o.ID == address.FromContactAddressID).FirstOrDefaultAsync();
                    if (contactAddress != null)
                    {
                        AgreementToContactAdreessModel(ref contactAddress, address);
                        DB.Entry(contactAddress).State = EntityState.Modified;
                    }
                    else
                    {
                        var addressMasterCenterId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && o.Key == "0").Select(o => o.ID).FirstAsync();
                        var contactAddressModel = new ContactAddress()
                        {
                            ContactID = contact.ID
                        };

                        AgreementToContactAdreessModel(ref contactAddressModel, address);

                        await DB.ContactAddresses.AddAsync(contactAddressModel);

                        if (address.ContactAddressTypeMasterCenterID == addressMasterCenterId)
                        {
                            var addressProject = new ContactAddressProject()
                            {
                                ContactAddressID = contactAddressModel.ID,
                                ProjectID = projectId
                            };
                            await DB.ContactAddressProjects.AddAsync(addressProject);

                            
                        }
                        var agreementOwnerAdressModel = await DB.AgreementOwnerAddresses.Where(o => o.ID == address.ID).FirstAsync();
                        agreementOwnerAdressModel.FromContactAddressID = contactAddressModel.ID;
                        DB.Entry(agreementOwnerAdressModel).State = EntityState.Modified;
                    }
                }

                try
                {
                    await DB.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    throw e;
                }

                #endregion
            }
        }

        private async Task UpdateBookingOwnerInfoAsync(BookingOwner owner, Guid projectId, Contact contact)
        {
            #region Email
            var contactEmails = await DB.ContactEmails.Where(o => o.ContactID == contact.ID).ToListAsync();
            var ownerEmail = await DB.BookingOwnerEmails.Where(o => o.BookingOwnerID == owner.ID).ToListAsync();
            List<BookingOwnerEmail> ownerEmailAdd = new List<BookingOwnerEmail>();
            List<BookingOwnerEmail> ownerEmailUpdate = new List<BookingOwnerEmail>();
            List<BookingOwnerEmail> ownerEmailDelete = new List<BookingOwnerEmail>();

            foreach (var item in contactEmails)
            {
                if (ownerEmail.Any(e => e.FromContactEmailID == item.ID))
                {
                    var ownerEmailItem = await DB.BookingOwnerEmails.Where(e => e.BookingOwnerID == owner.ID && e.FromContactEmailID == item.ID).FirstAsync();
                    ownerEmailItem.Email = item.Email;
                    ownerEmailItem.IsMain = item.IsMain;
                    ownerEmailUpdate.Add(ownerEmailItem);
                }
                else
                {
                    ownerEmailAdd.Add(new BookingOwnerEmail
                    {
                        FromContactEmailID = item.ID,
                        Email = item.Email,
                        IsMain = item.IsMain,
                        BookingOwnerID = owner.ID
                    });
                }
            }

            foreach (var item in ownerEmail)
            {
                var existed = contactEmails.Where(o => o.ID == item.FromContactEmailID).FirstOrDefault();
                if (existed == null)
                {
                    ownerEmailDelete.Add(item);
                }
            }

            if (ownerEmailAdd.Any())
                await DB.BookingOwnerEmails.AddRangeAsync(ownerEmailAdd);

            if (ownerEmailUpdate.Any())
            {
                foreach (var emailItem in ownerEmailUpdate)
                    DB.Entry(emailItem).State = EntityState.Modified;
            }

            if (ownerEmailDelete.Any())
            {
                foreach (var emailItem in ownerEmailDelete)
                {
                    emailItem.IsDeleted = true;
                    DB.Entry(emailItem).State = EntityState.Modified;
                }

            }

            await DB.SaveChangesAsync();
            #endregion

            #region Phone
            var contactPhones = await DB.ContactPhones.Where(o => o.ContactID == contact.ID).ToListAsync();
            var ownerPhone = await DB.BookingOwnerPhones.Where(p => p.BookingOwnerID == owner.ID).ToListAsync();
            List<BookingOwnerPhone> ownerPhoneAdd = new List<BookingOwnerPhone>();
            List<BookingOwnerPhone> ownerPhoneUpdate = new List<BookingOwnerPhone>();
            List<BookingOwnerPhone> ownerPhoneDelete = new List<BookingOwnerPhone>();

            foreach (var item in contactPhones)
            {
                if (ownerPhone.Any(e => e.FromContactPhoneID == item.ID))
                {
                    var phoneItem = await DB.BookingOwnerPhones.Where(o => o.BookingOwnerID == owner.ID && o.FromContactPhoneID == item.ID).FirstAsync();
                    phoneItem.PhoneTypeMasterCenterID = item.PhoneTypeMasterCenterID;
                    phoneItem.PhoneNumber = item.PhoneNumber;
                    phoneItem.PhoneNumberExt = item.PhoneNumberExt;
                    phoneItem.IsMain = item.IsMain;
                    phoneItem.CountryCode = item.CountryCode;
                    ownerPhoneUpdate.Add(phoneItem);
                }
                else
                {
                    ownerPhoneAdd.Add(new BookingOwnerPhone
                    {
                        FromContactPhoneID = item.ID,
                        IsMain = item.IsMain,
                        PhoneNumber = item.PhoneNumber,
                        PhoneNumberExt = item.PhoneNumberExt,
                        PhoneTypeMasterCenterID = item.PhoneTypeMasterCenterID,
                        CountryCode = item.CountryCode,
                        BookingOwnerID = owner.ID
                    });
                }
            }
            foreach (var item in ownerPhone)
            {
                var existed = contactPhones.Where(o => o.ID == item.FromContactPhoneID).FirstOrDefault();
                if (existed == null)
                {
                    ownerPhoneDelete.Add(item);
                }
            }

            if (ownerPhoneAdd.Any())
                await DB.BookingOwnerPhones.AddRangeAsync(ownerPhoneAdd);

            if (ownerPhoneUpdate.Any())
            {
                foreach (var phoneItem in ownerPhoneUpdate)
                    DB.Entry(phoneItem).State = EntityState.Modified;
            }

            if (ownerPhoneDelete.Any())
            {
                foreach (var phoneItem in ownerPhoneDelete)
                {
                    phoneItem.IsDeleted = true;
                    DB.Entry(phoneItem).State = EntityState.Modified;
                }
            }

            await DB.SaveChangesAsync();
            #endregion

            #region Address
            var addressMasterCenterId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && o.Key == "0").Select(o => o.ID).FirstAsync();
            var contactAdresses = await DB.ContactAddresses.Where(o => o.ContactID == contact.ID && o.ContactAddressTypeMasterCenterID != addressMasterCenterId).ToListAsync();
            var contactAddressProject = await DB.ContactAddressProjects
                .Include(o => o.ContactAddress)
                .Where(o => o.ProjectID == projectId && o.ContactAddress.ContactID == contact.ID)
                .FirstOrDefaultAsync();

            if (contactAddressProject != null)
                contactAdresses.Add(contactAddressProject.ContactAddress);

            foreach (var address in contactAdresses)
            {
                var bookingOwnerAddress = await DB.BookingOwnerAddresses.Where(o => o.FromContactAddressID == address.ID).FirstOrDefaultAsync();
                if (bookingOwnerAddress != null)
                {
                    ContactToBookingOwnerAdreessModel(ref bookingOwnerAddress, address);
                    DB.Entry(bookingOwnerAddress).State = EntityState.Modified;
                }
                else
                {
                    var bookingOwnerAddressModel = new BookingOwnerAddress()
                    {
                        BookingOwnerID = owner.ID,
                        FromContactAddressID = address.ID
                    };
                    ContactToBookingOwnerAdreessModel(ref bookingOwnerAddressModel, address);
                    await DB.BookingOwnerAddresses.AddAsync(bookingOwnerAddressModel);
                }
            }

            await DB.SaveChangesAsync();
            #endregion
        }

        private async Task UpdateAgreementOwnerInfoAsync(AgreementOwner owner, Guid projectId, Contact contact)
        {
            #region Email
            var contactEmails = await DB.ContactEmails.Where(o => o.ContactID == contact.ID).ToListAsync();
            var ownerEmail = await DB.AgreementOwnerEmails.Where(o => o.AgreementOwnerID == owner.ID).ToListAsync();
            List<AgreementOwnerEmail> ownerEmailAdd = new List<AgreementOwnerEmail>();
            List<AgreementOwnerEmail> ownerEmailUpdate = new List<AgreementOwnerEmail>();
            List<AgreementOwnerEmail> ownerEmailDelete = new List<AgreementOwnerEmail>();

            foreach (var item in contactEmails)
            {
                if (ownerEmail.Any(e => e.FromContactEmailID == item.ID))
                {
                    var ownerEmailItem = await DB.AgreementOwnerEmails.Where(e => e.AgreementOwnerID == owner.ID && e.FromContactEmailID == item.ID).FirstAsync();
                    ownerEmailItem.Email = item.Email;
                    ownerEmailItem.IsMain = item.IsMain;
                    ownerEmailUpdate.Add(ownerEmailItem);
                }
                else
                {
                    ownerEmailAdd.Add(new AgreementOwnerEmail
                    {
                        FromContactEmailID = item.ID,
                        Email = item.Email,
                        IsMain = item.IsMain,
                        AgreementOwnerID = owner.ID
                    });
                }
            }

            foreach (var item in ownerEmail)
            {
                var existed = contactEmails.Where(o => o.ID == item.FromContactEmailID).FirstOrDefault();
                if (existed == null)
                {
                    ownerEmailDelete.Add(item);
                }
            }

            if (ownerEmailAdd.Any())
                await DB.AgreementOwnerEmails.AddRangeAsync(ownerEmailAdd);

            if (ownerEmailUpdate.Any())
            {
                foreach (var emailItem in ownerEmailUpdate)
                    DB.Entry(emailItem).State = EntityState.Modified;
            }

            if (ownerEmailDelete.Any())
            {
                foreach (var emailItem in ownerEmailDelete)
                {
                    emailItem.IsDeleted = true;
                    DB.Entry(emailItem).State = EntityState.Modified;
                }

            }

            await DB.SaveChangesAsync();
            #endregion

            #region Phone
            var contactPhones = await DB.ContactPhones.Where(o => o.ContactID == contact.ID).ToListAsync();
            var ownerPhone = await DB.AgreementOwnerPhones.Where(o => o.AgreementOwnerID == owner.ID).ToListAsync();
            List<AgreementOwnerPhone> ownerPhoneAdd = new List<AgreementOwnerPhone>();
            List<AgreementOwnerPhone> ownerPhoneUpdate = new List<AgreementOwnerPhone>();
            List<AgreementOwnerPhone> ownerPhoneDelete = new List<AgreementOwnerPhone>();

            foreach (var item in contactPhones)
            {
                if (ownerPhone.Any(e => e.FromContactPhoneID == item.ID))
                {
                    var phoneItem = await DB.AgreementOwnerPhones.Where(o => o.AgreementOwnerID == owner.ID && o.FromContactPhoneID == item.ID).FirstAsync();
                    phoneItem.PhoneTypeMasterCenterID = item.PhoneTypeMasterCenterID;
                    phoneItem.PhoneNumber = item.PhoneNumber;
                    phoneItem.PhoneNumberExt = item.PhoneNumberExt;
                    phoneItem.IsMain = item.IsMain;
                    phoneItem.CountryCode = item.CountryCode;
                    ownerPhoneUpdate.Add(phoneItem);
                }
                else
                {
                    ownerPhoneAdd.Add(new AgreementOwnerPhone
                    {
                        FromContactPhoneID = item.ID,
                        IsMain = item.IsMain,
                        PhoneNumber = item.PhoneNumber,
                        PhoneNumberExt = item.PhoneNumberExt,
                        PhoneTypeMasterCenterID = item.PhoneTypeMasterCenterID,
                        CountryCode = item.CountryCode,
                        AgreementOwnerID = owner.ID
                    });
                }
            }
            foreach (var item in ownerPhone)
            {
                var existed = contactPhones.Where(o => o.ID == item.FromContactPhoneID).FirstOrDefault();
                if (existed == null)
                {
                    ownerPhoneDelete.Add(item);
                }
            }

            if (ownerPhoneAdd.Any())
                await DB.AgreementOwnerPhones.AddRangeAsync(ownerPhoneAdd);

            if (ownerPhoneUpdate.Any())
            {
                foreach (var phoneItem in ownerPhoneUpdate)
                    DB.Entry(phoneItem).State = EntityState.Modified;
            }

            if (ownerPhoneDelete.Any())
            {
                foreach (var phoneItem in ownerPhoneDelete)
                {
                    phoneItem.IsDeleted = true;
                    DB.Entry(phoneItem).State = EntityState.Modified;
                }
            }

            await DB.SaveChangesAsync();
            #endregion

            #region Address
            var addressMasterCenterId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType && o.Key == "0").Select(o => o.ID).FirstAsync();
            var contactAdresses = await DB.ContactAddresses.Where(o => o.ContactID == contact.ID && o.ContactAddressTypeMasterCenterID != addressMasterCenterId).ToListAsync();
            var contactAddressProject = await DB.ContactAddressProjects
                .Include(o => o.ContactAddress)
                .Where(o => o.ProjectID == projectId && o.ContactAddress.ContactID == contact.ID)
                .FirstOrDefaultAsync();

            if (contactAddressProject != null)
                contactAdresses.Add(contactAddressProject.ContactAddress);

            foreach (var address in contactAdresses)
            {
                var agreementOwnerAddress = await DB.AgreementOwnerAddresses.Where(o => o.FromContactAddressID == address.ID).FirstOrDefaultAsync();
                if (agreementOwnerAddress != null)
                {
                    ContactToAgreementOwnerAdreessModel(ref agreementOwnerAddress, address);
                    DB.Entry(agreementOwnerAddress).State = EntityState.Modified;
                }
                else
                {
                    var agreementOwnerAddressModel = new AgreementOwnerAddress()
                    {
                        AgreementOwnerID = owner.ID,
                        FromContactAddressID = address.ID
                    };
                    ContactToAgreementOwnerAdreessModel(ref agreementOwnerAddressModel, address);
                    await DB.AgreementOwnerAddresses.AddAsync(agreementOwnerAddressModel);
                }
            }

            await DB.SaveChangesAsync();
            #endregion
        }
    }
}
