using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("LC ที่ไม่ต้องการรับ Leads")]
    [Table("LeadUserIgnore", Schema = Schema.CUSTOMER)]
    public class LeadUserIgnore : BaseEntityWithoutMigrate
    {
        [Description("ProjectID")]
        public Guid? ProjectID { get; set; }

        [Description("UserID")]
        public Guid? UserID { get; set; }

        [Description("EmployeeNo")]
        public string EmployeeNo { get; set; }
    }
}
