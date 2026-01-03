using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("เบอร์โทรผู้จอง")]
    [Table("BookingOwnerPhone", Schema = Schema.SALE)]
    public class BookingOwnerPhone : BaseEntity
    {
        public Guid BookingOwnerID { get; set; }
        [ForeignKey("BookingOwnerID")]
        public SAL.BookingOwner BookingOwner { get; set; }

        public Guid? FromContactPhoneID { get; set; }
        [ForeignKey("FromContactPhoneID")]
        public CTM.ContactPhone FromContactPhone { get; set; }

        //ContactPhone
        [Description("ประเภทเบอร์โทรศัพท์")]
        public Guid? PhoneTypeMasterCenterID { get; set; }
        [ForeignKey("PhoneTypeMasterCenterID")]
        public MST.MasterCenter PhoneType { get; set; }

        [Description("เบอร์โทรศัพท์")]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }
        [Description("เบอร์โทรศัพท์ (ต่อ)")]
        [MaxLength(100)]
        public string PhoneNumberExt { get; set; }
        [Description("รหัสประเทศ")]
        [MaxLength(50)]
        public string CountryCode { get; set; }
        [Description("สถานะเบอร์โทรศัพท์หลัก")]
        public bool IsMain { get; set; }

        public static BookingOwnerPhone CreateFromContact(CTM.ContactPhone contactPhone)
        {
            BookingOwnerPhone model = new BookingOwnerPhone();
            var contactProps = contactPhone.GetType().GetProperties();
            var props = model.GetType().GetProperties();
            foreach (var contactProp in contactProps)
            {
                foreach (var prop in props)
                {
                    if (contactProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(model, contactProp.GetValue(contactPhone));
                    }
                }
            }
            model.FromContactPhoneID = contactPhone.ID;

            return model;
        }

        public void UpdateFromContact(CTM.ContactPhone contactPhone)
        {
            var contactProps = contactPhone.GetType().GetProperties();
            var props = this.GetType().GetProperties();
            foreach (var contactProp in contactProps)
            {
                foreach (var prop in props)
                {
                    if (contactProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(this, contactProp.GetValue(contactPhone));
                    }
                }
            }
        }
    }
}
