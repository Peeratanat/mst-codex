using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.CTM
{
    public class ContactEmailDTO
    {
        /// <summary>
        /// ID ของ Email
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [Description("Email")]
        public string Email { get; set; }
        /// <summary>
        /// สถานะ: อีเมลหลัก
        /// </summary>
        public bool IsMain { get; set; }
    }
}
