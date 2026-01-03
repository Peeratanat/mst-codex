using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ข้อมูล Agent")]
    [Table("Agent", Schema = Schema.MASTER)]
    public class Agent : BaseEntity
    {
        [Description("ชื่อ Agent ภาษาไทย")]
        [MaxLength(100)]
        public string NameTH { get; set; }
        [Description("ชื่อ Agent ภาษาอังกฤษ")]
        [MaxLength(100)]
        public string NameEN { get; set; }
        [Description("ที่ตั้ง")]
        [MaxLength(5000)]
        public string Address { get; set; }
        [Description("ตึก")]
        [MaxLength(1000)]
        public string Building { get; set; }
        [Description("ซอย")]
        [MaxLength(1000)]
        public string Soi { get; set; }
        [Description("ถนน")]
        [MaxLength(1000)]
        public string Road { get; set; }
        [MaxLength(50)]
        public string PostalCode { get; set; }
        [Description("จังหวัด")]
        public Guid? ProvinceID { get; set; }
        [ForeignKey("ProvinceID")]
        public MST.Province Province { get; set; }
        [Description("อำเภอ/เขต")]
        public Guid? DistrictID { get; set; }
        [ForeignKey("DistrictID")]
        public MST.District District { get; set; }
        [Description("ตำบล/แขวง")]
        public Guid? SubDistrictID { get; set; }
        [ForeignKey("SubDistrictID")]
        public MST.SubDistrict SubDistrict { get; set; }
        [Description("เบอร์โทรศัพท์")]
        [MaxLength(100)]
        public string TelNo { get; set; }
        [Description("เบอร์ Fax")]
        [MaxLength(100)]
        public string FaxNo { get; set; }
        [Description("เว็บไซต์")]
        [MaxLength(1000)]
        public string Website { get; set; }
    }
}
