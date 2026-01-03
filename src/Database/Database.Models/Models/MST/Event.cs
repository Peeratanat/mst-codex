using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ข้อมูล Event")]
    [Table("Event", Schema = Schema.MASTER)]
    public class Event : BaseEntityWithoutMigrate
    {
        [Description("ชื่อ Agent ภาษาไทย")]
        [MaxLength(100)]
        public string NameTH { get; set; }

        [Description("ชื่อ Agent ภาษาอังกฤษ")]
        [MaxLength(100)]
        public string NameEN { get; set; }

        [Description("วันที่จัด Event")]
        public DateTime? EventDateFrom { get; set; }

        [Description("วันที่จัด Event")]
        public DateTime? EventDateTo { get; set; }

        [Description("status Event")]
        public bool? Isactive { get; set; }
    }
}
