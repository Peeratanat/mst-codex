using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.CTM
{
    [Description("Background Job สำหรับการดึงข้อมูล Lead")]
    [Table("LeadSyncJob", Schema = Schema.CUSTOMER)]
    public class LeadSyncJob : BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        public double Progress { get; set; }
        public BackgroundJobStatus Status { get; set; }
        public string Params { get; set; }
        public string ResponseMessage { get; set; }
    }
}
