using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRM
{
    [Description("รายการจ่ายโปรโมชั่นโอน (จากระบบ Stock)")]
    [Table("TransferPromotionStockReceiveItem", Schema = Schema.PROMOTION)]
    public class TransferPromotionStockReceiveItem : BaseEntity
    {
        [Description("ผูกการเบิกโปรโมชั่นขาย")]
        public Guid? TransferPromotionRequestID { get; set; }
        [ForeignKey("TransferPromotionRequestID")]
        public TransferPromotionRequest TransferPromotionRequest { get; set; }

        [Description("จำนวนที่จ่าย")]
        public int Quantity { get; set; }
        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        [Description("หน่วย (TH)")]
        [MaxLength(100)]
        public string UnitTH { get; set; }
        [Description("หน่วย (EN)")]
        [MaxLength(100)]
        public string UnitEN { get; set; }
        [Description("ราคาต่อหน่วย")]
        [Column(TypeName = "Money")]
        public decimal PricePerUnit { get; set; }
        [Description("ราคารวม")]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }

        [Description("Agreement No.")]
        [MaxLength(100)]
        public string AgreementNo { get; set; }
        [Description("เลขที่สิ่งของ")]
        [MaxLength(100)]
        public string ItemNo { get; set; }
        [Description("Material Code")]
        [MaxLength(100)]
        public string MaterialCode { get; set; }

        [Description("ชื่อผลิตภัณฑ์ (TH)")]
        [MaxLength(1000)]
        public string NameTH { get; set; }
        [Description("ชื่อผลิตภัณฑ์ (EN)")]
        [MaxLength(1000)]
        public string NameEN { get; set; }

        [Description("วันหมดอายุ")]
        public DateTime? ExpireDate { get; set; }
        [Description("Plant")]
        [MaxLength(1000)]
        public string Plant { get; set; }

    }
}
