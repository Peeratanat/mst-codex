using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูล Visitor")]
    [Table("Visitor", Schema = Schema.CUSTOMER)]
    public class Visitor : BaseEntity
    {
        [Description("รหัสลูกค้า")]
        public Guid? ContactID { get; set; }
        [ForeignKey("ContactID")]
        public Contact Contact { get; set; }
        [Description("รหัสโครงสร้าง")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("สถานะลูกค้า")]
        public Guid? ContactStatusMasterCenterID { get; set; }
        [ForeignKey("ContactStatusMasterCenterID")]
        public MST.MasterCenter ContactStatus { get; set; }

        [Description("เวลาเข้าโครงการ")]
        public DateTime? VisitDateIn { get; set; }
        [Description("เวลาออกโครงการ")]
        public DateTime? VisitDateOut { get; set; }
        [Description("เลขที่รับ")]
        [MaxLength(100)]
        public string ReceiveNumber { get; set; }

        [Description("เดินทางโดย")]
        public Guid? VisitByMasterCenterID { get; set; }
        [ForeignKey("VisitByMasterCenterID")]
        public MST.MasterCenter VisitBy { get; set; }
        [Description("ประเภทรถยนต์")]
        public Guid? VehicleMasterCenterID { get; set; }
        [ForeignKey("VehicleMasterCenterID")]
        public MST.MasterCenter Vehicle { get; set; }
        [Description("เลขทะเบียนรถ")]
        [MaxLength(50)]
        public string VehicleRegistrationNo { get; set; }
        [Description("สีรถ")]
        [MaxLength(100)]
        public string VehicleColor { get; set; }
        [Description("ยี่ห้อรถ")]
        [MaxLength(1000)]
        public string VehicleBrand { get; set; }
        [Description("สถานะ Walk")]
        public Guid? VisitorWalkStatusMasterCenterID { get; set; }
        [ForeignKey("VisitorWalkStatusMasterCenterID")]
        public MST.MasterCenter WalkStatus { get; set; }

        [Description("รายละเอียดการเดินทางเพิ่มเติม")]
        [MaxLength(100)]
        public string VehicleDescription { get; set; }

        [Description("ผู้ดูแล")]
        public Guid? OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public USR.User Owner { get; set; }

        [Description("แนบไฟล์รูปถ่ายบัตร")]
        [MaxLength(1000)]
        public string IDCardImage { get; set; }

        [Description("สถานะบันทึกว่าต้อนรับแล้ว")]
        public bool? IsWelcome { get; set; }

        [Description("คำนำหน้า (ภาษาไทย)")]
        [MaxLength(100)]
        public string TitleTH { get; set; }
        [Description("ชื่อจริง (ภาษาไทย)")]
        [MaxLength(100)]
        public string FirstNameTH { get; set; }
        [Description("นามสกุล (ภาษาไทย)")]
        [MaxLength(100)]
        public string LastNameTH { get; set; }
        [Description("คำนำหน้า (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string TitleEN { get; set; }
        [Description("ชื่อจริง (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string FirstNameEN { get; set; }
        [Description("นามสกุล (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string LastNameEN { get; set; }
        [Description("เพศ")]
        [MaxLength(50)]
        public string Gender { get; set; }
        [Description("วันเกิด")]
        public DateTime? BirthDate { get; set; }
        [Description("กรุ๊ปเลือด")]
        [MaxLength(50)]
        public string BloodType { get; set; }
        [Description("บ้านเลขที่")]
        [MaxLength(100)]
        public string HouseNo { get; set; }
        [Description("หมู่ที่")]
        [MaxLength(100)]
        public string Moo { get; set; }
        [Description("หมู่บ้าน/อาคาร")]
        [MaxLength(100)]
        public string Village { get; set; }
        [Description("ซอย")]
        [MaxLength(100)]
        public string Soi { get; set; }
        [Description("ถนน")]
        [MaxLength(100)]
        public string Road { get; set; }
        [Description("รหัสไปรษณีย์")]
        [MaxLength(50)]
        public string PostalCode { get; set; }
        [Description("ประเทศ")]
        [MaxLength(100)]
        public string Country { get; set; }
        [Description("จังหวัด")]
        [MaxLength(100)]
        public string Province { get; set; }
        [Description("เขต/อำเภอ")]
        [MaxLength(100)]
        public string District { get; set; }
        [Description("แขวง/ตำบล")]
        [MaxLength(100)]
        public string SubDistrict { get; set; }
        [Description("สัญชาติ")]
        [MaxLength(50)]
        public string National { get; set; }
        [Description("Issue")]
        [MaxLength(1000)]
        public string Issue { get; set; }
        [Description("วัน Issue")]
        public DateTime? IssueDate { get; set; }
        [Description("วันหมดอายุ Issue")]
        public DateTime? IssueExpireDate { get; set; }
        [Description("หมายเลขบัตรประชาชน")]
        [MaxLength(50)]
        public string CitizenIdentityNo { get; set; }
        [Description("เบอร์โทรศัพท์")]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [Description("เลขรับรายการเยี่ยมชมโครงการ")]
        [MaxLength(100)]
        public string VisitorRunning { get; set; }
        [Description("ชื่อ/นามสกุล ผู้มาเยี่ยมชมโครงการ")]
        [MaxLength(1000)]
        public string VisitorFullName { get; set; }
        [Description("ชื่อ/นามสกุล ภาษาอังกฤษ")]
        [MaxLength(1000)]
        public string VisitorFullNameEN { get; set; }
        [Description("ที่อยู่ตามบัตรประชาชนของผู้เยี่ยมชมโครงการ")]
        [MaxLength(1000)]
        public string VisitorIDCardFullAddress { get; set; }
        [Description("ที่อยู่ที่ติดต่อได้ ของผู้เยี่ยมชมโครงการ")]
        [MaxLength(1000)]
        public string VisitorFullAddress { get; set; }
        [Description("ที่อยู่ที่ทำงาน ของผู้เยี่ยมชมโครงการ")]
        [MaxLength(1000)]
        public string VisitorWorkingFullAddress { get; set; }
        [Description("Email Address")]
        [MaxLength(1000)]
        public string VisitorEmailAddress { get; set; }
        [Description("เบอร์ติดต่อ ผู้มาเยี่ยมชมโครงการ")]
        [MaxLength(100)]
        public string VisitorMobile { get; set; }
        [Description("เป็นพนักงานขับรถหรือไม่")]
        public bool IsDriver { get; set; }
        [Description("ชนิดของบัตรที่ใช้ยืนยันเข้าโครงการ")]
        public Guid? PersonalVisitCardTypeMasterCenterID { get; set; }
        [ForeignKey("PersonalVisitCardTypeMasterCenterID")]
        public MST.MasterCenter PersonalVisitCardType { get; set; }
        [Description("รูปจากบัตร")]
        [MaxLength(100)]
        public string PersonalVisitImageFromCard { get; set; }
        [Description("LC ผู้บันทึกรายการ")]
        public Guid? WelcomeLCUserID { get; set; }
        [ForeignKey("WelcomeLCUserID")]
        public USR.User WelcomeLCUser { get; set; }
        
        [Description("Transaction ID จากเครื่องรูดบัตร")]
        [MaxLength(100)]
        public string VisitKioskTransactionID { get; set; }
        [Description("Device ID ของเครื่องรูดบัตร")]
        [MaxLength(100)]
        public string VisitKioskDeviceID { get; set; }
        [Description("อ้างอิงกลับสำหรับกรณีที่รายการนั้นๆ ไม่ใช่ลูกค้า")]
        public Guid? RefVisitorID { get; set; }
        [Description("ความสัมพันธ์ของ Ref Visitor กับผู้มาติดต่อ")]
        public Guid? RefVisitorRelationMasterCenterID { get; set; }
        [ForeignKey("RefVisitorRelationMasterCenterID")]
        public MST.MasterCenter RefVisitorRelation { get; set; }

        [Description("โอกาสขาย (สถานะ Opportunity)")]
        public Guid? SalesOpportunityMasterCenterID { get; set; }
        [ForeignKey("SalesOpportunityMasterCenterID")]
        public MST.MasterCenter SalesOpportunity { get; set; }

    }
}
