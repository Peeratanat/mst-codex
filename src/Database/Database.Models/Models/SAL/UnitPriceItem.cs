using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("รายการราคาของ Unit")]
    [Table("UnitPriceItem", Schema = Schema.SALE)]
    public class UnitPriceItem : BaseEntity
    {
        [Description("ผูกกับราคา")]
        public Guid UnitPriceID { get; set; }
        [ForeignKey("UnitPriceID")]
        public UnitPrice UnitPrice { get; set; }

        [Description("ลำดับของราคา")]
        public int Order { get; set; }


        [Description("ชนิดของราคา")]
        public Guid? MasterPriceItemID { get; set; }
        [ForeignKey("MasterPriceItemID")]
        public MST.MasterPriceItem MasterPriceItem { get; set; }
        [Description("ชื่อของรายการ")]
        public string Name { get; set; }
        [Description("จำนวนหน่วย")]
        public double? PriceUnitAmount { get; set; }
        [Description("หน่วย")]
        public Guid? PriceUnitMasterCenterID { get; set; }
        [ForeignKey("PriceUnitMasterCenterID")]
        public MST.MasterCenter PriceUnit { get; set; }
        [Description("ราคาต่อหน่วย")]
        [Column(TypeName = "Money")]
        public decimal? PricePerUnitAmount { get; set; }
        [Description("ราคารวม")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }
        [Description("เป็นรายการที่ต้องจ่ายหรือไม่")]
        public bool IsToBePay { get; set; }

        //จำนวนงวด (ถ้ามี)
        //[Description("จำนวนงวด (ถ้ามี)")]
        //public int? Installment { get; set; }

        [Description("ประเภทของราคา")]
        public Guid? PriceTypeMasterCenterID { get; set; }
        [ForeignKey("PriceTypeMasterCenterID")]
        public MST.MasterCenter PriceType { get; set; }

        [Description("วันที่จ่ายล่าสุด")]
        public DateTime? PayDate { get; set; }
        [Description("จ่ายครบแล้ว")]
        public bool? IsPaid { get; set; }
        [Description("วันที่ต้องจ่าย")]
        public DateTime? DueDate { get; set; }

        [Description("สร้างมาจาก Master ตัวไหน")]
        public Guid? FromMasterPriceListItemID { get; set; }

    }
}
