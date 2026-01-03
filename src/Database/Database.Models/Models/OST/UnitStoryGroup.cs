using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.OST
{
    [Description("กลุ่มของประวัติแปลง")]
    [Table("UnitStoryGroup", Schema = Schema.OBJECT_STORY)]
    public class UnitStoryGroup : BaseEntity
    {
        [Description("ชื่อ")]
        public string Name { get; set; }

    }
}
