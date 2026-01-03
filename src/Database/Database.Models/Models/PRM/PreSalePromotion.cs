using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("โปรโมชั่นก่อนขาย")]
    [Table("PreSalePromotion", Schema = Schema.PROMOTION)]
    public class PreSalePromotion : BaseEntity
    {
        [Description("ใบจอง")]
        public Guid BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("เลขที่โปรโมชั่นก่อนขาย")]
        [MaxLength(100)]
        public string PreSalePromotionNo { get; set; }

        [Description("Master โปรก่อนขาย")]
        public Guid? MasterPreSalePromotionID { get; set; }
        [ForeignKey("MasterPreSalePromotionID")]
        public MasterPreSalePromotion MasterPromotion { get; set; }
    }
}
