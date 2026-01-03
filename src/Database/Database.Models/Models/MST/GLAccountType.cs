using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Database.Models.MST
{
    [Description("เครื่องรูดบัตร")]
    [Table("GLAccountType", Schema = Schema.MASTER)]
    public class GLAccountType : BaseEntity
    {
        [Description("ลำดับ")]
        public int Order { get; set; }

        [Description("รหัส")]
        [MaxLength(50)]
        public string Key { get; set; }

        [Description("ชื่อ")]
        [MaxLength(1000)]
        public string Name { get; set; }

        [Description("หมายเหตุ")]
        [MaxLength(1000)]
        public string Remark { get; set; }

        [Description("ID Header")]
        public Guid? PostGLFormatTextFileHeaderID { get; set; }
        [ForeignKey("PostGLFormatTextFileHeaderID")]
        public ACC.PostGLFormatTextFileHeader PostGLFormatTextFileHeader { get; set; }        

        [Description("Active อยู่หรือไม่")]
        public bool IsActive { get; set; }
    }
}
