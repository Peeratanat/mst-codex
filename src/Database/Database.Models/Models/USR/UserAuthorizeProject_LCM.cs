using Database.Models.PRJ;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.USR
{
    [Description("LCM ประจำโครงการ")]
    [Table("UserAuthorizeProject_LCM", Schema = Schema.USER)]
    public class UserAuthorizeProject_LCM : BaseEntity
    {
        [Description("ผูกกับUser")]
        public Guid? UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        [Description("ผูกกับโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }
    }
}
