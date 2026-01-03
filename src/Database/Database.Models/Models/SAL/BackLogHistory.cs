using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("BackLogHistory")]
    [Table("BackLogHistory", Schema = Schema.SALE)]
    public class BackLogHistory : BaseEntityWithoutMigrate
    {
        [Description("BackLog")]
        public Guid BackLogID { get; set; }
        [ForeignKey("BackLogID")]
        public SAL.BackLog BackLog { get; set; }
        [Description("วันที่คาดว่าจะโอน (ของเก่า)")]
        public DateTime? OldDueTransferDate { get; set; }
        [Description("วันที่คาดว่าจะโอน (ของใหม่)")]
        public DateTime? NewDueTransferDate { get; set; }
        [Description("เกรดความน่าจะเป็นในการโอน (ของเก่า)")]
        public Guid? OldBacklogGradeMasterCenterID { get; set; }
        [ForeignKey("OldBacklogGradeMasterCenterID")]
        public MST.MasterCenter OldBacklogGrade { get; set; }
        [Description("เกรดความน่าจะเป็นในการโอน (ของใหม่)")]
        public Guid? NewBacklogGradeMasterCenterID { get; set; }
        [ForeignKey("NewBacklogGradeMasterCenterID")]
        public MST.MasterCenter NewBacklogGrade { get; set; }
    }
}
