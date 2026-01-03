using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูลที่อยู่ของลูกค้ากับโครงการ")]
    [Table("ContactAddressProject", Schema = Schema.CUSTOMER)]
    public class ContactAddressProject : BaseEntity
    {
        [Description("ที่อยู่ของลูกค้า")]
        public Guid? ContactAddressID { get; set; }
        [ForeignKey("ContactAddressID")]
        public ContactAddress ContactAddress { get; set; }

        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }
    }
}
