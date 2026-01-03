using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.LOG
{
    [Description("ประวัติ Happy Refund")]
    [Table("RefundHistory", Schema = Schema.LOG)]
    public class RefundHistory : BaseEntity
    {
        [Description("โครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("ยูนิต")]
        public Guid UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }

        [Description("ประเภท Action")]
        public string ActionType { get; set; }

        [Description("ชื่อลูกค้า")]
        public string CustomerName { get; set; }

        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        [Description("Refund ID")]
        public int hyrf_id { get; set; }

        [Description("tf01,tf02,ac01(โอน1,โอน2,บัญชี)")]
        public string ReferentType { get; set; }

    }
}
