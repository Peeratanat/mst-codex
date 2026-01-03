using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.USR
{
    [Description("กฎการเข้าถึงระบบ")]
    [Table("AuthorizeRule", Schema = Schema.USER)]
    public class AuthorizeRule : BaseEntity
    {
        [Description("ชื่อ")]
        public string Name { get; set; }

        [Description("กลุ่มของกฎ")]
        public Guid? ParentRuleID { get; set; }
        [ForeignKey("ParentRuleID")]
        public AuthorizeRule ParentRule { get; set; }
        //has checkbox in UI
        [Description("มีการกำหนดการเข้าถึงในกฎนี้หรือไม่")]
        public bool HasAuthorize { get; set; }
        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

    }
}
