using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LOG
{
    [Description("เก็บ log การ Undo Cancel")]
    [Table("UndoCancelHistory", Schema = Schema.LOG)]
    public class UndoCancelHistory : BaseEntity
    {

        [Description("เบขที่ Job Helpdesk")]
        public string JobHelpdeskNo { get; set; }

        [Description("โครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("แปลง")]
        public Guid UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }

        [Description("จอง")]
        public Guid BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("สัญญา")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("หมายเหตุต่างๆ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

    }
}
