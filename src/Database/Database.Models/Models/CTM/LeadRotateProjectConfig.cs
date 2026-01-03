using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("Rotate Lead Config")]
    [Table("LeadRotateProjectConfig", Schema = Schema.CUSTOMER)]
    public class LeadRotateProjectConfig : BaseEntityWithoutMigrate
    {
        public Guid ProjectID { get; set;}
        public int NumberRotateDay{ get; set;}
        public string Remark{ get; set;}
        public bool IsActived { get; set; }
    }
}
