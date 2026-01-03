using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ข้อมูลธนาคาร")]
    [Table("Bank", Schema = Schema.MASTER)]
    public class Bank : BaseEntity
    {
        [Description("รหัสธนาคาร")]
        [MaxLength(50)]
        public string BankNo { get; set; }
        [Description("ชื่อธนาคารภาษาไทย")]
        [MaxLength(100)]
        public string NameTH { get; set; }
        [Description("ชื่อธนาคารภาษาอังกฤษ")]
        [MaxLength(100)]
        public string NameEN { get; set; }
        [Description("ชื่อย่อ")]
        [MaxLength(100)]
        public string Alias { get; set; }
        [Description("มีบริการบัตร Credit ไหม")]
        public bool IsCreditCard { get; set; }
        [Description("เป็น Bank หรือ NonBank")]
        public bool IsNonBank { get; set; }
        [Description("เป็น Coorperative Bank หรือเปล่า")]
        public bool IsCoorperative { get; set; }
        [Description("ขอสินเชื่อฟรีไหม")]
        public bool IsFreeMortgage { get; set; }
        [Description("SWIFTCode")]
        [MaxLength(50)]
        public string SwiftCode { get; set; }

        public List<BankBranch> BankBranches { get; set; }
    }
}
