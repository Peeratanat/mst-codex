using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูล LC Target")]
    [Table("LCTarget", Schema = Schema.PROJECT)]
    public class LCTarget : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("LC")]
        public Guid EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public USR.User Employee { get; set; }

        [Description("Year")]
        public int Y { get; set; }

        [Description("Q")]
        public int Q { get; set; }

        [Description("")]
        public int BookUnit { get; set; }

        [Description("")]
        public int TransferUnit { get; set; }

        [Description("")]
        public decimal BookingAmount { get; set; }

        [Description("")]
        public decimal TransferAmount { get; set; }
    }
}
