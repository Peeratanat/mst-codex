using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("ค่าใช้จ่ายในโปรโมชั่นโอน")]
    [Table("TransferExpense", Schema = Schema.SALE)]
    public class TransferExpense : BaseEntity
    {
        [Description("โอนกรรมสิทธิ์")]
        public Guid TransferID { get; set; }
        [ForeignKey("TransferID")]
        public SAL.Transfer Transfer { get; set; }

        [Description("จ่ายโดยใคร (บริษัท=0, ลูกค้า=1, คนละครึ่ง=2)")]
        public Guid? ExpenseReponsibleByMasterCenterID { get; set; }
        [ForeignKey("ExpenseReponsibleByMasterCenterID")]
        public MST.MasterCenter ExpenseReponsibleBy { get; set; }

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

        [Description("ราคาบริษัทจ่าย")]
        [Column(TypeName = "Money")]
        public decimal SellerAmount { get; set; }

        [Description("ราคาลูกค้าจ่าย")]
        [Column(TypeName = "Money")]
        public decimal BuyerAmount { get; set; }

        [Description("จ่ายให้")]
        public Guid? PaymentReceiverMasterCenterID { get; set; }
        [ForeignKey("PaymentReceiverMasterCenterID")]
        public MST.MasterCenter PaymentReceiver { get; set; }
    }
}
