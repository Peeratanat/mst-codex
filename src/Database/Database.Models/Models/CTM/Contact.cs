using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูลลูกค้า")]
    [Table("Contact", Schema = Schema.CUSTOMER)]
    public class Contact : BaseEntity
    {
        [Description("รหัสลูกค้า")]
        [MaxLength(25)]
        public string ContactNo { get; set; }

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
        [Description("ชื่อเต็ม (ภาษาไทย) (คำนำหน้าชื่อ + ชื่อจริง + ' ' + ชื่อกลาง + ' ' + นามสกุล)")]
        [MaxLength(500)]
        public string FullnameTH { get; set; }
        [Description("ชื่อเล่น (ภาษาไทย)")]
        [MaxLength(250)]
        public string Nickname { get; set; }

        [Description("คำนำหน้าชื่อ (ภาษาอังกฤษ)")]
        public Guid? ContactTitleENMasterCenterID { get; set; }
        [ForeignKey("ContactTitleENMasterCenterID")]
        public MST.MasterCenter ContactTitleEN { get; set; }

        [Description("คำนำหน้าเพิ่มเติม (ภาษาอังกฤษ)")]
        [MaxLength(250)]
        public string TitleExtEN { get; set; }
        [Description("ชื่อจริง (ภาษาอังกฤษ)")]
        [MaxLength(250)]
        public string FirstNameEN { get; set; }
        [Description("ชื่อกลาง (ภาษาอังกฤษ)")]
        [MaxLength(250)]
        public string MiddleNameEN { get; set; }
        [Description("นามสกุล (ภาษาอังกฤษ)")]
        [MaxLength(250)]
        public string LastNameEN { get; set; }
        [Description("ชื่อเต็ม (ภาษาอังกฤษ) (คำนำหน้าชื่อ + ชื่อจริง + ' ' + ชื่อกลาง + ' ' + นามสกุล)")]
        [MaxLength(500)]
        public string FullnameEN { get; set; }
        [Description("หมายเลขบัตรประชาชน")]
        [MaxLength(50)]
        public string CitizenIdentityNo { get; set; }
        [Description("วันหมดอายุบัตรประชาชน")]
        public DateTime? CitizenExpireDate { get; set; }

        [Description("สัญชาติ")]
        public Guid? NationalMasterCenterID { get; set; }
        [ForeignKey("NationalMasterCenterID")]
        public MST.MasterCenter National { get; set; }
        [Description("สัญชาติอื่นๆ (จากข้อมูล crm เก่า)")]
        [MaxLength(1000)]
        public string OtherNationalTH { get; set; }
        [Description("สัญชาติอื่นๆ (จากข้อมูล crm เก่า)")]
        [MaxLength(1000)]
        public string OtherNationalEN { get; set; }

        [Description("เพศ")]
        public Guid? GenderMasterCenterID { get; set; }
        [ForeignKey("GenderMasterCenterID")]
        public MST.MasterCenter Gender { get; set; }

        [Description("เลขประจำตัวผู้เสียภาษี")]
        [MaxLength(100)]
        public string TaxID { get; set; }
        [Description("เบอร์โทรศัพท์")]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [Description("เบอร์ต่อ")]
        [MaxLength(50)]
        public string PhoneNumberExt { get; set; }
        [Description("ชื่อผู้ติดต่อ")]
        [MaxLength(100)]
        public string ContactFirstName { get; set; }
        [Description("นามสกุลผู้ติดต่อ")]
        [MaxLength(100)]
        public string ContactLastname { get; set; }
        [Description("WeChat ID")]
        [MaxLength(100)]
        public string WeChatID { get; set; }
        [Description("WhatsApp ID")]
        [MaxLength(100)]
        public string WhatsAppID { get; set; }
        [Description("Line ID")]
        [MaxLength(100)]
        public string LineID { get; set; }
        [Description("วันเกิด")]
        public DateTime? BirthDate { get; set; }

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

        [Description("ชื่อบิดา")]
        [MaxLength(100)]
        public string FatherName { get; set; }
        [Description("สัญชาติของบิดา")]
        public Guid? FatherNationalMasterCenterID { get; set; }
        [ForeignKey("FatherNationalMasterCenterID")]
        public MST.MasterCenter FatherNational { get; set; }
        [Description("สัญชาติอื่นๆ ของบิดา")]
        [MaxLength(100)]
        public string FatherOtherNational { get; set; }

        [Description("ชื่อมารดา")]
        [MaxLength(100)]
        public string MotherName { get; set; }
        [Description("สัญชาติของมารดา")]
        public Guid? MotherNationalMasterCenterID { get; set; }
        [ForeignKey("MotherNationalMasterCenterID")]
        public MST.MasterCenter MotherNational { get; set; }
        [Description("สัญชาติอื่นๆ ของมารดา")]
        [MaxLength(100)]
        public string MotherOtherNational { get; set; }

        [Description("ลูกค้า VIP")]
        public bool IsVIP { get; set; }
        [Description("ลำดับของ Contact")]
        public int Order { get; set; }
        [Description("เป็นคนไทยหรือไม่")]
        public bool IsThaiNationality { get; set; }

        [Description("Last Opportunity")]
        public Guid? LastOpportunityID { get; set; }
        [ForeignKey("LastOpportunityID")]
        public Opportunity LastOpportunity { get; set; }

        [Description("จำนวน Opportunity")]
        public int OpportunityCount { get; set; }

        public List<ContactPhone> ContactPhones { get; set; }

        [Description("แผนกของลูกค้าที่เป็นพนักงาน/ญาติ")]
        [MaxLength(350)]
        public string DepartmentName { get; set; }

        [Description("ประเภทลูกค้า")]
        public Guid? CustomerTypeMasterCenterID { get; set; }
        [ForeignKey("CustomerTypeMasterCenterID")]
        public MST.MasterCenter CustomerType { get; set; }

        [Description("ประเภทการยินยอม (ทุกโครงการ = AllProject, เฉพาะโครงการ = OnlyProject, ไม่ยินยอม = NotConsent)")]
        public Guid? ConsentTypeMasterCenterID { get; set; }
        [ForeignKey("ConsentTypeMasterCenterID")]
        public MST.MasterCenter ConsentType { get; set; }

        [Description("ส่งข้อมูล Contact ให้ BC หรือไม่")]
        public bool IsSendToBC { get; set; }

        [Description("Flag แสดงว่าเป็น VVIP")]
        public bool IsVVIP { get; set; }
        [Description("วันที่มีการแก้ไขค่า IsVVIP")]
        public DateTime? ChangeVVIPDate { get; set; }

        [Description("User ที่ทำการแก้ไขค่า IsVVIP (UUID)")]
        public Guid? ChangeVVIPByUserID { get; set; }
        [ForeignKey("ChangeVVIPByUserID")]
        public USR.User ChangeVVIPByUser { get; set; }

        [Description("ผู้อนุมัติ VVIP")]
        [MaxLength(100)]
        public string VVIPExcomApprover { get; set; }

    }
}
