using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("Rate Commission Ranking ขาย")]
    [Table("RateSettingSale", Schema = Schema.COMMISSION)]
    public class RateSettingSale : BaseEntity
    {
        [Description("GroupID")]
        public Guid? GroupID { get; set; }


        [Description("วันที่ Active")]
        public DateTime? ActiveDate { get; set; }


        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }


        [Description("จำนวนเงินเริ่มต้น")]
        [Column(TypeName = "Money")]
        public decimal StartRange { get; set; }
        [Description("จำนวนเงินสิ้นสุด")]
        [Column(TypeName = "Money")]
        public decimal EndRange { get; set; }


        [Description("จำนวนเปอร์เซ็น")]
        public double Amount { get; set; }

        [Description("สถานะ")]
        public bool IsActive { get; set; }
    }
}
