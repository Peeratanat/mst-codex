using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("ข้อผิดพลาดในระบบ")]
    [Table("Servitude", Schema = Schema.MASTER)]
    public class Servitude : BaseEntityWithoutMigrate
    {
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }
        public string MsgTH { get; set; }
        public string MsgEN { get; set; }
        public bool? IsBooking { get; set; }
        public bool? IsAgreement { get; set; }

    }
}
