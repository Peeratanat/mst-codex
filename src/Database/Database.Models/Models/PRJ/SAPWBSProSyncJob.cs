using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.PRJ
{
    [Description("Background Job สำหรับการ Sync WBS กิ่ง P")]
    [Table("SAPWBSProSyncJob", Schema = Schema.PROJECT)]
    public class SAPWBSProSyncJob : BaseEntity
    {
        public double Progress { get; set; }
        public BackgroundJobStatus Status { get; set; }
        public string Params { get; set; }
        public string ResponseMessage { get; set; }
    }
}
