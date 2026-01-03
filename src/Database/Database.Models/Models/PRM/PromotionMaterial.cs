using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("Master Promotion Material")]
    [Table("PromotionMaterial", Schema = Schema.PROMOTION)]
    public class PromotionMaterial : BaseEntity
    {
        [Description("Plant ของ Project")]
        [MaxLength(100)]
        public string Plant { get; set; }
        [Description("Material Code")]
        [MaxLength(100)]
        public string Code { get; set; }
        [Description("Material Type")]
        [MaxLength(10)]
        public string TypeCode { get; set; }
        [Description("Material Type Name")]
        [MaxLength(1000)]
        public string TypeName { get; set; }
        [Description("Material Group Key")]
        [MaxLength(100)]
        public string MaterialGroupKey { get; set; }
        [Description("Material Group Name")]
        [MaxLength(100)]
        public string MaterialGroupName { get; set; }
        [Description("Material Group")]
        public Guid? PromotionMaterialGroupID { get; set; }
        [ForeignKey("PromotionMaterialGroupID")]
        public PromotionMaterialGroup PromotionMaterialGroup { get; set; }
        [Description("หน่วย EN")]
        [MaxLength(100)]
        public string UnitEN { get; set; }
        [Description("หน่วย TH")]
        [MaxLength(100)]
        public string UnitTH { get; set; }
        [Description("หน่วยใน PO")]
        [MaxLength(100)]
        public string UnitPO { get; set; }
        [Description("Material Description")]
        [MaxLength(1000)]
        public string Name { get; set; }
        [Description("Valuation Class")]
        [MaxLength(100)]
        public string ValuationClass { get; set; }
        [Description("G/L Account Number")]
        [MaxLength(100)]
        public string GLAccountNo { get; set; }
        [Description("สถานะ Active")]
        public bool IsActive { get; set; }

    }
}
