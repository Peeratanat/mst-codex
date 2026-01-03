using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.EXT
{
    public class DeliveryItemDTO
    {

        public string RequestNo { get; set; }
        public string EmployeeNo { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public List<string> MaterialCode { get; set; }

        /// <summary>
        /// ประเภทการส่งมอบ <S=Salepromotin , T=TranferPromotion>
        /// </summary>
        public string Type { get; set; }
    }
}
