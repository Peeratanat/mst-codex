using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("บริษัท")]
    [Table("Company", Schema = Schema.MASTER)]
    public class Company : BaseEntity
    {
        [Description("รหัสบริษัท (จากระบบ AP Authorize)")]
        [MaxLength(100)]
        public string APAuthorizeRefID { get; set; }
        [Description("ตัวย่อบริษัท")]
        [MaxLength(50)]
        public string Code { get; set; }
        [Description("ชื่อบริษัทภาษาไทย")]
        [MaxLength(100)]
        public string NameTH { get; set; }
        [Description("ชื่อบริษัทภาษาอังกฤษ")]
        [MaxLength(100)]
        public string NameEN { get; set; }
        [Description("ประจำตัวผู้เสียภาษี")]
        [MaxLength(1000)]
        public string TaxID { get; set; }
        [Description("ที่อยู่ภาษาไทย")]
        [MaxLength(1000)]
        public string AddressTH { get; set; }
        [Description("ที่อยู่ภาษาอังกฤษ")]
        [MaxLength(1000)]
        public string AddressEN { get; set; }
        [Description("ชื่อตึกภาษาไทย")]
        [MaxLength(1000)]
        public string BuildingTH { get; set; }
        [Description("ชื่อตึกภาษาอังกฤษ")]
        [MaxLength(1000)]
        public string BuildingEN { get; set; }
        [Description("ซอยภาษาไทย")]
        [MaxLength(1000)]
        public string SoiTH { get; set; }
        [Description("ซอยภาษาอังกฤษ")]
        [MaxLength(1000)]
        public string SoiEN { get; set; }
        [Description("ถนนภาษาไทย")]
        [MaxLength(1000)]
        public string RoadTH { get; set; }
        [Description("ถนนภาษาอังกฤษ")]
        [MaxLength(1000)]
        public string RoadEN { get; set; }

        public Guid? ProvinceID { get; set; }
        [ForeignKey("ProvinceID")]
        public Province Province { get; set; }

        public Guid? DistrictID { get; set; }
        [ForeignKey("DistrictID")]
        public District District { get; set; }

        public Guid? SubDistrictID { get; set; }
        [ForeignKey("SubDistrictID")]
        public SubDistrict SubDistrict { get; set; }

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

        [Description("รหัสไปรษณีย์")]
        [MaxLength(50)]
        public string PostalCode { get; set; }
        [Description("เบอร์โทร")]
        [MaxLength(50)]
        public string Telephone { get; set; }
        [Description("เบอร์แฟ๊กซ์")]
        [MaxLength(50)]
        public string Fax { get; set; }
        [Description("เว๊ปไซส์")]
        [MaxLength(1000)]
        public string Website { get; set; }
        [Description("รหัสบริษัทใน SAP")]
        [MaxLength(50)]
        public string SAPCompanyID { get; set; }
        [Description("ชื่อเก่าภาษาไทย")]
        [MaxLength(100)]
        public string NameTHOld { get; set; }
        [Description("ชื่อเก่าภาษอังกฤษ")]
        [MaxLength(100)]
        public string NameENOld { get; set; }

        [Description("ใช้งานที่ระบบ CRM")]
        public bool IsUseInCRM { get; set; }
        [Description("สถานะบริษัท")]
        public bool IsActive { get; set; }

    }
}
