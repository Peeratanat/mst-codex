using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("เอกสารสัญญา")]
    [Table("AgreementConfig", Schema = Schema.PROJECT)]
    public class AgreementConfig : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }
        [Description("ผู้รับมอบอำนาจ 1 (ภาษาไทย)")]
        [MaxLength(100)]
        public string AttorneyNameTH1 { get; set; }
        [Description("ผู้รับมอบอำนาจ 2 (ภาษาไทย)")]
        [MaxLength(100)]
        public string AttorneyNameTH2 { get; set; }
        [Description("ผู้รับมอบอำนาจ 1 (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string AttorneyNameEN1 { get; set; }
        [Description("ผู้รับมอบอำนาจ 2 (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string AttorneyNameEN2 { get; set; }
        [Description("พยาน 1 (ภาษาไทย)")]
        [MaxLength(100)]
        public string WitnessTH1 { get; set; }
        [Description("พยาน 2 (ภาษาไทย)")]
        [MaxLength(100)]
        public string WitnessTH2 { get; set; }
        [Description("พยาน 1 (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string WitnessEN1 { get; set; }
        [Description("พยาน 2 (ภาษาอังกฤษ)")]
        [MaxLength(100)]
        public string WitnessEN2 { get; set; }
        [Description("ผู้รับมอบอำนาจขอปลอด")]
        [MaxLength(100)]
        public string PreferApproveName { get; set; }
        [Description("ตำแหน่งของผู้รับมอบอำนาจขอปลอด")]
        [MaxLength(100)]
        public string PreferApprovePosition { get; set; }

        [Description("ผู้รับมอบอำนาจโอนกรรมสิทธิ์")]
        //[MaxLength(100)]
        //public string AttorneyNameTransfer { get; set; }
        public Guid? AttorneyNameTransferID { get; set; }
        [ForeignKey("AttorneyNameTransferID")]
        public MST.AttorneyTransfer AttorneyNameTransfer { get; set; }


        [Description("ชื่อนิติบุคคล")]
        public Guid? LegalEntityID { get; set; }
        [ForeignKey("LegalEntityID")]
        public MST.LegalEntity LegalEntity { get; set; }


        [Description("อัตรากองทุนคอนโด (บาท)")]
        [Column(TypeName = "Money")]
        public decimal? CondoFundRate { get; set; }
        [Description("ค่าเบี้ยประกันอาคาร (บาท)")]
        [Column(TypeName = "Money")]
        public decimal? BuildingInsurance { get; set; }
        [Description("อัตราค่าส่วนกลาง (บาท)")]
        [Column(TypeName = "Money")]
        public decimal? PublicFundRate { get; set; }
        [Description("อัตราค่าส่วนกลาง AP ช่วยจ่าย (บาท)")]
        [Column(TypeName = "Money")]
        public decimal? PublicFundRateAP { get; set; }
        [Description("จำนวนเดือนที่เก็บค่าส่วนกลาง")]
        public int? PublicFundMonths { get; set; }
        [Description("จำนวนเดือนที่เก็บค่าส่วนกลาง AP ช่วยจ่าย")]
        public int? PublicFundMonthsAP { get; set; }
        [Description("ค่าธรรมเนียมการย้ายห้อง")]
        [Column(TypeName = "Money")]
        public decimal? RoomTransferFee { get; set; }
        [Description("ค่าธรรมเนียมการเปลี่ยนชื่อ")]
        [Column(TypeName = "Money")]
        public decimal? ChangeNameFee { get; set; }
        [Description("ค่าปรับอาศัยอยู่ร่วมกัน (บาท)")]
        [Column(TypeName = "Money")]
        public decimal? VisitFine { get; set; }
        [Description("ค่าปรับอาสัยอยู่ร่วมกัน (วัน)")]
        public int? VisitFineDay { get; set; }
        [Description("อัตราค่าปรับโอนกรรมสิทธิ์ล่าช้า")]
        public decimal? DelayTransfer { get; set; }
        [Description("จำนวนที่จอดรถ")]
        public int? ParkingUnits { get; set; }
        [Description("รวมจอดรถซ้อนคัน")]
        public bool IsIncludeDoubleParking { get; set; }
        [Description("วันที่หนังสือมอบอำนาจ")]
        public DateTime? AttorneyIssueDate { get; set; }
        [Description("วันสิ้นสุดสาธารณะ")]
        public DateTime? EndPublicDate { get; set; }
        [Description("วันที่หนังสือกรรมสิทธิ์ห้องชุด")]
        public DateTime? OwnerShipDate { get; set; }
        [Description("วันที่ Apporved")]
        public DateTime? EIAApprovedDate { get; set; }
        [Description("เลขที่ใบรับคำขออนุญาตจัดสรรที่ดิน")]
        public string PreLicenseLandNo { get; set; }
        [Description("วันที่ออกใบรับคำขออนุญาตจัดสรรที่ดิน")]
        public DateTime? PreLicenseLandIssueDate { get; set; }
        [Description("วันที่หมดอายุใบรับคำขออนุญาตจัดสรรที่ดิน")]
        public DateTime? PreLicenseLandExpireDate { get; set; }
        [Description("เลขที่ใบอนุญาตจัดสรรที่ดิน")]
        [MaxLength(1000)]
        public string LicenseLandNo { get; set; }
        [Description("วันที่ออกใบอนุญาตจัดสรรที่ดิน")]
        public DateTime? LicenseLandIssueDate { get; set; }
        [Description("วันที่หมดอายุใบอนุญาตจัดสรรที่ดิน")]
        public DateTime? LicenseLandExpireDate { get; set; }
        [Description("ไม่จัดสรรที่ดิน")]
        public bool IsNotLicenseLand { get; set; }
        [Description("เลขที่ใบอนุญาตก่อสร้างโครงการ")]
        [MaxLength(1000)]
        public string LicenseProductNo { get; set; }
        [Description("วันที่ออกใบอนุญาตก่อสร้างโครงการ")]
        public DateTime? LicenseProductIssueDate { get; set; }
        [Description("วันที่หมดอายุใบอนุญาตก่อสร้างโครงการ")]
        public DateTime? LicenseProductExpireDate { get; set; }
        [Description("หมายเหตุใบอนุญาตก่อสร้างโครงการ")]
        [MaxLength(5000)]
        public string LicenseProductRemark { get; set; }

        [Description("การพิมพ์เอกสาร หนังสือสัญญาขายที่ดิน/ห้องชุด สำหรับผู้ซื้อ")]
        public bool IsPrintAgreementForBuyer { get; set; }
        [Description("การพิมพ์เอกสาร หนังสือสัญญาขายที่ดิน/ห้องชุด สำหรับผู้ขาย")]
        public bool IsPrintAgreementForSeller { get; set; }
        [Description("การพิมพ์เอกสาร หนังสือสัญญาขายที่ดิน/ห้องชุด สำหรับสรรพกร")]
        public bool IsPrintAgreementForRevenue { get; set; }
        [Description("การพิมพ์เอกสาร หนังสือสัญญาขายที่ดิน/ห้องชุด ใบเปล่า")]
        public bool IsPrintAgreementEmpty { get; set; }


        [Description("เก็บจำนวนวันที่จะเปลี่ยนจากโครงการใหม่เป็นโครงการเก่า เช่น 90 วัน")]
        public int? AutoCCDurationForChangeNewToOld { get; set; }
        [Description("เก็บจำนวนวันที่จะ Auto ยกเลิกสำหรับโครงการใหม่ เช่น 60 วัน")]
        public int? AutoCCDurationForNewProject { get; set; }
        [Description("เก็บจำนวนวันที่จะ Auto ยกเลิกสำหรับโครงการเก่า เช่น 30 วัน")]
        public int? AutoCCDurationForOldProject { get; set; }

        [Description("วันที่มีการแก้ไข")]
        public DateTime? AutoCCUpdated { get; set; }

        [Description("User ที่ทำการแก้ไข")]
        public Guid? AutoCCUpdatedByUserID { get; set; }
        [ForeignKey("AutoCCUpdatedByUserID")]
        public USR.User AutoCCUpdatedByUser { get; set; }

        public bool? IsLockPublicFundRate { get; set; }
        public DateTime? LockPublicFundRateDate { get; set; }

        [Description("วันที่คาดว่าจะได้หนังสือเห็นชอบจากสำนักงานนโยบายและแผนทรัพยากรธรรมชาติและสิ่งแวดล้อม")]
        public DateTime? ExpectedEnvironmentalApprovalDate { get; set; }
        [Description("วันยื่นขออนุญาตก่อสร้างอาคารชุด หรือ วันที่ยื่นแจ้งการก่อสร้างอาคารชุดตามมาตรา 39 ทวิ")]
        public DateTime? CondoConstructionPermitSubmitDate { get; set; }
        [Description("วันที่คาดว่าจะได้รับใบอนุญาติ")]
        public DateTime? ExpectedPermitReceiveDate { get; set; }
        public string? CondoConstructionPermitNo { get; set; }
        public DateTime? CondoConstructionPermitDate { get; set; }
        public DateTime? CondoConstructionPermitExpireDate { get; set; }
        public string? CondoConstructionPermitRemark { get; set; }


        public string ReasonCancelFQTQ { get; set; }

        public DateTime? ReasonCancelFQTQDateTime { get; set; }

        [Description("User ReasonCancelFQTQ")]
        public Guid? ReasonCancelFQTQBy { get; set; }
    }
}