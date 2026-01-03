using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("ข้อมูลลูกค้า")]
    [Table("ContactRegister", Schema = Schema.CUSTOMER)]
    public class ContactRegister : BaseEntityWithoutMigrate
    {

        public Guid? ContactID { get; set; }
        [ForeignKey("ContactID")]
        public CTM.Contact Contact { get; set; }

        public DateTime? RegisterDate { get; set; }

        public Guid? EventID { get; set; }
        [ForeignKey("EventID")]
        public MST.Event Event { get; set; }

        public int Queue { get; set; }
        public string Prefix { get; set; }
    }
}
