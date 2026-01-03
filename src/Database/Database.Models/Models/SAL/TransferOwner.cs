using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("ผู้โอนกรรมสิทธิ์")]
    [Table("TransferOwner", Schema = Schema.SALE)]
    public class TransferOwner : BaseEntity
    {
        [Description("โอนกรรมสิทธิ์")]
        public Guid TransferID { get; set; }
        [ForeignKey("TransferID")]
        public SAL.Transfer Transfer { get; set; }

        [Description("ลูกค้า")]
        public Guid? FromContactID { get; set; }
        [ForeignKey("FromContactID")]
        public CTM.Contact FromContact { get; set; }

        [Description("มอบอำนาจหรือไม่?")]
        public bool IsAssignAuthority { get; set; }
        [Description("มอบอำนาจโดยบริษัท")]
        public bool IsAssignAuthorityByCompany { get; set; }
        [Description("ชื่อผู้รับมอบอำนาจ")]
        [MaxLength(1000)]
        public string AuthorityName { get; set; }
        [Description("ลำดับที่")]
        public int Order { get; set; }
        [Description("ประเภทของลูกค้า (บุคคลทั่วไป/นิติบุคคล)")]
        public Guid? ContactTypeMasterCenterID { get; set; }
        [ForeignKey("ContactTypeMasterCenterID")]
        public MST.MasterCenter ContactType { get; set; }
        [Description("คำนำหน้าชื่อ (ภาษาไทย)")]
        public Guid? ContactTitleTHMasterCenterID { get; set; }
        [ForeignKey("ContactTitleTHMasterCenterID")]
        public MST.MasterCenter ContactTitleTH { get; set; }

        [Description("คำนำหน้าเพิ่มเติม (ภาษาไทย)")]
        [MaxLength(250)]
        public string TitleExtTH { get; set; }
        [Description("ชื่อจริง (ภาษาไทย)")]
        [MaxLength(250)]
        public string FirstNameTH { get; set; }
        [Description("ชื่อกลาง (ภาษาไทย)")]
        [MaxLength(250)]
        public string MiddleNameTH { get; set; }
        [Description("นามสกุล (ภาษาไทย)")]
        [MaxLength(250)]
        public string LastNameTH { get; set; }
        [Description("หมายเลขบัตรประชาชน")]
        [MaxLength(50)]
        public string CitizenIdentityNo { get; set; }
        [Description("วันเกิด")]
        public DateTime? BirthDate { get; set; }
        [Description("เบอร์โทรศัพท์")]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }
        [Description("เบอร์มือถือ")]
        [MaxLength(100)]
        public string MobileNumber { get; set; }
        [Description("เบอร์โทรศัพท์ (ต่อ)")]
        [MaxLength(100)]
        public string PhoneNumberExt { get; set; }
        [Description("อีเมลล์")]
        [MaxLength(100)]
        public string Email { get; set; }
        [Description("ชื่อผู้ติดต่อ")]
        [MaxLength(100)]
        public string ContactFirstName { get; set; }
        [Description("นามสกุลผู้ติดต่อ")]
        [MaxLength(100)]
        public string ContactLastname { get; set; }
        [Description("สัญชาติ")]
        public Guid? NationalMasterCenterID { get; set; }
        [ForeignKey("NationalMasterCenterID")]
        public MST.MasterCenter National { get; set; }

        [Description("ชื่อคู่สมรส")]
        [MaxLength(1000)]
        public string MarriageName { get; set; }
        [Description("สัญชาติของคู่สมรส")]
        public Guid? MarriageNationalMasterCenterID { get; set; }
        [ForeignKey("MarriageNationalMasterCenterID")]
        public MST.MasterCenter MarriageNational { get; set; }
        [Description("สัญชาติอื่นๆ ของคู่สมรส")]
        [MaxLength(1000)]
        public string MarriageOtherNational { get; set; }

        [Description("ชื่อบิดา-มารดา")]
        [MaxLength(1000)]
        public string ParentName { get; set; }

        //ที่อยู่ตามทะเบียนบ้าน
        [Description("บ้านเลขที่ (ภาษาไทย)")]
        [MaxLength(100)]
        public string HouseNoTH { get; set; }
        [Description("หมู่ที่ (ภาษาไทย)")]
        [MaxLength(100)]
        public string MooTH { get; set; }
        [Description("หมู่บ้าน/อาคาร (ภาษาไทย)")]
        [MaxLength(1000)]
        public string VillageTH { get; set; }
        [Description("ซอย (ภาษาไทย)")]
        [MaxLength(100)]
        public string SoiTH { get; set; }
        [Description("ถนน (ภาษาไทย)")]
        [MaxLength(100)]
        public string RoadTH { get; set; }
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


        [Description("สถานะสมรส")]
        public Guid? TransferMarriageStatusMasterCenterID { get; set; }
        [ForeignKey("TransferMarriageStatusMasterCenterID")]
        public MST.MasterCenter TransferMarriageStatus { get; set; }

        [Description("คำนำหน้าชื่อคู่สมรส")]
        public Guid? MarriageTitleTHMasterCenterID { get; set; }
        [ForeignKey("MarriageTitleTHMasterCenterID")]
        public MST.MasterCenter MarriageTitleTH { get; set; }


        [Description("ชื่อเต็ม (ภาษาไทย) (คำนำหน้าชื่อ + ชื่อจริง + ' ' + ชื่อกลาง + ' ' + นามสกุล)")]
        [MaxLength(500)]
        public string FullnameTH { get; set; }
        [Description("ชื่อเต็ม (ภาษาอังกฤษ) (คำนำหน้าชื่อ + ชื่อจริง + ' ' + ชื่อกลาง + ' ' + นามสกุล)")]
        [MaxLength(500)]
        public string FullnameEN { get; set; }

        [Description("เลขประจำตัวผู้เสียภาษี")]
        [MaxLength(100)]
        public string TaxID { get; set; }


        [Description("สัญชาติ ภาษาไทย")]
        [MaxLength(1000)]
        public string NationalTH { get; set; }
        [Description("ประเทศ ภาษาไทย")]
        [MaxLength(1000)]
        public string CountryTH { get; set; }
    }
}
