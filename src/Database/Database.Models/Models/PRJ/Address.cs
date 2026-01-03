using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมููลที่ตั้งโครงการ")]
    [Table("Address", Schema = Schema.PROJECT)]
    public class Address : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }
        [Description("ประเภทที่ตั้งโครงการ")]
        public Guid? ProjectAddressTypeMasterCenterID { get; set; }
        [ForeignKey("ProjectAddressTypeMasterCenterID")]
        public MST.MasterCenter ProjectAddressType { get; set; }
        [Description("ชื่อที่ตั้งหลัก (ภาษาไทย)")]
        [MaxLength(100)]
        public string AddressNameTH { get; set; }
        [Description("ชื่อที่ตั้งหลัก (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string AddressNameEN { get; set; }
        [Description("เลขที่โฉนด")]
        [MaxLength(1000)]
        public string TitleDeedNo { get; set; }
        [Description("เลขที่ดิน")]
        [MaxLength(1000)]
        public string LandNo { get; set; }
        [Description("หน้าสำรวจ")]
        [MaxLength(1000)]
        public string InspectionNo { get; set; }
        [Description("รหัสไปรษณีย์")]
        [MaxLength(50)]
        public string PostalCode { get; set; }
        [Description("จังหวัด")]
        public Guid? ProvinceID { get; set; }
        [ForeignKey("ProvinceID")]
        public MST.Province Province { get; set; }
        [Description("อำเภอ/เขต")]
        public Guid? DistrictID { get; set; }
        [ForeignKey("DistrictID")]
        public MST.District District { get; set; }
        [Description("ตำบล/แขวง")]
        public Guid? SubDistrictID { get; set; }
        [ForeignKey("SubDistrictID")]
        public MST.SubDistrict SubDistrict { get; set; }

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

        [Description("หมู่ที่")]
        [MaxLength(1000)]
        public string Moo { get; set; }
        [Description("ซอย ภาษาไทย")]
        [MaxLength(1000)]
        public string SoiTH { get; set; }
        [Description("ซอย ภาษาอังกฤษ")]
        [MaxLength(1000)]
        public string SoiEN { get; set; }
        [Description("ถนน ภาษาไทย")]
        [MaxLength(1000)]
        public string RoadTH { get; set; }
        [Description("ถนน ภาษาอังกฤษ")]
        [MaxLength(1000)]
        public string RoadEN { get; set; }

        //ข้อมูลตามทะเบียนบ้าน
        [Description("ตำบล ตามทะเบียนบ้าน")]
        public Guid? HouseSubDistrictID { get; set; }
        [ForeignKey("HouseSubDistrictID")]
        public MST.SubDistrict HouseSubDistrict { get; set; }
        [Description("หมู่ ตามทะเบียนบ้าน")]
        [MaxLength(1000)]
        public string HouseMoo { get; set; }
        [Description("ซอย ตามทะเบียนบ้าน (ภาษาไทย)")]
        [MaxLength(1000)]
        public string HouseSoiTH { get; set; }
        [Description("ซอย ตามทะเบียนบ้าน (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string HouseSoiEN { get; set; }
        [Description("ถนน ตามทะเบียนบ้าน (ภาษาไทย)")]
        [MaxLength(1000)]
        public string HouseRoadTH { get; set; }
        [Description("ถนน ตามทะเบียนบ้าน (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string HouseRoadEN { get; set; }

        //ข้อมูลตามโฉนด
        [Description("ตำบล ตามโฉนด")]
        public Guid? TitledeedSubDistrictID { get; set; }
        [ForeignKey("TitledeedSubDistrictID")]
        public MST.SubDistrict TitledeedSubDistrict { get; set; }
        [Description("หมู่ ตามโฉนด")]
        [MaxLength(1000)]
        public string TitledeedMoo { get; set; }
        [Description("ซอย ตามโฉนด (ภาษาไทย)")]
        [MaxLength(1000)]
        public string TitledeedSoiTH { get; set; }
        [Description("ซอย ตามโฉนด (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string TitledeedSoiEN { get; set; }
        [Description("ถนน ตามโฉนด (ภาษาไทย)")]
        [MaxLength(1000)]
        public string TitledeedRoadTH { get; set; }
        [Description("ถนน ตามโฉนด (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string TitledeedRoadEN { get; set; }

        public Guid? LandOfficeID { get; set; }
        [ForeignKey("LandOfficeID")]
        public MST.LandOffice LandOffice { get; set; }

    }
}
