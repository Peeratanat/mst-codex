using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRM
{
    [Description("Background Job สำหรับการ Sync WBS กิ่ง P")]
    [Table("SAPMaterialSyncJob", Schema = Schema.PROMOTION)]
    public class SAPMaterialSyncJob : BaseEntity
    {
        [MaxLength(50)]
        public string JobNo { get; set; }
        public double Progress { get; set; }
        public BackgroundJobStatus Status { get; set; }
        public string Params { get; set; }
        public string ResponseMessage { get; set; }
    }
}
