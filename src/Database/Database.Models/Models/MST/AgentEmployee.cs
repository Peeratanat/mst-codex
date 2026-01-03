using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ข้อมูล AgentEmployee")]
    [Table("AgentEmployee", Schema = Schema.MASTER)]
    public class AgentEmployee : BaseEntity
    {
        [Description("ชื่อ")]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Description("นามสกุล")]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Description("เบอร์โทรศัพท์")]
        [MaxLength(100)]
        public string TelNo { get; set; }
        [Description("Agent")]
        public Guid? AgentID { get; set; }
        [ForeignKey("AgentID")]
        public Agent Agent { get; set; }
    }
}
