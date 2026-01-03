using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("SAP ZRFCMM01 - Material Master")]
    [Table("SAP_ZRFCMM01", Schema = Schema.PROMOTION)]
    public class SAP_ZRFCMM01 : BaseEntityWithoutKey
    {
        [Description("Plant (1001)")]
        [MaxLength(4)]
        public string WERKS { get; set; }
        [Description("Material Number (10101-000000)")]
        [MaxLength(18)]
        public string MATNR { get; set; }
        [Description("Material Type (RM01)")]
        [MaxLength(4)]
        public string MTART { get; set; }
        [Description("Material Group (10101)")]
        [MaxLength(9)]
        public string MATKL { get; set; }
        [Description("Base Unit of Measure (EA)")]
        [MaxLength(3)]
        public string MEINS { get; set; }
        [Description("Purchase Order Unit of Measure")]
        [MaxLength(3)]
        public string BSTME { get; set; }
        [Description("Material Description (Short Text) (I0.22*0.22*21.0 (2ท่อน/เชื่อม))")]
        [MaxLength(40)]
        public string MAKTX { get; set; }
        [Description("Material Group Description (เสาเข็มคอนกรีต)")]
        [MaxLength(20)]
        public string WGBEZ { get; set; }
        [Description("Description of material type (โครงการ-วัสดุ/งานก่อสร้าง RM)")]
        [MaxLength(25)]
        public string MTBEZ { get; set; }
        [Description("Valuation Class (RM01)")]
        [MaxLength(4)]
        public string BKLAS { get; set; }
        [Description("G/L Account Number (8120000)")]
        [MaxLength(10)]
        public string KONTS { get; set; }
        [Description("Unit of Measurement (ชิ้น)")]
        [MaxLength(10)]
        public string MSEHT { get; set; }
    }
}
