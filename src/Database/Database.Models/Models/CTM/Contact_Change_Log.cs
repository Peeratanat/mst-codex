using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.CTM
{
    [Description("Contact_Change_Log")]
    [Table("Contact_Change_Log", Schema = Schema.CUSTOMER)]
    public class Contact_Change_Log : BaseEntityWithoutMigrate
    {

        public Guid? ContactID { get; set; }
        [ForeignKey("ContactID")]
        public Contact Contact { get; set; }

        public string Actions { get; set; }

        public string ColumnChanged { get; set; }
        public string ValuesOld { get; set; }
        public string ValuesNew { get; set; }
    }
}
