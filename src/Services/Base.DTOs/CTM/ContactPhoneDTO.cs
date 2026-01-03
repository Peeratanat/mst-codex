using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.CTM
{
    public class ContactPhoneDTO
    {
        /// <summary>
        /// ID ของเบอร์โทรศัพท์
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ประเภทของเบอร์โทรศัพท์
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PhoneType
        /// </summary>
        [Description("ประเภทของเบอร์โทรศัพท์")]
        public MST.MasterCenterDropdownDTO PhoneType { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        [Description("เบอร์โทรศัพท์")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// หมายเลขต่อ
        /// </summary>
        [Description("เบอร์ต่อ")]
        public string PhoneNumberExt { get; set; }
        /// <summary>
        /// รหัสประเทศ
        /// </summary>
        [Description("รหัสประเทศ")]
        public string CountryCode { get; set; }
        /// <summary>
        /// <summary>
        /// สถานะ: เบอร์หลัก
        /// </summary>
        public bool IsMain { get; set; }
    }
}
