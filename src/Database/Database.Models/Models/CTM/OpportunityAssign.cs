using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ประวัติการ Assign Opportunity")]
    [Table("OpportunityAssign", Schema = Schema.CUSTOMER)]
    public class OpportunityAssign : BaseEntity
    {
        [Description("Opportunity")]
        public Guid? OpportunityID { get; set; }
        [ForeignKey("OpportunityID")]
        public Opportunity Opportunity { get; set; }
        [Description("LC Owner")]
        public Guid? OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public USR.User Owner { get; set; }
    }
}
