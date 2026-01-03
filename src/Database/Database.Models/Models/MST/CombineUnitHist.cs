using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("Combine Unit History")]
    [Table("CombineUnitHist", Schema = Schema.MASTER)]
    public class CombineUnitHist : BaseEntityWithoutMigrate
    {
        public Guid? CombineUnitID { get;set; }
        [ForeignKey("CombineUnitID")]
        public CombineUnit CombineUnit { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }
        public Guid? UnitIDCombine { get; set; }
        public Guid? CombineDocTypeMasterCenterID { get; set; }
        public Guid? CombineStatusMasterCenterID { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public Guid? ApprovedByUserID { get; set; }
        public string ReasonDel { get; set; }
        public string ReasonEdit { get; set; }
        public string ProcessType { get; set; }
    }
}
