using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// โปรก่อนขาย
    /// </summary>
    public class QuotationTransferPromotionPrebookItemsDTO
    {

        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID { get; set; }
        /// <summary>
        /// QuotationID
        /// </summary>
        public Guid? QuotationID { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }

        public string UnitName { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal TotalPrice { get; set; }


    }
}
