using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("BackLog")]
    [Table("BackLog", Schema = Schema.SALE)]
    public class BackLog : BaseEntityWithoutMigrate
    {
        [Description("โครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }
        [Description("แปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }
        [Description("ใบจอง")]
        public Guid BookingID { get; set; }
        [ForeignKey("BookingID")]
        public SAL.Booking Booking { get; set; }

        [Description("สัญญา")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("โอน")]
        public Guid? TransferID { get; set; }
        [ForeignKey("TransferID")]
        public SAL.Agreement Transfer { get; set; }

        [Description("สถานะการโอน")]
        public bool? IsTransfer { get; set; }
        [Description("หมายเหตุ")]
        public string Remark { get; set; }
        [Description("วันที่นัดลูกค้าเข้าตรวจบ้าน")]
        public DateTime? AppointmentsDate { get; set; }
        [Description("วันที่คาดว่าจะโอน")]
        public DateTime? DueTransferDate { get; set; }
        [Description("เกรดความน่าจะเป็นในการโอน")]
        public Guid? BacklogGradeMasterCenterID { get; set; }
        [ForeignKey("BacklogGradeMasterCenterID")]
        public MST.MasterCenter BacklogGrade { get; set; }
        [Description("สถานะ QA")]
        public bool? IsQAStatus { get; set; }
        [Description("เกรดความน่าจะเป็นในการโอน")]
        public Guid? QAStatusTypeMasterCenterID { get; set; }
        [ForeignKey("QAStatusTypeMasterCenterID")]
        public MST.MasterCenter QAStatusType { get; set; }
    }
}
