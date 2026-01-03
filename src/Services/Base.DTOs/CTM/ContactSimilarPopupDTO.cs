using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
    public class ContactSimilarPopupDTO
    {
        /// <summary>
        /// ข้อมูล Contact ที่ใกล้เคียง
        /// </summary>
        public List<ContactSimilarDTO> ContactSimilars { get; set; }
        /// <summary>
        /// สถานะการสร้าง Contact ใหม่ (true = สร้างได้, false = สร้างไม่ได้)
        /// </summary>
        public bool? CanCreateNewContact { get; set; }
    }
}
