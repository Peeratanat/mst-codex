using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("อำเภอ")]
    [Table("District", Schema = Schema.MASTER)]
    public class District : BaseEntity
    {
        [Description("จังหวัด")]
        public Guid ProvinceID { get; set; }
        [ForeignKey("ProvinceID")]
        public Province Province { get; set; }

        [Description("ชื่อภาษาไทย")]
        [MaxLength(100)]
        public string NameTH { get; set; }
        [Description("ชื่อภาษอังกฤษ")]
        [MaxLength(100)]
        public string NameEN { get; set; }
        [Description("รหัสไปรษณีย์")]
        [MaxLength(50)]
        public string PostalCode { get; set; }

        public List<SubDistrict> SubDistricts { get; set; }

    }
}
