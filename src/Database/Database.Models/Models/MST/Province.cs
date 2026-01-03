using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("จังหวัด")]
    [Table("Province", Schema = Schema.MASTER)]
    public class Province : BaseEntity
    {
        [Description("ชื่อจังหวัดภาษาไทย")]
        [MaxLength(100)]
        public string NameTH { get; set; }
        [Description("ชื่อจังหวัดภาษาอังกฤษ")]
        [MaxLength(100)]
        public string NameEN { get; set; }
        [Description("Show อยู่หรือไม่")]
        public bool IsShow { get; set; }

    }
}
