using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LET
{
    [Description("จดหมายเตือนชำระเงินดาวน์")]
    [Table("DownPaymentLetter", Schema = Schema.LETTER)]
    public class DownPaymentLetter : BaseEntity
    {
        [Description("เลขที่จดหมาย")]
        public string DownPaymentLetterNo { get; set; }
        [Description("เลขที่สัญญา")]
        public Guid AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("จำนวนงวดที่ค้าง")]
        public int RemainDownPeriodCount { get; set; }
        [Description("จำนวนเงินที่ค้าง")]
        public decimal RemainDownTotalAmount { get; set; }
        [Description("งวดดาวน์เริ่มต้นที่ค้าง")]
        public int RemainDownPeriodStart { get; set; }
        [Description("งวดดาวน์สิ้นสุดที่ค้าง")]
        public int RemainDownPeriodEnd { get; set; }

        [Description("วันที่ออกจดหมาย")]
        public DateTime DownPaymentLetterDate { get; set; }
        [Description("วันที่ในจดหมาย")]
        public DateTime? LetterDate { get; set; }
        [Description("เวลาในจดหมาย")]
        public DateTime? LetterTime { get; set; }

        [Description("ประเภทจดหมาย")]
        public Guid DownPaymentLetterTypeMasterCenterID { get; set; }
        [ForeignKey("DownPaymentLetterTypeMasterCenterID")]
        public MST.MasterCenter DownPaymentLetterType { get; set; }

        [Description("สถานะจดหมาย")]
        public Guid? LetterStatusMasterCenterID { get; set; }
        [ForeignKey("LetterStatusMasterCenterID")]
        public MST.MasterCenter LetterStatus { get; set; }

        [Description("วันที่ตอบรับหรือตีกลับ")]
        public DateTime? ResponseDate { get; set; }

        [Description("ประเภทเหตุผลตีกลับ")]
        public Guid? LetterReasonResponseMasterCenterID { get; set; }
        [ForeignKey("LetterReasonResponseMasterCenterID")]
        public MST.MasterCenter LetterReasonResponse { get; set; }

        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        [Description("เลขที่พัสดุ")]
        public Guid? PostTrackingID { get; set; }
        [ForeignKey("PostTrackingID")]
        public MST.PostTracking PostTracking { get; set; }

        [Description("เกิน 12.5% หรือไหม")]
        public bool? IsOverTwelvePointFivePercent { get; set; }

        [Description("ที่อยู่ทีออกจดหมาย")]
        public Guid? ContactAddressTypeMasterCenterID { get; set; }
        [ForeignKey("ContactAddressTypeMasterCenterID")]
        public MST.MasterCenter ContactAddressType { get; set; }

        [Description("จำนวนงวดที่ค้างในเอกสาร ฉ.2 หรือ ฉ.ยกเลิก")]
        public int? TotalPeriodOverDue { get; set; }
        [Description("จำนวนเงินที่ค้างในเอกสาร ฉ.2 หรือ ฉ.ยกเลิก")]
        public decimal? TotalAmountOverDue { get; set; }
    }
}
