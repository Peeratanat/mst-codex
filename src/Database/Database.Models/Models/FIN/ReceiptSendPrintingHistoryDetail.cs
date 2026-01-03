using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("รายการใบเสร็จ ประวัติการส่งใบเสร็จเข้าโรงพิมพ์")]
    [Table("ReceiptSendPrintingHistoryDetail", Schema = Schema.FINANCE)]
    public class ReceiptSendPrintingHistoryDetail : BaseEntity
    {
        public Guid ReceiptSendPrintingHistoryID { get; set; }
        [ForeignKey("ReceiptSendPrintingHistoryID")]
        public ReceiptSendPrintingHistory ReceiptSendPrintingHistory { get; set; }

        public Guid ReceiptHeaderID { get; set; }
        [ForeignKey("ReceiptHeaderID")]
        public ReceiptHeader ReceiptHeader { get; set; }
    }
}
