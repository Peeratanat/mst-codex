using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("โปรโมชั่นขายในใบเสนอราคา")]
    [Table("QuotationSalePromotion", Schema = Schema.PROMOTION)]
    public class QuotationSalePromotion : BaseEntity
    {

        [Description("ผูกใบเสนอราคา")]
        public Guid QuotationID { get; set; }
        [ForeignKey("QuotationID")]
        public SAL.Quotation Quotation { get; set; }

        [Description("ผูกโปรโมชั่น")]
        public Guid? MasterSalePromotionID { get; set; }
        [ForeignKey("MasterSalePromotionID")]
        public MasterSalePromotion MasterPromotion { get; set; }

    }
}
