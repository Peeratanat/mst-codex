using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูลวันขอเบิกโฉนด")]
    [Table("TitledeedConfig", Schema = Schema.PROJECT)]
    public class TitledeedConfig : BaseEntityWithoutMigrate
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }


        public int NumberOfDays { get; set; }

        public bool? IsFullDay { get; set; }

        public int CompNumberOfDays { get; set; }

        public bool? IsCompFullDay { get; set; }

        public string Remark { get; set; }

        public int ShiftNumberOfDays { get; set; }

        public bool? IsShiftFullDay { get; set; }

        public bool? IsFIRequest { get; set; }

        public Guid? FIRequestByID { get; set; }
        [ForeignKey("FIRequestByID")]
        public User User { get; set; }

        public DateTime? FIRequestUpdated { get; set; }

    }
}
