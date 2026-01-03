using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.ACC
{
    [Description("Calendar Lock ปิดบัญชี")]
    [Table("CalendarLock", Schema = Schema.ACCOUNT)]
    public class CalendarLock : BaseEntity
    {
        [Description("วันที่ Lock ในปฏิทิน")]
        public DateTime LockDate { get; set; }

        [Description("บริษัทที่ Lock")]
        public Guid? CompanyID { get; set; }

        [ForeignKey("CompanyID")]
        public MST.Company Company { get; set; }

        [Description("Lock อยู่หรือไม่")]
        public bool IsLocked { get; set; }

        [Description("ผู้ดำเนินการ Lock")]
        public Guid? UserID { get; set; }

        [ForeignKey("UserID")]
        public USR.User User { get; set; }
    }
}
