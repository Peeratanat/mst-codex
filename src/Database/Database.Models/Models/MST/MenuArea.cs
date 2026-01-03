using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("Area ของ Menu")]
    [Table("MenuArea", Schema = Schema.MASTER)]
    public class MenuArea : BaseEntity
    {
        [Description("ชื่อ")]
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
