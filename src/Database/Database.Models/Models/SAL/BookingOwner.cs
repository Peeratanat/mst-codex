using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("ผู้จอง")]
    [Table("BookingOwner", Schema = Schema.SALE)]
    public class BookingOwner : BaseEntity
    {
        [Description("ลำดับ")]
        public int Order { get; set; }

        [Description("ใบจอง")]
        public Guid? BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("ลูกค้า")]
        public Guid? FromContactID { get; set; }
        [ForeignKey("FromContactID")]
        public CTM.Contact FromContact { get; set; }

        [Description("ผู้จองหลัก")]
        public bool IsMainOwner { get; set; }
        [Description("เป็นผู้ทำสัญญาในใบจอง")]
        public bool IsAgreementOwner { get; set; }

        [Description("เหตุผลที่ลบชื่อผู้ทำสัญญา")]
        [MaxLength(5000)]
        public string DeleteReason { get; set; }

        //FromContact
        [Description("รหัสลูกค้า")]
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
        [Description("หมายเลขบัตรประชาชน")]
        [MaxLength(50)]
        public string CitizenIdentityNo { get; set; }
        [Description("วันหมดอายุบัตรประชาชน")]
        public DateTime? CitizenExpireDate { get; set; }

        [Description("สัญชาติ")]
        public Guid? NationalMasterCenterID { get; set; }
        [ForeignKey("NationalMasterCenterID")]
        public MST.MasterCenter National { get; set; }
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

        //[Description("ชื่อคู่สมรส")]
        //[MaxLength(100)]
        //public string MarriageName { get; set; }
        //[Description("สัญชาติของคู่สมรส")]
        //[MaxLength(100)]
        //public string MarriageNational { get; set; }
        //[Description("สัญชาติอื่นๆ ของคู่สมรส")]
        //[MaxLength(100)]
        //public string MarriageOtherNational { get; set; }

        //[Description("ชื่อบิดา")]
        //[MaxLength(100)]
        //public string FatherName { get; set; }
        //[Description("สัญชาติของบิดา")]
        //[MaxLength(100)]
        //public string FatherNational { get; set; }
        //[Description("สัญชาติอื่นๆ ของบิดา")]
        //[MaxLength(100)]
        //public string FatherOtherNational { get; set; }

        //[Description("ชื่อมารดา")]
        //[MaxLength(100)]
        //public string MotherName { get; set; }
        //[Description("สัญชาติของมารดา")]
        //[MaxLength(100)]
        //public string MotherNational { get; set; }
        //[Description("สัญชาติอื่นๆ ของมารดา")]
        //[MaxLength(100)]
        //public string MotherOtherNational { get; set; }

        [Description("ลูกค้า VIP")]
        public bool IsVIP { get; set; }
        
        [Description("เป็นคนไทยหรือไม่")]
        public bool IsThaiNationality { get; set; }

        [Description("สัญชาติอื่นๆ")]
        [MaxLength(100)]
        public string OtherNational { get; set; }


        [Description("ชื่อเต็ม (ภาษาไทย) (คำนำหน้าชื่อ + ชื่อจริง + ' ' + ชื่อกลาง + ' ' + นามสกุล)")]
        [MaxLength(500)]
        public string FullnameTH { get; set; }
        [Description("ชื่อเต็ม (ภาษาอังกฤษ) (คำนำหน้าชื่อ + ชื่อจริง + ' ' + ชื่อกลาง + ' ' + นามสกุล)")]
        [MaxLength(500)]
        public string FullnameEN { get; set; }

        public static BookingOwner CreateFromContact(CTM.Contact contact)
        {
            BookingOwner model = new BookingOwner();
            var contactProps = contact.GetType().GetProperties();
            var props = model.GetType().GetProperties();
            foreach (var contactProp in contactProps)
            {
                foreach (var prop in props)
                {
                    if (contactProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(model, contactProp.GetValue(contact));
                    }
                }
            }
            model.FromContactID = contact.ID;

            return model;
        }

        public void UpdateFromContact(CTM.Contact contact)
        {
            var contactProps = contact.GetType().GetProperties();
            var props = this.GetType().GetProperties();
            foreach (var contactProp in contactProps)
            {
                foreach (var prop in props)
                {
                    if (contactProp.Name == prop.Name &&
                        (prop.Name != "ID" && prop.Name != "Order"))
                    {
                        prop.SetValue(this, contactProp.GetValue(contact));
                    }
                }
            }
        }

    }
}
