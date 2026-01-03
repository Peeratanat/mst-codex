using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Material")]
    [Table("PromotionMaterialGroup", Schema = Schema.PROMOTION)]
    public class PromotionMaterialGroup : BaseEntity
    {
        [Description("Group Key")]
        [MaxLength(100)]
        public string Key { get; set; }
        [Description("ชื่อ Group")]
        [MaxLength(1000)]
        public string Name { get; set; }
        [Description("Document Type สำหรับส่ง PR เช่น P01, P04")]
        [MaxLength(100)]
        public string DocType { get; set; }
        [Description("สถานะการทำ PR")]
        public bool IsGenPR { get; set; }
        [Description("สถานะการ Active")]
        public bool IsActive { get; set; }
    }
}
