using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("ใบส่งมอบโปรโมชั่นขาย")]
    [Table("SalePromotionDelivery", Schema = Schema.PROMOTION)]
    public class SalePromotionDelivery : BaseEntity
    {
        [Description("เบิกโปรโมชั่นขาย")]
        public Guid SalePromotionRequestID { get; set; }
        [ForeignKey("SalePromotionRequestID")]
        public SalePromotionRequest SalePromotionRequest { get; set; }
        [Description("เลขที่ส่งมอบ")]
        [MaxLength(100)]
        public string DeliveryNo { get; set; }
        [Description("วันที่ส่งมอบ")]
        public DateTime? DeliveryDate { get; set; }

        [Description("จำนวนครั้งที่พิมพ์")]
        public int? PrintCount { get; set; }
        [Description("วันที่พิมพ์")]
        public DateTime? PrintDate { get; set; }
        [Description("ผู้พิมพ์")]
        public Guid? PrintUserID { get; set; }
        [ForeignKey("PrintUserID")]
        public USR.User PrintUser { get; set; }

        [Description("ไฟล์แนบ")]
        [MaxLength(1000)]
        public string AttachFile { get; set; }

        [Description("ชื่อไฟล์แนบ")]
        [MaxLength(1000)]
        public string AttachFileName { get; set; }
    }
}
