using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CMS
{
    [Description("ข้อมูลเปลี่ยนแปลงพนักงานโอน")]
    [Table("ChangeLCTransfer", Schema = Schema.COMMISSION)]
    public class ChangeLCTransfer : BaseEntity
    {
        [Description("วันที่ Active")]
        public DateTime? ActiveDate { get; set; }


        [Description("เลขที่โอน")]
        public Guid? TransferID { get; set; }
        [ForeignKey("TransferID")]
        public SAL.Transfer Transfer { get; set; }


        [Description("รหัส Sale(เดิม)")]
        public Guid? OldLCTransferID { get; set; }
        [ForeignKey("OldLCTransferID")]
        public USR.User OldLCTransfer { get; set; }


        [Description("รหัส Sale(ใหม่)")]
        public Guid? NewLCTransferID { get; set; }
        [ForeignKey("NewLCTransferID")]
        public USR.User NewLCTransfer { get; set; }


        [Description("หมายเหตุ")]
        public string Remark { get; set; }
    }
}
