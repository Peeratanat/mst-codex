using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.NTF
{
    [Description("ข้อมูลการ ส่ง SMS ให้ลูกค้า")]
    [Table("SMS", Schema = Schema.NOTIFICATION)]
    public class SMS : BaseEntity
    {
        [Description("ประเภท SMS")]
        public Guid SMSTypeMasterCenterID { get; set; }
        [ForeignKey("SMSTypeMasterCenterID")]
        public MST.MasterCenter SMSType { get; set; }

        [Description("วันที่ส่ง SMS")]
        public DateTime SendDate { get; set; }

        [Description("เบอร์ที่ส่ง")]
        [MaxLength(50)]
        public string MobileNumber { get; set; }

        [Description("ข้อความที่ส่ง")]
        [MaxLength(2000)]
        public string MSG { get; set; }

        [Description("Key1")]
        [MaxLength(100)]
        public string Key1 { get; set; }

        [Description("Key2")]
        [MaxLength(100)]
        public string Key2 { get; set; }

        [Description("Key3")]
        [MaxLength(100)]
        public string Key3 { get; set; }

        [Description("Referent ID 1")]
        public Guid? ReferentID1 { get; set; }

        [Description("Referent ID 2")]
        public Guid? ReferentID2 { get; set; }

        [Description("Referent ID 3")]
        public Guid? ReferentID3 { get; set; }
    }
}
