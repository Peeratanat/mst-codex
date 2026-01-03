using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("ประวัติการพิมพ์เอกสารประกอบสินเชื่อ")]
    [Table("CreditBankingPrintingHistory", Schema = Schema.SALE)]
    public class CreditBankingPrintingHistory : BaseEntity
    {
        [Description("ใบจอง")]
        public Guid? BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("เลือกทั้งหมด")]
        public bool IsSelectAll { get; set; }
        [Description("บุคคลธรรมดา")]
        public bool IsPersonal { get; set; }
        [Description("บริษัท")]
        public bool IsCompany { get; set; }
        [Description("ห้างหุ้นส่วน")]
        public bool IsPartnership { get; set; }
        [Description("ร้านจดทะเบียน")]
        public bool IsRegisteredStore { get; set; }
        [Description("ร้านไม่จดทะเบียน")]
        public bool IsNotRegisteredStore { get; set; }
    }
}
