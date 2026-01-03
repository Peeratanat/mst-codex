using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูลของ Lead")]
    [Table("Lead", Schema = Schema.CUSTOMER)]
    public class Lead : BaseEntity
    {
        [Description("รหัส ID ลูกค้า")]
        public Guid? ContactID { get; set; }
        [ForeignKey("ContactID")]
        public Contact Contact { get; set; }
        [Description("ชื่อจริง")]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Description("นามสกุล")]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Description("เบอร์โทรศัพท์")]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [Description("เบอร์โทรศัพท์บ้าน")]
        [MaxLength(50)]
        public string Telephone { get; set; }
        [Description("เบอร์โทรศัพท์บ้าน (ต่อ)")]
        [MaxLength(50)]
        public string TelephoneExt { get; set; }
        [Description("อีเมล")]
        [MaxLength(100)]
        public string Email { get; set; }
        [Description("โซนลูกค้าพักอาศัย")]
        [MaxLength(100)]
        public string VisitZone { get; set; }
        [Description("Remark")]
        [MaxLength(5000)]
        public string Remark { get; set; }
        [Description("Compaign")]
        [MaxLength(100)]
        public string Compaign { get; set; }
        [Description("สถานะของ Lead (InProgress = 0, Qualify = 1, DisQualify = 2)")]
        public Guid? LeadStatusMasterCenterID { get; set; }
        [ForeignKey("LeadStatusMasterCenterID")]
        public MST.MasterCenter LeadStatus { get; set; }

        [Description("ยอดใช้จ่าย")]
        [MaxLength(1000)]
        public string Payment { get; set; }

        [Description("ประเภทของ Lead")]
        public Guid? LeadTypeMasterCenterID { get; set; }
        [ForeignKey("LeadTypeMasterCenterID")]
        public MST.MasterCenter LeadType { get; set; }
        [Description("สื่อโฆษณา (มาจากสื่อ)")]
        public Guid? AdvertisementMasterCenterID { get; set; }
        [ForeignKey("AdvertisementMasterCenterID")]
        public MST.MasterCenter Advertisement { get; set; }

        [Description("คะแนนของ Lead")]
        public double? LeadScore { get; set; }

        [Description("ผู้ดูแล Lead")]
        public Guid? OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public USR.User Owner { get; set; }

        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

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

        [Description("แขวง/ตำบลที่ทำงาน")]
        [MaxLength(1000)]
        public string SubDistrictOfWorking { get; set; }

        [Description("ขนาดห้อง")]
        [MaxLength(1000)]
        public string RoomSize { get; set; }
        [Description("ประภทห้อง")]
        [MaxLength(1000)]
        public string RoomType { get; set; }
        [Description("จำนวนคน")]
        [MaxLength(1000)]
        public string NumberOfPerson { get; set; }
        [Description("จำนวน Unit")]
        [MaxLength(1000)]
        public string NumberOfUnit { get; set; }
        [Description("จำนวนครั้งที่มาติดต่อ")]
        [MaxLength(1000)]
        public string NumberOfContact { get; set; }
        [Description("อำเภอที่ทำงาน")]
        [MaxLength(1000)]
        public string DistrictOfWorking { get; set; }
        [Description("จังหวัดที่ทำงาน")]
        [MaxLength(1000)]
        public string ProvinceOfWorking { get; set; }
        [Description("LeadVisitDate")]
        public DateTime? LeadVisitDate { get; set; }
        [Description("LeadVisitTime")]
        [MaxLength(1000)]
        public string LeadVisitTime { get; set; }
        [Description("ลูกค้าต้องการให้โทรกลับหรือไม่?")]
        public bool? CallBack { get; set; }
        [Description("หมายเลขบัตรประชาชน")]
        [MaxLength(50)]
        public string CitizenIdentityNo { get; set; }
        [Description("UTMSource")]
        [MaxLength(1000)]
        public string UTMSource { get; set; }
        [Description("UTMMedium")]
        [MaxLength(1000)]
        public string UTMMedium { get; set; }
        [Description("UTMCampaign")]
        [MaxLength(1000)]
        public string UTMCampaign { get; set; }

        [Description("Reference ID จากระบบ Apthai และ callcenter (เป็น primary key ของ ระบบนั้น ใช้สำหรับเชคซ้ำตอนนำเข้า)")]
        [MaxLength(100)]
        public string RefID { get; set; }
        [Description("ประเภทย่อยของ Lead")]
        [MaxLength(100)]
        public string SubLeadType { get; set; }
        [Description("รหัส compaign จาก web apthai")]
        [MaxLength(100)]
        public string CampaignID { get; set; }
        [Description("สถานะปัจจุบันของ Lead Activity")]
        public Guid? CurrentLeadActivityStatusID { get; set; }
        [ForeignKey("CurrentLeadActivityStatusID")]
        public CTM.LeadActivityStatus CurrentLeadActivityStatus { get; set; }
        [Description("ส่งเมลล์ให้ LC แล้วหรือไม่")]
        public bool IsMailSendedToLC { get; set; }
        [Description("เหตุผลการซื้อ")]
        [MaxLength(5000)]
        public string BuyReason { get; set; }

        [Description("สถานะ Confirm Email")]
        public bool IsEmailConfirmed { get; set; }
        [Description("สถานะ Confirm Phone Number")]
        public bool IsPhoneNumberConfirmed { get; set; }

        [Description("วันที่ Lead")]
        public DateTime? LeadDate { get; set; }

        [Description("ประเภทการยินยอม (ทุกโครงการ = AllProject, เฉพาะโครงการ = OnlyProject, ไม่ยินยอม = NotConsent)")]
        public Guid? ConsentTypeMasterCenterID { get; set; }
        [ForeignKey("ConsentTypeMasterCenterID")]
        public MST.MasterCenter ConsentType { get; set; }

        [Description("ส่งข้อมูล Lead ให้ BC หรือไม่")]
        public bool IsSendToBC { get; set; }

        [Description("ช่องทางของข้อมูล Lead ที่เข้ามา")]
        public Guid? LeadSourceMasterCenterID { get; set; }
        [ForeignKey("LeadSourceMasterCenterID")]
        public MST.MasterCenter LeadSource { get; set; }

        [Description("สถานะการส่งข้อมูล Lead")]
        public Guid? SendLeadDataStatusMasterCenterID { get; set; }
        [ForeignKey("SendLeadDataStatusMasterCenterID")]
        public MST.MasterCenter SendLeadDataStatus { get; set; }

        public string LeadsGroupID { get; set; }

        public string UnitNo { get; set; }

        public string LineID { get; set; }

        public string CallbackTime { get; set; }

        public string EmployeeNo { get; set; }

        [Description("ReasonRevisit")]
        [MaxLength(5000)]
        public string ReasonRevisit { get; set; }

        [Description("RefLeadID")]
        [MaxLength(255)]
        public string RefLeadID { get; set; }


        [Description("LeadAppointDay")]
        [MaxLength(5000)]
        public string LeadAppointDay { get; set; }


        [Description("LeadAppointTime")]
        [MaxLength(5000)]
        public string LeadAppointTime { get; set; }


        [Description("LeadCallbackDay")]
        [MaxLength(5000)]
        public string LeadCallbackDay { get; set; }


        [Description("LeadCallbackTime")]
        [MaxLength(5000)]
        public string LeadCallbackTime { get; set; }

    }
}
