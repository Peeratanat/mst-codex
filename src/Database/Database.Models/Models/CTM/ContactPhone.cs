using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.CTM
{
    [Description("เบอร์โทรศัพท์ของลูกค้า")]
    [Table("ContactPhone", Schema = Schema.CUSTOMER)]
    public class ContactPhone : BaseEntity
    {
        [Description("รหัสลูกค้า")]
        public Guid ContactID { get; set; }
        [ForeignKey("ContactID")]
        public Contact Contact { get; set; }

        [Description("ประเภทเบอร์โทรศัพท์")]
        public Guid? PhoneTypeMasterCenterID { get; set; }
        [ForeignKey("PhoneTypeMasterCenterID")]
        public MST.MasterCenter PhoneType { get; set; }

        [Description("เบอร์โทรศัพท์")]
        [MaxLength(100)]
        public string PhoneNumber { get; set; }
        [Description("เบอร์โทรศัพท์ (ต่อ)")]
        [MaxLength(100)]
        public string PhoneNumberExt { get; set; }
        [Description("รหัสประเทศ")]
        [MaxLength(50)]
        public string CountryCode { get; set; }
        [Description("สถานะเบอร์โทรศัพท์หลัก")]
        public bool IsMain { get; set; }

    }
}
