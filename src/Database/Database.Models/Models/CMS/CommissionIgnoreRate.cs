using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("CommisionIgnorePercentRate")]
    [Table("CommissionIgnoreRate", Schema = Schema.COMMISSION)]
    public class CommissionIgnoreRate : BaseEntity
    {
        [Description("เลขที่สัญญา")]
        public Guid AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("แปลง")]
        public Guid UnitID { get; set; }
        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }

        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        public bool IsActive { get; set; }

    }
}