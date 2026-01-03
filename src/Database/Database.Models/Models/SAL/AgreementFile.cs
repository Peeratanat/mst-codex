using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("ไฟล์แนบสัญญา")]
    [Table("AgreementFile", Schema = Schema.SALE)]
    public class AgreementFile : BaseEntity
    {
        [Description("สัญญา")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public Agreement Agreement { get; set; }

        [Description("ชื่อไฟล์")]
        [MaxLength(1000)]
        public string FileName { get; set; }

        [Description("path ไฟล์")]
        [MaxLength(1000)]
        public string FilePath { get; set; }

        [Description("ประเภทเอกสาร")]
        [MaxLength(30)]
        public string DocType { get; set; }

        [Description("เลขที่เอกสารอ้างอิง")]
        public Guid? RefID { get; set; }

        [Description("Running")]
        [MaxLength(7)]
        public string DocYearMonth { get; set; }

        [Description("Remark")]
        [MaxLength(10000)]
        public string Remark { get; set; }

        [Description("IsCancelled")]
        public bool IsCancelled { get; set; }

        public Guid? CancelByUserID { get; set; }
        [ForeignKey("CancelByUserID")]
        public USR.User CancelBy { get; set; }

        [Description("CancelDate")]
        public DateTime? CancelDate { get; set; }


    }
}
