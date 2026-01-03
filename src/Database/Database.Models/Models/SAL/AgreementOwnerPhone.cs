using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("เบอร์โทรผู้สัญญา")]
    [Table("AgreementOwnerPhone", Schema = Schema.SALE)]
    public class AgreementOwnerPhone : BaseEntity
    {
        public Guid AgreementOwnerID { get; set; }
        [ForeignKey("AgreementOwnerID")]
        public SAL.AgreementOwner AgreementOwner { get; set; }

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

        public static AgreementOwnerPhone CreateFromContact(CTM.ContactPhone contactPhone)
        {
            AgreementOwnerPhone model = new AgreementOwnerPhone();
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
