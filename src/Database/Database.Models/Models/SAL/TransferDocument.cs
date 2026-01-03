using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("รับเอกสารหลังโอนกรรมสิทธิ์")]
    [Table("TransferDocument", Schema = Schema.SALE)]
    public class TransferDocument : BaseEntity
    {

        [Description("โอนกรรมสิทธิ์")]
        public Guid TransferID { get; set; }
        [ForeignKey("TransferID")]
        public SAL.Transfer Transfer { get; set; }

        [Description("เหตุผล")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        [Description("มอบอำนาจหรือไม่?")]
        public bool? IsAssignAuthority { get; set; }
        [Description("ผู้แก้ไขมอบอำนาจ")]
        public Guid? AssignAuthorityUserID { get; set; }
        [ForeignKey("AssignAuthorityUserID")]
        public USR.User AssignAuthorityUser { get; set; }
        [Description("วันที่แก้ไขมอบอำนาจ")]
        public DateTime? AssignAuthorityDate { get; set; }

        [Description("ลูกค้าได้รับเอกสารหลังโอนกรรมสิทธิ์แล้วหรือไม่?")]
        public bool? IsReceiveDocument { get; set; }
        [Description("ผู้แก้ไขลูกค้าได้รับเอกสารหลังโอนกรรมสิทธิ์แล้ว")]
        public Guid? ReceiveDocumentUserID { get; set; }
        [ForeignKey("ReceiveDocumentUserID")]
        public USR.User ReceiveDocumentUser { get; set; }
        [Description("วันที่แก้ไขลูกค้าได้รับเอกสารหลังโอนกรรมสิทธิ์แล้ว")]
        public DateTime? ReceiveDocumentDate { get; set; }

    }
}
