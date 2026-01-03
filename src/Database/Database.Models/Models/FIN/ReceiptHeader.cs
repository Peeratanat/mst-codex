using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("ใบเสร็จรับเงินตัวจริง")]
    [Table("ReceiptHeader", Schema = Schema.FINANCE)]
    public class ReceiptHeader : BaseEntity
    {
        [Description("เลขที่ใบเสร็จรับเงินตัวจริง")]
        [MaxLength(100)]
        public string ReceiptNo { get; set; }

        [Description("เลขที่ใบเสร็จชั่วคราว")]
        [MaxLength(100)]
        public string ReceiptTempNo { get; set; }

        [Description("เลขที่ใบจอง")]
        [MaxLength(100)]
        public string BookingNo { get; set; }

        [Description("เลขที่สัญญา")]
        [MaxLength(100)]
        public string AgreementNo { get; set; }

        [Description("จำนวนเงินรวมของใบเสร็จ")]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }

        [Description("ข้อมูลการรับชำระเงิน")]
        public Guid? PaymentID { get; set; }

        [ForeignKey("PaymentID")]
        public Payment Payment { get; set; }

        [Description("ID บริษัท")]
        public Guid? CompanyID { get; set; }

        [Description("ประจำตัวผู้เสียภาษี")]
        [MaxLength(1000)]
        public string CompanyTaxID { get; set; }

        [Description("ชื่อบริษัท")]
        [MaxLength(1000)]
        public string CompanyNameTH { get; set; }

        [Description("ชื่อบริษัท (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string CompanyNameEN { get; set; }

        [Description("บ้านเลขที่ (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanyHouseNoTH { get; set; }

        [Description("บ้านเลขที่ภาษาอังกฤษ (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanyHouseNoEN { get; set; }

        [Description("ชื่อตึกภาษาไทย (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanyBuildingTH { get; set; }

        [Description("ชื่อตึกภาษาอังกฤษ (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanyBuildingEN { get; set; }

        [Description("ซอยภาษาไทย (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanySoiTH { get; set; }

        [Description("ซอยภาษาอังกฤษ (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanySoiEN { get; set; }

        [Description("ถนนภาษาไทย (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanyRoadTH { get; set; }

        [Description("ถนนภาษาอังกฤษ (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanyRoadEN { get; set; }

        [Description("จังหวัดภาษาอังกฤษ (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanyProvinceEN { get; set; }

        [Description("จังหวัด (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanyProvinceTH { get; set; }

        [Description("อำเภอภาษาอังกฤษ (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanyDistrictEN { get; set; }

        [Description("อำเภอ (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanyDistrictTH { get; set; }

        [Description("ตำบลภาษาอังกฤษ (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanySubDistrictEN { get; set; }

        [Description("ตำบล (ที่อยู่บริษัท)")]
        [MaxLength(1000)]
        public string CompanySubDistrictTH { get; set; }

        [Description("รหัสไปรษณีย์ (ที่อยู่บริษัท)")]
        [MaxLength(50)]
        public string CompanyPostalCode { get; set; }
        [Description("เบอร์โทร (ที่อยู่บริษัท)")]
        [MaxLength(50)]
        public string CompanyTelephone { get; set; }
        [Description("เบอร์แฟ๊กซ์ (ที่อยู่บริษัท)")]
        [MaxLength(50)]
        public string CompanyFax { get; set; }

        [Description("ประเภทลูกค้า 0=ในประเทศ  1=ต่างประเทศ")]
        public bool IsForeigner { get; set; }

        [Description("ลูกค้าที่ส่งให้")]
        public Guid? SendToContactID { get; set; }
        [ForeignKey("SendToContactID")]
        public CTM.Contact SendToContact { get; set; }

        [Description("ชื่อ นามสกุลลูกค้าที่แสดงบนใบเสร็จ")]
        [MaxLength(500)]
        public string ContactName { get; set; }

        [Description("รหัสลูกค้า")]
        [MaxLength(100)]
        public string ContactNo { get; set; }
        [Description("คำนำหน้า")]
        [MaxLength(100)]
        public string ContactTitle { get; set; }
        [Description("ชื่อจริง (ภาษาไทย)")]
        [MaxLength(100)]
        public string ContactFirstNameTH { get; set; }
        [Description("ชื่อกลาง (ภาษาไทย)")]
        [MaxLength(100)]
        public string ContactMiddleNameTH { get; set; }
        [Description("นามสกุล (ภาษาไทย)")]
        [MaxLength(100)]
        public string ContactLastNameTH { get; set; }
        [Description("นามสกุล (ภาษาไทย)")]
        [MaxLength(100)]
        public string ContactDisplayNameTH { get; set; }
        [Description("คำนำหน้า (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string ContactTitleEN { get; set; }
        [Description("ชื่อจริง (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string ContactFirstNameEN { get; set; }
        [Description("ชื่อกลาง (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string ContactMiddleNameEN { get; set; }
        [Description("นามสกุล (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string ContactLastNameEN { get; set; }
        [MaxLength(100)]
        public string ContactDisplayNameEN { get; set; }
        [Description("บ้านเลขที่ (ภาษาไทย)")]
        [MaxLength(100)]
        public string ContactHouseNoTH { get; set; }
        [Description("หมู่ที่ (ภาษาไทย)")]
        [MaxLength(100)]
        public string ContactMooTH { get; set; }
        [Description("หมู่บ้าน/อาคาร (ภาษาไทย)")]
        [MaxLength(1000)]
        public string ContactVillageTH { get; set; }
        [Description("ซอย (ภาษาไทย)")]
        [MaxLength(100)]
        public string ContactSoiTH { get; set; }
        [Description("ถนน (ภาษาไทย)")]
        [MaxLength(100)]
        public string ContactRoadTH { get; set; }
        [Description("บ้านเลขที่ (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string ContactHouseNoEN { get; set; }
        [Description("หมู่ที่ (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string ContactMooEN { get; set; }
        [Description("หมู่บ้าน/อาคาร (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string ContactVillageEN { get; set; }
        [Description("ซอย (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string ContactSoiEN { get; set; }
        [Description("ถนน (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string ContactRoadEN { get; set; }
        [Description("รหัสไปรษณีย์")]
        [MaxLength(50)]
        public string ContactPostalCode { get; set; }
        [Description("ประเทศ")]
        [MaxLength(1000)]
        public string ContactCountryTH { get; set; }
        [Description("ประเทศ (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string ContactCountryEN { get; set; }
        [Description("จังหวัด")]
        [MaxLength(1000)]
        public string ContactProvinceTH { get; set; }
        [Description("จังหวัด (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string ContactProvinceEN { get; set; }
        [Description("เขต/อำเภอ")]
        [MaxLength(1000)]
        public string ContactDistrictTH { get; set; }
        [Description("เขต/อำเภอ (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string ContactDistrictEN { get; set; }
        [Description("แขวง/ตำบล")]
        [MaxLength(1000)]
        public string ContactSubDistrictTH { get; set; }
        [Description("แขวง/ตำบล (ภาษาอังกฤษ)")]
        [MaxLength(1000)]
        public string ContactSubDistrictEN { get; set; }

        
        [Description("วันที่ชำระเงิน")]
        public DateTime ReceiveDate { get; set; }

        [Description("ID โครงการ")]
        public Guid? ProjectID { get; set; }

        [Description("รหัสโครงการ")]
        [MaxLength(1000)]
        public string ProjectNo { get; set; }

        [Description("ชื่อโครงการ")]
        [MaxLength(1000)]
        public string ProjectName { get; set; }

        [Description("ชื่อโครงการ EN")]
        [MaxLength(1000)]
        public string ProjectNameEN { get; set; }

        [Description("ID Unit")]
        public Guid? UnitID { get; set; }

        [Description("เลขที่แปลง")]
        [MaxLength(100)]
        public string UnitNo { get; set; }

        [Description("ราคาตามสัญญา/Asset Value")]
        [Column(TypeName = "Money")]
        public decimal AgreementPrice { get; set; }

        [Description("จำนวนงวดตามสัญญา/Installment")]
        [Column(TypeName = "Money")]
        public decimal Installment { get; set; }

        [Description("เงินจอง")]
        [Column(TypeName = "Money")]
        public decimal BookingAmount { get; set; }

        [Description("เงินสัญญา")]
        [Column(TypeName = "Money")]
        public decimal AgreementAmount { get; set; }

        [Description("เงินดาวน์")]
        [Column(TypeName = "Money")]
        public decimal InstallmentAmount { get; set; }

        [Description("เงินโอน")]
        [Column(TypeName = "Money")]
        public decimal TransferAmount { get; set; }

        [Description("Ref1")]
        public string Ref1 { get; set; }

        [Description("Ref2")]
        public string Ref2 { get; set; }

        [Description("วันที่นำฝาก")]
        public DateTime? DepositDate { get; set; }

        [Description("วันที่บันทึกการนำฝาก   วันทีทำรายการ")]
        public DateTime? DepositSaveDate { get; set; }

        [Description("เลขที่ใบนำฝากเก่า")]
        public string OldDepoistNo { get; set; }

        [Description("วันที่นำฝากเก่า")]
        public DateTime? OldDepositDate { get; set; }

        [Description("Bank(Eng)")]
        public string BankNameEn { get; set; }

        [Description("จำนวนเงิน(Th)")]
        public string AmountTh { get; set; }

        [Description("จำนวนเงิน(Eng)")]
        public string AmountEn { get; set; }


        [Description("งวดถัดไป")]
        public string NextInstallmentPeriod { get; set; }

        [Description("ราคางวดถัดไป")]
        [Column(TypeName = "Money")]
        public decimal NextInstallmentAmount { get; set; }
        
        [Description("วันที่ต้องจ่ายงวดถัดไป")]
        public DateTime? NextInstallmentDueDate { get; set; }

        [Description("งวดที่ค้างชำระ")]
        public string ArrearsPeriod { get; set; }

        [Description("จำนวนเงินที่ค้างชำระ")]
        [Column(TypeName = "Money")]
        public decimal ArrearsAmount { get; set; }

        [Description("วันที่ค้างชำระ From")]
        public DateTime? ArrearsDueDate { get; set; }

        [Description("วันที่ค้างชำระ To")]
        public DateTime? ArrearsDueDateTo { get; set; }

        [Description("รวมจำนวนเงินที่ลูกค้าชำระมาแล้ว")]
        [Column(TypeName = "Money")]
        public decimal SumPaid { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        

        [Description("สถานะส่ง Email ส่งแล้ว/ยังไม่ได้ส่ง")]
        public Guid? ReceiptSendEmailStatusMastercenterID { get; set; }
        [ForeignKey("ReceiptSendEmailStatusMastercenterID")]
        public MST.MasterCenter ReceiptSendEmailStatus { get; set; }

        [Description("ระงับการส่ง Email")]
        public bool? IsLockEmail { get; set; }

        [Description("วันที่ ระงับการส่ง Email")]
        public DateTime LockEmailDate { get; set; }

        [Description("ระงับการส่ง Email โดย")]
        public Guid? LockEmailByUserID { get; set; }
        [ForeignKey("LockEmailByUserID")]
        public USR.User LockEmailByUser { get; set; }



        [Description("รวมจำนวนที่ส่งโรงพิมพ์")]
        public int TotalSendPrinting { get; set; }

        [Description("รวมจำนวนที่ส่งอีเมลล์")]
        public int TotalSendEmail { get; set; }

        [Description("LockEmailRemarkCancel")]
        [MaxLength(4000)]
        public string LockEmailRemarkCancel { get; set; }


        [Description("LockPrintingRemarkCancel")]
        [MaxLength(4000)]
        public string LockPrintingRemarkCancel { get; set; }


        [Description("สถานะส่ง โรงพิมพ์ ส่งแล้ว/ยังไม่ได้ส่ง")]
        public Guid? ReceiptSendPrintingStatusMastercenterID { get; set; }
        [ForeignKey("ReceiptSendPrintingStatusMastercenterID")]
        public MST.MasterCenter ReceiptSendPrintingStatus { get; set; }

        [Description("ระงับการส่ง โรงพิมพ์")]
        public bool? IsLockPrinting { get; set; }

        [Description("วันที่ ระงับการส่ง โรงพิมพ์")]
        public DateTime LockPrintingDate { get; set; }

        [Description("ระงับการส่ง โรงพิมพ์ โดย")]
        public Guid? LockPrintingByUserID { get; set; }
        [ForeignKey("LockPrintingByUserID")]
        public USR.User LockPrintingByUser { get; set; }

        [Description("FilePath")]
        [MaxLength(1000)]
        public string FilePath { get; set; }

        [Description("FileName")]
        [MaxLength(1000)]
        public string FileName { get; set; }
    }
}
