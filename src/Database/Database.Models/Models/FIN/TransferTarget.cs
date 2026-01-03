using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("Import Transfer Target")]
    [Table("TransferTarget", Schema = Schema.FINANCE)]
    public class TransferTarget
    {
        [Description("Target ของ ปี")]
        public int Year { get; set; }

        [Description("Target ของ Quarter")]
        public int Quarter { get; set; }

        [Description("Target ของ Month")]
        public int Month { get; set; }

        [Description("Target ของ Week")]
        public int Week { get; set; }

        [Description("ID ของ โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("มูลค่า Target")]
        public int Amount { get; set; }

        [Description("ประเภท Target")]
        public Guid? TransferTargetTypeID { get; set; }
        [ForeignKey("TransferTargetTypeMasterCenterID")]
        public MST.MasterCenter TransferTargetType { get; set; }
    }
}
