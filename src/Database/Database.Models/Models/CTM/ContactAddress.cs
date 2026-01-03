using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูลที่อยู่ของลูกค้า")]
    [Table("ContactAddress", Schema = Schema.CUSTOMER)]
    public class ContactAddress : BaseEntity
    {
        [Description("รหัสลูกค้า")]
        public Guid ContactID { get; set; }
        [ForeignKey("ContactID")]
        public Contact Contact { get; set; }

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

        [Description("จังหวัดอื่นๆ (จากข้อมูล crm เก่า)")]
        [MaxLength(1000)]
        public string OtherProvinceEN { get; set; }
        [Description("จังหวัดอื่นๆ (จากข้อมูล crm เก่า)")]
        [MaxLength(1000)]
        public string OtherProvinceTH { get; set; }
        [Description("อำเภออื่นๆ (จากข้อมูล crm เก่า)")]
        [MaxLength(1000)]
        public string OtherDistrictEN { get; set; }
        [Description("อำเภออื่นๆ (จากข้อมูล crm เก่า)")]
        [MaxLength(1000)]
        public string OtherDistrictTH { get; set; }
        [Description("ตำบลอื่นๆ (จากข้อมูล crm เก่า)")]
        [MaxLength(1000)]
        public string OtherSubDistrictEN { get; set; }
        [Description("ตำบลอื่นๆ (จากข้อมูล crm เก่า)")]
        [MaxLength(1000)]
        public string OtherSubDistrictTH { get; set; }
        [Description("ประเทศอื่นๆ (จากข้อมูล crm เก่า)")]
        [MaxLength(1000)]
        public string OtherCountryEN { get; set; }
        [Description("ประเทศอื่นๆ (จากข้อมูล crm เก่า)")]
        [MaxLength(1000)]
        public string OtherCountryTH { get; set; }

    }
}
