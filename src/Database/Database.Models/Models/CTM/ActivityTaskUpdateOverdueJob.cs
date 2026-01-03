using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.CTM
{
    [Description("Background Job สำหรับการ Update ค่า Overdue ของ Activity Task")]
    [Table("ActivityTaskUpdateOverdueJob", Schema = Schema.CUSTOMER)]
    public class ActivityTaskUpdateOverdueJob : BaseEntity
    {
        public double Progress { get; set; }
        public BackgroundJobStatus Status { get; set; }
        public string Params { get; set; }
        public string ResponseMessage { get; set; }
    }
}
