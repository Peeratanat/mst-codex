using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ข้อมูลสำนักงานที่ดิน")]
    [Table("LandOffice", Schema = Schema.MASTER)]
    public class LandOffice : BaseEntity
    {
        [Description("ชื่อภาษาไทย")]
        [MaxLength(100)]
        public string NameTH { get; set; }
        [Description("ชื่อภาษาอังกฤษ")]
        [MaxLength(100)]
        public string NameEN { get; set; }

        public List<SubDistrict> SubDistricts { get; set; }

        [Description("รหัสสำนักงานที่ดิน")]
        [MaxLength(100)]
        public string Code { get; set; }
    }
}
