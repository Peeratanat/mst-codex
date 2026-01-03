using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LET
{
    [Description("Email ส่งคอนเฟิร์มการยกเลิกสัญญา")]
    [Table("MailConfirmCancel", Schema = Schema.LETTER)]
    public class MailConfirmCancel : BaseEntity
    {
        [Description("เลขที่จดหมายเตือนชำระเงินดาวน์")]
        public Guid? DownPaymentLetterID { get; set; }
        [ForeignKey("DownPaymentLetterID")]
        public DownPaymentLetter DownPaymentLetter { get; set; }

        [Description("เลขที่จดหมายนัดโอน")]
        public Guid? TransferLetterID { get; set; }
        [ForeignKey("TransferLetterID")]
        public TransferLetter TransferLetter { get; set; }

        [Description("LCM")]
        public Guid? LCMID { get; set; }
        [ForeignKey("LCMID")]
        public USR.User LCMUser { get; set; }

        [Description("วันที่ส่ง Email")]
        public DateTime SendDate { get; set; }

        [Description("วันที่ LCM ตอบรับ")]
        public DateTime? ResponseDate { get; set; }

        [Description("สถานะการตอบรับ")]
        public Guid? MailConfirmCancelResponseTypeID { get; set; }
        [ForeignKey("MailConfirmCancelResponseTypeID")]
        public MST.MasterCenter MailConfirmCancelResponseType { get; set; }
    }
}
