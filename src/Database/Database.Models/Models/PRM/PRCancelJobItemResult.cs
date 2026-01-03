using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Item งานยกเลิก PR จาก SAP")]
    [Table("PRCancelJobItemResult", Schema = Schema.PROMOTION)]
    public class PRCancelJobItemResult : BaseEntity
    {
        public Guid? PRCancelJobItemID { get; set; }
        [ForeignKey("PRCancelJobItemID")]
        public PRCancelJobItem PRCancelJobItem { get; set; }

        [Description("Flag Error")]
        public bool IsError { get; set; }
        [Description("Error Code")]
        [MaxLength(10)]
        public string ErrorCode { get; set; }
        [Description("Error Description")]
        [MaxLength(100)]
        public string ErrorDescription { get; set; }
        [Description("Flag PR")]
        public bool IsFMCreatePR { get; set; }
        [Description("เลขที่ PR")]
        [MaxLength(100)]
        public string PRNo { get; set; }
        [MaxLength(100)]
        public string ItemNo { get; set; }
        [Description("Delete Indicator")]
        public bool SAPDeleteFlag { get; set; }
        [Description("User SAP")]
        [MaxLength(100)]
        public string SAPCreateBy { get; set; }
        [Description("SAP Create Date/Time")]
        public DateTime SAPCreateDateTime { get; set; }

    }
}
