using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("รายชื่อลูกค้าใน Memo คืนเงินลูกค้า")]
    [Table("RefundMemoCustomer", Schema = Schema.SALE)]
    public class RefundMemoCustomer : BaseEntity
    {
        [Description("Memo คืนเงินลูกค้า")]
        public Guid RefundMemoID { get; set; }
        [ForeignKey("RefundMemoID")]
        public RefundMemo RefundMemo { get; set; }

        [Description("ผู้โอน")]
        public Guid? TransferOwnerID { get; set; }
        [ForeignKey("TransferOwnerID")]
        public TransferOwner TransferOwner { get; set; }
    }
}
