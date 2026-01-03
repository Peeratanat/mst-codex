using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("Email ผู้สัญญา")]
    [Table("AgreementOwnerEmail", Schema = Schema.SALE)]
    public class AgreementOwnerEmail : BaseEntity
    {
        public Guid AgreementOwnerID { get; set; }
        [ForeignKey("AgreementOwnerID")]
        public SAL.AgreementOwner AgreementOwner { get; set; }

        public Guid? FromContactEmailID { get; set; }
        [ForeignKey("FromContactEmailID")]
        public CTM.ContactEmail FromContactEmail { get; set; }

        //ContactEmail
        [Description("อีเมล")]
        [MaxLength(100)]
        public string Email { get; set; }
        [Description("สถานะอีเมลหลัก")]
        public bool IsMain { get; set; }

        public static AgreementOwnerEmail CreateFromContact(CTM.ContactEmail contactEmail)
        {
            AgreementOwnerEmail model = new AgreementOwnerEmail();
            var contactProps = contactEmail.GetType().GetProperties();
            var props = model.GetType().GetProperties();
            foreach (var contactProp in contactProps)
            {
                foreach (var prop in props)
                {
                    if (contactProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(model, contactProp.GetValue(contactEmail));
                    }
                }
            }
            model.FromContactEmailID = contactEmail.ID;

            return model;
        }

        public void UpdateFromContact(CTM.ContactEmail contactEmail)
        {
            var contactProps = contactEmail.GetType().GetProperties();
            var props = this.GetType().GetProperties();
            foreach (var contactProp in contactProps)
            {
                foreach (var prop in props)
                {
                    if (contactProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(this, contactProp.GetValue(contactEmail));
                    }
                }
            }
        }
    }
}
