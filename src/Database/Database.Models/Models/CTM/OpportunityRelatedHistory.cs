using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ประวัติการ Change Opportunity")]
    [Table("OpportunityRelatedHistory", Schema = Schema.CUSTOMER)]
    public class OpportunityRelatedHistory
    {
        [Description("ID")]
        public Guid ID { get; set; }

        [Description("OpportunityFrom")]
        public Guid OpportunityFrom { get; set; }

        [Description("OpportunityTo")]
        public Guid OpportunityTo { get; set; }

        [Description("ContactIDFrom")]
        public Guid ContactIDFrom { get; set; }

        [Description("ContactIDTo")]
        public Guid ContactIDTo { get; set; }

        [Description("ProjectID")]
        public Guid ProjectID { get; set; }
        [Description("UnitID")]
        public Guid UnitID { get; set; }
        [Description("BookingID")]
        public Guid BookingID { get; set; }
        

        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? CreatedByUserID { get; set; }
        public Guid? UpdatedByUserID { get; set; }

    }
}
