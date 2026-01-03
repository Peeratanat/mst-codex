using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.ACC
{
    [Description("Format การ Gen text file")]
    [Table("PostGLFormatTextFileHeader", Schema = Schema.ACCOUNT)]
    public class PostGLFormatTextFileHeader : BaseEntity
    {
        [Description("รหัส/ชื่อ Format")]
        [MaxLength(50)]
        public string FormatTextFileCode { get; set; }

        [Description("DR=Debit  CR=Credit")]
        [MaxLength(50)]
        public string FormatType { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }
    }
}
