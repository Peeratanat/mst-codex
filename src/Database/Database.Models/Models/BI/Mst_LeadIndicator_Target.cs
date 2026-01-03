using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace Database.Models.BI
{
    [Description("ข้อมูล Lead Lag Target")]
    [Table("Mst_LeadIndicator_Target", Schema = Schema.BI)]
    public class Mst_LeadIndicator_Target : BaseEntityWithoutMigrate
    {
        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }

        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("Year")]
        public int? Y { get; set; }

        [Description("Quarter")]
        public int? Q { get; set; }

        [Description("Month")]
        public int? M { get; set; }

        [Description("Week")]
        public int? W { get; set; }

        [Description("Amount")]
        public decimal Amount { get; set; }

        [Description("Unit")]
        public int? Unit { get; set; }

        [Description("ประเภทข้อมูล")]
        [MaxLength(50)]
        public string RecType { get; set; }
    }
}
