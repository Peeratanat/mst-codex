using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("สัญญา")]
    [Table("Agreement", Schema = Schema.SALE)]
    public class Agreement : BaseEntity
    {
        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("แปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }

        [Description("ใบจอง")]
        public Guid? BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("สถานะสัญญา")]
        public Guid? AgreementStatusMasterCenterID { get; set; }
        [ForeignKey("AgreementStatusMasterCenterID")]
        public MST.MasterCenter AgreementStatus { get; set; }

        [Description("สัญญาเลขที่")]
        [MaxLength(50)]
        public string AgreementNo { get; set; }
        [Description("วันที่ทำสัญญา")]
        public DateTime? ContractDate { get; set; }
        [Description("โอนกรรมสิทธิ์ภายในวันที่")]
        public DateTime? TransferOwnershipDate { get; set; }

        [Description("วันที่ลงนามสัญญา")]
        public DateTime? SignAgreementDate { get; set; }

        public Guid? SignContractRequestUserID { get; set; }
        [ForeignKey("SignContractRequestUserID")]
        public USR.User SignContractRequestUser { get; set; }
        [Description("วันที่ขออนุมัติ Sign Contract ล่าสุด")]
        public DateTime? SignContractRequestDate { get; set; }
        [Description("วันที่อนุมัติ Sign Contract")]
        public DateTime? SignContractApprovedDate { get; set; }
        [Description("สถานะอนุมัติ Sign Contract")]
        public bool? IsSignContractApproved { get; set; }

        [Description("สถานะอนุมัติพิมพ์สัญญา")]
        public bool? IsPrintApproved { get; set; }
        [Description("วัน-เวลาที่อนุมัติพิมพ์สัญญา")]
        public DateTime? PrintApprovedDate { get; set; }
        [Description("ผู้อนุมัติพิมพ์สัญญา")]
        public Guid? PrintApprovedByUserID { get; set; }
        [ForeignKey("PrintApprovedByUserID")]
        public USR.User PrintApprovedBy { get; set; }

        [Description("ราคาพื้นที่ต่อหน่วย")]
        [Column(TypeName = "Money")]
        public decimal AreaPricePerUnit { get; set; }
        [Description("พื้นที่เพิ่มลด ค่าบวก = พื้นที่โฉนด > พื้นที่ขาย")]
        public double? OffsetArea { get; set; }
        [Description("ราคาพื้นที่เพิ่มลด")]
        [Column(TypeName = "Money")]
        public decimal? OffsetAreaPrice { get; set; }

        [Description("สถานะการก่อสร้าง (สำหรับโครงการแนวสูง)")]
        public Guid? HighRiseConstructionStatusMasterCenterID { get; set; }
        [ForeignKey("HighRiseConstructionStatusMasterCenterID")]
        public MST.MasterCenter HighRiseConstructionStatus { get; set; }

        [Description("บริษัทจ่ายงวดสุดท้าย")]
        public bool IsSellerPayLastDownInstallment { get; set; }

        [Description("ถูกพิมพ์ Payment card แล้ว")]
        public bool IsPrintPaymentCard { get; set; }
        [Description("วันที่พิมพ์ Payment card")]
        public DateTime? PrintPaymentCardDate { get; set; }

        [Description("ชื่อไฟล์ QR Code")]
        [MaxLength(1000)]
        public string QRCodeFileName { get; set; }

        [Description("รายการยกเลิก")]
        public bool IsCancel { get; set; }

        [Description("Remark")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Description("เลขที่ Ref1")]
        [MaxLength(50)]
        public string BillPaymentNo { get; set; }

        [Description("ชื่อเต็ม ผู้ทำสัญญาหลัก")]
        [MaxLength(500)]
        public string MainOwnerName { get; set; }

        [Description("ชื่อเต็ม ผู้ทำสัญญาทั้งหมด")]
        [MaxLength(1500)]
        public string AllOwnerName { get; set; }

        //รอยกเลิก
        [Description("รอยกเลิกหรือไม่?")]
        public bool? PreCancel { get; set; }
        [Description("วันที่รอยกเลิก")]
        public DateTime? PreCancelDate { get; set; }

        public Guid? SpecMaterialCollectionID { get; set; }
        [Description("ปลอด Lock แก้ไข freedown")]
        public bool? IsUnlockChangeFreedown { get; set; }
        [Description("วันที่ปลอด Lock แก้ไข freedown")]
        public DateTime? UnlockChangeFreedownDate { get; set; }
        [Description("ผู้ปลอด Lock แก้ไข freedown")]
        public Guid? UnlockChangeFreedownByUserId { get; set; }
        [Description("ราคาหน้าสัญญา #2")]
        public decimal? AgreementPrice2 { get; set; }
    }
}
