using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.CTM
{
    [Description("Contact_Audit")]
    [Table("Contact_Audit", Schema = Schema.CUSTOMER)]
    public class Contact_Audit : BaseEntityWithoutMigrate
    {

        public string Actions { get; set; }

        public Guid? ContactID { get; set; }
        [ForeignKey("ContactID")]
        public Contact Contact { get; set; }

        public string ContactNo { get; set; }
        public string FirstNameTH { get; set; }
        public string LastNameTH { get; set; }
    }
}
