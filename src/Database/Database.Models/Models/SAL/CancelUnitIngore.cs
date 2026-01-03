using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("เก็บข้อมูล แปลงที่เข้าเงื่อนไขแต่ยังไม่ยกเลิก")]
    [Table("CancelUnitIgnore", Schema = Schema.SALE)]
    public class CancelUnitIgnore : BaseEntity
    {
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

        [Description("วันที่ครบกำหนดไม่ให้ยกเลิก เช่น 31.10.21 ระบบจะยกเลิก Auto ในวันที่ 1.11.21")]
        public DateTime DueIngoreAutoCancelDate { get; set; }

        [Description("หมายเหตุต่างๆ")]
        [MaxLength(1000)]
        public string Remark { get; set; }


   
    }
}
