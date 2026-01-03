using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูลสถานะกิจกรรมของ Revisit")]
    [Table("RevisitActivityStatus", Schema = Schema.CUSTOMER)]
    public class RevisitActivityStatus : BaseEntity
    {
        [Description("รหัส")]
        [MaxLength(50)]
        public string Code { get; set; }
        [Description("รายละเอียดสถานะ")]
        [MaxLength(100)]
        public string Description { get; set; }
        [Description("ลำดับ")]
        public int Order { get; set; }
        [Description("สถานะของข้อมูล (Active/Inactive)")]
        public bool IsActive { get; set; }

    }
}
