using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LOG
{
    [Description("เก็บ log การ Auto ยกเลิกในแต่ละวัน")]
    [Table("AutoCancelHistory", Schema = Schema.LOG)]
    public class AutoCancelHistory : BaseEntity
    {

        [Description("รหัสเลขที่ CancelMemo")]
        public Guid CancelMemoID { get; set; }
        [ForeignKey("CancelMemoID")]
        public SAL.CancelMemo CancelMemo { get; set; }

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

        [Description("1 = ยกเลิกจองไม่มาทำสัญญา 2 = สัญญามีงวดดาวน์ 1 งวด: ค้าง 1 งวด 3 = สัญญามีงวดดาวน์ < 24 งวด: พิจารณาที่ ค้างดาวน์ 12.5 % ของเงินดาวน์ทั้งหมดตามสัญญา 4 = สัญญามีงวดดาวน์ >= 24 งวด: พิจารณาที่ ค้างดาวน์ 3 งวดติด")]
        public int CancelType { get; set; }

        [Description("หมายเหตุต่างๆ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

    }
}
