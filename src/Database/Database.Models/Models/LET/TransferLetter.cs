using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LET
{
    [Description("จดหมายนัดโอน")]
    [Table("TransferLetter", Schema = Schema.LETTER)]
    public class TransferLetter : BaseEntity
    {
        [Description("เลขที่จดหมาย")]
        public string TransferLetterNo { get; set; }
        [Description("เลขที่สัญญา")]
        public Guid AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("วันที่ออกจดหมาย")]
        public DateTime TransferLetterDate { get; set; }

        //[Description("")]
        //public string TransferStatus { get; set; }

        [Description("ประเภทจดหมาย")]
        public Guid TransferLetterTypeMasterCenterID { get; set; }
        [ForeignKey("TransferLetterTypeMasterCenterID")]
        public MST.MasterCenter TransferLetterType { get; set; }

        [Description("เลขที่พัสดุ")]
        public Guid? PostTrackingID { get; set; }
        [ForeignKey("PostTrackingID")]
        public MST.PostTracking PostTracking { get; set; }

        [Description("วันนัดโอนกรรมสิทธิ์")]
        public DateTime? AppointmentTransferDate { get; set; }
        [Description("เวลานัดโอนกรรมสิทธิ์")]
        public DateTime? AppointmentTransferTime { get; set; }

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

        [Description("รูปแบบจดหมาย  (1=ปกติ/2=พิเศษ)")]
        public Guid? TransferLetterFormatMasterCenterID { get; set; }
        [ForeignKey("TransferLetterFormatMasterCenterID")]
        public MST.MasterCenter TransferLetterFormat { get; set; }

        [Description("วันที่เริ่มตรวจรับบ้าน")]
        public DateTime? CheckDefectStartDate { get; set; }
        [Description("วันที่สิ้นสุดตรวจรับบ้าน")]
        public DateTime? CheckDefectEndDate { get; set; }

        [Description("ที่อยู่ทีออกจดหมาย")]
        public Guid? ContactAddressTypeMasterCenterID { get; set; }
        [ForeignKey("ContactAddressTypeMasterCenterID")]
        public MST.MasterCenter ContactAddressType { get; set; }

    }
}
