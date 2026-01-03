using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูล Waive QC")]
    [Table("WaiveQC", Schema = Schema.PROJECT)]
    public class WaiveQC : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("เลขที่แปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }

        [Description("วันที่โอนจริง")]
        public DateTime? ActualTransferDate { get; set; }
        [Description("วันที่ Waive QC")]
        public DateTime? WaiveQCDate { get; set; }
        [Description("วันที่ผ่าน End Major Product")]
        public DateTime? EndMajorDate { get; set; }
        [Description("วันที่ผ่าน End Full Product")]
        public DateTime? EndFullDate { get; set; }
        [Description("วันที่ Waive Sign")]
        public DateTime? WaiveSignDate { get; set; }
        [Description("วันที่ลูกค้าเข้าอยู่")]
        public DateTime? ArriveDate { get; set; }


        public string WaiveQCBy { get; set; }
        public string WaiveSignBy { get; set; }
    }
}
