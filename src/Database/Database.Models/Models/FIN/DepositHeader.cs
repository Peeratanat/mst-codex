using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.FIN
{
    [Description("การนำฝาก")]
    [Table("DepositHeader", Schema = Schema.FINANCE)]
    public class DepositHeader : BaseEntity
    {
        [Description("ผูกบัญชีธนาคารของบริษัท")]
        public Guid? ReferentID { get; set; }
        [ForeignKey("ReferentID")]
        public DepositHeader ReferentDepositHeader { get; set; }

        [Description("เลขที่นำฝาก")]
        [MaxLength(100)]
        public string DepositNo { get; set; }

        [Description("วันที่นำฝาก")]
        public DateTime? DepositDate { get; set; }

        [Description("ผูกบัญชีธนาคารของบริษัท")]
        public Guid? BankAccountID { get; set; }
        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }

        [Description("ยอดเงินรวม")]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }

        [Description("ยอดเงินรวม ค่าธรรมเนียม")]
        [Column(TypeName = "Money")]
        public decimal TotalFee { get; set; }

        [Description("จำนวนรายการที่นำฝาก")]
        public int TotalRecord { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Description("รหัสการ Post PI")]
        [MaxLength(100)]
        public string PostGLDocumentNo { get; set; }

        [Description("วันที่ Post PI")]
        public DateTime? PostGLDate { get; set; }
    }    
}
