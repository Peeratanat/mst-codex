using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LOG
{
    [Table("EventLogType", Schema = Schema.LOG)]
    public class EventLogType : BaseEntity
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        //จำนวนวันที่เก็บข้อมูล
        public int? KeepDay { get; set; }

    }
}
