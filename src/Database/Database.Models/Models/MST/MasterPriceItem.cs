using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("รายการชำระเงินต่างๆ")]
    [Table("MasterPriceItem", Schema = Schema.MASTER)]
    public class MasterPriceItem : BaseEntity
    {
        [Description("ประเภทของราคา")]
        public Guid? PriceTypeMasterCenterID { get; set; }
        [ForeignKey("PriceTypeMasterCenterID")]
        public MST.MasterCenter PriceType { get; set; }

        [Description("รหัส MasterPriceItem")]
        [MaxLength(100)]
        public string Key { get; set; }

        [Description("Short Key")]
        public string ShortKey { get; set; }

        [Description("คำอธิบาย")]
        public string Detail { get; set; }

        [Description("คำอธิบายภาษาอังกฤษ")]
        public string DetailEN { get; set; }

        [Description("จ่ายให้")]
        public Guid? PaymentReceiverMasterCenterID { get; set; }
        [ForeignKey("PaymentReceiverMasterCenterID")]
        public MasterCenter PaymentReceiver { get; set; }

        [Description("ลำดับ")]
        public int Order { get; set; }

        [Description("รายการที่ต้องจ่ายเงิน")]
        public bool? IsToBePay { get; set; }

        [Description("รับชำระตั้งแต่ขั้นตอนไหน")]
        public Guid? UnitPriceStageMasterCenterID { get; set; }
        [ForeignKey("UnitPriceStageMasterCenterID")]
        public MasterCenter UnitPriceStage { get; set; }

        [Description("ประเภทบัญชี GL")]
        public Guid? GLAccountTypeMasterCenterID { get; set; }
        [ForeignKey("GLAccountTypeMasterCenterID")]
        public MasterCenter GLAccountType { get; set; }
    }
}
