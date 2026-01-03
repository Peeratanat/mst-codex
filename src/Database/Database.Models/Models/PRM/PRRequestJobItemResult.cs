using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Item งานขอสร้าง PR จาก SAP")]
    [Table("PRRequestJobItemResult", Schema = Schema.PROMOTION)]
    public class PRRequestJobItemResult : BaseEntity
    {
        public Guid? PRRequestJobItemID { get; set; }
        [ForeignKey("PRRequestJobItemID")]
        public PRRequestJobItem PRRequestJobItem { get; set; }

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
        [MaxLength(100)]
        public string MaterialNo { get; set; }
        [Description("User SAP")]
        [MaxLength(100)]
        public string SAPCreateBy { get; set; }
        [Description("SAP Create Date/Time")]
        public DateTime SAPCreateDateTime { get; set; }

    }
}
