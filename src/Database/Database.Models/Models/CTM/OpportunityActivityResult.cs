using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูลผลติดตามของ Opportunity")]
    [Table("OpportunityActivityResult", Schema = Schema.CUSTOMER)]
    public class OpportunityActivityResult : BaseEntity
    {
        [Description("รหัสข้อมูลกิจกรรมของ Opportunity")]
        public Guid? OpportunityAcitivityID { get; set; }
        [ForeignKey("OpportunityAcitivityID")]
        public OpportunityActivity OpportunityAcitivity { get; set; }
        [Description("รหัสข้อมูลสถานะกิจกรรมของ Opportunity")]
        public Guid? StatusID { get; set; }
        [ForeignKey("StatusID")]
        public OpportunityActivityStatus OpportunityAcitivityStatus { get; set; }
        [Description("เหตุผลอื่นๆ")]
        [MaxLength(5000)]
        public string OtherReasons { get; set; }

    }
}
