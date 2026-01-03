using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LOG
{
    [Table("EventLog", Schema = Schema.LOG)]
    public class EventLog : BaseEntity
    {
        public string Name { get; set; }
        public Guid? UserID { get; set; }
        public string UserName { get; set; }
        public string Object { get; set; }

    }
}
