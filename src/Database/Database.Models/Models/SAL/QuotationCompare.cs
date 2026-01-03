using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("ใบเปรียบเทียบราคา")]
    [Table("QuotationCompare", Schema = Schema.SALE)]
    public class QuotationCompare : BaseEntity
    {
        [Description("แปลง")]
        public Guid UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }


    }
}
