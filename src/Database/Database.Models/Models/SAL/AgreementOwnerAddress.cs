using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("ที่อยู่ผู้ทำสัญญา")]
    [Table("AgreementOwnerAddress", Schema = Schema.SALE)]
    public class AgreementOwnerAddress : BaseEntity
    {
        public Guid AgreementOwnerID { get; set; }
        [ForeignKey("AgreementOwnerID")]
        public SAL.AgreementOwner AgreementOwner { get; set; }

        public Guid? FromContactAddressID { get; set; }
        [ForeignKey("FromContactAddressID")]
        public CTM.ContactAddress FromContactAddress { get; set; }

        //Contact Address
        [Description("ประเภทของที่อยู่")]
        public Guid? ContactAddressTypeMasterCenterID { get; set; }
        [ForeignKey("ContactAddressTypeMasterCenterID")]
        public MST.MasterCenter ContactAddressType { get; set; }

        [Description("บ้านเลขที่ (ภาษาไทย)")]
        [MaxLength(1000)]
        public string HouseNoTH { get; set; }
        [Description("หมู่ที่ (ภาษาไทย)")]
        [MaxLength(1000)]
        public string MooTH { get; set; }
        [Description("หมู่บ้าน/อาคาร (ภาษาไทย)")]
        [MaxLength(1000)]
        public string VillageTH { get; set; }
        [Description("ซอย (ภาษาไทย)")]
        [MaxLength(1000)]
        public string SoiTH { get; set; }
        [Description("ถนน (ภาษาไทย)")]
        [MaxLength(1000)]
        public string RoadTH { get; set; }
        [Description("บ้านเลขที่ (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string HouseNoEN { get; set; }
        [Description("หมู่ที่ (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string MooEN { get; set; }
        [Description("หมู่บ้าน/อาคาร (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string VillageEN { get; set; }
        [Description("ซอย (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string SoiEN { get; set; }
        [Description("ถนน (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string RoadEN { get; set; }
        [Description("รหัสไปรษณีย์")]
        [MaxLength(50)]
        public string PostalCode { get; set; }

        [Description("ประเทศ")]
        public Guid? CountryID { get; set; }
        [ForeignKey("CountryID")]
        public MST.Country Country { get; set; }
        [Description("จังหวัด")]
        public Guid? ProvinceID { get; set; }
        [ForeignKey("ProvinceID")]
        public MST.Province Province { get; set; }
        [Description("เขต/อำเภอ")]
        public Guid? DistrictID { get; set; }
        [ForeignKey("DistrictID")]
        public MST.District District { get; set; }
        [Description("แขวง/ตำบล")]
        public Guid? SubDistrictID { get; set; }
        [ForeignKey("SubDistrictID")]
        public MST.SubDistrict SubDistrict { get; set; }

        [Description("จังหวัด (ต่างประเทศ)")]
        [MaxLength(100)]
        public string ForeignProvince { get; set; }
        [Description("อำเภอ (ต่างประเทศ)")]
        [MaxLength(100)]
        public string ForeignDistrict { get; set; }
        [Description("ตำบล (ต่างประเทศ)")]
        [MaxLength(100)]
        public string ForeignSubDistrict { get; set; }

        [Description("ประเทศ (อื่นๆ)")]
        [MaxLength(100)]
        public string OtherCountry { get; set; }

        [Description("จังหวัด (อื่นๆ)")]
        [MaxLength(100)]
        public string OtherProvince { get; set; }

        [Description("อำเภอ (อื่นๆ)")]
        [MaxLength(100)]
        public string OtherDistrict { get; set; }

        [Description("ตำบล (อื่นๆ)")]
        [MaxLength(100)]
        public string OtherSubDistrict { get; set; }

        public static AgreementOwnerAddress CreateFromContact(CTM.ContactAddress contactAddress)
        {
            AgreementOwnerAddress model = new AgreementOwnerAddress();
            var contactProps = contactAddress.GetType().GetProperties();
            var props = model.GetType().GetProperties();
            foreach (var contactProp in contactProps)
            {
                foreach (var prop in props)
                {
                    if (contactProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(model, contactProp.GetValue(contactAddress));
                    }
                }
            }
            model.FromContactAddressID = contactAddress.ID;

            return model;
        }

        public void UpdateFromContact(CTM.ContactAddress contactAddress)
        {
            var contactProps = contactAddress.GetType().GetProperties();
            var props = this.GetType().GetProperties();
            foreach (var contactProp in contactProps)
            {
                foreach (var prop in props)
                {
                    if (contactProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(this, contactProp.GetValue(contactAddress));
                    }
                }
            }
        }
    }
}
