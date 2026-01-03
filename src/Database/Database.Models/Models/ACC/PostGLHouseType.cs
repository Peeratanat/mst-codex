using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.ACC
{
    [Description("คู่บัญชีตามประเภทบ้าน")]
    [Table("PostGLHouseType", Schema = Schema.ACCOUNT)]
    public class PostGLHouseType : BaseEntity
    {

        [Description("เลขที่บัญชี GL")]
        public string GLAccountID { get; set; }

        [Description("ชื่อบัญชีรายได้")]
        public string IncomeAccountName { get; set; }
        [Description("เลขที่บัญชีรายได้")]
        public string IncomeAccountNo { get; set; }
        [Description("ประเภทบ้าน")]
        public string HouseType { get; set; }

    }
}
