using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("โปรโมชั่นโอนในใบเสนอราคา")]
    [Table("QuotationTransferPromotion", Schema = Schema.PROMOTION)]
    public class QuotationTransferPromotion : BaseEntity
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

    }
}
