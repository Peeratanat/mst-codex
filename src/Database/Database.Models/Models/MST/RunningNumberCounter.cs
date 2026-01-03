using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.MST
{
    [Description("RunningNumberCounter")]
    [Table("RunningNumberCounter", Schema = Schema.MASTER)]
    public class RunningNumberCounter : BaseEntityWithoutKey
    {
        [MaxLength(100)]
        public string Key { get; set; }
        [Description("ชนิดของ Object")]

        [MaxLength(100)]
        public string Type { get; set; }
        [Description("ตัวนับ")]

        public int Count { get; set; }
    }
}
