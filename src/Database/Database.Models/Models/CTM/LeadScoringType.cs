using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ชนิดของการให้คะแนน Lead")]
    [Table("LeadScoringType", Schema = Schema.CUSTOMER)]
    public class LeadScoringType : BaseEntity
    {
        [Description("หัวข้อ")]
        [MaxLength(1000)]
        public string Topic { get; set; }
        [Description("คะแนน")]
        public double Score { get; set; }
        [Description("ลำดับ")]
        public int Order { get; set; }
        [Description("Key")]
        [MaxLength(100)]
        public string Key { get; set; }
    }
}
