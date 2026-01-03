using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("โปรโมชั่นโอนในใบเสนอราคา")]
    [Table("QuotationTransferPromotionPrebook", Schema = Schema.PROMOTION)]
    public class QuotationTransferPromotionPrebook : BaseEntityWithoutMigrate
    {
        [Description("ผูกใบเสนอราคา")]
        public Guid QuotationID { get; set; }
        [ForeignKey("QuotationID")]
        public SAL.Quotation Quotation { get; set; }

        [Description("ผูกโปรโมชั่น")]
        public Guid? MasterTransferPromotionID { get; set; }
        [ForeignKey("MasterTransferPromotionID")]
        public MasterTransferPromotion MasterPromotion { get; set; }

        [Description("หมายเหตุโปรโมชั่นโอน")]
        [MaxLength(5000)]
        public string Remark { get; set; }
        [Description("UnitName")]
        [MaxLength(5000)]
        public string UnitName { get; set; }

        [Description("PricePerUnit")]
        [Column(TypeName = "Money")]
        public decimal PricePerUnit { get; set; }

        [Description("TotalPrice")]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }

    }
}
