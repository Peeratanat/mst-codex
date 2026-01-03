using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.FIN
{
    [Description("กระเป๋าเงินของลูกค้า")]
    [Table("CustomerWallet", Schema = Schema.FINANCE)]
    public class CustomerWallet : BaseEntity
    {
        [Description("ผูกข้อมูลลูกค้า")]
        public Guid ContactID { get; set; }
        [ForeignKey("ContactID")]
        public CTM.Contact Contact { get; set; }

        [Description("ผู้ข้อมูลโครงการ")]
        public Guid ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }

        [Description("จำนวนเงินที่มีอยู่")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

    }
}
