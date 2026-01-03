using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.TMP
{
    [Description("ข้อมูลจอง")]
    [Table("Booking", Schema = Schema.TEMP)]
    public class Booking : BaseEntity
    {
        [Description("เลขที่จอง")]
        [MaxLength(50)]
        public string BookingNumber { get; set; }
        [Description("วันที่จอง")]
        public DateTime BookingDate { get; set; }
        [Description("วันที่จอง format longdate")]
        [MaxLength(100)]
        public string BookingDateString { get; set; }
        [Description("รหัสลูกค้าจอง")]
        [MaxLength(100)]
        public string BookingCustomerID { get; set; }
        [Description("รหัสลูกค้าทำสัญญา")]
        [MaxLength(100)]
        public string ContractCustomerID { get; set; }
        [Description("รหัสโครงการ")]
        [MaxLength(50)]
        public string ProjectID { get; set; }
        [Description("เลขที่แปลง/ห้อง")]
        [MaxLength(50)]
        public string UnitNumber { get; set; }
        [Description("ชั่น")]
        [MaxLength(50)]
        public string Floor { get; set; }
        [Description("พื้นที่ขาย")]
        public double Area { get; set; }
        [Description("แบบบ้าน")]
        [MaxLength(250)]
        public string ModelName { get; set; }
        [Description("หน้ากว้าง")]
        public double ModelWidth { get; set; }
        [Description("ราคาขายตั้งต้น")]
        [Column(TypeName = "Money")]
        public decimal? SellPrice { get; set; }
        [Description("ส่วนลดเงินสด")]
        [Column(TypeName = "Money")]
        public decimal? Discount { get; set; }
        [Description("ราคาขายสุทธิ")]
        [Column(TypeName = "Money")]
        public decimal? SellNetPrice { get; set; }
        [Description("เงินจอง")]
        [Column(TypeName = "Money")]
        public decimal? BookingAmount { get; set; }
        [Description("เงินทำสัญญา")]
        [Column(TypeName = "Money")]
        public decimal? ContractAmount { get; set; }
        [Description("เงินดาวน์")]
        [Column(TypeName = "Money")]
        public decimal? DownAmount { get; set; }
        [Description("จำนวนงวดดาวน์")]
        public int? DownTimeAmount { get; set; }
        [Description("วันจ่ายเงินดาวน์งวดแรก")]
        public DateTime? DownFirstDate { get; set; }
        [Description("วันที่นัดโอน")]
        public DateTime? TransferDate { get; set; }
        [Description("วันที่นัดทำสัญญา")]
        public DateTime? ContractDueDate { get; set; }
        [Description("ราคาต่อหน่วย")]
        [Column(TypeName = "Money")]
        public decimal? IncreasingAreaPrice { get; set; }
        [Description("ราคาห้องมุม")]
        [Column(TypeName = "Money")]
        public decimal? CornerRoomPrice { get; set; }
        [Description("ผู้สร้าง")]
        public string CreateBy { get; set; }
        [Description("วันที่สร้าง")]
        public DateTime CreateDate { get; set; }
        [Description("ผู้แก้ไข")]
        public string EditBy { get; set; }
        [Description("วันที่แก้ไข")]
        public DateTime? EditDate { get; set; }
        [Description("สถานะยกเลิก")]
        public bool? IsCancel { get; set; }
        [Description("ผู้ยกเลิก")]
        public string CancelBy { get; set; }
        [Description("วันที่ยกเลิก")]
        public DateTime? CancelDate { get; set; }
        [Description("วันที่อีพโหลด")]
        public DateTime? UploadDate { get; set; }
        [Description("วันที่พิมพ์เอกสาร")]
        public DateTime? PrintTime { get; set; }
        [Description("จำนวนครั้งที่พิมพ์")]
        public int? PrintCount { get; set; }
        [Description("เปอร์เซ็นเงินดาวน์")]
        public decimal? PercentDown { get; set; }
        [Description("เงินดาวน์ทั้งหมด")]
        [Column(TypeName = "Money")]
        public decimal? DownTotalAmount { get; set; }
        [Description("งวดดาวน์พิเศษ")]
        public string SpecialDownTime { get; set; }
        [Description("เงินดาวน์พิเศษ")]
        [Column(TypeName = "Money")]
        public decimal? SpecialDownAmount { get; set; }
        [Description("เงินโอน")]
        [Column(TypeName = "Money")]
        public decimal? TransferAmount { get; set; }
        [Description("ส่วนลด ณ. วันโอน")]
        [Column(TypeName = "Money")]
        public decimal? TransferDiscount { get; set; }
        [Description("ราคาเฟอร์นิเจอร์")]
        public decimal? FerniturePrice { get; set; }
        [Description("สถานะ AP ช่วยจ่าย")]
        [Column(TypeName = "Money")]
        public bool? IsApPay { get; set; }
        [Description("เงินดาวน์งวดสุดท้าย")]
        [Column(TypeName = "Money")]
        public decimal? LastDownAmount { get; set; }
        [Description("วันที่เงินดาวน์งวดสุดท้าย")]
        public DateTime? LastDownDate { get; set; }
        [Description("สถานะยืนยัน")]
        public bool? IsConfirm { get; set; }
        [Description("ผู้ยยืนยัน")]
        public string ConfirmBy { get; set; }
        [Description("วันที่ยืนยัน")]
        public DateTime? ConfirmDate { get; set; }
        [Description("เหตุผลใช้ระบบ offline")]
        public int? ReasonID { get; set; }
        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }
        [Description("ผู้ใช้งาน")]
        public string UserID { get; set; }
        [Description("ผู้อัพโหลด")]
        public string UploadBy { get; set; }
        [Description("ประเถทการจอง")]
        [MaxLength(100)]
        public string BookingType { get; set; }
        [Description("เลขที่ใบเสนอราคา")]
        [MaxLength(100)]
        public string QuatationID { get; set; }
        [Description("รหัสโปรโมชั่น")]
        [MaxLength(100)]
        public string PromotionID { get; set; }
        [Description("ผมรวมใช้โปรโมชั่น")]
        [Column(TypeName = "Money")]
        public decimal? TotalPromotionAmount { get; set; }
        [Description("ผมรวมใช้ budget โปรโมชั่น")]
        [Column(TypeName = "Money")]
        public decimal? TotalBudgetPromotion { get; set; }
        [Description("รหัส LC ปิดการขาย")]
        [MaxLength(100)]
        public string LCID { get; set; }
        [Description("รหัส LC ประจำโครงการ")]
        [MaxLength(100)]
        public string LCProjectID { get; set; }
        [Description("รหัส LC Center")]
        [MaxLength(100)]
        public string LCCID { get; set; }
        [Description("ประเภทการจ่ายเงิน (1=เงินจอง,2=เงินจอง+สัญญา,3=เงินจอง5%)")]
        public int? PaymentType { get; set; }    }
}
