using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.ADM
{
    [Description("Admin History")]
    [Table("AdminLogHistory", Schema = Schema.ADMIN)]
    public class AdminLogHistory : BaseEntityWithoutUpdater
    {
        [Description("MenuCode")]
        public string MenuCode { get; set; }

        [Description("MenuName")]
        public string MenuName { get; set; }

        [Description("วันที่แก้ไข")]
        public DateTime LogDate { get; set; }

        [Description("เลขจ๊อบอ้างอิง")]
        [MaxLength(200)]
        public string RequestNo { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Description("HistoryData")]
        public string HistoryData { get; set; }

        [Description("ข้อมูลอ้างอิง 1")]
        [MaxLength(200)]
        public string Ref1 { get; set; }

        [Description("ข้อมูลอ้างอิง 2")]
        [MaxLength(200)]
        public string Ref2 { get; set; }

        [Description("ข้อมูลอ้างอิง 3")]
        [MaxLength(200)]
        public string Ref3 { get; set; }
    }
}
