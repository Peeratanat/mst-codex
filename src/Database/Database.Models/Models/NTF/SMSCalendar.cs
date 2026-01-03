using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.NTF
{
    [Description("ตารางวันที่กำหนดส่ง SMS")]
    [Table("SMSCalendar", Schema = Schema.NOTIFICATION)]
    public class SMSCalendar : BaseEntity
    {
        [Description("ประเภท SMS")]
        public Guid SMSTypeMasterCenterID { get; set; }
        [ForeignKey("SMSTypeMasterCenterID")]
        public MST.MasterCenter SMSType { get; set; }

        [Description("วันที่กำหนดส่ง SMS")]
        public DateTime PlanDate { get; set; }
    }
}
