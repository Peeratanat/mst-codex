using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูลผลติดตามของ Revisit")]
    [Table("RevisitActivityResult", Schema = Schema.CUSTOMER)]
    public class RevisitActivityResult : BaseEntity
    {
        [Description("รหัสข้อมูลกิจกรรมของ Revisit")]
        public Guid? RevisitAcitivityID { get; set; }
        [ForeignKey("RevisitAcitivityID")]
        public RevisitActivity RevisitActivity { get; set; }
        [Description("รหัสข้อมูลสถานะกิจกรรมของ Revisit")]
        public Guid? StatusID { get; set; }
        [ForeignKey("StatusID")]
        public RevisitActivityStatus RevisitActivityStatus { get; set; }
        [Description("เหตุผลอื่นๆ")]
        [MaxLength(5000)]
        public string OtherReasons { get; set; }

    }
}
