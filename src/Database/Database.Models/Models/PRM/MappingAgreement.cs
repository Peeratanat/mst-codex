using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("ข้อมูล Mapping Agreement")]
    [Table("MappingAgreement", Schema = Schema.PROMOTION)]
    public class MappingAgreement : BaseEntity
    {
        [Description("เลขที่ Agreement เดิม")]
        [MaxLength(100)]
        public string OldAgreement { get; set; }
        [Description("เลขที่ Item เดิม")]
        [MaxLength(100)]
        public string OldItem { get; set; }
        [Description("เลขที่ Material Code เดิม")]
        [MaxLength(100)]
        public string OldMaterialCode { get; set; }
        [Description("เลขที่ Agreement ใหม่")]
        [MaxLength(100)]
        public string NewAgreement { get; set; }
        [Description("เลขที่ Item ใหม่")]
        [MaxLength(100)]
        public string NewItem { get; set; }
        [Description("เลขที่ Material Code เดิม")]
        [MaxLength(100)]
        public string NewMaterialCode { get; set; }
        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }
    }
}
