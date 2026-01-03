using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("นิติบุคคล")]
    [Table("LegalEntity", Schema = Schema.MASTER)]
    public class LegalEntity : BaseEntity
    {
        [Description("ชื่อนิติบุคคลภาษา (EN)")]
        [MaxLength(500)]
        public string NameEN { get; set; }
        [Description("ชื่อนิติบุคคลภาษา (TH)")]
        [MaxLength(500)]
        public string NameTH { get; set; }
        [Description("ธนาคาร")]
        public Guid? BankID { get; set; }
        [ForeignKey("BankID")]
        public Bank Bank { get; set; }
        [Description("ประเภทบัญชี")]
        public Guid? BankAccountTypeMasterCenterID { get; set; }
        [ForeignKey("BankAccountTypeMasterCenterID")]
        public MST.MasterCenter BankAccountType { get; set; }
        [Description("เลขบัญชีธนาคาร")]
        [MaxLength(10)]
        public string BankAccountNo { get; set; }
        [Description("สถานะ Acitve")]
        public bool IsActive { get; set; }
        [Description("Agent Code")]
        [MaxLength(500)]
        public string AgentCode { get; set; }
    }
}
