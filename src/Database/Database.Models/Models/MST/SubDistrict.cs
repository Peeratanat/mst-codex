using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ตำบล")]
    [Table("SubDistrict", Schema = Schema.MASTER)]
    public class SubDistrict : BaseEntity
    {
        [Description("อำเภอ")]
        public Guid DistrictID { get; set; }
        [ForeignKey("DistrictID")]
        public District District { get; set; }

        [Description("สำนักงานที่ดิน")]
        public Guid? LandOfficeID { get; set; }
        [ForeignKey("LandOfficeID")]
        public LandOffice LandOffice { get; set; }

        [Description("ชื่อตำบลภาษาไทย")]
        [MaxLength(100)]
        public string NameTH { get; set; }
        [Description("ชื่อตำบลภาษาอังกฤษ")]
        [MaxLength(100)]
        public string NameEN { get; set; }
        [Description("รหัสไปรษณีย์")]
        [MaxLength(50)]
        public string PostalCode { get; set; }

    }
}
