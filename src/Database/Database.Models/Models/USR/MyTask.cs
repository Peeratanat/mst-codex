using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.USR
{
    [Description("งานที่ต้องทำของ User")]
    [Table("MyTask", Schema = Schema.USER)]
    public class MyTask : BaseEntity
    {
        [Description("หัวข้อ")]
        public string Topic { get; set; }
        [Description("รายละเอียด")]
        public string Detail { get; set; }
        [Description("วันที่ต้องทำ")]
        public DateTime? DueDate { get; set; }

        [Description("ตัวชี้ที่ 1")]
        public string Ref1 { get; set; }
        [Description("ตัวชี้ที่ 2")]
        public string Ref2 { get; set; }
        [Description("ตัวชี้ที่ 3")]
        public string Ref3 { get; set; }
        [Description("ตัวชี้ที่ 4")]
        public string Ref4 { get; set; }

        [Description("ผู้ใช้งาน")]
        public Guid? UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        [Description("ได้รับจากใคร")]
        public Guid? FromUserID { get; set; }
        [ForeignKey("FromUserID")]
        public User FromUser { get; set; }

        [Description("ชนิดของงาน")]
        public Guid TaskTypeID { get; set; }
        [ForeignKey("TaskTypeID")]
        public TaskType TaskType { get; set; }

    }
}
