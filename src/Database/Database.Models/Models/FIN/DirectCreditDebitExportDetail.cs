using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("Detail ข้อมูล Export Direct Debit/Credit เพื่อตัดเงิน")]
    [Table("DirectCreditDebitExportDetail", Schema = Schema.FINANCE)]
    public class DirectCreditDebitExportDetail : BaseEntity
    {
        [Description("ID ของ DirectCreditDebitExportHeader")]
        public Guid DirectCreditDebitExportHeaderID { get; set; }
        [ForeignKey("DirectCreditDebitExportHeaderID")]
        public DirectCreditDebitExportHeader DirectCreditDebitExportHeader { get; set; }

        [Description("ID ของ DirectCreditDebitApprovalForm")]
        public Guid DirectCreditDebitApprovalFormID { get; set; }
        [ForeignKey("DirectCreditDebitApprovalFormID")]
        public DirectCreditDebitApprovalForm DirectCreditDebitApprovalForm { get; set; }

        [Description("Code ที่ใช้ Referent ใน Textfile")]
        [MaxLength(50)]
        public string BatchID { get; set; }

        [Description("ลำดับรายการ")]
        public int Seq { get; set; }

        [Description("ID ของ UnitPriceInstallment งวดที่จะตัดเงินลูกค้า")]
        public Guid? UnitPriceInstallmentID { get; set; }
        [ForeignKey("UnitPriceInstallmentID")]
        public SAL.UnitPriceInstallment UnitPriceInstallment { get; set; }        

        [Description("จำนวนเงินที่จะตัดเงินลูกค้า")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }
        
        [Description("Code ผลลัพธ์จากธนาคาร")]
        [MaxLength(50)]
        public string TransCode { get; set; }
        
        [Description("สถานะตัดเงินจากธนาคาร")]
        public Guid DirectCreditDebitExportDetailStatusMasterCenterID { get; set; }
        [ForeignKey("DirectCreditDebitExportDetailStatusMasterCenterID")]
        public MST.MasterCenter DirectCreditDebitExportDetailStatus { get; set; }

        [Description("งวดที่จะตัด")]
        [MaxLength(50)]
        public string Period { get; set; }
    }
}
