using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.TMP
{
    [Description("ข้อมูลผู้จอง")]
    [Table("Customer", Schema = Schema.TEMP)]
    public class Customer : BaseEntity
    {
        [Description("รหัสลูกค้า")]
        [MaxLength(50)]
        public string CustomerID { get; set; }
        [Description("Running Year")]
        public int? RunningYear { get; set; }
        [Description("Running")]
        public long? RunningID { get; set; }
        [Description("รหัสลูกค้า ใน contact")]
        [MaxLength(50)]
        public string ContactID { get; set; }
        [Description("คำนำหน้า")]
        [MaxLength(250)]
        public string TitleName { get; set; }
        [Description("ขื่อ")]
        [MaxLength(250)]
        public string FirstName { get; set; }
        [Description("นามสกุล")]
        [MaxLength(250)]
        public string LastName { get; set; }
        [Description("เลขบัตรประชาชน")]
        [MaxLength(50)]
        public string PersonCardID { get; set; }
        [Description("วันเกิด")]
        public DateTime? BirthDate { get; set; }
        [Description("สัญชาติ")]
        [MaxLength(250)]
        public string Nationality { get; set; }
        [Description("E-mail")]
        [MaxLength(250)]
        public string Email { get; set; }
        [Description("เลขที่ (ที่ติดต่อได้)")]
        [MaxLength(250)]
        public string HomeNo_1 { get; set; }
        [Description("หมู่ (ที่ติดต่อได้)")]
        [MaxLength(250)]
        public string Moo_1 { get; set; }
        [Description("หมู่บ้าน (ที่ติดต่อได้)")]
        [MaxLength(250)]
        public string Village_1 { get; set; }
        [Description("ซอย (ที่ติดต่อได้)")]
        public string Soi_1 { get; set; }
        [Description("ถนน (ที่ติดต่อได้)")]
        [MaxLength(250)]
        public string Road_1 { get; set; }
        [Description("ตำบล (ที่ติดต่อได้)")]
        [MaxLength(250)]
        public string SubDistrict_1 { get; set; }
        [Description("อำเภอ (ที่ติดต่อได้)")]
        [MaxLength(250)]
        public string District_1 { get; set; }
        [Description("จังหวัด (ที่ติดต่อได้)")]
        [MaxLength(250)]
        public string Province_1 { get; set; }
        [Description("รหัสไปรษณีย์ (ที่ติดต่อได้)")]
        [MaxLength(250)]
        public string Post_1 { get; set; }
        [Description("ประเทศ (ที่ติดต่อได้)")]
        [MaxLength(250)]
        public string Country_1 { get; set; }
        [Description("เบอร์โทรศัพท์ (บ้าน)")]
        [MaxLength(250)]
        public string Phone_1 { get; set; }
        [Description("เบอร์โทรศัพท์ (มือถือ)")]
        [MaxLength(250)]
        public string Mobile_1 { get; set; }
        [Description("เลขที่ (ตามบัตรปชช)")]
        [MaxLength(250)]
        public string HomeNo_2 { get; set; }
        [Description("หมู่ (ตามบัตรปชช)")]
        [MaxLength(250)]
        public string Moo_2 { get; set; }
        [Description("หมู่บ้าน (ตามบัตรปชช)")]
        [MaxLength(250)]
        public string Village_2 { get; set; }
        [Description("ซอย (ตามบัตรปชช)")]
        [MaxLength(250)]
        public string Soi_2 { get; set; }
        [Description("ถนน (ตามบัตรปชช)")]
        [MaxLength(250)]
        public string Road_2 { get; set; }
        [Description("ตำบล (ตามบัตรปชช)")]
        [MaxLength(250)]
        public string SubDistrict_2 { get; set; }
        [Description("อำเภอ (ตามบัตรปชช)")]
        [MaxLength(250)]
        public string District_2 { get; set; }
        [Description("จังหวัด (ตามบัตรปชช)")]
        [MaxLength(250)]
        public string Province_2 { get; set; }
        [Description("รหัสไปรษณีย์ (ตามบัตรปชช)")]
        [MaxLength(250)]
        public string Post_2 { get; set; }
        [Description("ประเทศ  (ตามบัตรปชช)")]
        [MaxLength(250)]
        public string Country_2 { get; set; }
        //[Description("")]
        //[MaxLength(250)]
        //public string Phone_2 { get; set; }
        //[Description("")]
        //[MaxLength(250)]
        //public string Mobile_2 { get; set; }
        [Description("ผู้สร้าง")]
        [MaxLength(250)]
        public string CreateBy { get; set; }
        [Description("วันที่สร้าง")]
        public DateTime? CreateDate { get; set; }
        [Description("ผู้แก้ไข")]
        [MaxLength(250)]
        public string EditBy { get; set; }
        [Description("วันที่แก้ไข")]
        public DateTime? EditDate { get; set; }
        [Description("คำนำหน้า ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string TitleNameENG { get; set; }
        [Description("ชื่อ ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string FirstNameENG { get; set; }
        [Description("นามสกุล ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string LastNameENG { get; set; }
        [Description("สัญชาติ ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string NationalityENG { get; set; }
        [Description("เลขที่ (ที่ติดต่อได้)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string HomeNo_1ENG { get; set; }
        [Description("หมู่ (ที่ติดต่อได้)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string Moo_1ENG { get; set; }
        [Description("หมู่บ้าน (ที่ติดต่อได้)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string Village_1ENG { get; set; }
        [Description("ซอย (ที่ติดต่อได้)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string Soi_1ENG { get; set; }
        [Description("ถนน (ที่ติดต่อได้)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string Road_1ENG { get; set; }
        [Description("ตำบล (ที่ติดต่อได้)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string SubDistrict_1ENG { get; set; }
        [Description("อำเภอ (ที่ติดต่อได้)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string District_1ENG { get; set; }
        [Description("จังหวัด (ที่ติดต่อได้)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string Province_1ENG { get; set; }
        //[Description("รหัสไปรษณีย์ (ที่ติดต่อได้)ภาษาอังกฤษ")]
        //[MaxLength(250)]
        //public string Post_1ENG { get; set; }
        [Description("ประเทศ (ที่ติดต่อได้)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string Country_1ENG { get; set; }
        //[Description("")]
        //[MaxLength(250)]
        //public string Phone_1ENG { get; set; }
        //[Description("")]
        //[MaxLength(250)]
        //public string Mobile_1ENG { get; set; }
        [Description("เลขที่ (ตามบัตรปชช)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string HomeNo_2ENG { get; set; }
        [Description("หมู่ (ตามบัตรปชช)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string Moo_2ENG { get; set; }
        [Description("หมู่บ้าน (ตามบัตรปชช)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string Village_2ENG { get; set; }
        [Description("ซอย (ตามบัตรปชช)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string Soi_2ENG { get; set; }
        [Description("ถนน (ตามบัตรปชช)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string Road_2ENG { get; set; }
        [Description("ตำบล (ตามบัตรปชช)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string SubDistrict_2ENG { get; set; }
        [Description("อำเภอ (ตามบัตรปชช)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string District_2ENG { get; set; }
        [Description("จังหวัด (ตามบัตรปชช)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string Province_2ENG { get; set; }
        //[Description("ภาษาอังกฤษ")]
        //[MaxLength(250)]
        //public string Post_2ENG { get; set; }
        [Description("ประเทศ (ตามบัตรปชช)ภาษาอังกฤษ")]
        [MaxLength(250)]
        public string Country_2ENG { get; set; }
        [Description("WhatsApp ID")]
        [MaxLength(250)]
        public string WhatsAppID { get; set; }
        [Description("WeChat ID")]
        [MaxLength(250)]
        public string WeChatID { get; set; }
        [Description("Line ID")]
        [MaxLength(250)]
        public string LineID { get; set; }
        [Description("สถานะสัญชาติไทย")]
        public bool? IsNationalityThai { get; set; }
    }
}
