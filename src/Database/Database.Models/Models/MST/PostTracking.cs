using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("PostTracking")]
    [Table("PostTracking", Schema = Schema.MASTER)]
    public class PostTracking : BaseEntity
    {
        [Description("รหัส PostTracking")]
        [MaxLength(50)]
        public string PostTrackingNo { get; set; }

        [Description("ถูกใช้งานแล้ว?")]
        public bool? IsUsed { get; set; }

        [Description("รหัส PostTracking สำหรับต่างประเทศ")]
        public bool? IsForeign { get; set; }

        [Description("ลำดับ")]
        public int? Order { get; set; }
    }
}
