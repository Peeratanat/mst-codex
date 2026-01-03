using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("ประวัติการพิมพ์สัญญา")]
    [Table("AgreementPrintingHistory", Schema = Schema.SALE)]
    public class AgreementPrintingHistory : BaseEntity
    {
        [Description("สัญญา")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public Agreement Agreement { get; set; }

        [Description("วันที่พิมพ์สัญญา")]
        public DateTime? AgreementPrintingDate { get; set; }

        [Description("ผู้พิมพ์สัญญา")]
        public Guid? AgreementPrintingByUserID { get; set; }
        [ForeignKey("AgreementPrintingByUserID")]
        public USR.User AgreementPrintingBy { get; set; }
    }
}
