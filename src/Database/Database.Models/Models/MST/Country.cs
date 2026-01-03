using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ประเทศ")]
    [Table("Country", Schema = Schema.MASTER)]
    public class Country : BaseEntity
    {
        [Description("รหัสประเทศ")]
        public string Code { get; set; }
        [Description("ชื่อประเทศภาษาไทย")]
        [MaxLength(100)]
        public string NameTH { get; set; }
        [Description("ชื่อประเทศภาษาอังกฤษ")]
        [MaxLength(100)]
        public string NameEN { get; set; }

    }
}
