using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("อีเมลของลูกค้า")]
    [Table("ContactEmail", Schema = Schema.CUSTOMER)]
    public class ContactEmail : BaseEntity
    {
        [Description("รหัสลูกค้า")]
        public Guid ContactID { get; set; }
        [ForeignKey("ContactID")]
        public Contact Contact { get; set; }
        [Description("อีเมล")]
        [MaxLength(100)]
        public string Email { get; set; }
        [Description("สถานะอีเมลหลัก")]
        public bool IsMain { get; set; }

    }
}
