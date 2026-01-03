using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
    public class OpportunityRelationDTO
    {
        public Guid? BookingID { get; set; }
        /// <summary>
        /// รหัสของ Contact
        /// </summary>
        public Guid? OpportunityFrom { get; set; }
        /// <summary>
        /// ชื่อจริง/ชื่อบริษัท (ภาษาไทย)
        /// </summary>
        public Guid? OpportunityTo { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาไทย)
        /// </summary>
        public Guid? UserID { get; set; }
    }
}
