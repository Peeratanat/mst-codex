using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ประวัติการ Assign Lead")]
    [Table("LeadAssign", Schema = Schema.CUSTOMER)]
    public class LeadAssign : BaseEntity
    {
        [Description("Lead")]
        public Guid? LeadID { get; set; }
        [ForeignKey("LeadID")]
        public Lead Lead { get; set; }
        [Description("LC Owner")]
        public Guid? OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public USR.User Owner { get; set; }
        [Description("Assign โดย User")]
        public bool IsAssignByUser { get; set; }
    }
}
