using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("โอนกรรมสิทธิ์ของแปลง")]
    [Table("TransferUnit", Schema = Schema.SALE)]
    public class TransferUnit : BaseEntity
    {
        [Description("สัญญา")]
        public Guid?  AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("แปลงเก่า")]
        public Guid? OldUnitID { get; set; }
        [ForeignKey("OldUnitID")]
        public PRJ.Unit OldUnit { get; set; }

        [Description("แปลงใหม่")]
        public Guid?  NewUnitID { get; set; }
        [ForeignKey("NewUnitID")]
        public PRJ.Unit NewUnit { get; set; }

        [Description("วันที่อนุมัติ")]
        public DateTime? ApproveDate { get; set; }
        [Description("สถานะ")]
        public string Status { get; set; }

    }
}
