using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.LOG
{
    [Description("ถอยเลขที่สัญญา")]
    [Table("RevertAgreementNo", Schema = Schema.LOG)]
    public class RevertAgreementNo : BaseEntityWithoutMigrate
    {
        [Description("บริษัท")]
        public Guid? CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public MST.Company Company { get; set; }

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

        [Description("เลขจอง")]
        [MaxLength(150)]
        public string BookingNo { get; set; }

        [Description("สัญญา")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("เลขสัญญา")]
        [MaxLength(150)]
        public string AgreementNo { get; set; }

        [Description("ผู้ทำสัญญาทั้งหมด")]
        [MaxLength(1500)]
        public string AllOwnerName { get; set; }

        [Description("วันที่จอง")]
        public DateTime? BookingDate { get; set; }

        [Description("วันที่ทำสัญญา")]
        public DateTime? ContractDate { get; set; }

        [Description("Remark")]
        //[MaxLength(1500)]
        public string Remark { get; set; }

    }
}
