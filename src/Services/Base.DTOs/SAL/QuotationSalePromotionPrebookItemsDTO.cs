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
    public class QuotationSalePromotionPrebookItemsDTO
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
        /// SeqNo
        /// </summary>
        public int? SeqNo { get; set; }
        /// <summary>
        /// PromotionDescription
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public int? Quantity { get; set; }

        public string UnitName { get; set; }

        public decimal PricePerUnit { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
