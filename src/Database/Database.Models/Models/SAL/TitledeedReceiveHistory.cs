using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("ประวัติการเบิกโฉนด")]
    [Table("TitledeedReceiveHistory", Schema = Schema.SALE)]
    public class TitledeedReceiveHistory : BaseEntity
    {
        [Description("โฉนดเลขที่")]
        public Guid TitledeedReceiveID { get; set; }
        [ForeignKey("TitledeedReceiveID")]
        public TitledeedReceive TitledeedReceive { get; set; }

        [Description("รหัสพนักงาน")]
        public Guid ActorUserID { get; set; }
        [ForeignKey("ActorUserID")]
        public USR.User ActorUser { get; set; }

        [Description("วันที่ดำเนินการ")]
        public DateTime ProceedDate { get; set; }
        [Description("สถานะก่อนหน้า")]
        public string PreviousStatus { get; set; }
        [Description("สถานะ")]
        public string ChangedStatus { get; set; }

    }
}
