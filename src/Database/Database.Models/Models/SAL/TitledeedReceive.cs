using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("การเบิกโฉนด")]
    [Table("TitledeedReceive", Schema = Schema.SALE)]
    public class TitledeedReceive : BaseEntity
    {
        [Description("โฉนด")]
        public Guid TitledeedDetailID { get; set; }
        [ForeignKey("TitledeedDetailID")]
        public PRJ.TitledeedDetail TitledeedDetail { get; set; }

        [Description("วันที่ LC ดำเนินการ")]
        public DateTime? LCProceedDate { get; set; }
        [Description("วันที่ FI ดำเนินการ")]
        public DateTime? FIProceedDate { get; set; }
        [Description("สถานะของ LC")]
        public string LCStatus { get; set; }
        [Description("สถานะของ FI")]
        public string FIStatus { get; set; }
        
        [Description("สถานะโฉนด")]
        public string Status { get; set; }

    }
}
