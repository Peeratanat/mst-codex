using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("การนำเข้า Bill Payment จากธนาคาร")]
    [Table("OnlinePaymentPrebook", Schema = Schema.FINANCE)]
    public class OnlinePaymentPrebook : BaseEntityWithoutMigrate
    {
        [Description("วันที่เงินเข้า/วันที่ชำระเงิน")]
        public DateTime? PaymentDate { get; set; }

        [Description("ช่องทางการชำระเงิน")]
        public string PaymentChannel { get; set; }

        [Description("OnlinePaymentHistory ID")]
        public Guid? OnlinePaymentHistoryID { get; set; }

        [Description("คำนำหน้าชื่อ")]
        public string TitleName { get; set; }

        [Description("ชื่อ")]
        public string FirstName { get; set; }

        [Description("นามสกุล")]
        public string LastName { get; set; }

        [Description("หมายเลขโทรศัพท์")]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Description("E-Mail")]
        [MaxLength(50)]
        public string Email { get; set; }

        [Description("Contact ID")]
        public Guid? ContactID { get; set; }

        [Description("Project ID")]
        public Guid? ProjectID { get; set; }

        [Description("รหัสโครงการ")]
        public string ProjectNo { get; set; }

        [Description("ชื่อโครงการ")]
        public string ProjectName { get; set; }

        [Description("Unit ID")]
        public Guid? UnitID { get; set; }

        [Description("Unit No")]
        public string UnitNo { get; set; }

        [Description("Booking ID")]
        public Guid? BookingID { get; set; }

        [Description("จำนวนเงินรวม")]
        [Column(TypeName = "Money")]
        public decimal? TotalAmount { get; set; }

        [Description("IsAPDirectMkt")]
        public bool? IsAPDirectMkt { get; set; }

        [Description("IsSendToBC")]
        public bool? IsSendToBC { get; set; }

        [Description("SendMailFlag")]
        public string SendMailFlag { get; set; }

        [Description("SendMailDate")]
        public DateTime? SendMailDate { get; set; }
        [Description("ยกเลิก ทำกลับรายการด้าน Sap")]
        public bool? IsCancel { get; set; }
        [Description("ยกเลิก ทำกลับรายการด้าน Sap โดย")]
        public Guid? CancelBy { get; set; }
        [Description("วันที่ ทำกลับรายการด้าน Sap")]
        public DateTime? CancelDate { get; set; }
        [Description("Cancal Remark")]
        public string CancelRemark { get; set; }
        [Description("Remark")]
        public string Remark { get; set; }
    }
}