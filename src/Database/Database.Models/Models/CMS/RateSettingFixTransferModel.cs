using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("Rate Commission Fix ตามแบบบ้าน โอน")]
    [Table("RateSettingFixTransferModel", Schema = Schema.COMMISSION)]
    public class RateSettingFixTransferModel : BaseEntity
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

        [Description("แบบบ้าน")]
        public Guid? ModelID { get; set; }
        [ForeignKey("ModelID")]
        public PRJ.Model Model { get; set; }

        [Description("จำนวนเงิน")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        [Description("สถานะ")]
        public bool IsActive { get; set; }
    }
}
