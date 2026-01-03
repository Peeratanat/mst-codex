using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LOG
{
    [Description("เก็บ log การ ส่งเมล CombineUnit")]
    [Table("CombineUnitMailLog", Schema = Schema.LOG)]
    public class CombineUnitMailLog : BaseEntityWithoutMigrate
    {
        public Guid? CombineUnitID { get; set; }
        public string ProcessType { get; set; }
        public Guid? UnitID { get; set; }
        public Guid? UnitIDCombine { get; set; }
        public Guid? CombineDocTypeMasterCenterID { get; set; }
        public string ReasonDel { get; set; }
        public string ReasonEdit { get; set; }
        public bool? SentEmail { get; set; }
        public string MessageErr { get; set; }
        public string Action { get; set; }
    }
}
