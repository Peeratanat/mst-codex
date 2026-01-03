using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.USR
{
    [Description("สิทธิ์ของ Role กับกฎการเข้าถึงระบบ")]
    [Table("AuthorizeRuleByRole", Schema = Schema.USER)]
    public class AuthorizeRuleByRole : BaseEntity
    {
        [Description("ผูกกับกฎการเข้าถึง")]
        public Guid AuthorizeRuleID { get; set; }
        [ForeignKey("AuthorizeRuleID")]
        public AuthorizeRule AuthorizeRule { get; set; }

        [Description("สิทธิ์ของ User")]
        public Guid RoleID { get; set; }
        [ForeignKey("RoleID")]
        public Role Role { get; set; }

    }
}
