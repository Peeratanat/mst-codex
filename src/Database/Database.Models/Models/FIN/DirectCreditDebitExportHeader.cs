using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    //Export เพื่อตัดเงิน
    [Description("Header ข้อมูล Export Direct Debit/Credit เพื่อตัดเงิน")]
    [Table("DirectCreditDebitExportHeader", Schema = Schema.FINANCE)]
    public class DirectCreditDebitExportHeader : BaseEntity
    {
        [Description("ชนิดของแบบฟอร์ม Direct Debit/Credit")]
        public Guid? DirectFormTypeMasterCenterID { get; set; }
        [ForeignKey("DirectFormTypeMasterCenterID")]
        public MST.MasterCenter DirectFormType { get; set; }

        [Description("บัญชีบริษัทที่รับชำระ")]
        public Guid? BankAccountID { get; set; }
        [ForeignKey("BankAccountID")]
        public MST.BankAccount BankAccount { get; set; }

        //รอบการตัดเงิน
        [Description("รอบการตัดเงิน")]
        public DateTime PeriodDate { get; set; }

        
        [Description("วันที่ตัดเงินลูกค้า")]
        public DateTime ReceiveDate { get; set; }

        [Description("วันที่ Import Textfile ที่ได้จากธนาคาร")]
        public DateTime? ImportDate { get; set; }

        [Description("ชื่อ Text file ที่ Import")]
        [MaxLength(100)]
        public string ImportFileName { get; set; }

        [Description("รวมจำนวนรายการใน Textfile ที่จะตัดเงินลูกค้า")]
        public int TotalRecord { get; set; }

        [Description("จำนวนรายการ Error")]
        public int TotalErrorRecord { get; set; }

        [Description("รวมจำนวนเงินใน Textfile ที่จะตัดเงินลูกค้า")]
        [Column(TypeName = "Money")]
        public decimal TotalAmount { get; set; }

        [Description("หมายเหตุการยกเลิกรายการ")]
        [MaxLength(1000)]
        public string CancelRemark { get; set; }
    }
}
