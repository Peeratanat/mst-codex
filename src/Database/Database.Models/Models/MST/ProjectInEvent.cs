using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ข้อมูล Project ใน Event")]
    [Table("ProjectInEvent", Schema = Schema.MASTER)]
    public class ProjectInEvent : BaseEntityWithoutMigrate
    {

        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        public Guid? EventID { get; set; }
        [ForeignKey("EventID")]
        public MST.Event Event { get; set; }
    }
}
