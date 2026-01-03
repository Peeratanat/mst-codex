using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.USR
{

    [Description("Module ของเมนู")]
    [Table("Module", Schema = Schema.USER)]
    public class Module : BaseEntity
    {
        [Description("โค้ด")]
        [MaxLength(250)]
        public string Code { get; set; }

        [Description("ชื่อ(ไทย)")]
        [MaxLength(250)]
        public string NameTH { get; set; }

        [Description("ชื่อ(อังกฤษ)")]
        [MaxLength(250)]
        public string NameEng { get; set; }
    }
}
