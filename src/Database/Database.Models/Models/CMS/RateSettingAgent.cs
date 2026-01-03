using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("Rate Commission Agency")]
    [Table("RateSettingAgent", Schema = Schema.COMMISSION)]
    public class RateSettingAgent : BaseEntity
    {
        [Description("GroupID")]
        public Guid? GroupID { get; set; }

        [Description("วันที่ Active")]
        public DateTime? ActiveDate { get; set; }

        [Description("วันที่ Expire")]
        public DateTime? ExpireDate { get; set; }

        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("Agency")]
        public Guid? AgentID { get; set; }
        [ForeignKey("AgentID")]
        public MST.Agent Agent { get; set; }

        [Description("จำนวนเงิน")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        [Description("สถานะ")]
        public bool IsActive { get; set; }
    }
}
