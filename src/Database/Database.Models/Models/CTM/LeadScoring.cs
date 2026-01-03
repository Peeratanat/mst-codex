using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("การให้คะแนน Lead")]
    [Table("LeadScoring", Schema = Schema.CUSTOMER)]
    public class LeadScoring : BaseEntity
    {
        [Description("Lead")]
        public Guid? LeadID { get; set; }
        [ForeignKey("LeadID")]
        public Lead Lead { get; set; }
        [Description("ชนิดของการให้คะแนน")]
        public Guid? LeadScoringTypeID { get; set; }
        [ForeignKey("LeadScoringTypeID")]
        public LeadScoringType LeadScoringType { get; set; }
        [Description("คะแนนที่จะได้")]
        public double Score { get; set; }
        [Description("ได้คะแนนนี้หรือไม่")]
        public bool IsGetScore { get; set; }
        [Description("เป็นการคำนวนคะแนนล่าสุด")]
        public bool IsLatestScore { get; set; }
    }
}
