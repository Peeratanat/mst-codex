using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("Rotate Lead Config")]
    [Table("LeadRotateAssign", Schema = Schema.CUSTOMER)]
    public class LeadRotateAssign : BaseEntity
    {
        public Guid? FromLeadID { get; set; }
        public Guid? ToLeadID { get; set; }
        public DateTime? AssignRotateDate { get; set; }
        public Guid? AssignfromOwnerID { get; set; }
        public Guid? AssignToOwnerID { get; set; }
    }
}
