using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.ACC
{
    [Description("รายการ PostGL")]
    [Table("GLDetail", Schema = Schema.ACCOUNT)]
    public class GLDetail : BaseEntity
    {
        [Description("รหัสโพส")]
        public string PostID { get; set; }
        [Description("หมวด")]
        public string Category { get; set; }
        [Description("วันที่ทำรายการ")]
        public DateTime? OperationDate { get; set; }
        [Description("รายละเอียด")]
        public string Detail { get; set; }
        [Description("บัญชีธนาคาร")]
        public string BankAccount { get; set; }
        [Description("เดบิต/เครดิต")]
        [Column(TypeName = "Money")]
        public decimal DebitCreditAmount { get; set; }
        [Description("วันที่ Post")]
        public DateTime? PostDate { get; set; }
        [Description("Post โดย")]
        public string PostBy { get; set; }
        [Description("สถานะ")]
        public string Status { get;set; }

    }
}
