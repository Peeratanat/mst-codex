using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ข้อมูล Agreement Project Owner")]
    [Table("AgreementProjectOwner", Schema = Schema.MASTER)]
    public class AgreementProjectOwner : BaseEntityWithoutMigrate
    {
        [Description("ID นิติกรรม")]
        [MaxLength(100)]
        public Guid? AGOwnerUserID { get; set; }
        [ForeignKey("AGOwnerUserID")]
        public User User { get; set; }

        [Description("รหัสพนักงาน")]
        [MaxLength(100)]
        public string AGOwnerEmployeeNo { get; set; }


        [Description("ชื่อ")]
        [MaxLength(100)]
        public string AGOwnerName { get; set; }


        [Description("ProjectID")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }


        [Description("ProjectNo")]
        public string ProjectNo { get; set; }
        [Description("ProjectName")]
        public string ProjectName { get; set; }
    }
}
