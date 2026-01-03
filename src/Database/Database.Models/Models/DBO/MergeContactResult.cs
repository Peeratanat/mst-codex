using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.DBO
{
    [Table("MergeContactResult", Schema = Schema.DATA_MIGRATION)]
    public class MergeContactResult : BaseEntity
    {
        [Description("รหัส ContactID เก่า")]
        [MaxLength(100)]
        public string OldContactID { get; set; }

        [Description("ID Contact ใหม่")]
        public Guid NewContactID { get; set; }
        [Description("ID Contact Phone ใหม่")]
        public Guid? NewTel1ID { get; set; }
        public Guid? NewTel2ID { get; set; }
        public Guid? NewTel3ID { get; set; }
        public Guid? NewTel4ID { get; set; }
        [Description("ID Contact Email ใหม่")]
        public Guid? NewEmailID { get; set; }

        [Description("MatchMergeTypeStep1")]
        [MaxLength(100)]
        public string MatchMergeTypeStep1 { get; set; }
        [Description("MatchMergeTypeStep2")]
        [MaxLength(100)]
        public string MatchMergeTypeStep2 { get; set; }
        [MaxLength(100)]
        public string MatchMergeTypeException { get; set; }

        [Description("เป็น Contact หลัก")]
        public bool IsMainContact { get; set; }

        [Description("Score")]
        public int Score { get; set; }

        [Description("เลขบัตรประชาชน")]
        public string PersonalID { get; set; }
        [Description("อีเมลล์")]
        public string EMail { get; set; }
        [Description("เบอร์โทร")]
        public string Tel_4 { get; set; }

        public string MergeKey { get; set; }

        public bool IsContactCreated { get; set; }

    }
}
