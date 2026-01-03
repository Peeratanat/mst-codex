using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("ข้อมูลหัก Commission")]
    [Table("DeductMoney", Schema = Schema.COMMISSION)]
    public class DeductMoney : BaseEntity
    {
        [Description("วันที่ Active")]
        public DateTime? ActiveDate { get; set; }

        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("พนักงานขาย")]
        public Guid? SaleUserID { get; set; }
        [ForeignKey("SaleUserID")]
        public USR.User SaleUser { get; set; }

        [Description("จำนวนเงิน")]
        [Column(TypeName = "Money")]
        public decimal? Amount { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Description("ประเภทคอมมิสชั่น")]
        public Guid? CommissionTypeMasterCenterID { get; set; }
        [ForeignKey("CommissionTypeMasterCenterID")]
        public MST.MasterCenter CommissionType { get; set; }
    }
}
