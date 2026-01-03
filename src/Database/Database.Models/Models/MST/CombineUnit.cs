using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("Combine Unit")]
    [Table("CombineUnit", Schema = Schema.MASTER)]
    public class CombineUnit : BaseEntityWithoutMigrate
    {

        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }

        public Guid? UnitIDCombine { get; set; }
        [ForeignKey("UnitIDCombine")]
        public Unit UnitCombine { get; set; }


        public Guid? CombineDocTypeMasterCenterID { get; set; }
        [ForeignKey("CombineDocTypeMasterCenterID")]
        public MasterCenter CombineDocType { get; set; }

        public Guid? CombineStatusMasterCenterID { get; set; }
        [ForeignKey("CombineStatusMasterCenterID")]
        public MasterCenter CombineStatus { get; set; }

        public DateTime? ApprovedDate { get; set; }
        public Guid? ApprovedByUserID { get; set; }
        [ForeignKey("ApprovedByUserID")]
        public User ApprovedBy { get; set; }

        public string ReasonDel { get; set; }
        public string ReasonEdit { get; set; }
        public bool? IsEdit { get; set; }
    }
}
